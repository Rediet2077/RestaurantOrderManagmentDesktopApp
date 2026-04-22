namespace RestaurantDesktopApp
{
    partial class AdminMainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Button btnManageMenu;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblPanelTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Timer fadeTimer;

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
            sidebarPanel = new Panel();
            btnSettings = new Button();
            btnStaff = new Button();
            btnLogout = new Button();
            btnReports = new Button();
            btnManageMenu = new Button();
            lblAdminTitle = new Label();
            picLogo = new PictureBox();
            headerPanel = new Panel();
            lblTime = new Label();
            lblGreeting = new Label();
            btnMinimize = new Button();
            btnMaximize = new Button();
            btnClose = new Button();
            lblPanelTitle = new Label();
            mainPanel = new Panel();
            contentPanel = new Panel();
            clockTimer = new System.Windows.Forms.Timer(components);
            fadeTimer = new System.Windows.Forms.Timer(components);
            sidebarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            headerPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // sidebarPanel
            // 
            sidebarPanel.BackColor = Color.FromArgb(44, 62, 80);
            sidebarPanel.Controls.Add(btnSettings);
            sidebarPanel.Controls.Add(btnStaff);
            sidebarPanel.Controls.Add(btnLogout);
            sidebarPanel.Controls.Add(btnReports);
            sidebarPanel.Controls.Add(btnManageMenu);
            sidebarPanel.Controls.Add(lblAdminTitle);
            sidebarPanel.Controls.Add(picLogo);
            sidebarPanel.Dock = DockStyle.Left;
            sidebarPanel.Location = new Point(0, 0);
            sidebarPanel.Name = "sidebarPanel";
            sidebarPanel.Size = new Size(220, 750);
            sidebarPanel.TabIndex = 0;
            // 
            // btnSettings
            // 
            btnSettings.Cursor = Cursors.Hand;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSettings.ForeColor = Color.White;
            btnSettings.Location = new Point(0, 300);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(220, 60);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "  \u2699\uFE0F System Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // btnStaff
            // 
            btnStaff.Cursor = Cursors.Hand;
            btnStaff.FlatAppearance.BorderSize = 0;
            btnStaff.FlatStyle = FlatStyle.Flat;
            btnStaff.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnStaff.ForeColor = Color.White;
            btnStaff.Location = new Point(0, 240);
            btnStaff.Name = "btnStaff";
            btnStaff.Size = new Size(220, 60);
            btnStaff.TabIndex = 5;
            btnStaff.Text = "  \uD83D\uDC65 Staff Management";
            btnStaff.TextAlign = ContentAlignment.MiddleLeft;
            btnStaff.UseVisualStyleBackColor = true;
            btnStaff.Click += btnStaff_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 690);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(220, 60);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "  \uD83D\uDEAA Logout";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnReports
            // 
            btnReports.Cursor = Cursors.Hand;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 180);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(220, 60);
            btnReports.TabIndex = 1;
            btnReports.Text = "  \uD83D\uDCCA View Reports";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;
            // 
            // btnManageMenu
            // 
            btnManageMenu.Cursor = Cursors.Hand;
            btnManageMenu.FlatAppearance.BorderSize = 0;
            btnManageMenu.FlatStyle = FlatStyle.Flat;
            btnManageMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnManageMenu.ForeColor = Color.White;
            btnManageMenu.Location = new Point(0, 120);
            btnManageMenu.Name = "btnManageMenu";
            btnManageMenu.Size = new Size(220, 60);
            btnManageMenu.TabIndex = 0;
            btnManageMenu.Text = "  \uD83D\uDCE6 Manage Menu";
            btnManageMenu.TextAlign = ContentAlignment.MiddleLeft;
            btnManageMenu.UseVisualStyleBackColor = true;
            btnManageMenu.Click += btnManageMenu_Click;
            // 
            // lblAdminTitle
            // 
            lblAdminTitle.AutoSize = true;
            lblAdminTitle.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold);
            lblAdminTitle.ForeColor = Color.White;
            lblAdminTitle.Location = new Point(70, 30);
            lblAdminTitle.Name = "lblAdminTitle";
            lblAdminTitle.Size = new Size(147, 25);
            lblAdminTitle.TabIndex = 3;
            lblAdminTitle.Text = "ADMIN PANEL";
            // 
            // picLogo
            // 
            picLogo.Location = new Point(10, 15);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(50, 50);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 4;
            picLogo.TabStop = false;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(41, 128, 185);
            headerPanel.Controls.Add(btnMinimize);
            headerPanel.Controls.Add(btnMaximize);
            headerPanel.Controls.Add(btnClose);
            headerPanel.Controls.Add(lblPanelTitle);
            headerPanel.Controls.Add(lblTime);
            headerPanel.Controls.Add(lblGreeting);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(220, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(780, 70);
            headerPanel.TabIndex = 2;
            // 
            // lblTime
            // 
            lblTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTime.AutoSize = true;
            lblTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTime.ForeColor = Color.White;
            lblTime.Location = new Point(560, 35);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(72, 21);
            lblTime.TabIndex = 2;
            lblTime.Text = "00:00:00";
            // 
            // lblGreeting
            // 
            lblGreeting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Segoe UI", 10F);
            lblGreeting.ForeColor = Color.White;
            lblGreeting.Location = new Point(560, 15);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(100, 19);
            lblGreeting.TabIndex = 1;
            lblGreeting.Text = "Good Morning";
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.Cursor = Cursors.Hand;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMinimize.ForeColor = Color.White;
            btnMinimize.Location = new Point(660, 15);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(35, 35);
            btnMinimize.TabIndex = 4;
            btnMinimize.Text = "\u2014";
            btnMinimize.UseVisualStyleBackColor = true;
            btnMinimize.Click += btnMinimize_Click;
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.Cursor = Cursors.Hand;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMaximize.ForeColor = Color.White;
            btnMaximize.Location = new Point(700, 15);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(35, 35);
            btnMaximize.TabIndex = 5;
            btnMaximize.Text = "\u274F";
            btnMaximize.UseVisualStyleBackColor = true;
            btnMaximize.Click += btnMaximize_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(740, 15);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(35, 35);
            btnClose.TabIndex = 3;
            btnClose.Text = "\u2715";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblPanelTitle
            // 
            lblPanelTitle.AutoSize = true;
            lblPanelTitle.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            lblPanelTitle.ForeColor = Color.White;
            lblPanelTitle.Location = new Point(20, 20);
            lblPanelTitle.Name = "lblPanelTitle";
            lblPanelTitle.Size = new Size(221, 30);
            lblPanelTitle.TabIndex = 0;
            lblPanelTitle.Text = "Dashboard Overview";
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.FromArgb(243, 244, 246);
            mainPanel.Controls.Add(contentPanel);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(220, 70);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(780, 680);
            mainPanel.TabIndex = 1;
            // 
            // contentPanel
            // 
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(780, 680);
            contentPanel.TabIndex = 1;
            contentPanel.Paint += contentPanel_Paint;
            // 
            // clockTimer
            // 
            clockTimer.Enabled = true;
            clockTimer.Interval = 1000;
            clockTimer.Tick += clockTimer_Tick;
            // 
            // fadeTimer
            // 
            fadeTimer.Interval = 30;
            fadeTimer.Tick += fadeTimer_Tick;
            // 
            // AdminMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 750);
            Controls.Add(mainPanel);
            Controls.Add(headerPanel);
            Controls.Add(sidebarPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AdminMainForm";
            Opacity = 0D;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Dashboard";
            Load += AdminMainForm_Load;
            sidebarPanel.ResumeLayout(false);
            sidebarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
