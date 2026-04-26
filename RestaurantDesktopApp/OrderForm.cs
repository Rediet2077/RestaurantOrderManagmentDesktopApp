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
        private List<MenuItemDto> allMenuItems = new List<MenuItemDto>();

        private FlowLayoutPanel flpCart;

        public OrderForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            
            dgvOrderDetails.Visible = false; // Hide old cart
            
            flpCart = new FlowLayoutPanel {
                AutoScroll = true,
                BackColor = Color.FromArgb(248, 250, 252)
            };
            this.Controls.Add(flpCart);
            this.Resize += OrderForm_Resize;

            SetupOrderDetailsTable();
            _ = LoadDataAsync();
        }

        private void OrderForm_Resize(object? sender, EventArgs e)
        {
            if (headerPanel != null) headerPanel.Width = this.Width;
            
            // Filters area
            if (lblMenuTitle != null) lblMenuTitle.Location = new Point(30, 100);
            if (lblCategory != null) lblCategory.Location = new Point(180, 100);
            if (cmbCategory != null) cmbCategory.Location = new Point(260, 97);
            
            int leftWidth = (int)(this.Width * 0.65);
            if (flpMenu != null)
            {
                flpMenu.Location = new Point(30, 140);
                flpMenu.Size = new Size(leftWidth - 60, this.Height - 170);
            }

            int rightX = leftWidth;
            int rightWidth = this.Width - leftWidth - 30;
            
            if (flpCart != null)
            {
                flpCart.Location = new Point(rightX, 140);
                flpCart.Size = new Size(rightWidth, this.Height - 300);
            }

            if (lblTotal != null) lblTotal.Location = new Point(rightX, this.Height - 140);
            if (lblTotalAmount != null) lblTotalAmount.Location = new Point(rightX + 150, this.Height - 140);
            
            if (btnPlaceOrder != null)
            {
                btnPlaceOrder.Location = new Point(rightX, this.Height - 90);
                btnPlaceOrder.Size = new Size(rightWidth, 55);
                btnPlaceOrder.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            }

            RenderCart();
        }

        private void RenderCart()
        {
            if (flpCart == null) return;
            flpCart.Controls.Clear();
            
            if (orderDetailsTable.Rows.Count == 0)
            {
                Label empty = new Label { Text = "Your cart is empty.", Font = new Font("Segoe UI", 12), ForeColor = Color.Gray, AutoSize = true, Margin = new Padding(20, 50, 0, 0) };
                flpCart.Controls.Add(empty);
                return;
            }

            foreach (DataRow row in orderDetailsTable.Rows)
            {
                Panel pItem = new Panel { Size = new Size(flpCart.Width - 25, 70), BackColor = Color.White, Margin = new Padding(5) };
                UIHelper.SetRoundedRegion(pItem, 10);

                int itemId = Convert.ToInt32(row["ItemID"]);
                string name = row["ItemName"].ToString() ?? "Unknown";
                int qty = Convert.ToInt32(row["Quantity"]);
                decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                Label lName = new Label { Text = name, Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(15, 10), AutoSize = true, ForeColor = Color.FromArgb(44, 62, 80) };
                Label lQty = new Label { Text = $"{qty} x {Convert.ToDecimal(row["Price"]):N2}", Font = new Font("Segoe UI", 10), Location = new Point(15, 35), AutoSize = true, ForeColor = Color.Gray };
                
                Label lSub = new Label { Text = $"{subtotal:N2} ETB", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(pItem.Width - 160, 25), AutoSize = true, ForeColor = Color.SeaGreen, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                
                Button btnRemove = new Button { Text = "❌", Font = new Font("Segoe UI Emoji", 10), Size = new Size(35, 35), Location = new Point(pItem.Width - 50, 18), BackColor = Color.FromArgb(239, 68, 68), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Top | AnchorStyles.Right, Cursor = Cursors.Hand };
                btnRemove.FlatAppearance.BorderSize = 0;
                UIHelper.SetRoundedRegion(btnRemove, 8);
                btnRemove.Click += (s, e) => {
                    orderDetailsTable.Rows.Remove(row);
                    UpdateTotalAmount();
                    RenderCart();
                };

                Button btnMinus = new Button { Text = "-", Font = new Font("Segoe UI", 12, FontStyle.Bold), Size = new Size(30, 30), Location = new Point(160, 20), BackColor = Color.FromArgb(226, 232, 240), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
                btnMinus.FlatAppearance.BorderSize = 0;
                UIHelper.SetRoundedRegion(btnMinus, 5);
                btnMinus.Click += (s, e) => {
                    if (qty > 1) {
                        row["Quantity"] = qty - 1;
                        row["Subtotal"] = (qty - 1) * Convert.ToDecimal(row["Price"]);
                    } else {
                        orderDetailsTable.Rows.Remove(row);
                    }
                    UpdateTotalAmount();
                    RenderCart();
                };

                Button btnPlus = new Button { Text = "+", Font = new Font("Segoe UI", 12, FontStyle.Bold), Size = new Size(30, 30), Location = new Point(200, 20), BackColor = Color.FromArgb(226, 232, 240), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
                btnPlus.FlatAppearance.BorderSize = 0;
                UIHelper.SetRoundedRegion(btnPlus, 5);
                btnPlus.Click += (s, e) => {
                    row["Quantity"] = qty + 1;
                    row["Subtotal"] = (qty + 1) * Convert.ToDecimal(row["Price"]);
                    UpdateTotalAmount();
                    RenderCart();
                };

                pItem.Controls.Add(lName);
                pItem.Controls.Add(lQty);
                pItem.Controls.Add(lSub);
                pItem.Controls.Add(btnRemove);
                pItem.Controls.Add(btnMinus);
                pItem.Controls.Add(btnPlus);

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

                if (dt.Rows.Count == 0)
                {
                    // Fallback: Add a Takeaway option if no tables are available in DB
                    dt.Rows.Add(0, "Takeaway / Counter");
                }
                
                if (dt.Rows.Count > 0)
                {
                    cmbTables.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error loading tables: " + ex.Message); }
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

                // Auto-select if logged in as customer
                if (Program.CurrentUser != null && Program.CurrentUser.Role == "Customer")
                {
                    cmbCustomers.SelectedValue = Program.CurrentUser.UserID;
                    // Optionally disable selection for customers so they can't order for others
                    cmbCustomers.Enabled = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async System.Threading.Tasks.Task LoadMenuCards()
        {
            try
            {
                allMenuItems = await ApiClient.GetMenuItemsAsync();

                var categories = new List<string> { "All Categories" };
                foreach (var item in allMenuItems)
                {
<<<<<<< HEAD
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
                    {
                        try { pic.Image = Image.FromFile(fullPath); }
                        catch { pic.BackColor = Color.LightGray; }
                    }
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
=======
                    if (!string.IsNullOrEmpty(item.Category) && !categories.Contains(item.Category))
                    {
                        categories.Add(item.Category);
                    }
>>>>>>> 22472b3 (Rebrand to BEST Restaurant, fix customer dashboard, and expand menu variety)
                }

                cmbCategory.SelectedIndexChanged -= cmbCategory_SelectedIndexChanged;
                cmbCategory.DataSource = categories;
                cmbCategory.SelectedIndex = 0;
                cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;

                DisplayMenuItems(allMenuItems);
            }
            catch (Exception ex)
            {
                UIHelper.ShowToast("Error loading menu: " + ex.Message, true);
            }
        }

        private void cmbCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "All Categories";
            if (selectedCategory == "All Categories")
            {
                DisplayMenuItems(allMenuItems);
            }
            else
            {
                var filtered = allMenuItems.FindAll(i => i.Category == selectedCategory);
                DisplayMenuItems(filtered);
            }
        }

        private void DisplayMenuItems(List<MenuItemDto> items)
        {
            flpMenu.Controls.Clear();
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
                _ = UIHelper.LoadImageAsync(pic, imgPath);

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

        private void AddToOrder(int itemId, string itemName, decimal price)
        {
            foreach (DataRow row in orderDetailsTable.Rows)
            {
                if (Convert.ToInt32(row["ItemID"]) == itemId)
                {
                    row["Quantity"] = Convert.ToInt32(row["Quantity"]) + 1;
                    row["Subtotal"] = Convert.ToDecimal(row["Quantity"]) * price;
                    UpdateTotalAmount();
                    RenderCart();
                    return;
                }
            }
            orderDetailsTable.Rows.Add(itemId, itemName, 1, price, price);
            UpdateTotalAmount();
            RenderCart();
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

                int? tableId = null;
                if (cmbTables.SelectedValue != null && Convert.ToInt32(cmbTables.SelectedValue) > 0)
                {
                    tableId = Convert.ToInt32(cmbTables.SelectedValue);
                }
                
                int orderId = await ApiClient.CreateOrderAsync(customerId, tableId, totalAmount, items);
                if (orderId > 0)
                {
                    UIHelper.ShowToast("Order Placed Successfully!");
                    // Automatically open payment dialog for this specific order
                    var payForm = new PaymentForm(orderId);
                    payForm.StartPosition = FormStartPosition.CenterParent;
                    payForm.TopMost = true;
                    payForm.ShowDialog(this); 
                    
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