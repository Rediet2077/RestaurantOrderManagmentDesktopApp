namespace RestaurantDesktopApp
{
    partial class OrderForm
    {
        private Label lblOrder;
        private TextBox txtOrder;
        private Button btnOrder;

        private void InitializeComponent()
        {
            lblOrder = new Label();
            txtOrder = new TextBox();
            btnOrder = new Button();

            lblOrder.Text = "Enter Order";
            lblOrder.Location = new System.Drawing.Point(50, 50);

            txtOrder.Location = new System.Drawing.Point(150, 50);

            btnOrder.Text = "Place Order";
            btnOrder.Location = new System.Drawing.Point(150, 90);
            btnOrder.Click += btnOrder_Click;

            Controls.Add(lblOrder);
            Controls.Add(txtOrder);
            Controls.Add(btnOrder);

            Text = "Order Form";
            ClientSize = new System.Drawing.Size(400, 200);
        }
    }
}