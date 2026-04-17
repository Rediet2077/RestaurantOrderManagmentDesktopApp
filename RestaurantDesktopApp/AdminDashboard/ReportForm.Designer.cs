namespace RestaurantDesktopApp
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalSales;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel headerPanel;
        
        private System.Windows.Forms.Panel statsPanel;
        private System.Windows.Forms.Panel cardRevenue;
        private System.Windows.Forms.Label lblRevenueVal;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Panel cardOrders;
        private System.Windows.Forms.Label lblOrdersVal;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Panel cardTables;
        private System.Windows.Forms.Label lblTablesVal;
        private System.Windows.Forms.Label lblTablesTitle;
        private System.Windows.Forms.Panel cardPending;
        private System.Windows.Forms.Label lblPendingVal;
        private System.Windows.Forms.Label lblPendingTitle;

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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dgvReport = new DataGridView();
            lblTitle = new Label();
            lblTotalLabel = new Label();
            lblTotalSales = new Label();
            btnRefresh = new Button();
            headerPanel = new Panel();
            statsPanel = new Panel();
            cardPending = new Panel();
            lblPendingVal = new Label();
            lblPendingTitle = new Label();
            cardTables = new Panel();
            lblTablesVal = new Label();
            lblTablesTitle = new Label();
            cardOrders = new Panel();
            lblOrdersVal = new Label();
            lblOrdersTitle = new Label();
            cardRevenue = new Panel();
            lblRevenueVal = new Label();
            lblRevenueTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvReport).BeginInit();
            headerPanel.SuspendLayout();
            statsPanel.SuspendLayout();
            cardPending.SuspendLayout();
            cardTables.SuspendLayout();
            cardOrders.SuspendLayout();
            cardRevenue.SuspendLayout();
            SuspendLayout();
            // 
            // dgvReport
            // 
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.BackgroundColor = Color.White;
            dgvReport.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReport.EnableHeadersVisualStyles = false;
            dgvReport.Location = new Point(20, 210);
            dgvReport.Name = "dgvReport";
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.Size = new Size(740, 190);
            dgvReport.TabIndex = 1;
            dgvReport.CellContentClick += dgvReport_CellContentClick;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(200, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Daily Sales Report";
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.AutoSize = true;
            lblTotalLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalLabel.ForeColor = Color.FromArgb(44, 62, 80);
            lblTotalLabel.Location = new Point(20, 420);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(180, 25);
            lblTotalLabel.TabIndex = 2;
            lblTotalLabel.Text = "Total Today's Sales:";
            // 
            // lblTotalSales
            // 
            lblTotalSales.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalSales.ForeColor = Color.FromArgb(39, 174, 96);
            lblTotalSales.Location = new Point(215, 420);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new Size(50, 25);
            lblTotalSales.TabIndex = 3;
            lblTotalSales.Text = "0.00";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(41, 128, 185);
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(610, 415);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(150, 35);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "View Report";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(44, 62, 80);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(780, 60);
            headerPanel.TabIndex = 6;
            // 
            // statsPanel
            // 
            statsPanel.Controls.Add(cardPending);
            statsPanel.Controls.Add(cardTables);
            statsPanel.Controls.Add(cardOrders);
            statsPanel.Controls.Add(cardRevenue);
            statsPanel.Dock = DockStyle.Top;
            statsPanel.Location = new Point(0, 60);
            statsPanel.Name = "statsPanel";
            statsPanel.Size = new Size(780, 130);
            statsPanel.TabIndex = 7;
            // 
            // cardPending
            // 
            cardPending.BackColor = Color.White;
            cardPending.Controls.Add(lblPendingVal);
            cardPending.Controls.Add(lblPendingTitle);
            cardPending.Location = new Point(590, 15);
            cardPending.Name = "cardPending";
            cardPending.Size = new Size(170, 100);
            cardPending.TabIndex = 3;
            // 
            // lblPendingVal
            // 
            lblPendingVal.AutoSize = true;
            lblPendingVal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPendingVal.ForeColor = Color.FromArgb(231, 76, 60);
            lblPendingVal.Location = new Point(10, 45);
            lblPendingVal.Name = "lblPendingVal";
            lblPendingVal.Size = new Size(26, 30);
            lblPendingVal.TabIndex = 1;
            lblPendingVal.Text = "0";
            // 
            // lblPendingTitle
            // 
            lblPendingTitle.AutoSize = true;
            lblPendingTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPendingTitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblPendingTitle.Location = new Point(10, 15);
            lblPendingTitle.Name = "lblPendingTitle";
            lblPendingTitle.Size = new Size(110, 15);
            lblPendingTitle.TabIndex = 0;
            lblPendingTitle.Text = "PENDING ORDERS";
            // 
            // cardTables
            // 
            cardTables.BackColor = Color.White;
            cardTables.Controls.Add(lblTablesVal);
            cardTables.Controls.Add(lblTablesTitle);
            cardTables.Location = new Point(400, 15);
            cardTables.Name = "cardTables";
            cardTables.Size = new Size(170, 100);
            cardTables.TabIndex = 2;
            // 
            // lblTablesVal
            // 
            lblTablesVal.AutoSize = true;
            lblTablesVal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTablesVal.ForeColor = Color.FromArgb(243, 156, 18);
            lblTablesVal.Location = new Point(10, 45);
            lblTablesVal.Name = "lblTablesVal";
            lblTablesVal.Size = new Size(26, 30);
            lblTablesVal.TabIndex = 1;
            lblTablesVal.Text = "0";
            // 
            // lblTablesTitle
            // 
            lblTablesTitle.AutoSize = true;
            lblTablesTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTablesTitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblTablesTitle.Location = new Point(10, 15);
            lblTablesTitle.Name = "lblTablesTitle";
            lblTablesTitle.Size = new Size(91, 15);
            lblTablesTitle.TabIndex = 0;
            lblTablesTitle.Text = "ACTIVE TABLES";
            // 
            // cardOrders
            // 
            cardOrders.BackColor = Color.White;
            cardOrders.Controls.Add(lblOrdersVal);
            cardOrders.Controls.Add(lblOrdersTitle);
            cardOrders.Location = new Point(210, 15);
            cardOrders.Name = "cardOrders";
            cardOrders.Size = new Size(170, 100);
            cardOrders.TabIndex = 1;
            // 
            // lblOrdersVal
            // 
            lblOrdersVal.AutoSize = true;
            lblOrdersVal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblOrdersVal.ForeColor = Color.FromArgb(41, 128, 185);
            lblOrdersVal.Location = new Point(10, 45);
            lblOrdersVal.Name = "lblOrdersVal";
            lblOrdersVal.Size = new Size(26, 30);
            lblOrdersVal.TabIndex = 1;
            lblOrdersVal.Text = "0";
            // 
            // lblOrdersTitle
            // 
            lblOrdersTitle.AutoSize = true;
            lblOrdersTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblOrdersTitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblOrdersTitle.Location = new Point(10, 15);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Size = new Size(93, 15);
            lblOrdersTitle.TabIndex = 0;
            lblOrdersTitle.Text = "TOTAL ORDERS";
            // 
            // cardRevenue
            // 
            cardRevenue.BackColor = Color.White;
            cardRevenue.Controls.Add(lblRevenueVal);
            cardRevenue.Controls.Add(lblRevenueTitle);
            cardRevenue.Location = new Point(20, 15);
            cardRevenue.Name = "cardRevenue";
            cardRevenue.Size = new Size(170, 100);
            cardRevenue.TabIndex = 0;
            cardRevenue.Paint += cardRevenue_Paint;
            // 
            // lblRevenueVal
            // 
            lblRevenueVal.AutoSize = true;
            lblRevenueVal.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblRevenueVal.ForeColor = Color.FromArgb(39, 174, 96);
            lblRevenueVal.Location = new Point(10, 45);
            lblRevenueVal.Name = "lblRevenueVal";
            lblRevenueVal.Size = new Size(87, 30);
            lblRevenueVal.TabIndex = 1;
            lblRevenueVal.Text = "Birr 0.00";
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.AutoSize = true;
            lblRevenueTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.FromArgb(127, 140, 141);
            lblRevenueTitle.Location = new Point(10, 15);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(80, 15);
            lblRevenueTitle.TabIndex = 0;
            lblRevenueTitle.Text = "TOTAL SALES";
            // 
            // ReportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(243, 244, 246);
            ClientSize = new Size(780, 530);
            Controls.Add(btnRefresh);
            Controls.Add(lblTotalSales);
            Controls.Add(lblTotalLabel);
            Controls.Add(dgvReport);
            Controls.Add(statsPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReportForm";
            Text = "Daily Sales Report";
            ((System.ComponentModel.ISupportInitialize)dgvReport).EndInit();
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            statsPanel.ResumeLayout(false);
            cardPending.ResumeLayout(false);
            cardPending.PerformLayout();
            cardTables.ResumeLayout(false);
            cardTables.PerformLayout();
            cardOrders.ResumeLayout(false);
            cardOrders.PerformLayout();
            cardRevenue.ResumeLayout(false);
            cardRevenue.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
