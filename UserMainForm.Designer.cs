namespace RestaurantDesktopApp
{
    partial class UserMainForm
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
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.btnManageTables = new System.Windows.Forms.Button();
            this.btnPayments = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblUserTitle = new System.Windows.Forms.Label();
            this.lblPanelTitle = new System.Windows.Forms.Label();
            this.sidebarPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
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
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(220, 600);
            this.sidebarPanel.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
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
            this.lblPanelTitle.Text = "Staff Operations";
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
            this.btnManageTables.Text = "  🪑 Manage Tables";
            this.btnManageTables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManageTables.UseVisualStyleBackColor = true;
            this.btnManageTables.Click += new System.EventHandler(this.btnManageTables_Click);
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
            // lblUserTitle
            // 
            this.lblUserTitle.AutoSize = true;
            this.lblUserTitle.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold);
            this.lblUserTitle.ForeColor = System.Drawing.Color.White;
            this.lblUserTitle.Location = new System.Drawing.Point(12, 30);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Size = new System.Drawing.Size(155, 32);
            this.lblUserTitle.TabIndex = 4;
            this.lblUserTitle.Text = "USER PANEL";
            // 
            // UserMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Name = "UserMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Dashboard";
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnManageTables;
        private System.Windows.Forms.Button btnPayments;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblUserTitle;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblPanelTitle;
    }
}
