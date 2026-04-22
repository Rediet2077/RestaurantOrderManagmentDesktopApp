using System;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class LoginForm : Form
    {
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

            Label navLogo = new Label { Text = "🍳  BEST RESTAURANTS", Font = new Font("Segoe UI", 15, FontStyle.Bold), ForeColor = Color.FromArgb(250, 163, 7), AutoSize = true, Location = new Point(28, 18) };
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
            Label t2 = MkLbl("BEST",         new Font("Segoe UI", 26, FontStyle.Bold), Color.FromArgb(250, 163, 7), new Point(60 + t1.PreferredWidth, 80));
            Label t3 = MkLbl("Restaurants", new Font("Segoe UI", 26, FontStyle.Bold), Color.FromArgb(250, 163, 7), new Point(60, 122));
            heroPanel.Controls.AddRange(new Control[] { t1, t2, t3 });

            Label desc = MkLbl("Order your favourite meals, track your orders,\nand manage your account with ease.",
                                new Font("Segoe UI", 12), Color.FromArgb(90, 100, 120), new Point(60, 186));
            heroPanel.Controls.Add(desc);

            string[] feats = { "Exclusive member discounts", "Easy balance management", "Fast delivery to your table", "Order history and tracking" };
            for (int i = 0; i < feats.Length; i++)
            {
                heroPanel.Controls.Add(MkLbl("✓",       new Font("Segoe UI", 12, FontStyle.Bold), Color.FromArgb(34, 197, 94),  new Point(60, 268 + i * 34)));
                heroPanel.Controls.Add(MkLbl(feats[i],  new Font("Segoe UI", 12),                  Color.FromArgb(17, 24, 39),   new Point(86, 268 + i * 34)));
            }

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
            loginPanel.Controls.Add(MkLbl("Sign in to your BEST Restaurants account",
                new Font("Segoe UI", 11), Color.FromArgb(100, 110, 130), new Point(40, 124)));

            // ── Email ───────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("Email Address *",
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
            loginPanel.Size = new Size(loginPanel.Width, 720);

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
            loginPanel.Controls.Add(MkLbl("Create Account",
                new Font("Segoe UI", 18, FontStyle.Bold), darkTxt, new Point(40, 88)));
            loginPanel.Controls.Add(MkLbl("Create your account to start ordering",
                new Font("Segoe UI", 11), midTxt, new Point(40, 124)));

            // ── Full Name ────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("Full Name *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 162)));
            var txtFN = MkField("e.g. John Smith", new Point(40, 183), new Size(fw, 36), fieldBg);
            loginPanel.Controls.Add(txtFN);

            // ── Username  +  Phone Number (side by side) ─────────────────
            int halfW = (fw - 14) / 2;
            loginPanel.Controls.Add(MkLbl("Username *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 233)));
            loginPanel.Controls.Add(MkLbl("Phone Number *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40 + halfW + 14, 233)));
            var txtSID   = MkField("e.g. john123",      new Point(40, 254),              new Size(halfW, 36), fieldBg);
            var txtPhone = MkField("e.g. 0912345678",   new Point(40 + halfW + 14, 254), new Size(halfW, 36), fieldBg);
            loginPanel.Controls.Add(txtSID);
            loginPanel.Controls.Add(txtPhone);

            // ── Email Address + Send OTP button ──────────────────────────
            loginPanel.Controls.Add(MkLbl("Email Address *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 304)));

            int sendBtnW    = 110;
            int emailFieldW = fw - sendBtnW - 8;
            var txtEmail = MkField("e.g. you@gmail.com", new Point(40, 325), new Size(emailFieldW, 36), fieldBg);
            loginPanel.Controls.Add(txtEmail);

            // OTP state variable captured in closure
            string generatedOtp = "";

            Button btnSendOtp = MkBtn("Send OTP", new Point(40 + emailFieldW + 8, 325), new Size(sendBtnW, 36),
                Color.FromArgb(37, 99, 235), Color.White, new Font("Segoe UI", 9, FontStyle.Bold));
            btnSendOtp.Click += async (s2, e2) =>
            {
                string email = txtEmail.Text.Trim();
                if (string.IsNullOrWhiteSpace(email) || email == "e.g. you@gmail.com")
                { UIHelper.ShowToast("Please enter your email address first.", true); return; }

                if (!email.Contains("@") || !email.Contains("."))
                { UIHelper.ShowToast("Please enter a valid email address.", true); return; }

                generatedOtp = new Random().Next(100000, 999999).ToString();
                btnSendOtp.Enabled   = false;
                btnSendOtp.Text      = "Sending...";
                btnSendOtp.BackColor = Color.FromArgb(150, 150, 150);

                bool sent = await Task.Run(() => SendOtpEmail(email, generatedOtp));

                if (sent)
                {
                    UIHelper.ShowToast($"OTP sent to {email}. Check your inbox.");
                    btnSendOtp.Text      = "Resend OTP";
                    btnSendOtp.BackColor = Color.FromArgb(34, 139, 34);
                }
                else
                {
                    UIHelper.ShowToast("Failed to send OTP. Check your internet connection.", true);
                    btnSendOtp.Text      = "Send OTP";
                    btnSendOtp.BackColor = Color.FromArgb(37, 99, 235);
                }
                btnSendOtp.Enabled = true;
            };
            loginPanel.Controls.Add(btnSendOtp);

            // ── OTP Field ────────────────────────────────────────────────
            loginPanel.Controls.Add(MkLbl("Enter OTP *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 375)));
            Label otpHint = MkLbl("(click Send OTP first to receive your code)",
                new Font("Segoe UI", 9), Color.FromArgb(150, 160, 180), new Point(135, 377));
            loginPanel.Controls.Add(otpHint);

            var txtOtp = MkField("", new Point(40, 396), new Size(fw, 36), fieldBg);
            loginPanel.Controls.Add(txtOtp);

            // ── Password  +  Confirm Password (side by side) ─────────────
            loginPanel.Controls.Add(MkLbl("Password *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40, 446)));
            loginPanel.Controls.Add(MkLbl("Confirm Password *",
                new Font("Segoe UI", 10, FontStyle.Bold), darkTxt, new Point(40 + halfW + 14, 446)));
            var txtPwd  = MkField("", new Point(40, 467),              new Size(halfW, 36), fieldBg, true);
            var txtCPwd = MkField("", new Point(40 + halfW + 14, 467), new Size(halfW, 36), fieldBg, true);
            loginPanel.Controls.Add(txtPwd);
            loginPanel.Controls.Add(txtCPwd);

            // ── Terms checkbox ───────────────────────────────────────────
            var chkTerms = new CheckBox
            {
                AutoSize  = true, Location = new Point(40, 520),
                Font      = new Font("Segoe UI", 10),
                ForeColor = midTxt,
                Text      = "I agree to the "
            };
            loginPanel.Controls.Add(chkTerms);

            LinkLabel lnkTerms = new LinkLabel { Text = "Terms of Service", Font = new Font("Segoe UI", 10),
                LinkColor = accent, AutoSize = true, Location = new Point(40 + chkTerms.PreferredSize.Width, 520) };
            loginPanel.Controls.Add(lnkTerms);

            Label andLbl = MkLbl(" and ", new Font("Segoe UI", 10), midTxt,
                new Point(lnkTerms.Left + lnkTerms.PreferredWidth, 520));
            loginPanel.Controls.Add(andLbl);

            LinkLabel lnkPriv = new LinkLabel { Text = "Privacy Policy", Font = new Font("Segoe UI", 10),
                LinkColor = accent, AutoSize = true, Location = new Point(andLbl.Left + andLbl.PreferredWidth, 520) };
            loginPanel.Controls.Add(lnkPriv);

            // ── Create Account button ────────────────────────────────────
            Button btnCreate = MkBtn("Create Account",
                new Point(40, 552), new Size(fw, 50),
                accent, Color.White, new Font("Segoe UI", 13, FontStyle.Bold));
            btnCreate.Click += async (s2, e2) =>
            {
                // 1. Required fields
                if (string.IsNullOrWhiteSpace(txtFN.Text) || txtFN.Text == "e.g. John Smith" ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) || txtEmail.Text == "e.g. you@gmail.com" ||
                    string.IsNullOrWhiteSpace(txtPwd.Text))
                { UIHelper.ShowToast("Please fill in all required fields.", true); return; }

                // 2. Email format check
                if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
                { UIHelper.ShowToast("Please enter a valid email address.", true); return; }
                
                // 3. OTP check
                if (string.IsNullOrEmpty(generatedOtp))
                { UIHelper.ShowToast("Please click 'Send OTP' first.", true); return; }
                if (txtOtp.Text.Trim() != generatedOtp && txtOtp.Text.Trim() != "123456")
                { UIHelper.ShowToast("Incorrect OTP. Please try again. (Hint: use 123456 if email fails)", true); return; }

                // 4. Password match
                if (txtPwd.Text != txtCPwd.Text)
                { UIHelper.ShowToast("Passwords do not match.", true); return; }

                // 5. Register via API
                btnCreate.Enabled = false;
                btnCreate.Text = "Creating Account...";

                string userName = txtSID.Text.Trim();
                if (userName == "e.g. john123") userName = "";
                string phone = txtPhone.Text.Trim();
                if (phone == "e.g. 0912345678") phone = "";

                bool registered = await ApiClient.RegisterAsync(
                    txtFN.Text.Trim(),
                    userName,
                    phone,
                    txtEmail.Text.Trim(),
                    txtPwd.Text
                );

                if (registered)
                {
                    UIHelper.ShowToast("Registration successful! Please sign in.");
                    ShowLoginPanel();
                    PositionPanels();
                }
                else
                {
                    UIHelper.ShowToast("Registration failed. Email may already exist.", true);
                }

                btnCreate.Enabled = true;
                btnCreate.Text = "Create Account";
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

        // ── SMTP Email sender for OTP ────────────────────────────────────
        // NOTE: Replace the values below with your own Gmail credentials.
        // To get an App Password:
        //   1. Go to myaccount.google.com > Security > 2-Step Verification
        //   2. Then go to  App Passwords and create one for "Mail"
        private bool SendOtpEmail(string toEmail, string otp)
        {
            // ▼ CONFIGURE YOUR SENDER EMAIL HERE ▼
            const string SenderEmail    = "redietsharew231@gmail.com";  // your gmail
            const string SenderPassword = "jjzo rdsd toau weri";          // 16-char app password
            // ▲──────────────────────────────────▲

            try
            {
                using var mail = new MailMessage();
                mail.From    = new MailAddress(SenderEmail, "Restaurant App");
                mail.To.Add(toEmail);
                mail.Subject = "Your OTP Verification Code";
                mail.IsBodyHtml = true;
                mail.Body = $@"
<div style='font-family:Segoe UI,sans-serif;max-width:480px;margin:auto;border:1px solid #e5e7eb;border-radius:12px;overflow:hidden'>
  <div style='background:#FAA307;padding:24px;text-align:center'>
    <h2 style='color:#fff;margin:0'>🍳 Restaurant App</h2>
  </div>
  <div style='padding:32px'>
    <h3 style='color:#111827'>Your Verification Code</h3>
    <p style='color:#6b7280'>Use the code below to complete your registration:</p>
    <div style='background:#f3f4f6;border-radius:8px;padding:20px;text-align:center;letter-spacing:8px;font-size:32px;font-weight:bold;color:#111827'>
      {otp}
    </div>
    <p style='color:#9ca3af;margin-top:24px;font-size:13px'>This code expires in 10 minutes. Do not share it with anyone.</p>
  </div>
</div>";

                using var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl   = true;
                smtp.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }


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

        // ── Login logic — now calls the API ─────────────────────────────
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string userEmail = txtUsername.Text.Trim();
            string userPass = txtPassword.Text;

            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userPass))
            {
                UIHelper.ShowToast("Please enter email and password.", true);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Signing In...";

            var result = await ApiClient.LoginAsync(userEmail, userPass);

            if (result != null && result.Success)
            {
                Program.IsLoggedIn = true;
                Program.CurrentUser = new AppUser
                {
                    UserID = result.UserID,
                    Name = result.FullName,
                    Email = result.Email,
                    Role = result.Role,
                    Token = result.Token
                };

                UIHelper.ShowToast($"Welcome back, {result.FullName}!");

                if (result.Role == "Admin")
                {
                    new AdminMainForm().Show();
                    this.Hide();
                }
                else if (result.Role == "Staff")
                {
                    new UserMainForm().Show();
                    this.Hide();
                }
                else
                {
                    new CustomerDashboardForm().Show();
                    this.Hide();
                }
            }
            else
            {
                string msg = result != null && !string.IsNullOrEmpty(result.Message) ? result.Message : "Invalid email or password.";
                UIHelper.ShowToast(msg, true);
            }

            btnLogin.Enabled = true;
            btnLogin.Text = "Sign In to Your Account";
        }

        // ── Stub handlers required by existing Designer wiring ──────────
        private void btnShowLogin_Click(object sender, EventArgs e) { }
        private void btnCancel_Click(object sender, EventArgs e) { }
        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Use the integrated register panel instead of the old standalone RegisterForm
            ShowRegisterPanel();
            PositionPanels();
        }
        private void heroPanel_Paint(object sender, PaintEventArgs e) { }
        private void lblTitle_Click(object sender, EventArgs e) { }
        private void loginPanel_Paint(object sender, PaintEventArgs e) { }
    }
}
