using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class TableForm : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public TableForm()
        {
            InitializeComponent();
            LoadTables();
        }

        private void LoadTables()
        {
            try
            {
                 MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM Tables", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTables.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0 && cmbStatus.SelectedItem != null)
            {
                int tableId = Convert.ToInt32(dgvTables.SelectedRows[0].Cells["TableID"].Value);
                string newStatus = cmbStatus.SelectedItem?.ToString() ?? "Available";

                try
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE Tables SET Status = @status WHERE TableID = @tid", con);
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@tid", tableId);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    UIHelper.ShowToast("Table status updated!");
                    LoadTables();
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
