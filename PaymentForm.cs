using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class PaymentForm : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public PaymentForm()
        {
            InitializeComponent();
            LoadPendingOrders();
        }

        private void LoadPendingOrders()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT OrderID, TotalAmount, OrderDate FROM Orders WHERE Status = 'Pending'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPendingOrders.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnProcessPayment_Click(object sender, EventArgs e)
        {
            if (dgvPendingOrders.SelectedRows.Count > 0)
            {
                int orderId = Convert.ToInt32(dgvPendingOrders.SelectedRows[0].Cells["OrderID"].Value);
                decimal amount = Convert.ToDecimal(dgvPendingOrders.SelectedRows[0].Cells["TotalAmount"].Value);

                try
                {
                    con.Open();
                    MySqlTransaction trans = con.BeginTransaction();

                    try
                    {
                        // 1. Insert Payment
                        MySqlCommand cmdPay = new MySqlCommand("INSERT INTO Payments (OrderID, Amount) VALUES (@oid, @amt)", con, trans);
                        cmdPay.Parameters.AddWithValue("@oid", orderId);
                        cmdPay.Parameters.AddWithValue("@amt", amount);
                        cmdPay.ExecuteNonQuery();

                        // 2. Update Order Status
                        MySqlCommand cmdOrder = new MySqlCommand("UPDATE Orders SET Status = 'Paid' WHERE OrderID = @oid", con, trans);
                        cmdOrder.Parameters.AddWithValue("@oid", orderId);
                        cmdOrder.ExecuteNonQuery();

                        // 3. Free the Table
                        MySqlCommand cmdTable = new MySqlCommand("UPDATE Tables SET Status = 'Available' WHERE TableID = (SELECT TableID FROM Orders WHERE OrderID = @oid)", con, trans);
                        cmdTable.Parameters.AddWithValue("@oid", orderId);
                        cmdTable.ExecuteNonQuery();

                        trans.Commit();
                        MessageBox.Show($"Payment of {amount:N2} processed successfully!\nReceipt printed (Simulated).");
                        LoadPendingOrders();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Payment failed: " + ex.Message);
                    }
                    finally { con.Close(); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
