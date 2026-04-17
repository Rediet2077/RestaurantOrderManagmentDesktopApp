using System;
using System.Data;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class PaymentForm : Form
    {
        public PaymentForm()
        {
            InitializeComponent();
            _ = LoadPendingOrdersAsync();
        }

        private async System.Threading.Tasks.Task LoadPendingOrdersAsync()
        {
            try
            {
                DataTable dt = await ApiClient.GetPendingOrdersTableAsync();
                dgvPendingOrders.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void btnProcessPayment_Click(object sender, EventArgs e)
        {
            if (dgvPendingOrders.SelectedRows.Count > 0)
            {
                var cellId = dgvPendingOrders.SelectedRows[0].Cells["OrderID"].Value;
                var cellAmt = dgvPendingOrders.SelectedRows[0].Cells["TotalAmount"].Value;

                if (cellId == null || cellId == DBNull.Value || cellAmt == null || cellAmt == DBNull.Value)
                {
                    MessageBox.Show("Selected order data is invalid.");
                    return;
                }

                int orderId = Convert.ToInt32(cellId);
                decimal amount = Convert.ToDecimal(cellAmt);

                bool success = await ApiClient.ProcessPaymentAsync(orderId, amount);
                if (success)
                {
                    UIHelper.ShowToast("Payment Processed Successfully!");
                    await LoadPendingOrdersAsync();
                }
                else
                {
                    MessageBox.Show("Payment failed. Please try again.");
                }
            }
        }
    }
}
