using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public class CustomerDashboardForm : Form
    {
        private Panel sidebar;
        private Panel content;
        private FlowLayoutPanel ordersFlow;
        private Label lblGreeting;
        private Label lblCustomerName;
        private Label lblCustomerEmail;
        private Color primaryColor = Color.FromArgb(41, 128, 185);
        private Color sidebarColor = Color.FromArgb(28, 40, 51);
        private Color accentColor = Color.FromArgb(230, 126, 34);

        public CustomerDashboardForm()
        {
            SetupUI();
            _ = LoadDataAsync();
        }

        private void SetupUI()
        {
            this.Text = "My Account - BEST Restaurants";
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 700);
            this.BackColor = Color.FromArgb(244, 247, 252);
            this.Font = new Font("Segoe UI", 10);

            // Sidebar
            sidebar = new Panel { Dock = DockStyle.Left, Width = 260, BackColor = sidebarColor };
            this.Controls.Add(sidebar);

            Panel logoBox = new Panel { Height = 100, Dock = DockStyle.Top };
            Label logo = new Label { Text = "BEST ACCOUNT", ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = false, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
            logoBox.Controls.Add(logo);
            sidebar.Controls.Add(logoBox);

            AddSidebarBtn("DASHBOARD", "\u25A3", 100).Click += (s, e) => { _ = LoadDataAsync(); content.VerticalScroll.Value = 0; };
            AddSidebarBtn("MY ORDERS", "\u231A", 160).Click += (s, e) => { 
                // Scroll to orders
                content.ScrollControlIntoView(ordersFlow);
            };
            AddSidebarBtn("CREATE ORDER", "\u271A", 220).Click += (s, e) => {
                this.Close(); // Return to landing form
            };
            AddSidebarBtn("ACCOUNT SETTINGS", "\u2699", 280).Click += (s, e) => {
                UIHelper.ShowToast("Account settings feature coming soon!");
            };

            Button btnLogout = new Button { 
                Text = "\u21AA  Logout", 
                Dock = DockStyle.Bottom, 
                Height = 60, 
                BackColor = Color.FromArgb(192, 57, 43), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Font = new Font("Segoe UI", 12, FontStyle.Bold), 
                Cursor = Cursors.Hand 
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => {
                Program.IsLoggedIn = false;
                Program.CurrentUser = null;
                this.Close();
            };
            sidebar.Controls.Add(btnLogout);

            // Main Content
            content = new Panel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(30) };
            this.Controls.Add(content);

            lblGreeting = new Label { Text = UIHelper.GetGreeting() + ",", Font = new Font("Segoe UI", 14), ForeColor = Color.FromArgb(127, 140, 141), AutoSize = true, Location = new Point(30, 30) };
            content.Controls.Add(lblGreeting);

            lblCustomerName = new Label { Text = Program.CurrentUser?.Name ?? "Valued Customer", Font = new Font("Segoe UI", 26, FontStyle.Bold), ForeColor = Color.FromArgb(44, 62, 80), AutoSize = true, Location = new Point(30, 60) };
            content.Controls.Add(lblCustomerName);

            // Stats row
            Panel statsRow = new Panel { Location = new Point(30, 140), Size = new Size(content.Width - 100, 120), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            content.Controls.Add(statsRow);
            
            AddStatCard(statsRow, 0, "Total Orders", "0", Color.FromArgb(52, 152, 219));
            AddStatCard(statsRow, 230, "Total Spent", "0.00 ETB", Color.FromArgb(46, 204, 113));
            AddStatCard(statsRow, 460, "Member Since", DateTime.Now.ToString("MMM yyyy"), Color.FromArgb(155, 89, 182));

            // Profile Info Card
            Panel infoCard = new Panel { Location = new Point(30, 280), Size = new Size(350, 320), BackColor = Color.White };
            infoCard.Paint += (s, e) => UIHelper.DrawGradient(e.Graphics, infoCard.ClientRectangle, Color.White, Color.FromArgb(250, 252, 255));
            UIHelper.SetRoundedRegion(infoCard, 15);
            content.Controls.Add(infoCard);

            Label infoTitle = new Label { Text = "Profile Information", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            infoCard.Controls.Add(infoTitle);

            lblCustomerEmail = new Label { Text = "\u2709  " + (Program.CurrentUser?.Email ?? "No email"), Location = new Point(20, 70), AutoSize = true, ForeColor = Color.DimGray };
            infoCard.Controls.Add(lblCustomerEmail);
            
            infoCard.Controls.Add(new Label { Text = "\u260E  +251 " + (Program.CurrentUser?.Phone ?? "No phone"), Location = new Point(20, 105), AutoSize = true, ForeColor = Color.DimGray });
            infoCard.Controls.Add(new Label { Text = "\u2302  Addis Ababa, Ethiopia", Location = new Point(20, 140), AutoSize = true, ForeColor = Color.DimGray });

            Button btnEdit = new Button { Text = "Edit Profile", Location = new Point(20, 250), Size = new Size(310, 45), BackColor = primaryColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold), Cursor = Cursors.Hand };
            UIHelper.SetRoundedRegion(btnEdit, 8);
            infoCard.Controls.Add(btnEdit);

            // Recent Orders section
            Label recTitle = new Label { Text = "Recent Order History", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(410, 280), AutoSize = true, ForeColor = Color.FromArgb(44, 62, 80) };
            content.Controls.Add(recTitle);

            ordersFlow = new FlowLayoutPanel { Location = new Point(410, 320), Size = new Size(content.Width - 450, 400), AutoScroll = true, BackColor = Color.Transparent, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom };
            content.Controls.Add(ordersFlow);
        }

        private Button AddSidebarBtn(string text, string icon, int y)
        {
            Button btn = new Button {
                Text = "  " + icon + "    " + text,
                Location = new Point(0, y),
                Size = new Size(260, 50),
                BackColor = sidebarColor,
                ForeColor = Color.FromArgb(189, 195, 199),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Padding = new Padding(25, 0, 0, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => { btn.BackColor = Color.FromArgb(52, 73, 94); btn.ForeColor = Color.White; };
            btn.MouseLeave += (s, e) => { btn.BackColor = sidebarColor; btn.ForeColor = Color.FromArgb(189, 195, 199); };
            
            sidebar.Controls.Add(btn);
            return btn;
        }

        private void AddStatCard(Panel container, int x, string title, string val, Color accent)
        {
            Panel card = new Panel { Location = new Point(x, 0), Size = new Size(210, 100), BackColor = Color.White };
            UIHelper.SetRoundedRegion(card, 12);
            container.Controls.Add(card);

            Panel indicator = new Panel { Dock = DockStyle.Left, Width = 6, BackColor = accent };
            card.Controls.Add(indicator);

            Label tLbl = new Label { Text = title, Location = new Point(15, 15), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 9) };
            card.Controls.Add(tLbl);

            Label vLbl = new Label { Name = "val_" + title.Replace(" ", ""), Text = val, Location = new Point(15, 40), AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(44, 62, 80) };
            card.Controls.Add(vLbl);
        }

        private async Task LoadDataAsync()
        {
            try
            {
                if (Program.CurrentUser == null) return;
                
                var orders = await ApiClient.GetCustomerOrdersAsync(Program.CurrentUser.UserID);
                
                if (this.IsDisposed) return;

                this.Invoke(new Action(() => {
                    // Update stats
                    var totalSpent = orders.Where(o => o.Status == "Paid").Sum(o => o.TotalAmount);
                    if (content.Controls.Find("val_TotalOrders", true).FirstOrDefault() is Label oLbl) oLbl.Text = orders.Count.ToString();
                    if (content.Controls.Find("val_TotalSpent", true).FirstOrDefault() is Label sLbl) sLbl.Text = $"{totalSpent:N0} ETB";

                    // Load orders
                    ordersFlow.Controls.Clear();
                    if (orders.Count == 0)
                    {
                        ordersFlow.Controls.Add(new Label { Text = "No orders yet. Enjoy our delicious menu!", AutoSize = true, ForeColor = Color.Gray, Margin = new Padding(0, 50, 0, 0) });
                    }
                    else
                    {
                        foreach (var order in orders.Take(5))
                        {
                            Panel p = MkOrderStrip(order);
                            ordersFlow.Controls.Add(p);
                        }
                    }
                }));
            }
            catch { /* API silent fail */ }
        }

        private Panel MkOrderStrip(OrderDto order)
        {
            Panel p = new Panel { Size = new Size(390, 80), BackColor = Color.White, Margin = new Padding(0, 0, 0, 12) };
            UIHelper.SetRoundedRegion(p, 10);
            
            Label id = new Label { Text = "Order #" + order.OrderID, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true };
            p.Controls.Add(id);

            Label dt = new Label { Text = order.OrderDate.ToString("MMM dd, yyyy"), ForeColor = Color.Gray, Location = new Point(15, 40), AutoSize = true };
            p.Controls.Add(dt);

            Label amt = new Label { Text = $"{order.TotalAmount:N0} ETB", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = accentColor, Location = new Point(260, 15), AutoSize = false, Size = new Size(110, 30), TextAlign = ContentAlignment.TopRight };
            p.Controls.Add(amt);

            string st = order.Status ?? "Pending";
            Label status = new Label { 
                Text = st.ToUpper(), 
                Font = new Font("Segoe UI", 8, FontStyle.Bold), 
                ForeColor = st == "Paid" ? Color.SeaGreen : Color.Orange, 
                Location = new Point(260, 45), 
                AutoSize = false, Size = new Size(110, 20), 
                TextAlign = ContentAlignment.TopRight 
            };
            p.Controls.Add(status);

            return p;
        }
    }
}
