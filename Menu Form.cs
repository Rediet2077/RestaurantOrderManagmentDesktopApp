using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class Menu_Form : Form
    {
        MySqlConnection con = new MySqlConnection(
        "server=localhost;user=root;password=;database=RestaurantDB");

        public Menu_Form()
        {
            InitializeComponent();
            LoadMenuItems();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO MenuItems(Name,Price,Category) VALUES(@n,@p,@c)", con);

                cmd.Parameters.AddWithValue("@n", txtItemName.Text);
                cmd.Parameters.AddWithValue("@p", txtPrice.Text);
                cmd.Parameters.AddWithValue("@c", txtCategory.Text);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Item Added Successfully");

                LoadMenuItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadMenuItems()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(
                "SELECT * FROM MenuItems", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvMenuItems.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}