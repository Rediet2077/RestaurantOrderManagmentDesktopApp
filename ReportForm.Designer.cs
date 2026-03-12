namespace RestaurantDesktopApp
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvReport;
        private Label lblTitle;
        private Label lblTotalLabel;
        private Label lblTotalSales;
        private Button btnRefresh;

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
            dgvReport = new DataGridView();
            lblTitle = new Label();
            lblTotalLabel = new Label();
            lblTotalSales = new Label();
            btnRefresh = new Button();
            ((System.ComponentModel.ISupportInitialize)(dgvReport)).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.Text = "Daily Sales Report";
            lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.AutoSize = true;

            // dgvReport
            dgvReport.Location = new System.Drawing.Point(20, 60);
            dgvReport.Size = new System.Drawing.Size(440, 150);
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // lblTotalLabel
            lblTotalLabel.Text = "Total Today's Sales:";
            lblTotalLabel.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblTotalLabel.Location = new System.Drawing.Point(20, 230);
            lblTotalLabel.AutoSize = true;

            // lblTotalSales
            lblTotalSales.Text = "0.00";
            lblTotalSales.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            lblTotalSales.ForeColor = System.Drawing.Color.Green;
            lblTotalSales.Location = new System.Drawing.Point(160, 230);
            lblTotalSales.AutoSize = true;

            // btnRefresh
            btnRefresh.Text = "Refresh Report";
            btnRefresh.Location = new System.Drawing.Point(340, 260);
            btnRefresh.Size = new System.Drawing.Size(120, 30);
            btnRefresh.Click += btnRefresh_Click;

            // ReportForm
            ClientSize = new System.Drawing.Size(480, 310);
            Controls.Add(lblTitle);
            Controls.Add(dgvReport);
            Controls.Add(lblTotalLabel);
            Controls.Add(lblTotalSales);
            Controls.Add(btnRefresh);
            Text = "Daily Sales Report";

            ((System.ComponentModel.ISupportInitialize)(dgvReport)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
