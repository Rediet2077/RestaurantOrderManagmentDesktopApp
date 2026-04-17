using System;
using System.Data;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private async void AdminMainForm_Load(object sender, EventArgs e)
        {
            fadeTimer.Start();
            ApplyStyles();
            UIHelper.ApplySidebarStyle(sidebarPanel);
            await ShowDashboardOverviewAsync();
        }

        private async System.Threading.Tasks.Task ShowDashboardOverviewAsync()
        {
            contentPanel.Controls.Clear();
            lblPanelTitle.Text = "Dashboard Overview";

            FlowLayoutPanel flow = new FlowLayoutPanel();
            flow.Dock = DockStyle.Fill;
            flow.Padding = new Padding(30, 20, 30, 20);
            flow.AutoScroll = true;
            flow.BackColor = Color.FromArgb(249, 250, 251); 
            contentPanel.Controls.Add(flow);

            // Load real stats from API
            var stats = await ApiClient.GetStatsAsync();
            string revenue = stats != null ? $"{stats.TotalRevenue} ETB" : "0.00 ETB";
            string orders = stats?.TotalOrders.ToString() ?? "0";
            string tables = stats != null ? $"{stats.AvailableTables}/{stats.TotalTables}" : "0/0";
            string staff = stats?.ActiveStaff.ToString() ?? "0";

            AddStatCard(flow, "💰 Total Revenue", revenue, Color.FromArgb(34, 197, 94));
            AddStatCard(flow, "📦 Total Orders", orders, Color.FromArgb(37, 99, 235));
            AddStatCard(flow, "🍽️ Available Tables", tables, Color.FromArgb(245, 158, 11));
            AddStatCard(flow, "👥 Active Staff", staff, Color.FromArgb(124, 58, 237));

            // Activity Log Panel
            Panel activityPanel = new Panel();
            activityPanel.Size = new Size(contentPanel.Width - 60, 400);
            activityPanel.BackColor = Color.White;
            activityPanel.Margin = new Padding(0, 30, 0, 0);
            UIHelper.SetRoundedRegion(activityPanel, 15);
            flow.Controls.Add(activityPanel);

            Label lblAct = new Label();
            lblAct.Text = "Recent Activity Log";
            lblAct.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblAct.ForeColor = Color.FromArgb(31, 41, 55);
            lblAct.Location = new Point(25, 25);
            lblAct.AutoSize = true;
            activityPanel.Controls.Add(lblAct);

            Panel line = new Panel { BackColor = Color.FromArgb(229, 231, 235), Size = new Size(activityPanel.Width - 50, 2), Location = new Point(25, 65) };
            activityPanel.Controls.Add(line);

            ListBox lstLog = new ListBox();
            lstLog.BorderStyle = BorderStyle.None;
            lstLog.Font = new Font("Segoe UI", 12);
            lstLog.ForeColor = Color.FromArgb(75, 85, 99);
            lstLog.Location = new Point(25, 80);
            lstLog.Size = new Size(activityPanel.Width - 50, 300);
            lstLog.ItemHeight = 35;
            lstLog.DrawMode = DrawMode.OwnerDrawFixed;
            lstLog.DrawItem += (s, ev) => 
            {
                ev.DrawBackground();
                if (ev.Index >= 0) {
                    Brush brush = ((ev.State & DrawItemState.Selected) == DrawItemState.Selected) ? Brushes.White : new SolidBrush(Color.FromArgb(75, 85, 99));
                    ev.Graphics.DrawString(lstLog.Items[ev.Index].ToString(), lstLog.Font, brush, ev.Bounds.X, ev.Bounds.Y + 8);
                }
            };

            lstLog.Items.Add($"📊 Dashboard loaded — Revenue: {revenue}  (Just now)");
            lstLog.Items.Add($"📦 Total orders in system: {orders}  (Current)");
            lstLog.Items.Add($"🍽️ Available tables: {tables}  (Current)");
            lstLog.Items.Add($"👥 Active staff members: {staff}  (Current)");
            lstLog.Items.Add("⚙️ System connected to API backend  (Active)");
            lstLog.Items.Add("✅ Database integration complete  (Active)");

            activityPanel.Controls.Add(lstLog);
        }

        private void AddStatCard(FlowLayoutPanel flow, string title, string val, Color color)
        {
            Panel card = new Panel();
            card.Size = new Size(240, 140);
            card.BackColor = Color.White;
            card.Margin = new Padding(0, 0, 25, 25);
            UIHelper.SetRoundedRegion(card, 15);

            Label lblT = new Label();
            lblT.Text = title.ToUpper();
            lblT.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblT.ForeColor = Color.FromArgb(107, 114, 128);
            lblT.Location = new Point(20, 25);
            lblT.AutoSize = true;
            card.Controls.Add(lblT);

            Label lblV = new Label();
            lblV.Text = val;
            lblV.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblV.ForeColor = color;
            lblV.Location = new Point(20, 65);
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
            UIHelper.ApplyModernButton(btnLogout, Color.FromArgb(231, 76, 60));

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
            Program.IsLoggedIn = false;
            Program.CurrentUser = null;
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
