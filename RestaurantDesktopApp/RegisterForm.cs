using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class RegisterForm : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtUsername.Text) || 
                string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                UIHelper.ShowToast("Please fill in all fields.", true);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                UIHelper.ShowToast("Passwords do not match.", true);
                return;
            }

            try
            {
                con.Open();
                
                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username=@user";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@user", txtUsername.Text);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    con.Close();
                    MessageBox.Show("Username already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert new user
                string role = cmbRole.SelectedItem?.ToString() ?? "User";
                string insertQuery = "INSERT INTO Users (Name, Role, Username, Password) VALUES (@name, @role, @user, @pass)";
                MySqlCommand insertCmd = new MySqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@name", txtName.Text);
                insertCmd.Parameters.AddWithValue("@role", role);
                insertCmd.Parameters.AddWithValue("@user", txtUsername.Text);
                insertCmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                insertCmd.ExecuteNonQuery();
                con.Close();

                UIHelper.ShowToast("Registration Successful!");
                this.Close(); // Return to Login
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Registration error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
