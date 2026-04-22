using System;
using System.Data;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class TableForm : Form
    {
        public TableForm()
        {
            InitializeComponent();
            _ = LoadTablesAsync();
        }

        private async System.Threading.Tasks.Task LoadTablesAsync()
        {
            try
            {
                DataTable dt = await ApiClient.GetTablesTableAsync();
                dgvTables.DataSource = dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0 && cmbStatus.SelectedItem != null)
            {
                int tableId = Convert.ToInt32(dgvTables.SelectedRows[0].Cells["TableID"].Value);
                string newStatus = cmbStatus.SelectedItem?.ToString() ?? "Available";

                bool success = await ApiClient.UpdateTableStatusAsync(tableId, newStatus);
                if (success)
                {
                    UIHelper.ShowToast("Table status updated!");
                    await LoadTablesAsync();
                }
                else
                {
                    MessageBox.Show("Failed to update table status.");
                }
            }
        }
    }
}
