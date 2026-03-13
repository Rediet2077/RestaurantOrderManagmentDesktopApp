using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class LoginForm : Form
    {
        private MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                con.Open();
                string query = "SELECT Role FROM Users WHERE Username=@user AND Password=@pass";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", txtUsername.Text);
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text);

                object result = cmd.ExecuteScalar();
                con.Close();

                if (result != null)
                {
                    string role = result.ToString();
                    MessageBox.Show($"Login Successful! Welcome, {role}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    if (role == "Admin")
                    {
                        AdminMainForm adminForm = new AdminMainForm();
                        adminForm.Show();
                    }
                    else
                    {
                        UserMainForm userForm = new UserMainForm();
                        userForm.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
