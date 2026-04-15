using System;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public class CustomerDashboardForm : Form
    {
        public CustomerDashboardForm()
        {
            this.Text = "User Dashboard";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 248);

            Label lblTitle = new Label
            {
                Text = $"Welcome back, {Program.CurrentUser?.Name ?? "Guest"}!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(17, 24, 39),
                AutoSize = true,
                Location = new Point(40, 40)
            };
            this.Controls.Add(lblTitle);

            Label lblEmail = new Label
            {
                Text = $"Account Email: {Program.CurrentUser?.Email}",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(40, 95)
            };
            this.Controls.Add(lblEmail);

            Panel ordersPanel = new Panel
            {
                Location = new Point(40, 150),
                Size = new Size(850, 360),
                BackColor = Color.White
            };
            UIHelper.SetRoundedRegion(ordersPanel, 16);
            this.Controls.Add(ordersPanel);

            Label lblOrders = new Label
            {
                Text = "Your Recent Orders",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(30, 25),
                AutoSize = true,
                ForeColor = Color.FromArgb(17, 24, 39)
            };
            ordersPanel.Controls.Add(lblOrders);

            // Mock order item
            Panel mockOrder = new Panel
            {
                Location = new Point(30, 80),
                Size = new Size(790, 80),
                BackColor = Color.FromArgb(249, 250, 251)
            };
            UIHelper.SetRoundedRegion(mockOrder, 10);
            ordersPanel.Controls.Add(mockOrder);

            mockOrder.Controls.Add(new Label {
                Text = "Order #1042",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            });
            mockOrder.Controls.Add(new Label {
                Text = "2x Burger, 1x Coke",
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 40),
                ForeColor = Color.DimGray,
                AutoSize = true
            });
            mockOrder.Controls.Add(new Label {
                Text = "Processing",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(680, 30),
                ForeColor = Color.FromArgb(250, 163, 7),
                AutoSize = true
            });

            Label lblNoOrders = new Label
            {
                Text = "More orders will appear here as you make purchases.",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(30, 180),
                AutoSize = true
            };
            ordersPanel.Controls.Add(lblNoOrders);

            Button btnNewOrder = new Button
            {
                Text = "Create New Order",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(250, 163, 7),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(40, 540),
                Size = new Size(200, 50),
                Cursor = Cursors.Hand
            };
            btnNewOrder.FlatAppearance.BorderSize = 0;
            UIHelper.SetRoundedRegion(btnNewOrder, 10);
            btnNewOrder.Click += (s, e) => {
                new LandingForm().Show();
                this.Hide();
            };
            this.Controls.Add(btnNewOrder);
            
            Button btnLogout = new Button
            {
                Text = "Logout",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(260, 540),
                Size = new Size(120, 50),
                Cursor = Cursors.Hand
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            UIHelper.SetRoundedRegion(btnLogout, 10);
            btnLogout.Click += (s, e) => {
                Program.IsLoggedIn = false;
                Program.CurrentUser = null;
                // Landing form will prompt login when needed
                new LandingForm().Show();
                this.Close();
            };
            this.Controls.Add(btnLogout);
        }
    }
}
