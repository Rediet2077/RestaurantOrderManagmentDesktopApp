using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
        }

        private async void StaffForm_Load(object sender, EventArgs e)
        {
            await LoadStaffAsync();
            UIHelper.ApplyModernButton(btnAdd, UIHelper.SuccessColor);
            UIHelper.ApplyModernButton(btnDelete, UIHelper.DangerColor);
            UIHelper.ApplyModernButton(btnUpdate, UIHelper.AccentColor);
        }

        private async System.Threading.Tasks.Task LoadStaffAsync()
        {
            try
            {
                DataTable dt = await ApiClient.GetStaffTableAsync();
                dgvStaff.DataSource = dt;
            }
            catch (Exception ex)
            {
                UIHelper.ShowToast("Error loading staff: " + ex.Message, true);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                UIHelper.ShowToast("Please fill all fields.", true);
                return;
            }

            try
            {
                string role = cmbRole.SelectedItem?.ToString() ?? "Staff";
                bool success = await ApiClient.AddStaffAsync(txtUsername.Text, txtPassword.Text, role);
                if (success)
                {
                    UIHelper.ShowToast("Staff added successfully!");
                    await LoadStaffAsync();
                    ClearFields();
                }
                else
                {
                    UIHelper.ShowToast("Error adding staff.", true);
                }
            }
            catch (Exception ex)
            {
                UIHelper.ShowToast("Error adding staff: " + ex.Message, true);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count > 0)
            {
                var val = dgvStaff.SelectedRows[0].Cells["Username"].Value;
                if (val == null) return;

                string user = val?.ToString() ?? "";
                if (string.IsNullOrEmpty(user) || user == "admin")
                {
                    UIHelper.ShowToast("Cannot delete admin.", true);
                    return;
                }

                try
                {
                    bool success = await ApiClient.DeleteStaffAsync(user);
                    if (success)
                    {
                        UIHelper.ShowToast("Staff removed.");
                        await LoadStaffAsync();
                    }
                    else
                    {
                        UIHelper.ShowToast("Error deleting staff.", true);
                    }
                }
                catch (Exception ex)
                {
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
