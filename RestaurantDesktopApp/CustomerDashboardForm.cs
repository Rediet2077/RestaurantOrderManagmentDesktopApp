using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public class CustomerDashboardForm : Form
    {
        private Panel sidebar = null!;
        private Panel mainContent = null!;
        private Label lblPanelTitle = null!;
        private Color primaryColor = Color.FromArgb(41, 128, 185);
        private Color sidebarColor = Color.FromArgb(15, 23, 42); // Darker blue-black
        private Color accentColor = Color.FromArgb(250, 163, 7); // Gold/Orange

        public CustomerDashboardForm()
        {
            SetupUI();
            this.Load += (s, e) => {
                // Highlight dashboard button by default
                foreach (Control c in sidebar.Controls)
                {
                    if (c is Button b && b.Text.Contains("DASHBOARD"))
                    {
                        b.PerformClick();
                        break;
                    }
                }
            };
        }

        private void SetupUI()
        {
            this.Text = "My Account - BEST Restaurants";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(243, 244, 246);
            this.Font = new Font("Segoe UI", 10);
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Sidebar
            sidebar = new Panel { Dock = DockStyle.Left, Width = 260, BackColor = sidebarColor };
            this.Controls.Add(sidebar);

            Panel logoBox = new Panel { Height = 100, Dock = DockStyle.Top, Padding = new Padding(20) };
            Label logo = new Label { Text = "BEST ACCOUNT", ForeColor = Color.White, Font = new Font("Segoe UI", 16, FontStyle.Bold), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
            logoBox.Controls.Add(logo);
            sidebar.Controls.Add(logoBox);

            AddSidebarBtn("DASHBOARD", "🏠", 100, ShowOverview);
            AddSidebarBtn("MY ORDERS", "📦", 160, ShowOrders);
            AddSidebarBtn("CREATE ORDER", "➕", 220, OpenOrderForm);
            AddSidebarBtn("ACCOUNT SETTINGS", "⚙️", 280, ShowSettings);

            Button btnLogout = new Button { 
                Text = "🚪  Logout", 
                Dock = DockStyle.Bottom, 
                Height = 60, 
                BackColor = Color.FromArgb(220, 38, 38), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Font = new Font("Segoe UI", 11, FontStyle.Bold), 
                Cursor = Cursors.Hand 
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) => {
                Program.IsLoggedIn = false;
                Program.CurrentUser = null;
                this.Close();
                new LandingForm().Show();
            };
            sidebar.Controls.Add(btnLogout);

            // Header/Main Area
            Panel header = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White };
            this.Controls.Add(header);

            lblPanelTitle = new Label { Text = "Dashboard Overview", Font = new Font("Segoe UI Semibold", 16), ForeColor = Color.FromArgb(31, 41, 55), Location = new Point(25, 20), AutoSize = true };
            header.Controls.Add(lblPanelTitle);

            mainContent = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30), AutoScroll = true };
            this.Controls.Add(mainContent);
            mainContent.BringToFront();
        }

        private void AddSidebarBtn(string text, string icon, int y, Action onClick)
        {
            Button btn = new Button {
                Text = "  " + icon + "    " + text,
                Location = new Point(0, y),
                Size = new Size(260, 50),
                BackColor = sidebarColor,
                ForeColor = Color.FromArgb(156, 163, 175),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btn.Click += (s, e) => {
                foreach (Control c in sidebar.Controls) if (c is Button b) b.ForeColor = Color.FromArgb(156, 163, 175);
                btn.ForeColor = accentColor;
                onClick();
            };
            sidebar.Controls.Add(btn);
        }

        private void ShowOverview()
        {
            lblPanelTitle.Text = "Dashboard Overview";
            mainContent.Controls.Clear();

            FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true };
            mainContent.Controls.Add(flow);

            Label lblWelcome = new Label { Text = $"Welcome back, {Program.CurrentUser?.Name}!", Font = new Font("Segoe UI", 20, FontStyle.Bold), Margin = new Padding(0, 0, 0, 20), AutoSize = true };
            flow.Controls.Add(lblWelcome);
            flow.SetFlowBreak(lblWelcome, true);

            // Stats Cards
            int gridW = mainContent.ClientSize.Width > 100 ? mainContent.ClientSize.Width - 60 : 800;
            FlowLayoutPanel statsFlow = new FlowLayoutPanel { Width = gridW, Height = 140 };
            flow.Controls.Add(statsFlow);

            AddStatCard(statsFlow, "Total Orders", "...", Color.FromArgb(59, 130, 246));
            AddStatCard(statsFlow, "Total Spent", "...", Color.FromArgb(16, 185, 129));
            AddStatCard(statsFlow, "Member Since", Program.CurrentUser?.CreatedAt.ToString("MMM yyyy") ?? "N/A", Color.FromArgb(139, 92, 246));

            // Two columns layout for Overview
            Panel cols = new Panel { Width = gridW, Height = 500, Margin = new Padding(0, 20, 0, 0) };
            flow.Controls.Add(cols);

            // Left: Profile Summary
            Panel profileCard = new Panel { Width = 350, Height = 400, BackColor = Color.White, Location = new Point(0, 0) };
            UIHelper.SetRoundedRegion(profileCard, 15);
            cols.Controls.Add(profileCard);
            
            Label pTitle = new Label { Text = "Profile Details", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(25, 25), AutoSize = true };
            profileCard.Controls.Add(pTitle);

            AddProfileInfo(profileCard, "📧 Email", Program.CurrentUser?.Email ?? "", 80);
            AddProfileInfo(profileCard, "📱 Phone", Program.CurrentUser?.Phone ?? "", 130);
            AddProfileInfo(profileCard, "📍 Location", "Addis Ababa, Ethiopia", 180);

            Button btnEdit = new Button { Text = "Edit Profile", Location = new Point(25, 320), Size = new Size(300, 45), BackColor = Color.FromArgb(243, 244, 246), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            UIHelper.SetRoundedRegion(btnEdit, 8);
            btnEdit.Click += (s, e) => ShowSettings();
            profileCard.Controls.Add(btnEdit);

            // Right: Recent Orders
            Panel recentCard = new Panel { Width = cols.Width - 380, Height = 400, BackColor = Color.White, Location = new Point(380, 0), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            UIHelper.SetRoundedRegion(recentCard, 15);
            cols.Controls.Add(recentCard);

            Label rTitle = new Label { Text = "Recent Orders", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(25, 25), AutoSize = true };
            recentCard.Controls.Add(rTitle);

            FlowLayoutPanel recFlow = new FlowLayoutPanel { Name = "recFlow", Location = new Point(25, 70), Size = new Size(recentCard.Width - 50, 300), AutoScroll = true, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            recentCard.Controls.Add(recFlow);

            _ = LoadOverviewDataAsync(statsFlow, recFlow);
        }

        private void AddProfileInfo(Panel p, string label, string val, int y)
        {
            Label l = new Label { Text = label, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(25, y), AutoSize = true };
            Label v = new Label { Text = val ?? "N/A", Font = new Font("Segoe UI", 11), Location = new Point(25, y + 20), AutoSize = true };
            p.Controls.Add(l);
            p.Controls.Add(v);
        }

        private void AddStatCard(FlowLayoutPanel container, string title, string val, Color color)
        {
            Panel card = new Panel { Width = 220, Height = 100, BackColor = Color.White, Margin = new Padding(0, 0, 20, 0) };
            UIHelper.SetRoundedRegion(card, 15);
            container.Controls.Add(card);

            Panel accent = new Panel { Dock = DockStyle.Left, Width = 6, BackColor = color };
            card.Controls.Add(accent);

            Label t = new Label { Text = title, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(20, 20), AutoSize = true };
            Label v = new Label { Name = "stat_" + title.Replace(" ", ""), Text = val, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(31, 41, 55), Location = new Point(20, 45), AutoSize = true };
            card.Controls.Add(t);
            card.Controls.Add(v);
        }

        private async Task LoadOverviewDataAsync(FlowLayoutPanel stats, FlowLayoutPanel recFlow)
        {
            try
            {
                if (Program.CurrentUser == null) return;
                var orders = await ApiClient.GetCustomerOrdersAsync(Program.CurrentUser.UserID);
                
                if (this.IsDisposed) return;

                this.Invoke(new Action(() => {
                    var totalSpent = orders.Where(o => o.Status == "Paid").Sum(o => o.TotalAmount);
                    if (stats.Controls.Find("stat_TotalOrders", true).FirstOrDefault() is Label oL) oL.Text = orders.Count.ToString();
                    if (stats.Controls.Find("stat_TotalSpent", true).FirstOrDefault() is Label sL) sL.Text = $"{totalSpent:N0} ETB";

                    recFlow.Controls.Clear();
                    if (orders.Count == 0)
                    {
                        recFlow.Controls.Add(new Label { Text = "No orders yet. Place your first order today!", AutoSize = true, Margin = new Padding(0, 50, 0, 0) });
                    }
                    else
                    {
                        foreach (var o in orders.Take(4))
                        {
                            recFlow.Controls.Add(MkOrderStrip(o, recFlow.Width - 30));
                        }
                    }
                }));
            }
            catch { }
        }

        private void ShowOrders()
        {
            lblPanelTitle.Text = "My Order History";
            mainContent.Controls.Clear();

            FlowLayoutPanel flow = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, Padding = new Padding(0, 0, 20, 0) };
            mainContent.Controls.Add(flow);

            _ = LoadFullOrdersAsync(flow);
        }

        private async Task LoadFullOrdersAsync(FlowLayoutPanel flow)
        {
            try
            {
                if (Program.CurrentUser == null) return;
                var orders = await ApiClient.GetCustomerOrdersAsync(Program.CurrentUser.UserID);
                this.Invoke(new Action(() => {
                    flow.Controls.Clear();
                    foreach (var o in orders)
                    {
                        flow.Controls.Add(MkOrderStrip(o, mainContent.Width - 100));
                    }
                }));
            }
            catch { }
        }

        private Control MkOrderStrip(OrderModel o, int w)
        {
            Panel p = new Panel { Size = new Size(w, 70), BackColor = Color.FromArgb(249, 250, 251), Margin = new Padding(0, 0, 0, 10) };
            UIHelper.SetRoundedRegion(p, 10);

            Label id = new Label { Text = "#" + o.OrderID, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(15, 25), AutoSize = true };
            Label date = new Label { Text = o.OrderDate.ToString("MMM dd, yyyy"), Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(80, 25), AutoSize = true };
            Label amt = new Label { Text = o.TotalAmount.ToString("N0") + " ETB", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.FromArgb(31, 41, 55), Location = new Point(w - 200, 24), AutoSize = true };
            
            Label status = new Label { 
                Text = o.Status.ToUpper(), 
                Font = new Font("Segoe UI", 8, FontStyle.Bold), 
                ForeColor = Color.White, 
                BackColor = o.Status == "Paid" ? Color.FromArgb(16, 185, 129) : Color.FromArgb(245, 158, 11),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(80, 24),
                Location = new Point(w - 90, 23)
            };
            UIHelper.SetRoundedRegion(status, 12);

            p.Controls.Add(id); p.Controls.Add(date); p.Controls.Add(amt); p.Controls.Add(status);
            return p;
        }

        private void OpenOrderForm()
        {
            var orderForm = new OrderForm();
            orderForm.ShowDialog();
            ShowOverview(); // Refresh
        }

        private void ShowSettings()
        {
            lblPanelTitle.Text = "Account Settings";
            mainContent.Controls.Clear();
            Label lbl = new Label { Text = "Setting page under construction. Contact support to update profile.", AutoSize = true, Location = new Point(20, 20) };
            mainContent.Controls.Add(lbl);
        }
    }

    public class OrderModel
    {
        public int OrderID { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
        public DateTime OrderDate { get; set; }
    }
}
