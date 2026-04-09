using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class StaffForm : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public StaffForm()
        {
            InitializeComponent();
        }

        private void StaffForm_Load(object sender, EventArgs e)
        {
            LoadStaff();
            UIHelper.ApplyModernButton(btnAdd, UIHelper.SuccessColor);
            UIHelper.ApplyModernButton(btnDelete, UIHelper.DangerColor);
            UIHelper.ApplyModernButton(btnUpdate, UIHelper.AccentColor);
        }

        private void LoadStaff()
        {
            try
            {
                con.Open();
                string query = "SELECT Username, Role FROM Users";
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvStaff.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                UIHelper.ShowToast("Error loading staff: " + ex.Message, true);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                UIHelper.ShowToast("Please fill all fields.", true);
                return;
            }

            try
            {
                con.Open();
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@user, @pass, @role)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);
                string role = cmbRole.SelectedItem?.ToString() ?? "Staff";
                cmd.Parameters.AddWithValue("@role", role);
                cmd.ExecuteNonQuery();
                con.Close();
                UIHelper.ShowToast("Staff added successfully!");
                LoadStaff();
                ClearFields();
            }
            catch (Exception ex)
            {
                con.Close();
                UIHelper.ShowToast("Error adding staff: " + ex.Message, true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count > 0)
            {
                var val = dgvStaff.SelectedRows[0].Cells["Username"].Value;
                if (val == null) return;
                
                string user = val.ToString();
                if (user == "admin") 
                {
                    UIHelper.ShowToast("Cannot delete admin.", true);
                    return;
                }

                try
                {
                    con.Open();
                    string query = "DELETE FROM Users WHERE Username=@user";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    UIHelper.ShowToast("Staff removed.");
                    LoadStaff();
                }
                catch (Exception ex)
                {
                    con.Close();
                    UIHelper.ShowToast("Error deleting staff: " + ex.Message, true);
                }
            }
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = 0;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select a user to update is not implemented yet in this basic version, but the logic is ready.", "Info");
        }
    }
}
