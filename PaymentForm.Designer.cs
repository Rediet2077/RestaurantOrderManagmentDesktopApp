namespace RestaurantDesktopApp
{
    partial class PaymentForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvPendingOrders;
        private Button btnProcessPayment;
        private Label lblTitle;

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
            dgvPendingOrders = new DataGridView();
            btnProcessPayment = new Button();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)(dgvPendingOrders)).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.Text = "Process Payments";
            lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.AutoSize = true;

            // dgvPendingOrders
            dgvPendingOrders.Location = new System.Drawing.Point(20, 60);
            dgvPendingOrders.Size = new System.Drawing.Size(440, 200);
            dgvPendingOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPendingOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // btnProcessPayment
            btnProcessPayment.Text = "Process Payment";
            btnProcessPayment.Location = new System.Drawing.Point(340, 280);
            btnProcessPayment.Size = new System.Drawing.Size(120, 35);
            btnProcessPayment.Click += btnProcessPayment_Click;

            // PaymentForm
            ClientSize = new System.Drawing.Size(480, 340);
            Controls.Add(lblTitle);
            Controls.Add(dgvPendingOrders);
            Controls.Add(btnProcessPayment);
            Text = "Payments";

            ((System.ComponentModel.ISupportInitialize)(dgvPendingOrders)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
