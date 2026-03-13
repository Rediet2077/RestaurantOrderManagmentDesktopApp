using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class AdminMainForm : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public AdminMainForm()
        {
            InitializeComponent();
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            fadeTimer.Start();
            LoadStats();
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            UIHelper.ApplyModernButton(btnManageMenu, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnReports, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnStaff, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnSettings, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnLogout, Color.FromArgb(231, 76, 60)); // Brighter red on hover
            
            lblGreeting.Text = $"{UIHelper.GetGreeting()}, Admin";
            lblTime.Text = DateTime.Now.ToString("T");

            // Rounded cards
            UIHelper.SetRoundedRegion(cardRevenue, 15);
            UIHelper.SetRoundedRegion(cardOrders, 15);
            UIHelper.SetRoundedRegion(cardTables, 15);
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("T");
        }

        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
            else
                fadeTimer.Stop();
        }

        private void LoadStats()
        {
            try
            {
                con.Open();
                
                // Revenue
                string revQuery = "SELECT SUM(TotalAmount) FROM Orders WHERE Status='Paid'";
                MySqlCommand revCmd = new MySqlCommand(revQuery, con);
                object rev = revCmd.ExecuteScalar();
                lblRevenueVal.Text = (rev != DBNull.Value && rev != null) ? $"${Convert.ToDecimal(rev):N2}" : "$0.00";

                // Orders
                string ordQuery = "SELECT COUNT(*) FROM Orders";
                MySqlCommand ordCmd = new MySqlCommand(ordQuery, con);
                lblOrdersVal.Text = ordCmd.ExecuteScalar().ToString();

                // Active Tables
                string tblQuery = "SELECT COUNT(*) FROM Tables WHERE Status='Occupied'";
                MySqlCommand tblCmd = new MySqlCommand(tblQuery, con);
                lblTablesVal.Text = tblCmd.ExecuteScalar().ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                // Silently fail or log to Console
                Console.WriteLine("Stats load error: " + ex.Message);
            }
        }

        private void btnManageMenu_Click(object sender, EventArgs e)
        {
            LoadForm(new Menu_Form());
            lblPanelTitle.Text = "Menu Management";
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            LoadForm(new ReportForm());
            lblPanelTitle.Text = "Daily Sales Reports";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            LoadForm(new StaffForm());
            lblPanelTitle.Text = "Staff management & Access Control";
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadForm(new SettingsForm());
            lblPanelTitle.Text = "System Configuration";
        }

        private void LoadForm(Form form)
        {
            contentPanel.Controls.Clear();
            
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(form);
            form.Show();
        }
    }
}
