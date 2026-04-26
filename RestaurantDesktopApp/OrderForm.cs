using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace RestaurantDesktopApp
{
    public partial class OrderForm : Form
    {
        DataTable orderDetailsTable = new DataTable();
        decimal totalAmount = 0;
        private List<MenuItemDto> allMenuItems = new List<MenuItemDto>();

        private FlowLayoutPanel flpCart;

        public OrderForm()
        {
            InitializeComponent();
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.BackColor = Color.FromArgb(243, 244, 246);
            
            dgvOrderDetails.Visible = false; // Hide old cart
            
            flpCart = new FlowLayoutPanel {
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            this.Controls.Add(flpCart);
            this.Resize += OrderForm_Resize;

            SetupOrderDetailsTable();
            _ = LoadDataAsync();
        }

        private void OrderForm_Resize(object? sender, EventArgs e)
        {
            if (headerPanel != null) headerPanel.Width = this.Width;
            
            // Compact Header area
            if (lblTable != null) lblTable.Location = new Point(25, 75);
            if (cmbTables != null) cmbTables.Location = new Point(75, 72);
            if (lblCustomer != null) lblCustomer.Location = new Point(220, 75);
            if (cmbCustomers != null) cmbCustomers.Location = new Point(300, 72);

            if (lblMenuTitle != null) {
                lblMenuTitle.Text = "MENU";
                lblMenuTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblMenuTitle.Location = new Point(25, 115);
            }
            if (lblCategory != null) lblCategory.Location = new Point(140, 118);
            if (cmbCategory != null) cmbCategory.Location = new Point(210, 115);
            
            int leftWidth = (int)(this.ClientSize.Width * 0.62);
            if (flpMenu != null)
            {
                flpMenu.Location = new Point(25, 150);
                flpMenu.Size = new Size(leftWidth - 40, this.ClientSize.Height - 170);
                flpMenu.BackColor = Color.Transparent;
            }

            int rightX = leftWidth;
            int rightWidth = this.ClientSize.Width - leftWidth - 25;
            
            if (flpCart != null)
            {
                flpCart.Location = new Point(rightX, 70);
                flpCart.Size = new Size(rightWidth, this.ClientSize.Height - 220);
                UIHelper.SetRoundedRegion(flpCart, 15);
            }

            if (lblTotal != null) {
                lblTotal.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                lblTotal.Location = new Point(rightX + 10, this.ClientSize.Height - 130);
            }
            if (lblTotalAmount != null) {
                lblTotalAmount.Font = new Font("Segoe UI", 16, FontStyle.Bold);
                lblTotalAmount.ForeColor = Color.FromArgb(220, 38, 38);
                lblTotalAmount.Location = new Point(rightX + 130, this.ClientSize.Height - 135);
            }
            
            if (btnPlaceOrder != null)
            {
                btnPlaceOrder.Location = new Point(rightX, this.ClientSize.Height - 80);
                btnPlaceOrder.Size = new Size(rightWidth, 50);
                btnPlaceOrder.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                UIHelper.ApplyModernButton(btnPlaceOrder, Color.FromArgb(37, 99, 235));
            }

            RenderCart();
        }

        private void RenderCart()
        {
            if (flpCart == null) return;
            flpCart.Controls.Clear();
            
            if (orderDetailsTable.Rows.Count == 0)
            {
                Label empty = new Label { Text = "Cart is empty", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, AutoSize = true, Margin = new Padding(20, 50, 0, 0) };
                flpCart.Controls.Add(empty);
                return;
            }

            foreach (DataRow row in orderDetailsTable.Rows)
            {
                Panel pItem = new Panel { Size = new Size(flpCart.Width - 35, 60), BackColor = Color.FromArgb(249, 250, 251), Margin = new Padding(0, 0, 0, 8) };
                UIHelper.SetRoundedRegion(pItem, 10);

                string name = row["ItemName"].ToString() ?? "Unknown";
                int qty = Convert.ToInt32(row["Quantity"]);
                decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                Label lName = new Label { Text = name, Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true, ForeColor = Color.FromArgb(31, 41, 55) };
                Label lQty = new Label { Text = $"{qty} x {Convert.ToDecimal(row["Price"]):N0}", Font = new Font("Segoe UI", 8), Location = new Point(10, 30), AutoSize = true, ForeColor = Color.Gray };
                
                Label lSub = new Label { Text = $"{subtotal:N0}", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(pItem.Width - 80, 20), AutoSize = true, ForeColor = Color.FromArgb(17, 24, 39), TextAlign = ContentAlignment.MiddleRight };
                
                Button btnRemove = new Button { Text = "×", Font = new Font("Arial", 10, FontStyle.Bold), Size = new Size(24, 24), Location = new Point(pItem.Width - 30, 18), FlatStyle = FlatStyle.Flat, ForeColor = Color.FromArgb(156, 163, 175), Cursor = Cursors.Hand };
                btnRemove.FlatAppearance.BorderSize = 0;
                int id = Convert.ToInt32(row["ItemID"]);
                btnRemove.Click += (s, e) => RemoveFromOrder(id);

                pItem.Controls.Add(lName); pItem.Controls.Add(lQty); pItem.Controls.Add(lSub); pItem.Controls.Add(btnRemove);
                flpCart.Controls.Add(pItem);
            }
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
        }

        private async System.Threading.Tasks.Task LoadTables()
        {
            try
            {
                var tables = await ApiClient.GetTablesAsync("Available");
                DataTable dt = new DataTable();
                dt.Columns.Add("TableID", typeof(int));
                dt.Columns.Add("TableNumber", typeof(string));
                
                // Add Takeaway option
                dt.Rows.Add(0, "Takeaway");

                foreach (var t in tables)
                    dt.Rows.Add(t.TableID, t.TableNumber);

                cmbTables.DataSource = dt;
                cmbTables.DisplayMember = "TableNumber";
                cmbTables.ValueMember = "TableID";
                cmbTables.SelectedIndex = 0;
            }
            catch { }
        }

        private async System.Threading.Tasks.Task LoadCustomers()
        {
            try
            {
                var customers = await ApiClient.GetCustomersAsync();
                DataTable dt = new DataTable();
                dt.Columns.Add("CustomerID", typeof(int));
                dt.Columns.Add("Name", typeof(string));

                dt.Rows.Add(DBNull.Value, "Walk-in Guest");

                foreach (var c in customers)
                    dt.Rows.Add(c.CustomerID, c.Name);

                cmbCustomers.DataSource = dt;
                cmbCustomers.DisplayMember = "Name";
                cmbCustomers.ValueMember = "CustomerID";

                if (Program.CurrentUser?.Role == "Customer")
                {
                    cmbCustomers.SelectedValue = Program.CurrentUser.UserID;
                    cmbCustomers.Enabled = false;
                }
            }
            catch { }
        }

        private async System.Threading.Tasks.Task LoadMenuCards()
        {
            try
            {
                allMenuItems = await ApiClient.GetMenuItemsAsync();
                var categories = new List<string> { "All Categories" };
                foreach (var item in allMenuItems)
                {
                    if (!string.IsNullOrEmpty(item.Category) && !categories.Contains(item.Category))
                        categories.Add(item.Category);
                }

                cmbCategory.SelectedIndexChanged -= cmbCategory_SelectedIndexChanged;
                cmbCategory.DataSource = categories;
                cmbCategory.SelectedIndex = 0;
                cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;

                DisplayMenuItems(allMenuItems);
            }
            catch (Exception ex) { UIHelper.ShowToast("Error loading menu: " + ex.Message, true); }
        }

        private void cmbCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "All Categories";
            DisplayMenuItems(selectedCategory == "All Categories" ? allMenuItems : allMenuItems.FindAll(i => i.Category == selectedCategory));
        }

        private void DisplayMenuItems(List<MenuItemDto> items)
        {
            flpMenu.Controls.Clear();
            foreach (var item in items)
            {
                Panel card = new Panel { Size = new Size(115, 160), BackColor = Color.White, Margin = new Padding(0, 0, 12, 12) };
                UIHelper.SetRoundedRegion(card, 12);

                PictureBox pic = new PictureBox { Size = new Size(115, 80), Dock = DockStyle.Top, SizeMode = PictureBoxSizeMode.CenterImage, BackColor = Color.FromArgb(249, 250, 251) };
                _ = UIHelper.LoadImageAsync(pic, item.ImagePath);

                Label lblName = new Label { Text = item.Name, Font = new Font("Segoe UI", 8, FontStyle.Bold), Location = new Point(8, 88), Size = new Size(100, 30), TextAlign = ContentAlignment.TopCenter };
                Label lblPrice = new Label { Text = $"{item.Price:N0} ETB", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(5, 150, 105), Location = new Point(8, 115), Size = new Size(100, 18), TextAlign = ContentAlignment.MiddleCenter };

                Button btnAdd = new Button { Text = "+ ADD", Font = new Font("Segoe UI", 8, FontStyle.Bold), Size = new Size(95, 24), Location = new Point(10, 134), FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(16, 185, 129), ForeColor = Color.White, Cursor = Cursors.Hand };
                btnAdd.FlatAppearance.BorderSize = 0;
                btnAdd.Click += (s, ev) => AddToOrder(item.ItemID, item.Name, item.Price);

                card.Controls.Add(btnAdd); card.Controls.Add(lblPrice); card.Controls.Add(lblName); card.Controls.Add(pic);
                flpMenu.Controls.Add(card);
            }
        }

        private void AddToOrder(int id, string name, decimal price)
        {
            DataRow[] existing = orderDetailsTable.Select("ItemID = " + id);
            if (existing.Length > 0)
            {
                existing[0]["Quantity"] = Convert.ToInt32(existing[0]["Quantity"]) + 1;
                existing[0]["Subtotal"] = Convert.ToInt32(existing[0]["Quantity"]) * price;
            }
            else
            {
                orderDetailsTable.Rows.Add(id, name, 1, price, price);
            }
            UpdateTotal();
            RenderCart();
        }

        private void RemoveFromOrder(int id)
        {
            DataRow[] rows = orderDetailsTable.Select("ItemID = " + id);
            if (rows.Length > 0) orderDetailsTable.Rows.Remove(rows[0]);
            UpdateTotal();
            RenderCart();
        }

        private void UpdateTotal()
        {
            totalAmount = orderDetailsTable.AsEnumerable().Sum(r => r.Field<decimal>("Subtotal"));
            lblTotalAmount.Text = $"{totalAmount:N2}";
        }

        private async void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (orderDetailsTable.Rows.Count == 0) { UIHelper.ShowToast("Cart is empty!", true); return; }

            int? tableId = cmbTables.SelectedValue as int?;
            if (tableId == 0) tableId = null; // Takeaway

            int? customerId = null;
            if (cmbCustomers.SelectedValue != null && cmbCustomers.SelectedValue != DBNull.Value)
                customerId = Convert.ToInt32(cmbCustomers.SelectedValue);

            var items = new List<CartItemDto>();
            foreach (DataRow row in orderDetailsTable.Rows)
                items.Add(new CartItemDto { ItemID = (int)row["ItemID"], Quantity = (int)row["Quantity"], Price = (decimal)row["Price"] });

            var req = new CreateOrderRequest { CustomerID = customerId, TableID = tableId, TotalAmount = totalAmount, Items = items };
            int orderId = await ApiClient.CreateOrderAsync(req);

            if (orderId > 0)
            {
                UIHelper.ShowToast("Order Placed Successfully!");
                this.Close();
                // Optionally open payment
                new PaymentForm(orderId, totalAmount).ShowDialog();
            }
            else { UIHelper.ShowToast("Failed to place order.", true); }
        }
    }
}