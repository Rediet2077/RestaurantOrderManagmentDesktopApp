namespace RestaurantDesktopApp
{
    partial class AdminMainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.btnManageMenu = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.lblPanelTitle = new System.Windows.Forms.Label();
            this.sidebarPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.sidebarPanel.Controls.Add(this.btnLogout);
            this.sidebarPanel.Controls.Add(this.btnReports);
            this.sidebarPanel.Controls.Add(this.btnManageMenu);
            this.sidebarPanel.Controls.Add(this.lblAdminTitle);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(220, 600);
            this.sidebarPanel.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
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
            this.lblPanelTitle.Size = new System.Drawing.Size(200, 30);
            this.lblPanelTitle.TabIndex = 0;
            this.lblPanelTitle.Text = "Dashboard Overview";
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(220, 70);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(780, 530);
            this.mainPanel.TabIndex = 1;
            // 
            // btnManageMenu
            // 
            this.btnManageMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnManageMenu.FlatAppearance.BorderSize = 0;
            this.btnManageMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageMenu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnManageMenu.ForeColor = System.Drawing.Color.White;
            this.btnManageMenu.Location = new System.Drawing.Point(0, 120);
            this.btnManageMenu.Name = "btnManageMenu";
            this.btnManageMenu.Size = new System.Drawing.Size(220, 60);
            this.btnManageMenu.TabIndex = 0;
            this.btnManageMenu.Text = "  📦 Manage Menu";
            this.btnManageMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManageMenu.UseVisualStyleBackColor = true;
            this.btnManageMenu.Click += new System.EventHandler(this.btnManageMenu_Click);
            // 
            // btnReports
            // 
            this.btnReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReports.FlatAppearance.BorderSize = 0;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Location = new System.Drawing.Point(0, 180);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(220, 60);
            this.btnReports.TabIndex = 1;
            this.btnReports.Text = "  📊 View Reports";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
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
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "  🚪 Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.ForeColor = System.Drawing.Color.White;
            this.lblAdminTitle.Location = new System.Drawing.Point(12, 30);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(185, 32);
            this.lblAdminTitle.TabIndex = 3;
            this.lblAdminTitle.Text = "ADMIN PANEL";
            // 
            // AdminMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "AdminMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin Dashboard";
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnManageMenu;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblPanelTitle;
    }
}
