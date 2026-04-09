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
            this.WindowState = FormWindowState.Maximized;
        }

        private void AdminMainForm_Load(object sender, EventArgs e)
        {
            fadeTimer.Start();
            ApplyStyles();
            UIHelper.ApplySidebarStyle(sidebarPanel);
            ShowDashboardOverview();
        }

        private void ShowDashboardOverview()
        {
            contentPanel.Controls.Clear();
            lblPanelTitle.Text = "Dashboard Overview";

            FlowLayoutPanel flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.Padding = new Padding(20);
            flow.AutoScroll = true;
            contentPanel.Controls.Add(flow);

            AddStatCard(flow, "Total Revenue", "$12,450", UIHelper.SuccessColor);
            AddStatCard(flow, "Daily Orders", "48", UIHelper.AccentColor);
            AddStatCard(flow, "Available Tables", "12/20", UIHelper.PrimaryColor);
            AddStatCard(flow, "Active Staff", "6", UIHelper.AccentColor);

            // Placeholder for a bigger chart or activity feed
            Panel activityPanel = new Panel();
            activityPanel.Size = new Size(contentPanel.Width - 60, 300);
            activityPanel.BackColor = Color.White;
            activityPanel.Margin = new Padding(0, 20, 0, 0);
            UIHelper.SetRoundedRegion(activityPanel, 15);
            flow.Controls.Add(activityPanel);

            Label lblAct = new Label();
            lblAct.Text = "Recent Activity Log";
            lblAct.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblAct.Location = new Point(20, 20);
            lblAct.AutoSize = true;
            activityPanel.Controls.Add(lblAct);

            ListBox lstLog = new ListBox();
            lstLog.BorderStyle = BorderStyle.None;
            lstLog.Font = new Font("Segoe UI", 10);
            lstLog.Location = new Point(20, 50);
            lstLog.Size = new Size(activityPanel.Width - 40, 230);
            lstLog.Items.Add("[\u2705] Order #1023 completed - $45.00");
            lstLog.Items.Add("[\uD83D\uDCC4] Inventory updated: Chicken Breast +20kg");
            lstLog.Items.Add("[\u26A0\uFE0F] Low stock alert: Table Salt");
            lstLog.Items.Add("[\uD83D\uDC64] Staff 'Rediet' clocked in at 08:30 AM");
            activityPanel.Controls.Add(lstLog);
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
            UIHelper.ApplyModernButton(btnManageMenu, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnReports, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnStaff, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnSettings, UIHelper.ControlColor);
            UIHelper.ApplyModernButton(btnLogout, Color.FromArgb(231, 76, 60)); // Brighter red on hover

            lblGreeting.Text = $"{UIHelper.GetGreeting()}, Admin";
            lblTime.Text = DateTime.Now.ToString("T");
            lblAdminTitle.Text = UIHelper.RestaurantName;

            try
            {
                string logoPath = System.IO.Path.Combine(Application.StartupPath, @"..\..\..\Resources\logo.png");
                if (System.IO.File.Exists(logoPath))
                    picLogo.Image = Image.FromFile(logoPath);
            }
            catch { }
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

        private void contentPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
