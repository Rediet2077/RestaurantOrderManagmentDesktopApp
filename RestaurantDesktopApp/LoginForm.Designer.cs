namespace RestaurantDesktopApp
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel navBar;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Panel heroPanel;
        private System.Windows.Forms.PictureBox picBackground;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.LinkLabel lnkRegister;
        private System.Windows.Forms.Button btnShowLogin;
        private System.Windows.Forms.Timer fadeTimer;
        private System.Windows.Forms.Label lblHeroText;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            navBar = new Panel();
            btnShowLogin = new Button();
            picLogo = new PictureBox();
            lblTitle = new Label();
            heroPanel = new Panel();
            lblHeroText = new Label();
            lblWelcome = new Label();
            loginPanel = new Panel();
            lnkRegister = new LinkLabel();
            btnCancel = new Button();
            btnLogin = new Button();
            lblPassword = new Label();
            lblUsername = new Label();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            picBackground = new PictureBox();
            fadeTimer = new System.Windows.Forms.Timer(components);
            navBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            heroPanel.SuspendLayout();
            loginPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBackground).BeginInit();
            picBackground.SuspendLayout();
            SuspendLayout();
            // 
            // navBar
            // 
            navBar.BackColor = Color.FromArgb(44, 62, 80);
            navBar.Controls.Add(btnShowLogin);
            navBar.Controls.Add(picLogo);
            navBar.Controls.Add(lblTitle);
            navBar.Dock = DockStyle.Top;
            navBar.Location = new Point(0, 0);
            navBar.Name = "navBar";
            navBar.Size = new Size(1000, 80);
            navBar.TabIndex = 0;
            // 
            // btnShowLogin
            // 
            btnShowLogin.BackColor = Color.FromArgb(41, 128, 185);
            btnShowLogin.Cursor = Cursors.Hand;
            btnShowLogin.FlatAppearance.BorderSize = 0;
            btnShowLogin.FlatStyle = FlatStyle.Flat;
            btnShowLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnShowLogin.ForeColor = Color.White;
            btnShowLogin.Location = new Point(860, 20);
            btnShowLogin.Name = "btnShowLogin";
            btnShowLogin.Size = new Size(110, 40);
            btnShowLogin.TabIndex = 8;
            btnShowLogin.Text = "LOG IN";
            btnShowLogin.UseVisualStyleBackColor = false;
            btnShowLogin.Click += btnShowLogin_Click;
            // 
            // picLogo
            // 
            picLogo.Location = new Point(20, 10);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(60, 60);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 7;
            picLogo.TabStop = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(90, 24);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(116, 32);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "LAUNCH";
            lblTitle.Click += lblTitle_Click;
            // 
            // heroPanel
            // 
            heroPanel.BackColor = Color.Transparent;
            heroPanel.Controls.Add(lblHeroText);
            heroPanel.Controls.Add(lblWelcome);
            heroPanel.Location = new Point(50, 120);
            heroPanel.Name = "heroPanel";
            heroPanel.Size = new Size(800, 200);
            heroPanel.TabIndex = 3;
            heroPanel.Paint += heroPanel_Paint;
            // 
            // lblHeroText
            // 
            lblHeroText.AutoSize = true;
            lblHeroText.Font = new Font("Segoe UI", 14F);
            lblHeroText.ForeColor = Color.White;
            lblHeroText.Location = new Point(10, 80);
            lblHeroText.Name = "lblHeroText";
            lblHeroText.Size = new Size(365, 25);
            lblHeroText.TabIndex = 8;
            lblHeroText.Text = "Experience Gourmet Excellence every day.";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI Black", 36F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(0, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(420, 65);
            lblWelcome.TabIndex = 7;
            lblWelcome.Text = "Best Restaurant!";
            // 
            // loginPanel
            // 
            loginPanel.BackColor = Color.FromArgb(240, 255, 255, 255);
            loginPanel.Controls.Add(lnkRegister);
            loginPanel.Controls.Add(btnCancel);
            loginPanel.Controls.Add(btnLogin);
            loginPanel.Controls.Add(lblPassword);
            loginPanel.Controls.Add(lblUsername);
            loginPanel.Controls.Add(txtPassword);
            loginPanel.Controls.Add(txtUsername);
            loginPanel.Location = new Point(712, 70);
            loginPanel.Name = "loginPanel";
            loginPanel.Size = new Size(238, 320);
            loginPanel.TabIndex = 1;
            loginPanel.Visible = false;
            loginPanel.Paint += loginPanel_Paint;
            // 
            // lnkRegister
            // 
            lnkRegister.AutoSize = true;
            lnkRegister.Font = new Font("Segoe UI", 10F);
            lnkRegister.LinkColor = Color.FromArgb(41, 128, 185);
            lnkRegister.Location = new Point(63, 275);
            lnkRegister.Name = "lnkRegister";
            lnkRegister.Size = new Size(134, 19);
            lnkRegister.TabIndex = 6;
            lnkRegister.TabStop = true;
            lnkRegister.Text = "Create New Account";
            lnkRegister.LinkClicked += lnkRegister_LinkClicked;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(189, 195, 199);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.Location = new Point(131, 205);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(66, 45);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "CLOSE";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(41, 128, 185);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 210);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(71, 45);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "LOG IN";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblPassword.Location = new Point(40, 125);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(68, 19);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblUsername.Location = new Point(40, 55);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(71, 19);
            lblUsername.TabIndex = 4;
            lblUsername.Text = "Username";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(40, 150);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(157, 29);
            txtPassword.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(40, 80);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(157, 29);
            txtUsername.TabIndex = 0;
            // 
            // picBackground
            // 
            picBackground.Controls.Add(loginPanel);
            picBackground.Controls.Add(heroPanel);
            picBackground.Dock = DockStyle.Fill;
            picBackground.Location = new Point(0, 80);
            picBackground.Name = "picBackground";
            picBackground.Size = new Size(1000, 520);
            picBackground.SizeMode = PictureBoxSizeMode.StretchImage;
            picBackground.TabIndex = 2;
            picBackground.TabStop = false;
            // 
            // fadeTimer
            // 
            fadeTimer.Interval = 30;
            fadeTimer.Tick += fadeTimer_Tick;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(picBackground);
            Controls.Add(navBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            Opacity = 0D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Welcome Page";
            Load += LoginForm_Load;
            navBar.ResumeLayout(false);
            navBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            heroPanel.ResumeLayout(false);
            heroPanel.PerformLayout();
            loginPanel.ResumeLayout(false);
            loginPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picBackground).EndInit();
            picBackground.ResumeLayout(false);
            ResumeLayout(false);

        }
    }
}
