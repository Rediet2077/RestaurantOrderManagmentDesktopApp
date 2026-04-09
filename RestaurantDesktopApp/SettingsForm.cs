using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class SettingsForm : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            UIHelper.SetRoundedRegion(panelMain, 20);
            UIHelper.ApplyModernButton(btnSave, UIHelper.SuccessColor);
            
            InitializeSettingsTable();
            LoadSettings();
            UIHelper.ApplyTheme(this);
        }

        private void InitializeSettingsTable()
        {
            try
            {
                con.Open();
                string query = @"CREATE TABLE IF NOT EXISTS AppSettings (
                                    SettingKey VARCHAR(100) PRIMARY KEY, 
                                    SettingValue VARCHAR(255)
                                );
                                INSERT IGNORE INTO AppSettings (SettingKey, SettingValue) VALUES 
                                ('RestaurantName', 'LAUNCH'), 
                                ('Currency', 'Birr (ETB)'), 
                                ('DarkMode', 'False');";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show("Init Error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void LoadSettings()
        {
            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM AppSettings", con);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string key = dr["SettingKey"].ToString();
                        string val = dr["SettingValue"].ToString();

                        if (key == "RestaurantName") txtRestName.Text = val;
                        else if (key == "Currency") cmbCurrency.SelectedItem = val;
                        else if (key == "DarkMode") chkDarkMode.Checked = (val == "True");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Load Error: " + ex.Message); }
            finally { con.Close(); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UIHelper.IsDarkMode = chkDarkMode.Checked;
                UIHelper.RestaurantName = txtRestName.Text;
                UIHelper.Currency = cmbCurrency.SelectedItem?.ToString() ?? "Birr (ETB)";

                con.Open();
                string query = @"UPDATE AppSettings SET SettingValue = @name WHERE SettingKey = 'RestaurantName';
                                UPDATE AppSettings SET SettingValue = @curr WHERE SettingKey = 'Currency';
                                UPDATE AppSettings SET SettingValue = @dark WHERE SettingKey = 'DarkMode';";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", UIHelper.RestaurantName);
                cmd.Parameters.AddWithValue("@curr", UIHelper.Currency);
                cmd.Parameters.AddWithValue("@dark", UIHelper.IsDarkMode.ToString());
                
                cmd.ExecuteNonQuery();
                
                // Apply theme to current form
                UIHelper.ApplyTheme(this);
                
                // Find top level form and apply theme
                Form parent = this.FindForm();
                if (parent != null)
                {
                    UIHelper.ApplyTheme(parent);
                    // If it has ApplyStyles method (like AdminMainForm/UserMainForm), call it
                    var method = parent.GetType().GetMethod("ApplyStyles", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
                    if (method != null) method.Invoke(parent, null);
                }

                UIHelper.ShowToast("Settings saved successfully!");
            }
            catch (Exception ex) { MessageBox.Show("Save Error: " + ex.Message); }
            finally { con.Close(); }
        }
    }
}
