namespace RestaurantDesktopApp
{
    partial class UserMainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnManageTables;
        private System.Windows.Forms.Button btnPayments;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblUserTitle;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblPanelTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.Panel cardTables;
        private System.Windows.Forms.Label lblTablesVal;
        private System.Windows.Forms.Label lblTablesTitle;
        private System.Windows.Forms.Panel cardPending;
        private System.Windows.Forms.Label lblPendingVal;
        private System.Windows.Forms.Label lblPendingTitle;
        private System.Windows.Forms.Panel cardMenu;
        private System.Windows.Forms.Label lblMenuVal;
        private System.Windows.Forms.Label lblMenuTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnMaximize;
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
            this.components = new System.ComponentModel.Container();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnPayments = new System.Windows.Forms.Button();
            this.btnManageTables = new System.Windows.Forms.Button();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.lblUserTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblPanelTitle = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.statsPanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.cardTables = new System.Windows.Forms.Panel();
            this.lblTablesVal = new System.Windows.Forms.Label();
            this.lblTablesTitle = new System.Windows.Forms.Label();
            this.cardPending = new System.Windows.Forms.Panel();
            this.lblPendingVal = new System.Windows.Forms.Label();
            this.lblPendingTitle = new System.Windows.Forms.Label();
            this.btnMaximize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.cardMenu = new System.Windows.Forms.Panel();
            this.lblMenuVal = new System.Windows.Forms.Label();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.lblGreeting = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            this.fadeTimer = new System.Windows.Forms.Timer(this.components);
            this.sidebarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.headerPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.statsPanel.SuspendLayout();
            this.cardTables.SuspendLayout();
            this.cardPending.SuspendLayout();
            this.cardMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.sidebarPanel.Controls.Add(this.btnLogout);
            this.sidebarPanel.Controls.Add(this.btnPayments);
            this.sidebarPanel.Controls.Add(this.btnManageTables);
            this.sidebarPanel.Controls.Add(this.btnCreateOrder);
            this.sidebarPanel.Controls.Add(this.lblUserTitle);
            this.sidebarPanel.Controls.Add(this.picLogo);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new Size(220, 750);
            this.sidebarPanel.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 690);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new Size(220, 60);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "  \uD83D\uDEAA Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnPayments
            // 
            this.btnPayments.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayments.FlatAppearance.BorderSize = 0;
            this.btnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnPayments.ForeColor = System.Drawing.Color.White;
            this.btnPayments.Location = new System.Drawing.Point(0, 240);
            this.btnPayments.Name = "btnPayments";
            this.btnPayments.Size = new Size(220, 60);
            this.btnPayments.TabIndex = 2;
            this.btnPayments.Text = "  \uD83D\uDCB3 Payments";
            this.btnPayments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayments.UseVisualStyleBackColor = true;
            this.btnPayments.Click += new System.EventHandler(this.btnPayments_Click);
            // 
            // btnManageTables
            // 
            this.btnManageTables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageTables.FlatAppearance.BorderSize = 0;
            this.btnManageTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageTables.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageTables.ForeColor = System.Drawing.Color.White;
            this.btnManageTables.Location = new System.Drawing.Point(0, 180);
            this.btnManageTables.Name = "btnManageTables";
            this.btnManageTables.Size = new Size(220, 60);
            this.btnManageTables.TabIndex = 1;
            this.btnManageTables.Text = "  \uD83E\uDE91 Tables";
            this.btnManageTables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManageTables.UseVisualStyleBackColor = true;
            this.btnManageTables.Click += new System.EventHandler(this.btnManageTables_Click);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateOrder.FlatAppearance.BorderSize = 0;
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCreateOrder.ForeColor = System.Drawing.Color.White;
            this.btnCreateOrder.Location = new System.Drawing.Point(0, 120);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new Size(220, 60);
            this.btnCreateOrder.TabIndex = 0;
            this.btnCreateOrder.Text = "  \uD83D\uDCDD Create Order";
            this.btnCreateOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // lblUserTitle
            // 
            this.lblUserTitle.AutoSize = true;
            this.lblUserTitle.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblUserTitle.ForeColor = System.Drawing.Color.White;
            this.lblUserTitle.Location = new Point(70, 30);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Size = new Size(142, 25);
            this.lblUserTitle.TabIndex = 4;
            this.lblUserTitle.Text = "STAFF PANEL";
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(10, 15);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new Size(50, 50);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.headerPanel.Controls.Add(this.btnMinimize);
            this.headerPanel.Controls.Add(this.btnMaximize);
            this.headerPanel.Controls.Add(this.btnClose);
            this.headerPanel.Controls.Add(this.lblPanelTitle);
            this.headerPanel.Controls.Add(this.lblTime);
            this.headerPanel.Controls.Add(this.lblGreeting);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(220, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new Size(780, 70);
            this.headerPanel.TabIndex = 2;
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new Point(560, 35);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new Size(76, 21);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "00:00:00";
            // 
            // lblGreeting
            // 
            this.lblGreeting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGreeting.AutoSize = true;
            this.lblGreeting.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblGreeting.ForeColor = System.Drawing.Color.White;
            this.lblGreeting.Location = new Point(560, 15);
            this.lblGreeting.Name = "lblGreeting";
            this.lblGreeting.Size = new Size(95, 19);
            this.lblGreeting.TabIndex = 1;
            this.lblGreeting.Text = "Good Morning";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new Point(740, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(35, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "\u2715";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMaximize
            // 
            this.btnMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximize.FlatAppearance.BorderSize = 0;
            this.btnMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMaximize.ForeColor = System.Drawing.Color.White;
            this.btnMaximize.Location = new Point(700, 15);
            this.btnMaximize.Name = "btnMaximize";
            this.btnMaximize.Size = new Size(35, 35);
            this.btnMaximize.TabIndex = 5;
            this.btnMaximize.Text = "\u274F";
            this.btnMaximize.UseVisualStyleBackColor = true;
            this.btnMaximize.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimize.ForeColor = System.Drawing.Color.White;
            this.btnMinimize.Location = new Point(660, 15);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new Size(35, 35);
            this.btnMinimize.TabIndex = 4;
            this.btnMinimize.Text = "\u2014";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // lblPanelTitle
            // 
            this.lblPanelTitle.AutoSize = true;
            this.lblPanelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblPanelTitle.ForeColor = System.Drawing.Color.White;
            this.lblPanelTitle.Location = new Point(20, 20);
            this.lblPanelTitle.Name = "lblPanelTitle";
            this.lblPanelTitle.Size = new Size(117, 30);
            this.lblPanelTitle.TabIndex = 0;
            this.lblPanelTitle.Text = "Staff Home";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.mainPanel.Controls.Add(this.contentPanel);
            this.mainPanel.Controls.Add(this.statsPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new Point(220, 70);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new Size(780, 530);
            this.mainPanel.TabIndex = 1;
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new Point(0, 150);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new Size(780, 530);
            this.contentPanel.TabIndex = 1;
            // 
            // statsPanel
            // 
            this.statsPanel.Controls.Add(this.cardMenu);
            this.statsPanel.Controls.Add(this.cardPending);
            this.statsPanel.Controls.Add(this.cardTables);
            this.statsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statsPanel.Location = new Point(0, 0);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Size = new Size(780, 150);
            this.statsPanel.TabIndex = 0;
            // 
            // cardTables
            // 
            this.cardTables.BackColor = System.Drawing.Color.White;
            this.cardTables.Controls.Add(this.lblTablesVal);
            this.cardTables.Controls.Add(this.lblTablesTitle);
            this.cardTables.Location = new Point(30, 25);
            this.cardTables.Name = "cardTables";
            this.cardTables.Size = new Size(220, 100);
            this.cardTables.TabIndex = 0;
            // 
            // lblTablesVal
            // 
            this.lblTablesVal.AutoSize = true;
            this.lblTablesVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTablesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.lblTablesVal.Location = new Point(15, 45);
            this.lblTablesVal.Name = "lblTablesVal";
            this.lblTablesVal.Size = new Size(28, 32);
            this.lblTablesVal.TabIndex = 1;
            this.lblTablesVal.Text = "0";
            // 
            // lblTablesTitle
            // 
            this.lblTablesTitle.AutoSize = true;
            this.lblTablesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTablesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblTablesTitle.Location = new Point(15, 15);
            this.lblTablesTitle.Name = "lblTablesTitle";
            this.lblTablesTitle.Size = new Size(129, 19);
            this.lblTablesTitle.TabIndex = 0;
            this.lblTablesTitle.Text = "FREE TABLES";
            // 
            // cardPending
            // 
            this.cardPending.BackColor = System.Drawing.Color.White;
            this.cardPending.Controls.Add(this.lblPendingVal);
            this.cardPending.Controls.Add(this.lblPendingTitle);
            this.cardPending.Location = new Point(280, 25);
            this.cardPending.Name = "cardPending";
            this.cardPending.Size = new Size(220, 100);
            this.cardPending.TabIndex = 1;
            // 
            // lblPendingVal
            // 
            this.lblPendingVal.AutoSize = true;
            this.lblPendingVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPendingVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblPendingVal.Location = new Point(15, 45);
            this.lblPendingVal.Name = "lblPendingVal";
            this.lblPendingVal.Size = new Size(28, 32);
            this.lblPendingVal.TabIndex = 1;
            this.lblPendingVal.Text = "0";
            // 
            // lblPendingTitle
            // 
            this.lblPendingTitle.AutoSize = true;
            this.lblPendingTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblPendingTitle.Location = new Point(15, 15);
            this.lblPendingTitle.Name = "lblPendingTitle";
            this.lblPendingTitle.Size = new Size(127, 19);
            this.lblPendingTitle.TabIndex = 0;
            this.lblPendingTitle.Text = "PENDING ORDERS";
            // 
            // cardMenu
            // 
            this.cardMenu.BackColor = System.Drawing.Color.White;
            this.cardMenu.Controls.Add(this.lblMenuVal);
            this.cardMenu.Controls.Add(this.lblMenuTitle);
            this.cardMenu.Location = new Point(530, 25);
            this.cardMenu.Name = "cardMenu";
            this.cardMenu.Size = new Size(220, 100);
            this.cardMenu.TabIndex = 2;
            // 
            // lblMenuVal
            // 
            this.lblMenuVal.AutoSize = true;
            this.lblMenuVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMenuVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.lblMenuVal.Location = new Point(15, 45);
            this.lblMenuVal.Name = "lblMenuVal";
            this.lblMenuVal.Size = new Size(28, 32);
            this.lblMenuVal.TabIndex = 1;
            this.lblMenuVal.Text = "0";
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblMenuTitle.Location = new Point(15, 15);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new Size(95, 19);
            this.lblMenuTitle.TabIndex = 0;
            this.lblMenuTitle.Text = "MENU ITEMS";
            // 
            // fadeTimer
            // 
            this.fadeTimer.Interval = 30;
            this.fadeTimer.Tick += new System.EventHandler(this.fadeTimer_Tick);
            // 
            // UserMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 750);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserMainForm";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Staff Dashboard";
            this.Load += new System.EventHandler(this.UserMainForm_Load);
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.statsPanel.ResumeLayout(false);
            this.cardTables.ResumeLayout(false);
            this.cardTables.PerformLayout();
            this.cardPending.ResumeLayout(false);
            this.cardPending.PerformLayout();
            this.cardMenu.ResumeLayout(false);
            this.cardMenu.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
