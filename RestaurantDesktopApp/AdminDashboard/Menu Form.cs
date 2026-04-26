using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace RestaurantDesktopApp
{
    public partial class Menu_Form : Form
    {
        private TextBox txtSearch = null!;
        private Button btnExport = null!;
        private PictureBox picPreview = null!;

        public Menu_Form()
        {
            InitializeComponent();
            SetupAdvancedFeatures();
            dgvMenuItems.CellClick += dgvMenuItems_CellClick;
            _ = LoadMenuItemsAsync();
            ApplyInteractivity();
        }
        private void SetupAdvancedFeatures()
        {
            // Search Bar
            txtSearch = new TextBox();
            txtSearch.Size = new Size(200, 25);
            txtSearch.Location = new Point(550, 220); // Moved down below inputs
            txtSearch.PlaceholderText = "Search menu...";
            txtSearch.TextChanged += (s, e) => FilterMenu();
            this.Controls.Add(txtSearch);

            Label lblSearch = new Label();
            lblSearch.Text = "🔍 Search:";
            lblSearch.Location = new Point(470, 223); // Moved down below inputs
            lblSearch.AutoSize = true;
            this.Controls.Add(lblSearch);

            // Export Button
            btnExport = new Button();
            btnExport.Text = "Export to CSV";
            btnExport.Size = new Size(120, 30);
            btnExport.Location = new Point(30, 220); // Moved down below inputs
            btnExport.Click += (s, e) => UIHelper.ExportToCSV(dgvMenuItems, "Menu_Export_" + DateTime.Now.ToString("yyyyMMdd"));
            this.Controls.Add(btnExport);

            // Image Preview (Restored)
            picPreview = new PictureBox();
            picPreview.Size = new Size(150, 150);
            picPreview.Location = new Point(450, 60); // Placed safely on the right
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.BackColor = Color.White;
            this.Controls.Add(picPreview);

            UIHelper.ApplyModernButton(btnExport, UIHelper.SuccessColor);

            if (this.Controls["headerPanel"] is Panel header)
            {
                header.Visible = false;
            }
            
            // Adjust DataGridView location
            dgvMenuItems.Location = new Point(30, 260);
            dgvMenuItems.Size = new Size(720, 240);
            dgvMenuItems.Visible = true;
        }

        private void ApplyInteractivity()
        {
            UIHelper.ApplyModernButton(btnAddItem, UIHelper.SuccessColor);
            UIHelper.ApplyModernButton(btnUpdateItem, UIHelper.AccentColor);
            UIHelper.ApplyModernButton(btnDeleteItem, UIHelper.DangerColor);
            UIHelper.ApplyModernButton(btnBrowse, UIHelper.ControlColor);
        }

        private void FilterMenu()
        {
            if (dgvMenuItems.DataSource is DataTable dt)
            {
                string searchText = txtSearch.Text.Replace("'", "''");
                string filter = $"Name LIKE '%{searchText}%' OR Category LIKE '%{searchText}%'";
                dt.DefaultView.RowFilter = filter;
            }
        }

        private async void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool success = await ApiClient.AddMenuItemAsync(
                    txtItemName.Text, txtPrice.Text, txtCategory.Text, txtImagePath.Text);

                if (success)
                {
                    UIHelper.ShowToast("Item Added Successfully");
                    ClearFields();
                    await LoadMenuItemsAsync();
                }
                else
                {
                    MessageBox.Show("Failed to add item.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (dgvMenuItems.SelectedRows.Count > 0)
            {
                try
                {
                    var cellVal = dgvMenuItems.SelectedRows[0].Cells["ItemID"].Value;
                    if (cellVal == null || cellVal == DBNull.Value) return;
                    int id = Convert.ToInt32(cellVal);

                    bool success = await ApiClient.UpdateMenuItemAsync(
                        id, txtItemName.Text, txtPrice.Text, txtCategory.Text, txtImagePath.Text);

                    if (success)
                    {
                        UIHelper.ShowToast("Item Updated Successfully");
                        ClearFields();
                        await LoadMenuItemsAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update item.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to update.");
            }
        }

        private async void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvMenuItems.SelectedRows.Count > 0)
            {
                try
                {
                    var cellVal = dgvMenuItems.SelectedRows[0].Cells["ItemID"].Value;
                    if (cellVal == null || cellVal == DBNull.Value) return;
                    int id = Convert.ToInt32(cellVal);

                    if (MessageBox.Show("Are you sure you want to delete this item?", "Delete Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bool success = await ApiClient.DeleteMenuItemAsync(id);
                        if (success)
                        {
                            MessageBox.Show("Item Deleted Successfully");
                            ClearFields();
                            await LoadMenuItemsAsync();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete item.");
                        }
                    }
                }
                catch (Exception ex)
                {
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
                    string fileName = Path.GetFileName(ofd.FileName);
                    string targetPath = Path.Combine(Application.StartupPath, @"..\..\..\Resources", fileName);

                    try
                    {
                        if (!File.Exists(targetPath))
                            File.Copy(ofd.FileName, targetPath);

                        txtImagePath.Text = "Resources/" + fileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error copying image: " + ex.Message);
                    }
                }
            }
        }

        private void dgvMenuItems_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMenuItems.Rows[e.RowIndex];
                txtItemName.Text = row.Cells["Name"].Value?.ToString() ?? "";
                txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
                txtCategory.Text = row.Cells["Category"].Value?.ToString() ?? "";
                txtImagePath.Text = row.Cells["ImagePath"].Value?.ToString() ?? "";

                string path = txtImagePath.Text;
                if (!string.IsNullOrEmpty(path))
                {
                    try
                    {
                        string fullPath = Path.Combine(Application.StartupPath, path.Replace("/", "\\"));
                        if (File.Exists(fullPath)) picPreview.Image = Image.FromFile(fullPath);
                        else picPreview.Image = null;
                    }
                    catch { picPreview.Image = null; }
                }
                else picPreview.Image = null;
            }
        }

        private void ClearFields()
        {
            txtItemName.Clear();
            txtPrice.Clear();
            txtCategory.Clear();
            txtImagePath.Clear();
            picPreview.Image = null;
        }

        private async System.Threading.Tasks.Task LoadMenuItemsAsync()
        {
            try
            {
                DataTable dt = await ApiClient.GetMenuItemsTableAsync();
                dgvMenuItems.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}