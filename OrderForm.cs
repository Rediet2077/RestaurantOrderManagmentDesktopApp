using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

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
            LoadMenuCards();
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
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT TableID FROM Tables WHERE Status = 'Available'", con);
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

        private void LoadMenuCards()
        {
            flpMenu.Controls.Clear();
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM MenuItems", con);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Panel card = new Panel();
                    card.Size = new Size(130, 180);
                    card.BackColor = Color.White;
                    card.Margin = new Padding(10);
                    card.BorderStyle = BorderStyle.FixedSingle;

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(110, 80);
                    pic.Location = new Point(10, 10);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    
                    string imgPath = dr["ImagePath"].ToString();
                    string fullPath = Path.Combine(Application.StartupPath, @"..\..\", imgPath);
                    if (File.Exists(fullPath))
                        pic.Image = Image.FromFile(fullPath);
                    else
                        pic.BackColor = Color.LightGray; // Placeholder

                    Label lblName = new Label();
                    lblName.Text = dr["Name"].ToString();
                    lblName.Location = new Point(10, 100);
                    lblName.Size = new Size(110, 20);
                    lblName.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    lblName.TextAlign = ContentAlignment.MiddleCenter;

                    Label lblPrice = new Label();
                    lblPrice.Text = "$" + dr["Price"].ToString();
                    lblPrice.Location = new Point(10, 120);
                    lblPrice.Size = new Size(110, 20);
                    lblPrice.ForeColor = Color.DarkGreen;
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;

                    Button btnAdd = new Button();
                    btnAdd.Text = "+ Add";
                    btnAdd.Location = new Point(20, 145);
                    btnAdd.Size = new Size(90, 25);
                    btnAdd.FlatStyle = FlatStyle.Flat;
                    btnAdd.BackColor = Color.FromArgb(46, 204, 113);
                    btnAdd.ForeColor = Color.White;
                    btnAdd.Cursor = Cursors.Hand;
                    
                    int id = Convert.ToInt32(dr["ItemID"]);
                    string name = dr["Name"].ToString();
                    decimal price = Convert.ToDecimal(dr["Price"]);
                    
                    btnAdd.Click += (s, ev) => AddToOrder(id, name, price);

                    card.Controls.Add(pic);
                    card.Controls.Add(lblName);
                    card.Controls.Add(lblPrice);
                    card.Controls.Add(btnAdd);

                    flpMenu.Controls.Add(card);
                }
                dr.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void AddToOrder(int itemId, string itemName, decimal price)
        {
            // Check if item already exists in order
            foreach (DataRow row in orderDetailsTable.Rows)
            {
                if (Convert.ToInt32(row["ItemID"]) == itemId)
                {
                    row["Quantity"] = Convert.ToInt32(row["Quantity"]) + 1;
                    row["Subtotal"] = Convert.ToDecimal(row["Quantity"]) * price;
                    UpdateTotalAmount();
                    return;
                }
            }

            orderDetailsTable.Rows.Add(itemId, itemName, 1, price, price);
            UpdateTotalAmount();
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
            if (cmbTables.SelectedValue == null)
            {
                MessageBox.Show("Please select a table.");
                return;
            }

            try
            {
                con.Open();
                MySqlTransaction trans = con.BeginTransaction();
                try
                {
                    MySqlCommand cmdOrder = new MySqlCommand(
                        "INSERT INTO Orders (CustomerID, TableID, TotalAmount, Status) VALUES (@cid, @tid, @total, @status); SELECT LAST_INSERT_ID();", con, trans);
                    
                    cmdOrder.Parameters.AddWithValue("@cid", cmbCustomers.SelectedValue ?? DBNull.Value);
                    cmdOrder.Parameters.AddWithValue("@tid", cmbTables.SelectedValue);
                    cmdOrder.Parameters.AddWithValue("@total", totalAmount);
                    cmdOrder.Parameters.AddWithValue("@status", "Pending");

                    int orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());

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
                finally { con.Close(); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}