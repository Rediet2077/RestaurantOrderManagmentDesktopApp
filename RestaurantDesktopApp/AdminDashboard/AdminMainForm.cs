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

        private async void AdminMainForm_Load(object? sender, EventArgs e)
        {
            fadeTimer.Start();
            ApplyStyles();
            UIHelper.ApplySidebarStyle(sidebarPanel);
            BuildSidebar();
            await ShowDashboardOverviewAsync();
        }


        private async System.Threading.Tasks.Task ShowDashboardOverviewAsync()
        {
            contentPanel.Controls.Clear();
            contentPanel.BackColor = Color.FromArgb(249, 250, 251);
            lblPanelTitle.Text = "Dashboard";

            Label lblSub = new Label { Text = "Welcome back, Admin! Here's what's happening with your restaurant today.", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(30, 15), AutoSize = true };
            contentPanel.Controls.Add(lblSub);

            // ── Fetch real data ──────────────────────────────────────────
            var stats   = await ApiClient.GetStatsAsync();
            var orders  = await ApiClient.GetPendingOrdersTableAsync();
            var menu    = await ApiClient.GetMenuItemsAsync();
            var tables  = await ApiClient.GetTablesAsync();

            string revenue   = stats != null ? $"${decimal.Parse(stats.TotalRevenue):N2}" : "$0.00";
            string totalOrd  = stats?.TotalOrders.ToString() ?? "0";
            string staffCnt  = stats?.ActiveStaff.ToString() ?? "0";
            string tableStr  = stats != null ? $"{stats.AvailableTables} / {stats.TotalTables}" : "0 / 0";

            // ── Stat cards ────────────────────────────────────────────────
            int cardW = (contentPanel.Width - 120) / 4;
            Panel pnlStats = new Panel { Location = new Point(30, 50), Size = new Size(contentPanel.Width - 60, 100), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            contentPanel.Controls.Add(pnlStats);

            AddModernStatCard(pnlStats, "Total Orders",    totalOrd,  $"↑ {stats?.PendingOrders ?? 0} pending",    Color.FromArgb(59, 130, 246),  0,             cardW);
            AddModernStatCard(pnlStats, "Total Revenue",   revenue,   $"{menu.Count} menu items",                  Color.FromArgb(34, 197, 94),   cardW + 20,    cardW);
            AddModernStatCard(pnlStats, "Active Staff",    staffCnt,  "Online now",                                Color.FromArgb(168, 85, 247),  (cardW+20)*2,  cardW);
            AddModernStatCard(pnlStats, "Active Tables",   tableStr,  $"{stats?.AvailableTables ?? 0} available",  Color.FromArgb(249, 115, 22),  (cardW+20)*3,  cardW);

            // ── Row 2: Revenue Chart + Recent Orders ──────────────────────
            Panel pnlRow2 = new Panel { Location = new Point(30, 170), Size = new Size(contentPanel.Width - 60, 300), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            contentPanel.Controls.Add(pnlRow2);

            int leftColW = (int)(pnlRow2.Width * 0.60);
            Panel pnlChart = MkCardPanel(new Size(leftColW - 10, 300), new Point(0, 0));
            pnlChart.Controls.Add(new Label { Text = "Revenue Overview", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true, ForeColor = Color.FromArgb(17, 24, 39) });
            pnlChart.Paint += (s, e) => {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Pen pen = new Pen(Color.FromArgb(59, 130, 246), 3);
                Point[] pts = { new Point(30, 230), new Point(120, 160), new Point(210, 100), new Point(300, 180), new Point(390, 120), new Point(480, 160), new Point(570, 140) };
                if (pnlChart.Width > 500) {
                    try { e.Graphics.DrawCurve(pen, pts); } catch {}
                    foreach (var pt in pts) { e.Graphics.FillEllipse(Brushes.White, pt.X - 6, pt.Y - 6, 12, 12); e.Graphics.DrawEllipse(pen, pt.X - 6, pt.Y - 6, 12, 12); }
                }
            };
            pnlRow2.Controls.Add(pnlChart);

            // Recent Orders panel — real data
            Panel pnlRecent = MkCardPanel(new Size(pnlRow2.Width - leftColW - 10, 300), new Point(leftColW + 10, 0));
            pnlRecent.Controls.Add(new Label { Text = "Recent Orders", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true, ForeColor = Color.FromArgb(17, 24, 39) });
            int ri = 0;
            foreach (DataRow row in orders.Rows)
            {
                if (ri >= 5) break;
                string ordId  = "#ORD-" + row["OrderID"];
                string amt    = "$" + Convert.ToDecimal(row["TotalAmount"]).ToString("N2");
                string st     = row["Status"]?.ToString() ?? "";
                Color stColor = st == "Completed" ? Color.SeaGreen : st == "Pending" ? Color.FromArgb(245,158,11) : Color.FromArgb(59,130,246);
                Panel ro = new Panel { Size = new Size(pnlRecent.Width - 40, 50), Location = new Point(20, 60 + ri * 50) };
                ro.Controls.Add(new Label { Text = ordId, Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(10, 8), AutoSize = true });
                ro.Controls.Add(new Label { Text = Convert.ToDateTime(row["OrderDate"]).ToString("hh:mm tt"), Font = new Font("Segoe UI", 8), ForeColor = Color.Gray, Location = new Point(10, 28), AutoSize = true });
                ro.Controls.Add(new Label { Text = st, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = stColor, Location = new Point(130, 15), AutoSize = true });
                ro.Controls.Add(new Label { Text = amt, Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(pnlRecent.Width - 90, 15), AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Right });
                pnlRecent.Controls.Add(ro);
                ri++;
            }
            if (ri == 0)
                pnlRecent.Controls.Add(new Label { Text = "No recent orders.", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(20, 70), AutoSize = true });
            pnlRow2.Controls.Add(pnlRecent);

            // ── Row 3: Top Menu Items + Table Status + Today Overview ─────
            Panel pnlRow3 = new Panel { Location = new Point(30, 490), Size = new Size(contentPanel.Width - 60, 300), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            contentPanel.Controls.Add(pnlRow3);
            int col3 = (pnlRow3.Width - 40) / 3;

            // Top Selling Items — real menu items
            Panel pnlSelling = MkCardPanel(new Size(col3, 300), new Point(0, 0));
            pnlSelling.Controls.Add(new Label { Text = "Menu Items", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true, ForeColor = Color.FromArgb(17, 24, 39) });
            for (int i = 0; i < Math.Min(5, menu.Count); i++)
            {
                var m = menu[i];
                pnlSelling.Controls.Add(new Label { Text = m.Name, Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(15, 60 + i * 44), AutoSize = true });
                pnlSelling.Controls.Add(new Label { Text = m.Category, Font = new Font("Segoe UI", 8), ForeColor = Color.Gray, Location = new Point(15, 76 + i * 44), AutoSize = true });
                pnlSelling.Controls.Add(new Label { Text = $"${m.Price:N2}", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(245,158,11), Location = new Point(col3 - 80, 65 + i * 44), AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Right });
            }
            pnlRow3.Controls.Add(pnlSelling);

            // Table Status — real tables
            Panel pnlTableSt = MkCardPanel(new Size(col3, 300), new Point(col3 + 20, 0));
            pnlTableSt.Controls.Add(new Label { Text = "Table Status", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true, ForeColor = Color.FromArgb(17, 24, 39) });
            int ti = 0;
            foreach (var t in tables)
            {
                if (ti >= 6) break;
                Color tbg = t.Status == "Available" ? Color.FromArgb(220,252,231) : t.Status == "Reserved" ? Color.FromArgb(254,249,195) : Color.FromArgb(254,226,226);
                Color tfg = t.Status == "Available" ? Color.FromArgb(21,128,61)   : t.Status == "Reserved" ? Color.FromArgb(161,98,7)    : Color.FromArgb(185,28,28);
                int tx = (ti % 3) * (col3 / 3), ty = (ti / 3) * 80 + 55;
                Panel tc = new Panel { BackColor = tbg, Location = new Point(tx + 10, ty), Size = new Size(col3/3 - 15, 68) };
                UIHelper.SetRoundedRegion(tc, 6);
                tc.Controls.Add(new Label { Text = t.TableNumber, Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = tfg, Location = new Point(8, 6), AutoSize = true });
                tc.Controls.Add(new Label { Text = t.Status,      Font = new Font("Segoe UI", 7),  ForeColor = tfg, Location = new Point(8, 30), AutoSize = true });
                tc.Controls.Add(new Label { Text = $"{t.Capacity} seats", Font = new Font("Segoe UI", 7), ForeColor = tfg, Location = new Point(8, 46), AutoSize = true });
                pnlTableSt.Controls.Add(tc);
                ti++;
            }
            pnlRow3.Controls.Add(pnlTableSt);

            // Today's Overview donut
            Panel pnlToday = MkCardPanel(new Size(col3, 300), new Point(col3*2 + 40, 0));
            pnlToday.Controls.Add(new Label { Text = "Today's Overview", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true, ForeColor = Color.FromArgb(17, 24, 39) });
            pnlToday.Controls.Add(new Label { Text = $"Total Orders: {totalOrd}", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(20, 60), AutoSize = true });
            pnlToday.Controls.Add(new Label { Text = $"Revenue: {revenue}", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(245,158,11), Location = new Point(20, 85), AutoSize = true });
            pnlToday.Controls.Add(new Label { Text = $"Staff Active: {staffCnt}", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(20, 110), AutoSize = true });
            pnlToday.Controls.Add(new Label { Text = $"Pending Orders: {stats?.PendingOrders ?? 0}", Font = new Font("Segoe UI", 10), ForeColor = Color.FromArgb(239,68,68), Location = new Point(20, 135), AutoSize = true });
            pnlToday.Paint += (s, e) => {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillPie(new SolidBrush(Color.FromArgb(34,197,94)),  new Rectangle(50, 160, 120, 120), -90, 140);
                e.Graphics.FillPie(new SolidBrush(Color.FromArgb(59,130,246)), new Rectangle(50, 160, 120, 120), 50, 90);
                e.Graphics.FillPie(new SolidBrush(Color.FromArgb(245,158,11)), new Rectangle(50, 160, 120, 120), 140, 100);
                e.Graphics.FillPie(new SolidBrush(Color.FromArgb(168,85,247)), new Rectangle(50, 160, 120, 120), 240, 30);
                e.Graphics.FillEllipse(Brushes.White, new Rectangle(75, 185, 70, 70));
            };
            pnlRow3.Controls.Add(pnlToday);
        }

        private Panel MkCardPanel(Size size, Point loc) {
            Panel p = new Panel { Size = size, Location = loc, BackColor = Color.White };
            UIHelper.SetRoundedRegion(p, 10);
            return p;
        }

        private void AddModernStatCard(Panel parent, string title, string val, string sub, Color color, int x, int width)
        {
            Panel card = new Panel { Size = new Size(width, 100), Location = new Point(x, 0), BackColor = Color.White, Anchor = AnchorStyles.Top | AnchorStyles.Left };
            UIHelper.SetRoundedRegion(card, 10);

            // Circle Icon
            Panel pIcon = new Panel { Size = new Size(50, 50), Location = new Point(15, 25), BackColor = Color.FromArgb(color.R, color.G, color.B) };
            UIHelper.SetRoundedRegion(pIcon, 25);
            card.Controls.Add(pIcon);

            Label lTitle = new Label { Text = title, Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(80, 15), AutoSize = true };
            Label lVal = new Label { Text = val, Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(17, 24, 39), Location = new Point(80, 35), AutoSize = true };
            Label lSub = new Label { Text = sub, Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = color, Location = new Point(80, 70), AutoSize = true };

            card.Controls.Add(lTitle);
            card.Controls.Add(lVal);
            card.Controls.Add(lSub);
            parent.Controls.Add(card);
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
            sidebarPanel.BackColor = Color.FromArgb(17, 24, 39); // TasteBite Dark sidebar
            headerPanel.BackColor = Color.White;
            lblPanelTitle.ForeColor = Color.FromArgb(17, 24, 39);
            lblGreeting.ForeColor = Color.Gray;
            lblTime.ForeColor = Color.FromArgb(17, 24, 39);
            
            lblAdminTitle.Text = "TasteBite\nRestaurant";
            lblAdminTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            try
            {
                string logoPath = System.IO.Path.Combine(Application.StartupPath, @"..\..\..\Resources\logo.png");
                if (System.IO.File.Exists(logoPath))
                    picLogo.Image = Image.FromFile(logoPath);
            }
            catch { }
        }

        private void clockTimer_Tick(object? sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("T");
        }

        private void fadeTimer_Tick(object? sender, EventArgs e)
        {
            if (this.Opacity < 1)
                this.Opacity += 0.05;
            else
                fadeTimer.Stop();
        }

        private void btnManageMenu_Click(object? sender, EventArgs e)
        {
            LoadForm(new Menu_Form());
            lblPanelTitle.Text = "Menu Management";
        }

        private void btnReports_Click(object? sender, EventArgs e)
        {
            LoadForm(new ReportForm());
            lblPanelTitle.Text = "Daily Sales Reports";
        }

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            Program.IsLoggedIn = false;
            Program.CurrentUser = null;
            this.Close();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void btnStaff_Click(object? sender, EventArgs e)
        {
            LoadForm(new StaffForm());
            lblPanelTitle.Text = "Staff management & Access Control";
        }

        private void btnSettings_Click(object? sender, EventArgs e)
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



        private void BuildSidebar()
        {
            // Remove existing buttons from designer
            for (int i = sidebarPanel.Controls.Count - 1; i >= 0; i--)
            {
                if (sidebarPanel.Controls[i] is Button)
                    sidebarPanel.Controls.RemoveAt(i);
            }

            // Need an AutoScroll panel for the sidebar content because it's tall
            Panel scrollPan = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            scrollPan.AutoScrollMinSize = new Size(0, 900);
            
            Panel btnPan = new Panel { Location = new Point(0, 90), Size = new Size(220, this.Height - 90), AutoScroll = true, Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left };
            sidebarPanel.Controls.Add(btnPan);
            btnPan.BringToFront();

            int y = 10;
            
            AddSidebarButton(btnPan, "Dashboard", "🏠", ref y, true);
            AddSidebarButton(btnPan, "Orders", "📦", ref y, false);
            AddSidebarButton(btnPan, "Table Management", "🪑", ref y, false);
            AddSidebarButton(btnPan, "Menu Management", "📖", ref y, false);
            AddSidebarButton(btnPan, "Category Management", "📑", ref y, false);
            AddSidebarButton(btnPan, "Customers", "👥", ref y, false);
            AddSidebarButton(btnPan, "Staff Management", "👨‍🍳", ref y, false);
            AddSidebarButton(btnPan, "Inventory Management", "📋", ref y, false);
            AddSidebarButton(btnPan, "Receipts", "🧾", ref y, false);
            AddSidebarButton(btnPan, "Reports & Analytics", "📊", ref y, false);
            AddSidebarButton(btnPan, "Coupons & Offers", "🎟️", ref y, false);
            AddSidebarButton(btnPan, "Settings", "⚙️", ref y, false);

            y += 20;

            // Add Special Offer Box
            Panel pnlOffer = new Panel { Size = new Size(180, 100), Location = new Point(20, y), BackColor = Color.FromArgb(31, 41, 55) };
            UIHelper.SetRoundedRegion(pnlOffer, 10);
            
            Label lOff1 = new Label { Text = "🔒 Special Offer", Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.White, Location = new Point(10, 10), AutoSize = true };
            Label lOff2 = new Label { Text = "Create and manage exciting\noffers for your customers.", Font = new Font("Segoe UI", 7), ForeColor = Color.Gray, Location = new Point(10, 35), AutoSize = true };
            Button bOff = new Button { Text = "Create Offer", Font = new Font("Segoe UI", 8, FontStyle.Bold), BackColor = Color.FromArgb(245, 158, 11), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Size = new Size(160, 25), Location = new Point(10, 65) };
            bOff.FlatAppearance.BorderSize = 0;
            UIHelper.SetRoundedRegion(bOff, 5);

            pnlOffer.Controls.Add(lOff1);
            pnlOffer.Controls.Add(lOff2);
            pnlOffer.Controls.Add(bOff);
            btnPan.Controls.Add(pnlOffer);
            y += 120;
            
            // Logout
            Button btnLogoutNew = new Button { Text = "🚪 Logout", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(239, 68, 68), BackColor = Color.Transparent, FlatStyle = FlatStyle.Flat, Size = new Size(220, 45), Location = new Point(0, y), TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(20, 0, 0, 0), Cursor = Cursors.Hand };
            btnLogoutNew.FlatAppearance.BorderSize = 0;
            btnLogoutNew.Click += btnLogout_Click;
            btnPan.Controls.Add(btnLogoutNew);
        }

        private void AddSidebarButton(Panel parent, string text, string icon, ref int y, bool active)
        {
            Button b = new Button { Text = "  " + icon + "  " + text, Font = new Font("Segoe UI", 10, active ? FontStyle.Bold : FontStyle.Regular), ForeColor = active ? Color.White : Color.FromArgb(156, 163, 175), BackColor = active ? Color.FromArgb(31, 41, 55) : Color.Transparent, FlatStyle = FlatStyle.Flat, Size = new Size(220, 45), Location = new Point(0, y), TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(15, 0, 0, 0), Cursor = Cursors.Hand };
            b.FlatAppearance.BorderSize = 0;
            if (active) {
                Panel p = new Panel { Size = new Size(4, 45), Location = new Point(0, y), BackColor = Color.FromArgb(245, 158, 11) };
                parent.Controls.Add(p);
                p.BringToFront();
            }
            b.MouseEnter += (s, e) => { if (!active) { b.BackColor = Color.FromArgb(31, 41, 55); b.ForeColor = Color.White; } };
            b.MouseLeave += (s, e) => { if (!active) { b.BackColor = Color.Transparent; b.ForeColor = Color.FromArgb(156, 163, 175); } };
            
            if (text == "Dashboard") b.Click += async (s, e) => await ShowDashboardOverviewAsync();
            else if (text == "Menu Management") b.Click += btnManageMenu_Click;
            else if (text == "Reports & Analytics") b.Click += btnReports_Click;
            else if (text == "Staff Management") b.Click += (s, e) => ShowAdminPage("Staff Management");
            else if (text == "Settings") b.Click += btnSettings_Click;
            else if (text == "Orders") b.Click += (s, e) => ShowAdminPage("Orders");
            else if (text == "Table Management") b.Click += (s, e) => ShowAdminPage("Table Management");
            else if (text == "Category Management") b.Click += (s, e) => ShowAdminPage("Category Management");
            else if (text == "Customers") b.Click += (s, e) => ShowAdminPage("Customers");
            else if (text == "Inventory Management") b.Click += (s, e) => ShowAdminPage("Inventory Management");
            else if (text == "Receipts") b.Click += (s, e) => ShowAdminPage("Receipts");
            else if (text == "Coupons & Offers") b.Click += (s, e) => ShowAdminPage("Coupons & Offers");
            else b.Click += (s, e) => ShowAdminPage(text);

            parent.Controls.Add(b);
            y += 45;
        }

        // ── Helper: content area setup ──────────────────────────────────────
        private Panel StartPage(string title, string subtitle)
        {
            contentPanel.Controls.Clear();
            contentPanel.BackColor = Color.FromArgb(249, 250, 251);
            lblPanelTitle.Text = title;

            Panel wrap = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.FromArgb(249, 250, 251) };
            contentPanel.Controls.Add(wrap);

            Label lSub = new Label { Text = subtitle, Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(30, 10), AutoSize = true };
            wrap.Controls.Add(lSub);
            return wrap;
        }

        private Panel MkCard(Panel parent, int x, int y, int w, int h)
        {
            Panel c = new Panel { BackColor = Color.White, Location = new Point(x, y), Size = new Size(w, h), Anchor = AnchorStyles.Top | AnchorStyles.Left };
            UIHelper.SetRoundedRegion(c, 10);
            parent.Controls.Add(c);
            return c;
        }

        private void AddTableHeader(Panel card, int y, params (string text, int x, int w)[] cols)
        {
            Panel hdr = new Panel { BackColor = Color.FromArgb(249, 250, 251), Location = new Point(0, y), Size = new Size(card.Width, 36) };
            foreach (var col in cols)
            {
                Label l = new Label { Text = col.text.ToUpper(), Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.FromArgb(107, 114, 128), Location = new Point(col.x, 10), Size = new Size(col.w, 20), AutoSize = false };
                hdr.Controls.Add(l);
            }
            card.Controls.Add(hdr);
        }

        private void AddTableRow(Panel card, int y, Color rowBg, params (string text, int x, int w, Color? color)[] cells)
        {
            Panel row = new Panel { BackColor = rowBg, Location = new Point(0, y), Size = new Size(card.Width, 44) };
            foreach (var cell in cells)
            {
                Color fc = cell.color ?? Color.FromArgb(31, 41, 55);
                Label l = new Label { Text = cell.text, Font = new Font("Segoe UI", 9), ForeColor = fc, Location = new Point(cell.x, 13), Size = new Size(cell.w, 20), AutoSize = false };
                row.Controls.Add(l);
            }
            card.Controls.Add(row);
        }

        private Button MkActionBtn(string text, Color bg, int x, int y)
        {
            Button b = new Button { Text = text, BackColor = bg, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 32), Location = new Point(x, y), Font = new Font("Segoe UI", 9, FontStyle.Bold), Cursor = Cursors.Hand };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        // ── Page dispatcher ─────────────────────────────────────────────────
        private void ShowAdminPage(string page)
        {
            switch (page)
            {
                case "Orders":             ShowOrdersPage(); break;
                case "Table Management":   ShowTableManagementPage(); break;
                case "Category Management": ShowCategoryPage(); break;
                case "Customers":           ShowCustomersPage(); break;
                case "Staff Management":    ShowStaffPage(); break;
                case "Inventory Management":ShowInventoryPage(); break;
                case "Coupons & Offers":    ShowCouponsPage(); break;
                case "Receipts":            ShowReceiptsPage(); break;
                default: ShowComingSoon(page); break;
            }
        }

        private void ShowComingSoon(string page)
        {
            var wrap = StartPage(page, "This section is coming soon.");
            Panel card = MkCard(wrap, 30, 40, 400, 160);
            Label l = new Label { Text = "🚧  " + page + "\n\nThis page is under development.", Font = new Font("Segoe UI", 13), ForeColor = Color.Gray, Location = new Point(30, 40), AutoSize = true };
            card.Controls.Add(l);
        }

        // ── ORDERS ──────────────────────────────────────────────────────────
        private async void ShowOrdersPage()
        {
            var wrap = StartPage("Orders", "Manage and track customer orders.");
            int cw = contentPanel.Width - 60;

            string[] tabs = { "All", "Dine In", "Takeaway", "Delivery" };
            for (int i = 0; i < tabs.Length; i++)
            {
                Button tb = new Button { Text = tabs[i], Size = new Size(90, 32), Location = new Point(30 + i * 100, 40), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold), BackColor = i == 0 ? Color.FromArgb(245, 158, 11) : Color.White, ForeColor = i == 0 ? Color.Black : Color.Gray, Cursor = Cursors.Hand };
                tb.FlatAppearance.BorderSize = 1; tb.FlatAppearance.BorderColor = Color.FromArgb(229, 231, 235);
                wrap.Controls.Add(tb);
            }

            var dt = await ApiClient.GetPendingOrdersTableAsync();
            Panel card = MkCard(wrap, 30, 90, cw, Math.Max(200, 60 + dt.Rows.Count * 44 + 36));
            AddTableHeader(card, 0, ("Order ID", 15, 90), ("Amount", 115, 110), ("Status", 235, 110), ("Date", 355, 160));

            int row = 0;
            foreach (DataRow r in dt.Rows)
            {
                Color bg = row % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                string st = r["Status"]?.ToString() ?? "";
                Color sc = st == "Completed" ? Color.FromArgb(34, 197, 94) : st == "Pending" ? Color.FromArgb(245, 158, 11) : Color.FromArgb(59, 130, 246);
                string amt = "$" + Convert.ToDecimal(r["TotalAmount"]).ToString("N2");
                string dt2 = Convert.ToDateTime(r["OrderDate"]).ToString("MMM dd, yyyy hh:mm tt");
                AddTableRow(card, 36 + row * 44, bg, ("#ORD-" + r["OrderID"], 15, 90, null), (amt, 115, 110, Color.FromArgb(31,41,55)), (st, 235, 110, sc), (dt2, 355, 160, Color.Gray));
                row++;
            }
            if (row == 0)
                card.Controls.Add(new Label { Text = "No orders found.", Font = new Font("Segoe UI", 11), ForeColor = Color.Gray, Location = new Point(20, 50), AutoSize = true });
        }

        // ── TABLE MANAGEMENT ────────────────────────────────────────────────
        private async void ShowTableManagementPage()
        {
            var wrap = StartPage("Table Management", "Manage and track your restaurant tables.");
            int cw = contentPanel.Width - 60;

            var tables = await ApiClient.GetTablesAsync();
            int avail = tables.Count(t => t.Status == "Available");
            int reserved = tables.Count(t => t.Status == "Reserved");
            int occupied = tables.Count(t => t.Status == "Occupied");

            var statDefs = new[] { ("Total Tables", tables.Count.ToString(), Color.FromArgb(59,130,246)), ("Available", avail.ToString(), Color.FromArgb(34,197,94)), ("Reserved", reserved.ToString(), Color.FromArgb(245,158,11)), ("Occupied", occupied.ToString(), Color.FromArgb(239,68,68)) };
            int sw = (cw - 60) / 4;
            for (int i = 0; i < statDefs.Length; i++)
            {
                Panel sc = MkCard(wrap, 30 + i * (sw + 20), 40, sw, 80);
                sc.Controls.Add(new Label { Text = statDefs[i].Item1, Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Location = new Point(15, 12), AutoSize = true });
                sc.Controls.Add(new Label { Text = statDefs[i].Item2, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = statDefs[i].Item3, Location = new Point(15, 32), AutoSize = true });
            }

            Panel grid = new Panel { Location = new Point(30, 140), Size = new Size(cw, 400), AutoScroll = true };
            wrap.Controls.Add(grid);

            int idx = 0;
            foreach (var t in tables)
            {
                Color bg = t.Status == "Available" ? Color.FromArgb(220,252,231) : t.Status == "Reserved" ? Color.FromArgb(254,249,195) : Color.FromArgb(254,226,226);
                Color fg = t.Status == "Available" ? Color.FromArgb(21,128,61)   : t.Status == "Reserved" ? Color.FromArgb(161,98,7)    : Color.FromArgb(185,28,28);
                int col = idx % 4, row = idx / 4;
                Panel tc = new Panel { BackColor = bg, Location = new Point(col * (cw / 4 - 5), row * 90), Size = new Size(cw / 4 - 10, 80) };
                UIHelper.SetRoundedRegion(tc, 8);
                tc.Controls.Add(new Label { Text = t.TableNumber, Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = fg, Location = new Point(10, 10), AutoSize = true });
                tc.Controls.Add(new Label { Text = $"{t.Capacity} Seats", Font = new Font("Segoe UI", 9), ForeColor = fg, Location = new Point(10, 38), AutoSize = true });
                tc.Controls.Add(new Label { Text = t.Status, Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = fg, Location = new Point(10, 56), AutoSize = true });
                grid.Controls.Add(tc);
                idx++;
            }
            if (idx == 0)
                grid.Controls.Add(new Label { Text = "No tables configured.", Font = new Font("Segoe UI", 11), ForeColor = Color.Gray, Location = new Point(20, 20), AutoSize = true });
        }

        // ── CATEGORY MANAGEMENT ─────────────────────────────────────────────
        private async void ShowCategoryPage()
        {
            var wrap = StartPage("Category Management", "Organize your menu categories.");
            int cw = contentPanel.Width - 60;

            var menu = await ApiClient.GetMenuItemsAsync();
            var cats = menu.GroupBy(m => m.Category)
                           .Select(g => new { Name = g.Key, Count = g.Count() })
                           .ToList();

            Panel card = MkCard(wrap, 30, 40, cw, Math.Max(200, 90 + cats.Count * 44));
            AddTableHeader(card, 0, ("Category Name", 15, 200), ("Item Count", 225, 100), ("Status", 335, 100), ("Action", 445, 100));

            int row = 0;
            foreach (var c in cats)
            {
                Color bg = row % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                AddTableRow(card, 36 + row * 44, bg, (c.Name, 15, 200, Color.FromArgb(31, 41, 55)), (c.Count.ToString(), 225, 100, null), ("Active", 335, 100, Color.FromArgb(34, 197, 94)), ("✏️  🗑️", 445, 100, Color.Gray));
                row++;
            }
            if (row == 0)
                card.Controls.Add(new Label { Text = "No categories found.", Font = new Font("Segoe UI", 11), ForeColor = Color.Gray, Location = new Point(20, 50), AutoSize = true });
        }

        // ── CUSTOMERS ───────────────────────────────────────────────────────
        private async void ShowCustomersPage()
        {
            var wrap = StartPage("Customers", "View and manage your customers.");
            int cw = contentPanel.Width - 60;

            var custs = await ApiClient.GetCustomersAsync();
            Panel card = MkCard(wrap, 30, 40, cw, Math.Max(200, 90 + custs.Count * 44));

            AddTableHeader(card, 0, ("ID", 15, 60), ("Name", 85, 180), ("Status", 275, 100), ("Action", 385, 100));

            int row = 0;
            foreach (var c in custs)
            {
                Color bg = row % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                AddTableRow(card, 36 + row * 44, bg, (c.CustomerID.ToString(), 15, 60, null), (c.Name, 85, 180, Color.FromArgb(31, 41, 55)), ("Active", 275, 100, Color.FromArgb(34, 197, 94)), ("✏️  🗑️", 385, 100, Color.Gray));
                row++;
            }
            if (row == 0)
                card.Controls.Add(new Label { Text = "No customers found.", Font = new Font("Segoe UI", 11), ForeColor = Color.Gray, Location = new Point(20, 50), AutoSize = true });
        }

        // ── STAFF MANAGEMENT ───────────────────────────────────────────────
        private async void ShowStaffPage()
        {
            var wrap = StartPage("Staff Management", "Manage employees and access control.");
            int cw = contentPanel.Width - 60;

            var dt = await ApiClient.GetStaffTableAsync();
            Panel card = MkCard(wrap, 30, 40, cw, Math.Max(200, 90 + dt.Rows.Count * 44));
            AddTableHeader(card, 0, ("Username", 15, 150), ("Role", 175, 120), ("Action", 305, 100));

            int row = 0;
            foreach (DataRow r in dt.Rows)
            {
                Color bg = row % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                string user = r["Username"]?.ToString() ?? "";
                string role = r["Role"]?.ToString() ?? "Staff";
                Color rc = role == "Admin" ? Color.FromArgb(168, 85, 247) : Color.FromArgb(59, 130, 246);

                AddTableRow(card, 36 + row * 44, bg, (user, 15, 150, Color.FromArgb(31, 41, 55)), (role, 175, 120, rc), ("🗑️", 305, 100, Color.FromArgb(239, 68, 68)));
                row++;
            }
            if (row == 0)
                card.Controls.Add(new Label { Text = "No staff found.", Font = new Font("Segoe UI", 11), ForeColor = Color.Gray, Location = new Point(20, 50), AutoSize = true });
        }

        // ── INVENTORY ───────────────────────────────────────────────────────
        private void ShowInventoryPage()
        {
            var wrap = StartPage("Inventory Management", "Track your stock and ingredients.");
            int cw = contentPanel.Width - 60;
            Panel card = MkCard(wrap, 30, 40, cw, 480);

            Button addBtn = MkActionBtn("+ Add Item", Color.FromArgb(245, 158, 11), card.Width - 120, 14);
            card.Controls.Add(addBtn);

            AddTableHeader(card, 50, ("Item", 15, 160), ("Category", 185, 120), ("Quantity", 315, 90), ("Unit", 415, 70), ("Status", 495, 100));

            var inv = new[] {
                ("Chicken Breast","Meat","50 kg","kg","Low Stock"),
                ("Tomatoes","Vegetables","120 kg","kg","In Stock"),
                ("Pasta","Dry Goods","35 kg","kg","In Stock"),
                ("Olive Oil","Oil","18 L","L","Low Stock"),
                ("Cheese","Dairy","22 kg","kg","In Stock"),
                ("Coffee Beans","Beverages","8 kg","kg","Out of Stock"),
                ("Rice","Dry Goods","180 kg","kg","In Stock"),
                ("Butter","Dairy","300 g","g","Low Stock"),
            };

            for (int i = 0; i < inv.Length; i++)
            {
                Color bg = i % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                Color sc = inv[i].Item5 == "In Stock" ? Color.FromArgb(34, 197, 94) : inv[i].Item5 == "Low Stock" ? Color.FromArgb(245, 158, 11) : Color.FromArgb(239, 68, 68);
                AddTableRow(card, 86 + i * 44, bg, (inv[i].Item1, 15, 160, null), (inv[i].Item2, 185, 120, Color.Gray), (inv[i].Item3, 315, 90, null), (inv[i].Item4, 415, 70, Color.Gray), (inv[i].Item5, 495, 100, sc));
            }
        }

        // ── COUPONS & OFFERS ────────────────────────────────────────────────
        private void ShowCouponsPage()
        {
            var wrap = StartPage("Coupons & Offers", "Create and manage discount codes.");
            int cw = contentPanel.Width - 60;
            Panel card = MkCard(wrap, 30, 40, cw, 420);

            Button addBtn = MkActionBtn("+ Create Coupon", Color.FromArgb(245, 158, 11), card.Width - 150, 14);
            card.Controls.Add(addBtn);

            AddTableHeader(card, 50, ("Coupon Code", 15, 150), ("Type", 175, 120), ("Discount", 305, 90), ("Last Used", 405, 120), ("Action", 535, 100));

            var coupons = new[] {
                ("WELCOME20","Percentage","20%","May 17, 2024","Active"),
                ("SAVE50","Fixed","$50","May 19, 2024","Active"),
                ("FREESHIP","Percentage","100% (Ship)","May 12, 2024","Inactive"),
                ("EASTER25","Percentage","25%","Apr 01, 2024","Expired"),
                ("LUNCH10","Fixed","$10","May 20, 2024","Active"),
            };

            for (int i = 0; i < coupons.Length; i++)
            {
                Color bg = i % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                Color sc = coupons[i].Item5 == "Active" ? Color.FromArgb(34, 197, 94) : coupons[i].Item5 == "Inactive" ? Color.FromArgb(245, 158, 11) : Color.FromArgb(239, 68, 68);
                AddTableRow(card, 86 + i * 44, bg, (coupons[i].Item1, 15, 150, Color.FromArgb(59, 130, 246)), (coupons[i].Item2, 175, 120, Color.Gray), (coupons[i].Item3, 305, 90, null), (coupons[i].Item4, 405, 120, Color.Gray), (coupons[i].Item5, 535, 100, sc));
            }
        }

        // ── RECEIPTS (from user payments) ────────────────────────────────────
        private async void ShowReceiptsPage()
        {
            var wrap = StartPage("Receipts", "Customer receipts submitted after payment.");
            int cw = contentPanel.Width - 60;

            var dt = await ApiClient.GetReceiptsTableAsync();

            if (dt.Rows.Count == 0)
            {
                Panel emptyCard = MkCard(wrap, 30, 40, cw, 180);
                emptyCard.Controls.Add(new Label { Text = "🧾  No receipts found.", Font = new Font("Segoe UI", 14), ForeColor = Color.FromArgb(156, 163, 175), Location = new Point(30, 55), AutoSize = true });
                emptyCard.Controls.Add(new Label { Text = "Receipts will appear here once customers complete a payment.", Font = new Font("Segoe UI", 10), ForeColor = Color.Gray, Location = new Point(30, 100), AutoSize = true });
                return;
            }

            // Summary stat
            Panel summCard = MkCard(wrap, 30, 40, 200, 80);
            summCard.Controls.Add(new Label { Text = "Total Receipts", Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Location = new Point(15, 12), AutoSize = true });
            summCard.Controls.Add(new Label { Text = dt.Rows.Count.ToString(), Font = new Font("Segoe UI", 26, FontStyle.Bold), ForeColor = Color.FromArgb(245, 158, 11), Location = new Point(15, 32), AutoSize = true });

            // Table header
            int cardH = Math.Max(200, 90 + dt.Rows.Count * 56);
            Panel card = MkCard(wrap, 30, 140, cw, cardH);
            AddTableHeader(card, 0, ("Receipt #", 15, 90), ("Order #", 115, 90), ("Date & Time", 215, 200), ("Receipt Preview", 425, cw - 510));

            int row = 0;
            foreach (System.Data.DataRow r in dt.Rows)
            {
                Color bg = row % 2 == 0 ? Color.White : Color.FromArgb(249, 250, 251);
                string receiptId  = "#REC-" + r["ReceiptID"];
                string orderId    = "#ORD-" + r["OrderID"];
                string date       = Convert.ToDateTime(r["CreatedAt"]).ToString("MMM dd, yyyy  hh:mm tt");
                string rawContent = r["ReceiptContent"]?.ToString() ?? "";
                string preview    = rawContent.Length > 55 ? rawContent.Substring(0, 52) + "..." : rawContent;

                Panel rowPan = new Panel { BackColor = bg, Location = new Point(0, 36 + row * 56), Size = new Size(card.Width, 56) };

                // Blue badge for receipt ID
                Panel badge = new Panel { BackColor = Color.FromArgb(239, 246, 255), Location = new Point(15, 14), Size = new Size(78, 26) };
                UIHelper.SetRoundedRegion(badge, 5);
                badge.Controls.Add(new Label { Text = receiptId, Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.FromArgb(37, 99, 235), Location = new Point(4, 5), AutoSize = true });
                rowPan.Controls.Add(badge);

                rowPan.Controls.Add(new Label { Text = orderId,  Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(31, 41, 55), Location = new Point(115, 18), AutoSize = true });
                rowPan.Controls.Add(new Label { Text = date,     Font = new Font("Segoe UI", 9), ForeColor = Color.Gray, Location = new Point(215, 18), AutoSize = true });
                rowPan.Controls.Add(new Label { Text = preview,  Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(75, 85, 99), Location = new Point(425, 18), AutoSize = false, Size = new Size(cw - 510, 20) });

                // View full receipt button
                Button viewBtn = new Button { Text = "📄 View", Font = new Font("Segoe UI", 8, FontStyle.Bold), BackColor = Color.FromArgb(245, 158, 11), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(70, 28), Location = new Point(card.Width - 90, 14), Cursor = Cursors.Hand };
                viewBtn.FlatAppearance.BorderSize = 0;
                UIHelper.SetRoundedRegion(viewBtn, 5);
                string fullContent = rawContent;
                string rId = receiptId;
                viewBtn.Click += (s, e) =>
                {
                    using var dlg = new Form { Text = "Receipt — " + rId, Size = new Size(500, 480), StartPosition = FormStartPosition.CenterParent, BackColor = Color.White };
                    var rtb = new RichTextBox { Dock = DockStyle.Fill, Text = fullContent, ReadOnly = true, BackColor = Color.White, BorderStyle = BorderStyle.None, Font = new Font("Consolas", 10), Padding = new Padding(16) };
                    dlg.Controls.Add(rtb);
                    dlg.ShowDialog(this);
                };
                rowPan.Controls.Add(viewBtn);
                card.Controls.Add(rowPan);
                row++;
            }
        }
    }
}
