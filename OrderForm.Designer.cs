namespace RestaurantDesktopApp
{
    partial class OrderForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTable;
        private ComboBox cmbTables;
        private Label lblCustomer;
        private ComboBox cmbCustomers;
        private Label lblMenuItem;
        private ComboBox cmbMenuItems;
        private Label lblQuantity;
        private NumericUpDown numQuantity;
        private Button btnAddItem;
        private DataGridView dgvOrderDetails;
        private Label lblTotal;
        private Label lblTotalAmount;
        private Button btnPlaceOrder;

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
            lblTable = new Label();
            cmbTables = new ComboBox();
            lblCustomer = new Label();
            cmbCustomers = new ComboBox();
            lblMenuItem = new Label();
            cmbMenuItems = new ComboBox();
            lblQuantity = new Label();
            numQuantity = new NumericUpDown();
            btnAddItem = new Button();
            dgvOrderDetails = new DataGridView();
            lblTotal = new Label();
            lblTotalAmount = new Label();
            btnPlaceOrder = new Button();

            ((System.ComponentModel.ISupportInitialize)(numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dgvOrderDetails)).BeginInit();
            SuspendLayout();

            // lblTable
            lblTable.Text = "Table:";
            lblTable.Location = new System.Drawing.Point(20, 20);
            lblTable.Size = new System.Drawing.Size(100, 23);

            // cmbTables
            cmbTables.Location = new System.Drawing.Point(120, 20);
            cmbTables.Size = new System.Drawing.Size(150, 23);

            // lblCustomer
            lblCustomer.Text = "Customer:";
            lblCustomer.Location = new System.Drawing.Point(20, 50);
            lblCustomer.Size = new System.Drawing.Size(100, 23);

            // cmbCustomers
            cmbCustomers.Location = new System.Drawing.Point(120, 50);
            cmbCustomers.Size = new System.Drawing.Size(150, 23);

            // lblMenuItem
            lblMenuItem.Text = "Menu Item:";
            lblMenuItem.Location = new System.Drawing.Point(20, 90);
            lblMenuItem.Size = new System.Drawing.Size(100, 23);

            // cmbMenuItems
            cmbMenuItems.Location = new System.Drawing.Point(120, 90);
            cmbMenuItems.Size = new System.Drawing.Size(150, 23);

            // lblQuantity
            lblQuantity.Text = "Quantity:";
            lblQuantity.Location = new System.Drawing.Point(280, 90);
            lblQuantity.Size = new System.Drawing.Size(60, 23);

            // numQuantity
            numQuantity.Location = new System.Drawing.Point(340, 90);
            numQuantity.Size = new System.Drawing.Size(50, 23);
            numQuantity.Value = 1;
            numQuantity.Minimum = 1;

            // btnAddItem
            btnAddItem.Text = "Add to Order";
            btnAddItem.Location = new System.Drawing.Point(400, 88);
            btnAddItem.Size = new System.Drawing.Size(100, 30);
            btnAddItem.Click += btnAddItem_Click;

            // dgvOrderDetails
            dgvOrderDetails.Location = new System.Drawing.Point(20, 130);
            dgvOrderDetails.Size = new System.Drawing.Size(550, 200);
            dgvOrderDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // lblTotal
            lblTotal.Text = "Total Amount:";
            lblTotal.Location = new System.Drawing.Point(380, 340);
            lblTotal.Size = new System.Drawing.Size(100, 23);
            lblTotal.Font = new System.Drawing.Font(lblTotal.Font, System.Drawing.FontStyle.Bold);

            // lblTotalAmount
            lblTotalAmount.Text = "0.00";
            lblTotalAmount.Location = new System.Drawing.Point(480, 340);
            lblTotalAmount.Size = new System.Drawing.Size(100, 23);
            lblTotalAmount.Font = new System.Drawing.Font(lblTotalAmount.Font, System.Drawing.FontStyle.Bold);

            // btnPlaceOrder
            btnPlaceOrder.Text = "Place Order";
            btnPlaceOrder.Location = new System.Drawing.Point(450, 380);
            btnPlaceOrder.Size = new System.Drawing.Size(120, 35);
            btnPlaceOrder.Click += btnPlaceOrder_Click;

            // OrderForm
            ClientSize = new System.Drawing.Size(600, 440);
            Controls.Add(lblTable);
            Controls.Add(cmbTables);
            Controls.Add(lblCustomer);
            Controls.Add(cmbCustomers);
            Controls.Add(lblMenuItem);
            Controls.Add(cmbMenuItems);
            Controls.Add(lblQuantity);
            Controls.Add(numQuantity);
            Controls.Add(btnAddItem);
            Controls.Add(dgvOrderDetails);
            Controls.Add(lblTotal);
            Controls.Add(lblTotalAmount);
            Controls.Add(btnPlaceOrder);
            Text = "Create Order";

            ((System.ComponentModel.ISupportInitialize)(numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dgvOrderDetails)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}