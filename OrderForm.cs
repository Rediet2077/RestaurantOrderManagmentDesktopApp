using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace RestaurantDesktopApp
{
    public partial class OrderForm : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");
        DataTable orderDetailsTable = new DataTable();
        decimal totalAmount = 0;

        public OrderForm()
        {
            InitializeComponent();
            SetupOrderDetailsTable();
            LoadTables();
            LoadCustomers();
            LoadMenuItems();
        }

        private void SetupOrderDetailsTable()
        {
            orderDetailsTable.Columns.Add("ItemID", typeof(int));
            orderDetailsTable.Columns.Add("ItemName", typeof(string));
            orderDetailsTable.Columns.Add("Quantity", typeof(int));
            orderDetailsTable.Columns.Add("Price", typeof(decimal));
            orderDetailsTable.Columns.Add("Subtotal", typeof(decimal));
            dgvOrderDetails.DataSource = orderDetailsTable;
        }

        private void LoadTables()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT TableID, Status FROM Tables WHERE Status = 'Available'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbTables.DataSource = dt;
                cmbTables.DisplayMember = "TableID";
                cmbTables.ValueMember = "TableID";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadCustomers()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT CustomerID, Name FROM Customers", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                // Add a default "Walk-in" customer if none exist or as an option
                DataRow dr = dt.NewRow();
                dr["CustomerID"] = DBNull.Value;
                dr["Name"] = "Walk-in";
                dt.Rows.InsertAt(dr, 0);

                cmbCustomers.DataSource = dt;
                cmbCustomers.DisplayMember = "Name";
                cmbCustomers.ValueMember = "CustomerID";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadMenuItems()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT ItemID, Name, Price FROM MenuItems", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbMenuItems.DataSource = dt;
                cmbMenuItems.DisplayMember = "Name";
                cmbMenuItems.ValueMember = "ItemID";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbMenuItems.SelectedValue != null)
            {
                DataRowView selectedItem = (DataRowView)cmbMenuItems.SelectedItem;
                int itemId = Convert.ToInt32(selectedItem["ItemID"]);
                string itemName = selectedItem["Name"].ToString();
                decimal price = Convert.ToDecimal(selectedItem["Price"]);
                int quantity = (int)numQuantity.Value;
                decimal subtotal = price * quantity;

                orderDetailsTable.Rows.Add(itemId, itemName, quantity, price, subtotal);
                UpdateTotalAmount();
            }
        }

        private void UpdateTotalAmount()
        {
            totalAmount = 0;
            foreach (DataRow row in orderDetailsTable.Rows)
            {
                totalAmount += Convert.ToDecimal(row["Subtotal"]);
            }
            lblTotalAmount.Text = totalAmount.ToString("N2");
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (orderDetailsTable.Rows.Count == 0)
            {
                MessageBox.Show("Please add items to the order.");
                return;
            }

            try
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();

                try
                {
                    // 1. Insert Order
                    MySqlCommand cmdOrder = new MySqlCommand(
                        "INSERT INTO Orders (CustomerID, TableID, TotalAmount, Status) VALUES (@cid, @tid, @total, 'Pending'); SELECT LAST_INSERT_ID();", con, trans);
                    
                    cmdOrder.Parameters.AddWithValue("@cid", cmbCustomers.SelectedValue ?? DBNull.Value);
                    cmdOrder.Parameters.AddWithValue("@tid", cmbTables.SelectedValue);
                    cmdOrder.Parameters.AddWithValue("@total", totalAmount);

                    int orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());

                    // 2. Insert Order Details
                    foreach (DataRow row in orderDetailsTable.Rows)
                    {
                        MySqlCommand cmdDetail = new MySqlCommand(
                            "INSERT INTO OrderDetails (OrderID, ItemID, Quantity, Price) VALUES (@oid, @iid, @qty, @prc)", con, trans);
                        cmdDetail.Parameters.AddWithValue("@oid", orderId);
                        cmdDetail.Parameters.AddWithValue("@iid", row["ItemID"]);
                        cmdDetail.Parameters.AddWithValue("@qty", row["Quantity"]);
                        cmdDetail.Parameters.AddWithValue("@prc", row["Price"]);
                        cmdDetail.ExecuteNonQuery();
                    }

                    // 3. Update Table Status
                    MySqlCommand cmdTable = new MySqlCommand("UPDATE Tables SET Status = 'Occupied' WHERE TableID = @tid", con, trans);
                    cmdTable.Parameters.AddWithValue("@tid", cmbTables.SelectedValue);
                    cmdTable.ExecuteNonQuery();

                    trans.Commit();
                    MessageBox.Show("Order Placed Successfully!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Transaction failed: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}