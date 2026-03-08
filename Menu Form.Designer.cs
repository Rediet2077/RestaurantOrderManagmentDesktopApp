namespace RestaurantDesktopApp
{
    partial class Menu_Form
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblItemName;
        private Label lblPrice;
        private Label lblCategory;
        private TextBox txtItemName;
        private TextBox txtPrice;
        private TextBox txtCategory;
        private Button btnAddItem;
        private DataGridView dgvMenuItems;

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
            lblTitle = new Label();
            lblItemName = new Label();
            lblPrice = new Label();
            lblCategory = new Label();
            txtItemName = new TextBox();
            txtPrice = new TextBox();
            txtCategory = new TextBox();
            btnAddItem = new Button();
            dgvMenuItems = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)(dgvMenuItems)).BeginInit();
            SuspendLayout();

            // Title
            lblTitle.AutoSize = true;
            lblTitle.Text = "Menu Management Form";
            lblTitle.Location = new System.Drawing.Point(200, 10);

            // Item Name Label
            lblItemName.AutoSize = true;
            lblItemName.Text = "Item Name";
            lblItemName.Location = new System.Drawing.Point(50, 60);

            // Item Name TextBox
            txtItemName.Location = new System.Drawing.Point(150, 60);
            txtItemName.Size = new System.Drawing.Size(200, 23);

            // Price Label
            lblPrice.AutoSize = true;
            lblPrice.Text = "Price";
            lblPrice.Location = new System.Drawing.Point(50, 100);

            // Price TextBox
            txtPrice.Location = new System.Drawing.Point(150, 100);
            txtPrice.Size = new System.Drawing.Size(200, 23);

            // Category Label
            lblCategory.AutoSize = true;
            lblCategory.Text = "Category";
            lblCategory.Location = new System.Drawing.Point(50, 140);

            // Category TextBox
            txtCategory.Location = new System.Drawing.Point(150, 140);
            txtCategory.Size = new System.Drawing.Size(200, 23);

            // Add Button
            btnAddItem.Text = "Add Item";
            btnAddItem.Location = new System.Drawing.Point(150, 180);
            btnAddItem.Size = new System.Drawing.Size(120, 30);
            btnAddItem.Click += btnAddItem_Click;

            // DataGridView
            dgvMenuItems.Location = new System.Drawing.Point(50, 230);
            dgvMenuItems.Size = new System.Drawing.Size(500, 200);
            dgvMenuItems.Name = "dgvMenuItems";

            // Add Controls
            Controls.Add(lblTitle);
            Controls.Add(lblItemName);
            Controls.Add(txtItemName);
            Controls.Add(lblPrice);
            Controls.Add(txtPrice);
            Controls.Add(lblCategory);
            Controls.Add(txtCategory);
            Controls.Add(btnAddItem);
            Controls.Add(dgvMenuItems);

            // Form Settings
            Text = "Menu Form";
            ClientSize = new System.Drawing.Size(620, 460);

            ((System.ComponentModel.ISupportInitialize)(dgvMenuItems)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}