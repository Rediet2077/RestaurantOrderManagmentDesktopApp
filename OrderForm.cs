using System;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order Placed Successfully!");
        }
    }
}