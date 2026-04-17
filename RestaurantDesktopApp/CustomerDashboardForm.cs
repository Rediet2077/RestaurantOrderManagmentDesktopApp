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

            // Load orders from API
            _ = LoadCustomerOrdersAsync(ordersPanel);

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
                new LandingForm().Show();
                this.Close();
            };
            this.Controls.Add(btnLogout);
        }

        private async System.Threading.Tasks.Task LoadCustomerOrdersAsync(Panel ordersPanel)
        {
            try
            {
                if (Program.CurrentUser == null) return;

                var orders = await ApiClient.GetCustomerOrdersAsync(Program.CurrentUser.UserID);

                if (orders.Count == 0)
                {
                    ordersPanel.Controls.Add(new Label
                    {
                        Text = "No orders yet. Place your first order!",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.Gray,
                        Location = new Point(30, 80),
                        AutoSize = true
                    });
                    return;
                }

                int yPos = 80;
                int maxShow = Math.Min(orders.Count, 4);
                for (int i = 0; i < maxShow; i++)
                {
                    var order = orders[i];
                    Panel orderCard = new Panel
                    {
                        Location = new Point(30, yPos),
                        Size = new Size(790, 60),
                        BackColor = Color.FromArgb(249, 250, 251)
                    };
                    UIHelper.SetRoundedRegion(orderCard, 10);
                    ordersPanel.Controls.Add(orderCard);

                    orderCard.Controls.Add(new Label
                    {
                        Text = $"Order #{order.OrderID}",
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        Location = new Point(20, 10),
                        AutoSize = true
                    });
                    orderCard.Controls.Add(new Label
                    {
                        Text = $"{UIHelper.GetCurrencySymbol()} {order.TotalAmount:N2} — {order.OrderDate:MMM dd, yyyy}",
                        Font = new Font("Segoe UI", 10),
                        Location = new Point(20, 32),
                        ForeColor = Color.DimGray,
                        AutoSize = true
                    });

                    Color statusColor = order.Status switch
                    {
                        "Paid" => Color.FromArgb(34, 197, 94),
                        "Pending" => Color.FromArgb(250, 163, 7),
                        "Cancelled" => Color.FromArgb(231, 76, 60),
                        _ => Color.FromArgb(37, 99, 235)
                    };

                    orderCard.Controls.Add(new Label
                    {
                        Text = order.Status,
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        Location = new Point(680, 20),
                        ForeColor = statusColor,
                        AutoSize = true
                    });

                    yPos += 70;
                }

                if (orders.Count > maxShow)
                {
                    ordersPanel.Controls.Add(new Label
                    {
                        Text = $"+ {orders.Count - maxShow} more orders...",
                        Font = new Font("Segoe UI", 11),
                        ForeColor = Color.Gray,
                        Location = new Point(30, yPos + 10),
                        AutoSize = true
                    });
                }
            }
            catch (Exception ex)
            {
                ordersPanel.Controls.Add(new Label
                {
                    Text = "Could not load orders. Make sure the API is running.",
                    Font = new Font("Segoe UI", 11),
                    ForeColor = Color.FromArgb(231, 76, 60),
                    Location = new Point(30, 80),
                    AutoSize = true
                });
                Console.WriteLine("Load orders error: " + ex.Message);
            }
        }
    }
}
