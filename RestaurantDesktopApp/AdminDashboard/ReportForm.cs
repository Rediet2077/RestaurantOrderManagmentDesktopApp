using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public partial class ReportForm : Form
    {
        private Button btnExport;

        public ReportForm()
        {
            InitializeComponent();
            SetupAdvancedFeatures();
            _ = LoadStatsAsync();
            lblTotalSales.Text = "Click View to load";
            dgvReport.DataSource = null;
        }

        private void SetupAdvancedFeatures()
        {
            btnExport = new Button();
            btnExport.Text = "Export Report";
            btnExport.Size = new Size(130, 35);
            btnExport.Location = new Point(btnRefresh.Left - 140, btnRefresh.Top);
            btnExport.Click += (s, e) => UIHelper.ExportToCSV(dgvReport, "Sales_Report_" + DateTime.Now.ToString("yyyyMMdd"));
            this.Controls.Add(btnExport);

            UIHelper.ApplyModernButton(btnExport, UIHelper.SuccessColor);
            UIHelper.ApplyModernButton(btnRefresh, UIHelper.AccentColor);
        }

        private async System.Threading.Tasks.Task LoadStatsAsync()
        {
            try
            {
                var stats = await ApiClient.GetStatsAsync();
                if (stats != null)
                {
                    lblRevenueVal.Text = $"{UIHelper.GetCurrencySymbol()} {stats.TotalRevenue}";
                    lblOrdersVal.Text = stats.TotalOrders.ToString();
                    lblTablesVal.Text = (stats.TotalTables - stats.AvailableTables).ToString();
                    lblPendingVal.Text = stats.PendingOrders.ToString();
                }
                else
                {
                    lblRevenueVal.Text = $"{UIHelper.GetCurrencySymbol()} 0.00";
                    lblOrdersVal.Text = "0";
                    lblTablesVal.Text = "0";
                    lblPendingVal.Text = "0";
                }

                // Rounded cards
                UIHelper.SetRoundedRegion(cardRevenue, 15);
                UIHelper.SetRoundedRegion(cardOrders, 15);
                UIHelper.SetRoundedRegion(cardTables, 15);
                UIHelper.SetRoundedRegion(cardPending, 15);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Stats load error: " + ex.Message);
            }
        }

        private async System.Threading.Tasks.Task LoadDailyReportAsync()
        {
            try
            {
                DataTable dt = await ApiClient.GetDailyReportTableAsync();
                dgvReport.DataSource = dt;

                if (dgvReport.Columns.Contains("TotalSales"))
                {
                    dgvReport.Columns["TotalSales"].DefaultCellStyle.Format = UIHelper.GetCurrencySymbol() + " #,##0.00";
                }

                if (dt.Rows.Count > 0 && dt.Rows[0]["TotalSales"] != DBNull.Value)
                {
                    lblTotalSales.Text = UIHelper.GetCurrencySymbol() + " " + Convert.ToDecimal(dt.Rows[0]["TotalSales"]).ToString("N2");
                }
                else
                {
                    lblTotalSales.Text = UIHelper.GetCurrencySymbol() + " 0.00";
                }
            }
            catch (Exception ex) { UIHelper.ShowToast("Load failed: " + ex.Message, true); }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadStatsAsync();
            await LoadDailyReportAsync();
        }

        private void dgvReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cardRevenue_Paint(object sender, PaintEventArgs e)
        {
            // Advanced: Draw a mini trend line or background bars
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen p = new Pen(Color.FromArgb(50, UIHelper.SuccessColor), 2);
            int[] values = { 20, 40, 35, 60, 50, 80, 70 }; // Mock daily trend
            int xStep = cardRevenue.Width / (values.Length + 1);
            
            for (int i = 0; i < values.Length - 1; i++)
            {
                g.DrawLine(p, 
                    xStep * (i + 1), cardRevenue.Height - 10, 
                    xStep * (i + 1), cardRevenue.Height - values[i]);
            }
        }
    }
}
