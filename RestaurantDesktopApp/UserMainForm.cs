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
            this.WindowState = FormWindowState.Maximized;
        }

        private void UserMainForm_Load(object sender, EventArgs e)
        {
            fadeTimer.Start();
            LoadStats();
            ApplyStyles();
            UIHelper.ApplySidebarStyle(sidebarPanel);
            ShowStaffOverview();
        }

        private void ShowStaffOverview()
        {
            contentPanel.Controls.Clear();
            lblPanelTitle.Text = "Staff Home Overview";

            FlowLayoutPanel flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.Padding = new Padding(20);
            flow.AutoScroll = true;
            contentPanel.Controls.Add(flow);

            AddStatCard(flow, "Current Tables", lblTablesVal.Text, UIHelper.SuccessColor);
            AddStatCard(flow, "Pending Orders", lblPendingVal.Text, UIHelper.AccentColor);
            AddStatCard(flow, "Menu Items", lblMenuVal.Text, UIHelper.PrimaryColor);

            Panel welcomePanel = new Panel();
            welcomePanel.Size = new Size(contentPanel.Width - 60, 200);
            welcomePanel.BackColor = Color.White;
            welcomePanel.Margin = new Padding(0, 20, 0, 0);
            UIHelper.SetRoundedRegion(welcomePanel, 15);
            flow.Controls.Add(welcomePanel);

            Label lblW = new Label();
            lblW.Text = "Welcome to your Shift!";
            lblW.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblW.Location = new Point(20, 20);
            lblW.AutoSize = true;
            welcomePanel.Controls.Add(lblW);

            Label lblNote = new Label();
            lblNote.Text = "Use the sidebar to create new orders, manage table status, and process payments.\nKeep up the great work!";
            lblNote.Font = new Font("Segoe UI", 11);
            lblNote.Location = new Point(20, 60);
            lblNote.Size = new Size(welcomePanel.Width - 40, 100);
            welcomePanel.Controls.Add(lblNote);
        }

        private void AddStatCard(FlowLayoutPanel flow, string title, string val, Color color)
        {
            Panel card = new Panel();
            card.Size = new Size(220, 120);
            card.BackColor = Color.White;
            card.Margin = new Padding(0, 0, 20, 20);
            UIHelper.SetRoundedRegion(card, 15);

            Label lblT = new Label();
            lblT.Text = title;
            lblT.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblT.ForeColor = Color.Gray;
            lblT.Location = new Point(20, 20);
            lblT.AutoSize = true;
            card.Controls.Add(lblT);

            Label lblV = new Label();
            lblV.Text = val;
            lblV.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblV.ForeColor = color;
            lblV.Location = new Point(20, 50);
            lblV.AutoSize = true;
            card.Controls.Add(lblV);

            flow.Controls.Add(card);
        }

        private void ApplyStyles()
        {
            UIHelper.ApplyTheme(this);
            UIHelper.ApplyModernButton(btnCreateOrder, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnManageTables, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnPayments, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnLogout, Color.FromArgb(231, 76, 60));
            
            lblGreeting.Text = $"{UIHelper.GetGreeting()}, Staff";
            lblTime.Text = DateTime.Now.ToString("T");
            lblUserTitle.Text = UIHelper.RestaurantName;

            try
            {
                string logoPath = System.IO.Path.Combine(Application.StartupPath, @"..\..\..\Resources\logo.png");
                if (System.IO.File.Exists(logoPath))
                    picLogo.Image = Image.FromFile(logoPath);
            }
            catch { }

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
            UIHelper.ApplyTheme(form);
            form.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }
    }
}
