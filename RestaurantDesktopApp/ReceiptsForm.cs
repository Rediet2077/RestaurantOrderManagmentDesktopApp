using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class ReceiptsForm : Form
    {
        private DataGridView dgvReceipts = null!;
        private RichTextBox rtbReceiptContent = null!;
        private Label lblTitle = null!;
        private Panel leftPanel = null!;

        public ReceiptsForm()
        {
            InitializeComponent();
            SetupManualLayout();
            _ = LoadReceiptsAsync();
        }

        private void InitializeComponent()
        {
            this.Text = "Customer Receipts";
            this.Size = new Size(900, 600);
            this.BackColor = Color.White;
        }

        private void SetupManualLayout()
        {
            lblTitle = new Label
            {
                Text = "SENT RECEIPTS LOG",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            leftPanel = new Panel
            {
                Location = new Point(20, 70),
                Size = new Size(400, 480),
                BorderStyle = BorderStyle.None
            };
            UIHelper.SetRoundedRegion(leftPanel, 15);
            this.Controls.Add(leftPanel);

            dgvReceipts = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 10)
            };
            dgvReceipts.SelectionChanged += DgvReceipts_SelectionChanged;
            leftPanel.Controls.Add(dgvReceipts);

            rtbReceiptContent = new RichTextBox
            {
                Location = new Point(440, 70),
                Size = new Size(420, 480),
                Font = new Font("Consolas", 11),
                ReadOnly = true,
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.None,
                Padding = new Padding(10)
            };
            UIHelper.SetRoundedRegion(rtbReceiptContent, 15);
            this.Controls.Add(rtbReceiptContent);
        }

        private async System.Threading.Tasks.Task LoadReceiptsAsync()
        {
            try
            {
                DataTable dt = await ApiClient.GetReceiptsTableAsync();
                dgvReceipts.DataSource = dt;
                
                if (dgvReceipts.Columns.Contains("ReceiptContent"))
                    dgvReceipts.Columns["ReceiptContent"]!.Visible = false;
            }
            catch (Exception ex)
            {
                UIHelper.ShowToast("Failed to load receipts: " + ex.Message, true);
            }
        }

        private void DgvReceipts_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvReceipts.SelectedRows.Count > 0)
            {
                var content = dgvReceipts.SelectedRows[0].Cells["ReceiptContent"].Value?.ToString();
                rtbReceiptContent.Text = content ?? "No content available.";
            }
        }
    }
}
