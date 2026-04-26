using System;
using System.Data;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class PaymentForm : Form
    {
        private int? _preSelectedOrderId;
        public PaymentForm(int? orderId = null)
        {
            InitializeComponent();
            _preSelectedOrderId = orderId;
            if (this.Parent == null)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Text = "Process Payment";
                this.ControlBox = true;
                this.StartPosition = FormStartPosition.CenterParent;
            }

            UIHelper.ApplyTheme(this);
            UIHelper.ApplyModernButton(btnProcessPayment, UIHelper.PrimaryColor);
            _ = LoadPendingOrdersAsync();
        }

        private async System.Threading.Tasks.Task LoadPendingOrdersAsync()
        {
            try
            {
                DataTable dt = await ApiClient.GetPendingOrdersTableAsync();
                dgvPendingOrders.DataSource = dt;

                if (_preSelectedOrderId.HasValue && dgvPendingOrders.Rows.Count > 0 && dgvPendingOrders.Columns.Contains("OrderID"))
                {
                    foreach (DataGridViewRow row in dgvPendingOrders.Rows)
                    {
                        if (row.Cells["OrderID"].Value != null && Convert.ToInt32(row.Cells["OrderID"].Value) == _preSelectedOrderId.Value)
                        {
                            row.Selected = true;
                            if (dgvPendingOrders.FirstDisplayedScrollingRowIndex != -1)
                                dgvPendingOrders.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }
                }
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
                    // Prepare and show the receipt
                    await PrepareAndSendReceiptAsync(orderId, amount);
                    
                    if (_preSelectedOrderId.HasValue)
                    {
                        this.Close();
                    }
                    else
                    {
                        await LoadPendingOrdersAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Payment failed. Please try again.");
                }
            }
        }

        private async System.Threading.Tasks.Task PrepareAndSendReceiptAsync(int orderId, decimal totalAmount)
        {
            try
            {
                var details = await ApiClient.GetOrderDetailsAsync(orderId);
                var settings = await ApiClient.GetSettingsAsync();
                string restaurantName = settings?.RestaurantName ?? "BEST Restaurant";

                System.Text.StringBuilder receipt = new System.Text.StringBuilder();
                receipt.AppendLine("==================================");
                receipt.AppendLine($"    {restaurantName.ToUpper()}    ");
                receipt.AppendLine("==================================");
                receipt.AppendLine($"Order ID: {orderId}");
                receipt.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm}");
                receipt.AppendLine("----------------------------------");
                receipt.AppendLine(string.Format("{0,-20} {1,5} {2,8}", "Item", "Qty", "Price"));
                
                foreach (var item in details)
                {
                    receipt.AppendLine(string.Format("{0,-20} {1,5} {2,8:N2}", 
                        item.ItemName.Length > 20 ? item.ItemName.Substring(0, 17) + "..." : item.ItemName, 
                        item.Quantity, 
                        item.Subtotal));
                }

                receipt.AppendLine("----------------------------------");
                receipt.AppendLine(string.Format("{0,-20} {1,14:N2}", "TOTAL:", totalAmount));
                receipt.AppendLine("==================================");
                receipt.AppendLine("      THANK YOU FOR VISITING!     ");
                receipt.AppendLine("==================================");

                string receiptContent = receipt.ToString();
                
                // Show the receipt to the user immediately
                UIHelper.ShowReceiptDialog(receiptContent, this);

                // Then try to send it to the admin log in the background
                bool sent = await ApiClient.SubmitReceiptAsync(orderId, receiptContent);
                if (sent)
                {
                    UIHelper.ShowToast("Receipt archived for Admin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error preparing receipt: " + ex.Message);
            }
        }
    }
}
