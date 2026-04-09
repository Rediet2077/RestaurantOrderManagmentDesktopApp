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

        private TextBox txtSearch;
        private Button btnExport;
        private PictureBox picPreview;

        public Menu_Form()
        {
            InitializeComponent();
            SetupAdvancedFeatures();
            dgvMenuItems.CellClick += dgvMenuItems_CellClick;
            LoadMenuItems();
            ApplyInteractivity();
        }

        private void SetupAdvancedFeatures()
        {
            // Search Bar
            txtSearch = new TextBox();
            txtSearch.Size = new Size(200, 25);
            txtSearch.Location = new Point(dgvMenuItems.Left + dgvMenuItems.Width - 200, 185);
            txtSearch.PlaceholderText = "Search menu...";
            txtSearch.TextChanged += (s, e) => FilterMenu();
            this.Controls.Add(txtSearch);

            Label lblSearch = new Label();
            lblSearch.Text = "🔍 Search:";
            lblSearch.Location = new Point(txtSearch.Left - 70, 188);
            lblSearch.AutoSize = true;
            // Export Button
            btnExport = new Button();
            btnExport.Text = "Export to CSV";
            btnExport.Size = new Size(120, 30);
            btnExport.Location = new Point(dgvMenuItems.Left, 182);
            btnExport.Click += (s, e) => UIHelper.ExportToCSV(dgvMenuItems, "Menu_Export_" + DateTime.Now.ToString("yyyyMMdd"));
            this.Controls.Add(btnExport);

            // Image Preview
            picPreview = new PictureBox();
            picPreview.Size = new Size(150, 150);
            picPreview.Location = new Point(450, 71);
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.BackColor = Color.White;
            this.Controls.Add(picPreview);

            UIHelper.ApplyModernButton(btnExport, UIHelper.SuccessColor);
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
                string filter = $"Name LIKE '%{txtSearch.Text}%' OR Category LIKE '%{txtSearch.Text}%'";
                dt.DefaultView.RowFilter = filter;
            }
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

                UIHelper.ShowToast("Item Added Successfully");

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
                    var cellVal = dgvMenuItems.SelectedRows[0].Cells["ItemID"].Value;
                    if (cellVal == null || cellVal == DBNull.Value) return;
                    int id = Convert.ToInt32(cellVal);
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
                    UIHelper.ShowToast("Item Updated Successfully");
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
                    var cellVal = dgvMenuItems.SelectedRows[0].Cells["ItemID"].Value;
                    if (cellVal == null || cellVal == DBNull.Value) return;
                    int id = Convert.ToInt32(cellVal);
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
                    string targetPath = Path.Combine(Application.StartupPath, @"..\..\..\Resources", fileName);
                    
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
                txtItemName.Text = row.Cells["Name"].Value?.ToString() ?? "";
                txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
                txtCategory.Text = row.Cells["Category"].Value?.ToString() ?? "";
                txtImagePath.Text = row.Cells["ImagePath"].Value?.ToString() ?? "";
                
                string path = txtImagePath.Text;
                if (!string.IsNullOrEmpty(path))
                {
                    try {
                        string fullPath = Path.Combine(Application.StartupPath, path.Replace("/", "\\"));
                        if (File.Exists(fullPath)) picPreview.Image = Image.FromFile(fullPath);
                        else picPreview.Image = null;
                    } catch { picPreview.Image = null; }
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