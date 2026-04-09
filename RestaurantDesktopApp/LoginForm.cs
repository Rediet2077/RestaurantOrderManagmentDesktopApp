using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class LoginForm : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private Panel _divider;

        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size            = new Size(1100, 730);
            this.StartPosition   = FormStartPosition.CenterScreen;
            AddWindowControls();
        }

        // Window controls are now built into the nav bar in LoginForm_Load
        private void AddWindowControls() { }

        private Button MkWinBtn(string text, int x, Color bg)
        {
            var b = new Button
            {
                Text      = text,
                Font      = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                BackColor = bg,
                FlatStyle = FlatStyle.Flat,
                Size      = new Size(45, 30),
                Location  = new Point(x, 0),
                Cursor    = Cursors.Hand
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 255, 255, 255);
            return b;
        }

        // ── Form Load — builds the full UI ──────────────────────────────
        private void LoginForm_Load(object sender, EventArgs e)
        {
            fadeTimer.Start();

            // ── Background ──────────────────────────────────────────────
            this.BackColor          = Color.FromArgb(240, 242, 248);
            picBackground.BackColor = Color.FromArgb(240, 242, 248);
            picBackground.Image     = null;

            // ── Nav bar ─────────────────────────────────────────────────
            navBar.BackColor = Color.FromArgb(15, 23, 42);
            navBar.Height    = 64;
            navBar.Controls.Clear();

            // Make nav draggable
            navBar.MouseDown += (s2, e2) => { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = this.Location; };
            navBar.MouseMove += (s2, e2) => { if (dragging) { var d = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); this.Location = Point.Add(dragFormPoint, new Size(d)); } };
            navBar.MouseUp   += (s2, e2) => dragging = false;

            Label navLogo = new Label { Text = "🍳  DBU RESTAURANTS", Font = new Font("Segoe UI", 15, FontStyle.Bold), ForeColor = Color.FromArgb(250, 163, 7), AutoSize = true, Location = new Point(28, 18) };
            navBar.Controls.Add(navLogo);

            string[] navLinks = { "Home", "Order as Guest" };
            for (int i = 0; i < navLinks.Length; i++)
            {
                string link = navLinks[i];
                Label nl = new Label { Text = link, Font = new Font("Segoe UI", 11), ForeColor = Color.White, AutoSize = true, Cursor = Cursors.Hand, Anchor = AnchorStyles.Top | AnchorStyles.Right };
                nl.Location  = new Point(navBar.Width - 475 + i * 160, 22);
                nl.MouseEnter += (s2, e2) => ((Label)s2).ForeColor = Color.FromArgb(250, 163, 7);
                nl.MouseLeave += (s2, e2) => ((Label)s2).ForeColor = Color.White;
                if (link == "Home") nl.Click += (s2, e2) => this.Close();
                navBar.Controls.Add(nl);
            }

            // ── Window controls embedded in nav bar (right edge) ─────────
            // Order: — (min) · 🗖 (max) · ✕ (close)  — same height as nav
            var winDefs = new[] {
                new { T = "—", H = Color.FromArgb(80,255,255,255), A = new Action(() => this.WindowState = FormWindowState.Minimized) },
                new { T = "🗖", H = Color.FromArgb(80,255,255,255), A = new Action(() => this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized) },
                new { T = "✕", H = Color.FromArgb(231,76,60),       A = new Action(() => Application.Exit()) }
            };
            for (int i = 0; i < winDefs.Length; i++)
            {
                var def = winDefs[i];
                Button wb = new Button {
                    Text      = def.T,
                    Font      = new Font("Segoe UI", 10),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(15, 23, 42),
                    FlatStyle = FlatStyle.Flat,
                    Size      = new Size(46, 64),
                    Location  = new Point(navBar.Width - 138 + i * 46, 0),
                    Anchor    = AnchorStyles.Top | AnchorStyles.Right,
                    Cursor    = Cursors.Hand
                };
                wb.FlatAppearance.BorderSize = 0;
                wb.FlatAppearance.MouseOverBackColor = def.H;
                var act = def.A;
                wb.Click += (s2, e2) => act();
                navBar.Controls.Add(wb);
            }

            // ── LEFT PANEL — welcome & features ─────────────────────────
            heroPanel.Controls.Clear();
            heroPanel.Location  = new Point(0, 0);
            heroPanel.BackColor = Color.FromArgb(240, 242, 248);

            Label t1 = MkLbl("Welcome to ", new Font("Segoe UI", 26, FontStyle.Bold), Color.FromArgb(17, 24, 39), new Point(60, 80));
            Label t2 = MkLbl("DBU",         new Font("Segoe UI", 26, FontStyle.Bold), Color.FromArgb(250, 163, 7), new Point(60 + t1.PreferredWidth, 80));
            Label t3 = MkLbl("Restaurants", new Font("Segoe UI", 26, FontStyle.Bold), Color.FromArgb(250, 163, 7), new Point(60, 122));
            heroPanel.Controls.AddRange(new Control[] { t1, t2, t3 });

            Label desc = MkLbl("Access affordable meals, track your orders, and\nmanage your contract balance with your student account.",
                                new Font("Segoe UI", 12), Color.FromArgb(90, 100, 120), new Point(60, 186));
            heroPanel.Controls.Add(desc);

            string[] feats = { "Student-exclusive discounts", "Easy contract balance management", "Fast delivery across campus", "Order history and tracking" };
            for (int i = 0; i < feats.Length; i++)
            {
                heroPanel.Controls.Add(MkLbl("✓",       new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(34, 197, 94),  new Point(60, 268 + i * 34)));
                heroPanel.Controls.Add(MkLbl(feats[i],  new Font("Segoe UI", 12),                  Color.FromArgb(17, 24, 39),   new Point(86, 268 + i * 34)));
            }

            // Blue info box
            Panel infoBox = new Panel { Size = new Size(380, 88), Location = new Point(60, 420), BackColor = Color.FromArgb(235, 244, 255) };
            infoBox.Paint += (s2, pe) => { using var pen = new System.Drawing.Pen(Color.FromArgb(190, 215, 255), 1); pe.Graphics.DrawRectangle(pen, 0, 0, infoBox.Width - 1, infoBox.Height - 1); };
            infoBox.Controls.Add(MkLbl("DBU Students Only",                                                      new Font("Segoe UI", 11, FontStyle.Bold), Color.FromArgb(29, 78, 216), new Point(14, 12)));
            infoBox.Controls.Add(MkLbl("Please use your official DBU email address\n(@dbu.edu.et) to register.", new Font("Segoe UI", 10),                  Color.FromArgb(29, 78, 216), new Point(14, 36)));
            heroPanel.Controls.Add(infoBox);

            // ── Vertical divider that splits left/right ─────────────────
            _divider = new Panel { BackColor = Color.FromArgb(210, 213, 223) };
            picBackground.Controls.Add(_divider);
            picBackground.Controls.SetChildIndex(_divider, 0);

            // ── RIGHT PANEL — starts with login view ─────────────────────
            loginPanel.Visible   = true;
            loginPanel.BackColor = Color.White;

            // Show login view by default
            ShowLoginPanel();

            // Initial layout
            PositionPanels();
        }

        // ── Tab helpers ─────────────────────────────────────────────────
        private void ShowLoginPanel()
        {
            loginPanel.Controls.Clear();
            loginPanel.Size = new Size(loginPanel.Width, 490);

            // ── Tabs ────────────────────────────────────────────────────
            Button tabLogin = MkBtn("Login",    new Point(40, 30),  new Size(110, 38),
                Color.FromArgb(250, 163, 7), Color.White, new Font("Segoe UI", 11, FontStyle.Bold));
            Button tabReg   = MkBtn("Register", new Point(158, 30), new Size(110, 38),
                Color.FromArgb(225, 226, 232), Color.FromArgb(60, 70, 90), new Font("Segoe UI", 11));

            tabReg.Click   += (s2, e2) => { ShowRegisterPanel(); PositionPanels(); };
            loginPanel.Controls.Add(tabLogin);
            loginPanel.Controls.Add(tabReg);

            // ── Heading ─────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("Welcome Back! 👋",
                new Font("Segoe UI", 18, FontStyle.Bold), Color.FromArgb(17, 24, 39), new Point(40, 88)));
            loginPanel.Controls.Add(MkLbl("Sign in to your DBU Restaurants account",
                new Font("Segoe UI", 11), Color.FromArgb(100, 110, 130), new Point(40, 124)));

            // ── Email ───────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("DBU Email Address *",
                new Font("Segoe UI", 10, FontStyle.Bold), Color.FromArgb(17, 24, 39), new Point(40, 168)));
            txtUsername.Location = new Point(40, 191); txtUsername.Size = new Size(360, 36);
            txtUsername.Font = new Font("Segoe UI", 12);
            txtUsername.BackColor = Color.FromArgb(245, 247, 252);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            loginPanel.Controls.Add(txtUsername);

            // ── Password ────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("Password *",
                new Font("Segoe UI", 10, FontStyle.Bold), Color.FromArgb(17, 24, 39), new Point(40, 244)));
            txtPassword.Location = new Point(40, 267); txtPassword.Size = new Size(360, 36);
            txtPassword.Font = new Font("Segoe UI", 12);
            txtPassword.BackColor = Color.FromArgb(245, 247, 252);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.PasswordChar = '●';
            loginPanel.Controls.Add(txtPassword);

            // ── Remember / Forgot ────────────────────────────────────────
            CheckBox remMe = new CheckBox { Text = "Remember me", Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(60, 70, 90), AutoSize = true, Location = new Point(40, 322) };
            loginPanel.Controls.Add(remMe);

            LinkLabel forgot = new LinkLabel { Text = "Forgot Password?", Font = new Font("Segoe UI", 10),
                LinkColor = Color.FromArgb(250, 163, 7), AutoSize = true, Location = new Point(268, 322) };
            loginPanel.Controls.Add(forgot);

            // ── Sign In button ───────────────────────────────────────────
            btnLogin.Text = "Sign In to Your Account";
            btnLogin.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            btnLogin.BackColor = Color.FromArgb(250, 163, 7); btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat; btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Size = new Size(360, 50); btnLogin.Location = new Point(40, 362); btnLogin.Cursor = Cursors.Hand;
            loginPanel.Controls.Add(btnLogin);

            // ── "Don't have an account?" ─────────────────────────────────
            Label dha = MkLbl("Don't have an account?  ",
                new Font("Segoe UI", 10), Color.FromArgb(100, 110, 130), new Point(60, 430));
            loginPanel.Controls.Add(dha);
            LinkLabel create = new LinkLabel { Text = "Create one here", Font = new Font("Segoe UI", 10),
                LinkColor = Color.FromArgb(250, 163, 7), AutoSize = true,
                Location = new Point(60 + dha.PreferredWidth, 430) };
            create.LinkClicked += (s2, e2) => { ShowRegisterPanel(); PositionPanels(); };
            loginPanel.Controls.Add(create);

            if (loginPanel.Width > 0 && loginPanel.Height > 0)
                UIHelper.SetRoundedRegion(loginPanel, 16);
        }

        private void ShowRegisterPanel()
        {
            loginPanel.Controls.Clear();
            loginPanel.Size = new Size(loginPanel.Width, 620);

            Color accent  = Color.FromArgb(250, 163, 7);
            Color darkTxt = Color.FromArgb(17, 24, 39);
            Color midTxt  = Color.FromArgb(100, 110, 130);
            Color fieldBg = Color.FromArgb(245, 247, 252);
            int   fw      = loginPanel.Width - 80;   // field width

            // ── Tabs ────────────────────────────────────────────────────
            Button tabLogin = MkBtn("Login",    new Point(40, 30),  new Size(110, 38),
                Color.FromArgb(225, 226, 232), Color.FromArgb(60, 70, 90), new Font("Segoe UI", 11));
            Button tabReg   = MkBtn("Register", new Point(158, 30), new Size(110, 38),
                accent, Color.White, new Font("Segoe UI", 11, FontStyle.Bold));

            tabLogin.Click += (s2, e2) => { ShowLoginPanel(); PositionPanels(); };
            loginPanel.Controls.Add(tabLogin);
            loginPanel.Controls.Add(tabReg);

            // ── Heading ─────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("Create Account 🎓",
                new Font("Segoe UI", 18, FontStyle.Bold), darkTxt, new Point(40, 88)));
            loginPanel.Controls.Add(MkLbl("Join DBU Restaurants as a student",
                new Font("Segoe UI", 11), midTxt, new Point(40, 124)));

            // ── Full Name ────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("Full Name *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 162)));
            var txtFN = MkField("Abebe Kebede", new Point(40, 183), new Size(fw, 36), fieldBg);
            loginPanel.Controls.Add(txtFN);

            // ── Student ID  +  Phone Number (side by side) ───────────────
            int halfW = (fw - 14) / 2;
            loginPanel.Controls.Add(MkLbl("Student ID *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 233)));
            loginPanel.Controls.Add(MkLbl("Phone Number *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40 + halfW + 14, 233)));
            var txtSID   = MkField("DBU/2024/001",   new Point(40, 254),            new Size(halfW, 36), fieldBg);
            var txtPhone = MkField("+251912345678",   new Point(40 + halfW + 14, 254), new Size(halfW, 36), fieldBg);
            loginPanel.Controls.Add(txtSID);
            loginPanel.Controls.Add(txtPhone);

            // ── DBU Email ────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("DBU Email Address *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 304)));
            var txtEmail = MkField("", new Point(40, 325), new Size(fw, 36), fieldBg);
            loginPanel.Controls.Add(txtEmail);

            // ── Password  +  Confirm Password (side by side) ─────────────
            loginPanel.Controls.Add(MkLbl("Password *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 375)));
            loginPanel.Controls.Add(MkLbl("Confirm Password *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40 + halfW + 14, 375)));
            var txtPwd  = MkField("", new Point(40, 396),            new Size(halfW, 36), fieldBg, true);
            var txtCPwd = MkField("Confirm your passw", new Point(40 + halfW + 14, 396), new Size(halfW, 36), fieldBg, true);
            loginPanel.Controls.Add(txtPwd);
            loginPanel.Controls.Add(txtCPwd);

            // ── Terms checkbox ───────────────────────────────────────────
            var chkTerms = new CheckBox
            {
                AutoSize  = true, Location = new Point(40, 450),
                Font      = new Font("Segoe UI", 10),
                ForeColor = midTxt,
                Text      = "I agree to the "
            };
            loginPanel.Controls.Add(chkTerms);

            LinkLabel lnkTerms = new LinkLabel { Text = "Terms of Service", Font = new Font("Segoe UI", 10),
                LinkColor = accent, AutoSize = true, Location = new Point(40 + chkTerms.PreferredSize.Width, 450) };
            loginPanel.Controls.Add(lnkTerms);

            Label andLbl = MkLbl(" and ", new Font("Segoe UI", 10), midTxt,
                new Point(lnkTerms.Left + lnkTerms.PreferredWidth, 450));
            loginPanel.Controls.Add(andLbl);

            LinkLabel lnkPriv = new LinkLabel { Text = "Privacy Policy", Font = new Font("Segoe UI", 10),
                LinkColor = accent, AutoSize = true, Location = new Point(andLbl.Left + andLbl.PreferredWidth, 450) };
            loginPanel.Controls.Add(lnkPriv);

            // ── Create Account button ────────────────────────────────────
            Button btnCreate = MkBtn("Create Student Account",
                new Point(40, 480), new Size(fw, 50),
                accent, Color.White, new Font("Segoe UI", 13, FontStyle.Bold));
            btnCreate.Click += (s2, e2) =>
            {
                if (string.IsNullOrWhiteSpace(txtFN.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtPwd.Text))
                { UIHelper.ShowToast("Please fill in all required fields.", true); return; }

                if (txtPwd.Text != txtCPwd.Text)
                { UIHelper.ShowToast("Passwords do not match.", true); return; }

                if (!txtEmail.Text.EndsWith("@dbu.edu.et", StringComparison.OrdinalIgnoreCase))
                { UIHelper.ShowToast("Please use your official @dbu.edu.et email.", true); return; }

                try
                {
                    using var conn2 = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");
                    conn2.Open();
                    string chk = "SELECT COUNT(*) FROM Users WHERE Username=@u";
                    using var chkCmd = new MySqlCommand(chk, conn2);
                    chkCmd.Parameters.AddWithValue("@u", txtEmail.Text);
                    if (Convert.ToInt32(chkCmd.ExecuteScalar()) > 0)
                    { UIHelper.ShowToast("Email already registered.", true); return; }

                    string ins = "INSERT INTO Users (Name, Role, Username, Password) VALUES (@n,'User',@u,@p)";
                    using var insCmd = new MySqlCommand(ins, conn2);
                    insCmd.Parameters.AddWithValue("@n", txtFN.Text);
                    insCmd.Parameters.AddWithValue("@u", txtEmail.Text);
                    insCmd.Parameters.AddWithValue("@p", txtPwd.Text);
                    insCmd.ExecuteNonQuery();

                    UIHelper.ShowToast("Registration successful! Please sign in.");
                    ShowLoginPanel();
                    PositionPanels();
                }
                catch (Exception ex)
                {
                    UIHelper.ShowToast("Error: " + ex.Message, true);
                }
            };
            loginPanel.Controls.Add(btnCreate);

            // ── Already have an account? ─────────────────────────────────
            Label alr = MkLbl("Already have an account?  ",
                new Font("Segoe UI", 10), midTxt, new Point(60, 548));
            loginPanel.Controls.Add(alr);
            LinkLabel lnkSign = new LinkLabel { Text = "Sign in here", Font = new Font("Segoe UI", 10),
                LinkColor = accent, AutoSize = true, Location = new Point(60 + alr.PreferredWidth, 548) };
            lnkSign.LinkClicked += (s2, e2) => { ShowLoginPanel(); PositionPanels(); };
            loginPanel.Controls.Add(lnkSign);

            if (loginPanel.Width > 0 && loginPanel.Height > 0)
                UIHelper.SetRoundedRegion(loginPanel, 16);
        }

        // Creates a styled TextBox field
        private TextBox MkField(string placeholder, Point loc, Size size, Color bg, bool isPassword = false)
        {
            var tb = new TextBox
            {
                Location    = loc, Size = size,
                Font        = new Font("Segoe UI", 12),
                BackColor   = bg,
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor   = Color.FromArgb(100, 110, 130)
            };
            if (isPassword) tb.PasswordChar = '●';
            // Placeholder simulation
            if (!string.IsNullOrEmpty(placeholder))
            {
                tb.Text = placeholder;
                tb.GotFocus  += (s, e) => { if (tb.Text == placeholder) { tb.Text = ""; tb.ForeColor = Color.FromArgb(17, 24, 39); } };
                tb.LostFocus += (s, e) => { if (string.IsNullOrEmpty(tb.Text)) { tb.Text = placeholder; tb.ForeColor = Color.FromArgb(100, 110, 130); } };
            }
            return tb;
        }

        // ── UI helpers ──────────────────────────────────────────────────
        private Label MkLbl(string text, Font font, Color fore, Point loc)
            => new Label { Text = text, Font = font, ForeColor = fore, AutoSize = true, Location = loc, BackColor = Color.Transparent };

        private Button MkBtn(string text, Point loc, Size size, Color back, Color fore, Font font)
        {
            var b = new Button { Text = text, Location = loc, Size = size, BackColor = back, ForeColor = fore, Font = font, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }

        // ── Responsive layout ───────────────────────────────────────────
        private void PositionPanels()
        {
            if (picBackground == null || _divider == null) return;
            int bg_w = picBackground.Width;
            int bg_h = picBackground.Height;
            if (bg_w <= 0 || bg_h <= 0) return;

            int divX = (int)(bg_w * 0.48);

            _divider.Size     = new Size(1, bg_h);
            _divider.Location = new Point(divX, 0);

            heroPanel.Size = new Size(divX, bg_h);

            int rightW = bg_w - divX;
            int cardW  = Math.Min(460, rightW - 40);
            // Keep existing height (changes between login/register views)
            loginPanel.Size     = new Size(cardW, loginPanel.Height);
            loginPanel.Location = new Point(divX + (rightW - cardW) / 2, Math.Max(20, (bg_h - loginPanel.Height) / 2));
            loginPanel.BringToFront();

            if (loginPanel.Width > 0 && loginPanel.Height > 0)
                UIHelper.SetRoundedRegion(loginPanel, 16);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PositionPanels();
        }

        // ── Timers & events ─────────────────────────────────────────────
        private void fadeTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            else fadeTimer.Stop();
        }

        // ── Login logic (unchanged) ─────────────────────────────────────
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                UIHelper.ShowToast("Please enter username and password.", true);
                return;
            }
            try
            {
                con.Open();
                string query = "SELECT Role FROM Users WHERE Username=@user AND Password=@pass";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                object result = cmd.ExecuteScalar();
                con.Close();

                if (result != null)
                {
                    string role = result.ToString();
                    UIHelper.ShowToast($"Welcome back, {role}!");
                    if (role == "Admin") new AdminMainForm().Show();
                    else                 new UserMainForm().Show();
                    this.Hide();
                }
                else
                {
                    UIHelper.ShowToast("Invalid username or password.", true);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                UIHelper.ShowToast("Connection error: " + ex.Message, true);
            }
        }

        // ── Stub handlers required by existing Designer wiring ──────────
        private void btnShowLogin_Click(object sender, EventArgs e) { }
        private void btnCancel_Click(object sender, EventArgs e) { }
        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new RegisterForm().ShowDialog();
        }
        private void heroPanel_Paint(object sender, PaintEventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void loginPanel_Paint(object sender, PaintEventArgs e) { }
    }
}
