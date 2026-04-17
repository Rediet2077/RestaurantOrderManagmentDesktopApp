using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace RestaurantDesktopApp
{
    public partial class OrderForm : Form
    {
        DataTable orderDetailsTable = new DataTable();
        decimal totalAmount = 0;

        public OrderForm()
        {
            InitializeComponent();
            SetupOrderDetailsTable();
            _ = LoadDataAsync();
        }

        private async System.Threading.Tasks.Task LoadDataAsync()
        {
            await LoadTables();
            await LoadCustomers();
            await LoadMenuCards();
        }

        private void SetupOrderDetailsTable()
        {
            orderDetailsTable.Columns.Add("ItemID", typeof(int));
            orderDetailsTable.Columns.Add("ItemName", typeof(string));
            orderDetailsTable.Columns.Add("Quantity", typeof(int));
            orderDetailsTable.Columns.Add("Price", typeof(decimal));
            orderDetailsTable.Columns.Add("Subtotal", typeof(decimal));
            dgvOrderDetails.DataSource = orderDetailsTable;
        }

        private async System.Threading.Tasks.Task LoadTables()
        {
            try
            {
                var tables = await ApiClient.GetAvailableTablesAsync();
                DataTable dt = new DataTable();
                dt.Columns.Add("TableID", typeof(int));
                dt.Columns.Add("TableNumber", typeof(string));
                foreach (var t in tables)
                    dt.Rows.Add(t.TableID, t.TableNumber);

                cmbTables.DataSource = dt;
                cmbTables.DisplayMember = "TableNumber";
                cmbTables.ValueMember = "TableID";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async System.Threading.Tasks.Task LoadCustomers()
        {
            try
            {
                var customers = await ApiClient.GetCustomersAsync();
                DataTable dt = new DataTable();
                dt.Columns.Add("CustomerID", typeof(int));
                dt.Columns.Add("Name", typeof(string));

                // Add walk-in option
                DataRow dr = dt.NewRow();
                dr["CustomerID"] = DBNull.Value;
                dr["Name"] = "Walk-in";
                dt.Rows.Add(dr);

                foreach (var c in customers)
                    dt.Rows.Add(c.CustomerID, c.Name);

                cmbCustomers.DataSource = dt;
                cmbCustomers.DisplayMember = "Name";
                cmbCustomers.ValueMember = "CustomerID";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async System.Threading.Tasks.Task LoadMenuCards()
        {
            flpMenu.Controls.Clear();
            try
            {
                var items = await ApiClient.GetMenuItemsAsync();

                foreach (var item in items)
                {
                    Panel card = new Panel();
                    card.Size = new Size(130, 180);
                    card.BackColor = Color.White;
                    card.Margin = new Padding(10);
                    card.BorderStyle = BorderStyle.FixedSingle;

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(110, 80);
                    pic.Location = new Point(10, 10);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;

                    string imgPath = item.ImagePath ?? "";
                    string fullPath = Path.Combine(Application.StartupPath, @"..\..\..\", imgPath);
                    if (!string.IsNullOrEmpty(imgPath) && File.Exists(fullPath))
                        pic.Image = Image.FromFile(fullPath);
                    else
                        pic.BackColor = Color.LightGray;

                    Label lblName = new Label();
                    lblName.Text = item.Name ?? "Unknown";
                    lblName.Location = new Point(10, 100);
                    lblName.Size = new Size(110, 20);
                    lblName.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    lblName.TextAlign = ContentAlignment.MiddleCenter;

                    Label lblPrice = new Label();
                    lblPrice.Text = UIHelper.GetCurrencySymbol() + " " + item.Price.ToString("F2");
                    lblPrice.Location = new Point(10, 120);
                    lblPrice.Size = new Size(110, 20);
                    lblPrice.ForeColor = Color.DarkGreen;
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;

                    Button btnAdd = new Button();
                    btnAdd.Text = "+ Add";
                    btnAdd.Location = new Point(20, 145);
                    btnAdd.Size = new Size(90, 25);
                    btnAdd.FlatStyle = FlatStyle.Flat;
                    btnAdd.BackColor = Color.FromArgb(46, 204, 113);
                    btnAdd.ForeColor = Color.White;
                    btnAdd.Cursor = Cursors.Hand;

                    int id = item.ItemID;
                    string name = item.Name ?? "Unknown";
                    decimal price = item.Price;
                    btnAdd.Click += (s, ev) => AddToOrder(id, name, price);

                    card.Controls.Add(pic);
                    card.Controls.Add(lblName);
                    card.Controls.Add(lblPrice);
                    card.Controls.Add(btnAdd);

                    flpMenu.Controls.Add(card);

                    UIHelper.SetRoundedRegion(card, 15);
                    UIHelper.ApplyModernButton(btnAdd, Color.FromArgb(39, 174, 96));
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowToast("Error loading menu: " + ex.Message, true);
            }
        }

        private void AddToOrder(int itemId, string itemName, decimal price)
        {
            foreach (DataRow row in orderDetailsTable.Rows)
            {
                if (Convert.ToInt32(row["ItemID"]) == itemId)
                {
                    row["Quantity"] = Convert.ToInt32(row["Quantity"]) + 1;
                    row["Subtotal"] = Convert.ToDecimal(row["Quantity"]) * price;
                    UpdateTotalAmount();
                    return;
                }
            }
            orderDetailsTable.Rows.Add(itemId, itemName, 1, price, price);
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            totalAmount = 0;
            foreach (DataRow row in orderDetailsTable.Rows)
            {
                totalAmount += Convert.ToDecimal(row["Subtotal"]);
            }
            lblTotalAmount.Text = UIHelper.GetCurrencySymbol() + " " + totalAmount.ToString("N2");
        }

        private async void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (orderDetailsTable.Rows.Count == 0)
            {
                UIHelper.ShowToast("Please add items.", true);
                return;
            }
            if (cmbTables.SelectedValue == null)
            {
                UIHelper.ShowToast("Please select a table.", true);
                return;
            }

            try
            {
                // Build order items list
                var items = new List<OrderItemDto>();
                foreach (DataRow row in orderDetailsTable.Rows)
                {
                    items.Add(new OrderItemDto
                    {
                        ItemID = Convert.ToInt32(row["ItemID"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        Price = Convert.ToDecimal(row["Price"])
                    });
                }

                int? customerId = null;
                if (cmbCustomers.SelectedValue != null && cmbCustomers.SelectedValue != DBNull.Value)
                {
                    customerId = Convert.ToInt32(cmbCustomers.SelectedValue);
                }

                int tableId = Convert.ToInt32(cmbTables.SelectedValue);

                int orderId = await ApiClient.CreateOrderAsync(customerId, tableId, totalAmount, items);
                if (orderId > 0)
                {
                    UIHelper.ShowToast("Order Placed Successfully!");
                    this.Close();
                }
                else
                {
                    UIHelper.ShowToast("Order failed. Please try again.", true);
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowToast("Order Failed: " + ex.Message, true);
            }
        }
    }
}