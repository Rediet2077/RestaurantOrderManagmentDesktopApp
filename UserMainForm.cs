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
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            UIHelper.ApplyModernButton(btnCreateOrder, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnManageTables, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnPayments, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnLogout, Color.FromArgb(231, 76, 60));
            
            lblGreeting.Text = $"{UIHelper.GetGreeting()}, Staff";
            lblTime.Text = DateTime.Now.ToString("T");

            // Rounded cards
            UIHelper.SetRoundedRegion(cardTables, 15);
            UIHelper.SetRoundedRegion(cardPending, 15);
            UIHelper.SetRoundedRegion(cardMenu, 15);
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
            contentPanel.Controls.Clear();
            
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(form);
            form.Show();
        }
    }
}
