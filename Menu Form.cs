using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace RestaurantDesktopApp
{
    public partial class Menu_Form : Form
    {
        MySqlConnection con = new MySqlConnection(
        "server=localhost;user=root;password=;database=RestaurantDB");

        public Menu_Form()
        {
            InitializeComponent();
            dgvMenuItems.CellClick += dgvMenuItems_CellClick;
            LoadMenuItems();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                MySqlCommand cmd = new MySqlCommand(
                "INSERT INTO MenuItems(Name,Price,Category,ImagePath) VALUES(@n,@p,@c,@img)", con);

                cmd.Parameters.AddWithValue("@n", txtItemName.Text);
                cmd.Parameters.AddWithValue("@p", txtPrice.Text);
                cmd.Parameters.AddWithValue("@c", txtCategory.Text);
                cmd.Parameters.AddWithValue("@img", txtImagePath.Text);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Item Added Successfully");

                ClearFields();
                LoadMenuItems();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (dgvMenuItems.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dgvMenuItems.SelectedRows[0].Cells["ItemID"].Value);
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE MenuItems SET Name=@n, Price=@p, Category=@c, ImagePath=@img WHERE ItemID=@id", con);
                    cmd.Parameters.AddWithValue("@n", txtItemName.Text);
                    cmd.Parameters.AddWithValue("@p", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@c", txtCategory.Text);
                    cmd.Parameters.AddWithValue("@img", txtImagePath.Text);
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Item Updated Successfully");
                    ClearFields();
                    LoadMenuItems();
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to update.");
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvMenuItems.SelectedRows.Count > 0)
            {
                try
                {
                    int id = Convert.ToInt32(dgvMenuItems.SelectedRows[0].Cells["ItemID"].Value);
                    if (MessageBox.Show("Are you sure you want to delete this item?", "Delete Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM MenuItems WHERE ItemID=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Item Deleted Successfully");
                        ClearFields();
                        LoadMenuItems();
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // Copy file to Resources if it's not already there
                    string fileName = Path.GetFileName(ofd.FileName);
                    string targetPath = Path.Combine(Application.StartupPath, @"..\..\Resources", fileName);
                    
                    try {
                        if (!File.Exists(targetPath))
                            File.Copy(ofd.FileName, targetPath);
                        
                        txtImagePath.Text = "Resources/" + fileName;
                    } catch (Exception ex) {
                        MessageBox.Show("Error copying image: " + ex.Message);
                    }
                }
            }
        }

        private void dgvMenuItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMenuItems.Rows[e.RowIndex];
                txtItemName.Text = row.Cells["Name"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtCategory.Text = row.Cells["Category"].Value.ToString();
                txtImagePath.Text = row.Cells["ImagePath"].Value.ToString();
            }
        }

        private void ClearFields()
        {
            txtItemName.Clear();
            txtPrice.Clear();
            txtCategory.Clear();
            txtImagePath.Clear();
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