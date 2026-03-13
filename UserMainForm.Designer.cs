namespace RestaurantDesktopApp
{
    partial class UserMainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel mainPanel;
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
            this.cardTables = new System.Windows.Forms.Panel();
            this.lblTablesVal = new System.Windows.Forms.Label();
            this.lblTablesTitle = new System.Windows.Forms.Label();
            this.cardPending = new System.Windows.Forms.Panel();
            this.lblPendingVal = new System.Windows.Forms.Label();
            this.lblPendingTitle = new System.Windows.Forms.Label();
            this.cardMenu = new System.Windows.Forms.Panel();
            this.lblMenuVal = new System.Windows.Forms.Label();
            this.lblMenuTitle = new System.Windows.Forms.Label();
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
            this.sidebarPanel.Size = new System.Drawing.Size(220, 600);
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
            this.btnLogout.Location = new System.Drawing.Point(0, 540);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(220, 60);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "  🚪 Logout";
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
            this.btnPayments.Size = new System.Drawing.Size(220, 60);
            this.btnPayments.TabIndex = 2;
            this.btnPayments.Text = "  💳 Payments";
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
            this.btnManageTables.Size = new System.Drawing.Size(220, 60);
            this.btnManageTables.TabIndex = 1;
            this.btnManageTables.Text = "  🪑 Tables";
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
            this.btnCreateOrder.Size = new System.Drawing.Size(220, 60);
            this.btnCreateOrder.TabIndex = 0;
            this.btnCreateOrder.Text = "  📝 Create Order";
            this.btnCreateOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // lblUserTitle
            // 
            this.lblUserTitle.AutoSize = true;
            this.lblUserTitle.Font = new System.Drawing.Font("Segoe UI Black", 14F, System.Drawing.FontStyle.Bold);
            this.lblUserTitle.ForeColor = System.Drawing.Color.White;
            this.lblUserTitle.Location = new System.Drawing.Point(70, 30);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Size = new System.Drawing.Size(142, 25);
            this.lblUserTitle.TabIndex = 4;
            this.lblUserTitle.Text = "STAFF PANEL";
            // 
            // picLogo
            // 
            this.picLogo.Image = System.Drawing.Image.FromFile(System.IO.Path.Combine(Application.StartupPath, @"..\..\Resources\logo.png"));
            this.picLogo.Location = new System.Drawing.Point(10, 15);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(50, 50);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 5;
            this.picLogo.TabStop = false;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.headerPanel.Controls.Add(this.lblPanelTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(220, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(780, 70);
            this.headerPanel.TabIndex = 2;
            // 
            // lblPanelTitle
            // 
            this.lblPanelTitle.AutoSize = true;
            this.lblPanelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblPanelTitle.ForeColor = System.Drawing.Color.White;
            this.lblPanelTitle.Location = new System.Drawing.Point(20, 20);
            this.lblPanelTitle.Name = "lblPanelTitle";
            this.lblPanelTitle.Size = new System.Drawing.Size(117, 30);
            this.lblPanelTitle.TabIndex = 0;
            this.lblPanelTitle.Text = "Staff Home";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.mainPanel.Controls.Add(this.statsPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(220, 70);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(780, 530);
            this.mainPanel.TabIndex = 1;
            // 
            // statsPanel
            // 
            this.statsPanel.Controls.Add(this.cardMenu);
            this.statsPanel.Controls.Add(this.cardPending);
            this.statsPanel.Controls.Add(this.cardTables);
            this.statsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statsPanel.Location = new System.Drawing.Point(0, 0);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Size = new System.Drawing.Size(780, 150);
            this.statsPanel.TabIndex = 0;
            // 
            // cardTables
            // 
            this.cardTables.BackColor = System.Drawing.Color.White;
            this.cardTables.Controls.Add(this.lblTablesVal);
            this.cardTables.Controls.Add(this.lblTablesTitle);
            this.cardTables.Location = new System.Drawing.Point(30, 25);
            this.cardTables.Name = "cardTables";
            this.cardTables.Size = new System.Drawing.Size(220, 100);
            this.cardTables.TabIndex = 0;
            // 
            // lblTablesVal
            // 
            this.lblTablesVal.AutoSize = true;
            this.lblTablesVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTablesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(160)))), ((int)(((byte)(133)))));
            this.lblTablesVal.Location = new System.Drawing.Point(15, 45);
            this.lblTablesVal.Name = "lblTablesVal";
            this.lblTablesVal.Size = new System.Drawing.Size(28, 32);
            this.lblTablesVal.TabIndex = 1;
            this.lblTablesVal.Text = "0";
            // 
            // lblTablesTitle
            // 
            this.lblTablesTitle.AutoSize = true;
            this.lblTablesTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTablesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblTablesTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTablesTitle.Name = "lblTablesTitle";
            this.lblTablesTitle.Size = new System.Drawing.Size(129, 19);
            this.lblTablesTitle.TabIndex = 0;
            this.lblTablesTitle.Text = "FREE TABLES";
            // 
            // cardPending
            // 
            this.cardPending.BackColor = System.Drawing.Color.White;
            this.cardPending.Controls.Add(this.lblPendingVal);
            this.cardPending.Controls.Add(this.lblPendingTitle);
            this.cardPending.Location = new System.Drawing.Point(280, 25);
            this.cardPending.Name = "cardPending";
            this.cardPending.Size = new System.Drawing.Size(220, 100);
            this.cardPending.TabIndex = 1;
            // 
            // lblPendingVal
            // 
            this.lblPendingVal.AutoSize = true;
            this.lblPendingVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPendingVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblPendingVal.Location = new System.Drawing.Point(15, 45);
            this.lblPendingVal.Name = "lblPendingVal";
            this.lblPendingVal.Size = new System.Drawing.Size(28, 32);
            this.lblPendingVal.TabIndex = 1;
            this.lblPendingVal.Text = "0";
            // 
            // lblPendingTitle
            // 
            this.lblPendingTitle.AutoSize = true;
            this.lblPendingTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblPendingTitle.Location = new System.Drawing.Point(15, 15);
            this.lblPendingTitle.Name = "lblPendingTitle";
            this.lblPendingTitle.Size = new System.Drawing.Size(127, 19);
            this.lblPendingTitle.TabIndex = 0;
            this.lblPendingTitle.Text = "PENDING ORDERS";
            // 
            // cardMenu
            // 
            this.cardMenu.BackColor = System.Drawing.Color.White;
            this.cardMenu.Controls.Add(this.lblMenuVal);
            this.cardMenu.Controls.Add(this.lblMenuTitle);
            this.cardMenu.Location = new System.Drawing.Point(530, 25);
            this.cardMenu.Name = "cardMenu";
            this.cardMenu.Size = new System.Drawing.Size(220, 100);
            this.cardMenu.TabIndex = 2;
            // 
            // lblMenuVal
            // 
            this.lblMenuVal.AutoSize = true;
            this.lblMenuVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMenuVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.lblMenuVal.Location = new System.Drawing.Point(15, 45);
            this.lblMenuVal.Name = "lblMenuVal";
            this.lblMenuVal.Size = new System.Drawing.Size(28, 32);
            this.lblMenuVal.TabIndex = 1;
            this.lblMenuVal.Text = "0";
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblMenuTitle.Location = new System.Drawing.Point(15, 15);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(95, 19);
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
            this.ClientSize = new System.Drawing.Size(1000, 600);
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
