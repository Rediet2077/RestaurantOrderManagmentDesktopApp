using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class Main_Form__Dashboard_ : Form
    {
        public Main_Form__Dashboard_()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnManageMenu_Click(object sender, EventArgs e)
        {
            Menu_Form menu = new Menu_Form();
            menu.Show();
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            OrderForm order = new OrderForm();
            order.Show();
        }
    }
}
