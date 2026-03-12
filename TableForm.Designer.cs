namespace RestaurantDesktopApp
{
    partial class TableForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvTables;
        private ComboBox cmbStatus;
        private Button btnUpdateStatus;
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
            dgvTables = new DataGridView();
            cmbStatus = new ComboBox();
            btnUpdateStatus = new Button();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)(dgvTables)).BeginInit();
            SuspendLayout();

            // lblTitle
            lblTitle.Text = "Table Management";
            lblTitle.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(20, 20);
            lblTitle.AutoSize = true;

            // dgvTables
            dgvTables.Location = new System.Drawing.Point(20, 60);
            dgvTables.Size = new System.Drawing.Size(340, 200);
            dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // cmbStatus
            cmbStatus.Location = new System.Drawing.Point(20, 280);
            cmbStatus.Size = new System.Drawing.Size(150, 23);
            cmbStatus.Items.AddRange(new object[] { "Available", "Reserved", "Occupied" });

            // btnUpdateStatus
            btnUpdateStatus.Text = "Update Status";
            btnUpdateStatus.Location = new System.Drawing.Point(180, 278);
            btnUpdateStatus.Size = new System.Drawing.Size(120, 30);
            btnUpdateStatus.Click += btnUpdateStatus_Click;

            // TableForm
            ClientSize = new System.Drawing.Size(400, 340);
            Controls.Add(lblTitle);
            Controls.Add(dgvTables);
            Controls.Add(cmbStatus);
            Controls.Add(btnUpdateStatus);
            Text = "Table Management";

            ((System.ComponentModel.ISupportInitialize)(dgvTables)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
