using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class UserMainForm : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public UserMainForm()
        {
            InitializeComponent();
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {
            fadeTimer.Start();
            LoadStats();
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

                // Free Tables
                string tblQuery = "SELECT COUNT(*) FROM Tables WHERE Status='Available'";
                MySqlCommand tblCmd = new MySqlCommand(tblQuery, con);
                lblTablesVal.Text = tblCmd.ExecuteScalar().ToString();

                // Pending Orders
                string ordQuery = "SELECT COUNT(*) FROM Orders WHERE Status='Pending'";
                MySqlCommand ordCmd = new MySqlCommand(ordQuery, con);
                lblPendingVal.Text = ordCmd.ExecuteScalar().ToString();

                // Menu Items
                string menuQuery = "SELECT COUNT(*) FROM MenuItems";
                MySqlCommand menuCmd = new MySqlCommand(menuQuery, con);
                lblMenuVal.Text = menuCmd.ExecuteScalar().ToString();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine("Stats load error: " + ex.Message);
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            LoadForm(new OrderForm());
            lblPanelTitle.Text = "New Order Creation";
        }

        private void btnManageTables_Click(object sender, EventArgs e)
        {
            LoadForm(new TableForm());
            lblPanelTitle.Text = "Table Status Management";
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            LoadForm(new PaymentForm());
            lblPanelTitle.Text = "Process Customer Payments";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void LoadForm(Form form)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(statsPanel);
            
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(form);
            form.Show();
        }
    }
}
