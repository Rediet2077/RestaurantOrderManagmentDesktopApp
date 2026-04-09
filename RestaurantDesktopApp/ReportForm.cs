using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class ReportForm : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        private Button btnExport;

        public ReportForm()
        {
            InitializeComponent();
            SetupAdvancedFeatures();
            LoadStats();
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

        private void LoadStats()
        {
            try
            {
                con.Open();

                // Revenue
                string revQuery = "SELECT SUM(TotalAmount) FROM Orders WHERE Status='Paid'";
                MySqlCommand revCmd = new MySqlCommand(revQuery, con);
                object rev = revCmd.ExecuteScalar();
                lblRevenueVal.Text = (rev != DBNull.Value && rev != null) ? $"{UIHelper.GetCurrencySymbol()} {Convert.ToDecimal(rev):N2}" : $"{UIHelper.GetCurrencySymbol()} 0.00";

                // Orders
                string ordQuery = "SELECT COUNT(*) FROM Orders";
                MySqlCommand ordCmd = new MySqlCommand(ordQuery, con);
                lblOrdersVal.Text = ordCmd.ExecuteScalar()?.ToString() ?? "0";
 
                // Active Tables
                string tblQuery = "SELECT COUNT(*) FROM Tables WHERE Status='Occupied'";
                MySqlCommand tblCmd = new MySqlCommand(tblQuery, con);
                lblTablesVal.Text = tblCmd.ExecuteScalar()?.ToString() ?? "0";
 
                // Pending Orders
                string pendQuery = "SELECT COUNT(*) FROM Orders WHERE Status='Pending'";
                MySqlCommand pendCmd = new MySqlCommand(pendQuery, con);
                lblPendingVal.Text = pendCmd.ExecuteScalar()?.ToString() ?? "0";

                con.Close();

                // Rounded cards
                UIHelper.SetRoundedRegion(cardRevenue, 15);
                UIHelper.SetRoundedRegion(cardOrders, 15);
                UIHelper.SetRoundedRegion(cardTables, 15);
                UIHelper.SetRoundedRegion(cardPending, 15);
            }
            catch (Exception ex)
            {
                con.Close();
                Console.WriteLine("Stats load error: " + ex.Message);
            }
        }

        private void LoadDailyReport()
        {
            try
            {
                // Summary of sales by date (default to today)
                string query = @"
                    SELECT 
                        DATE(OrderDate) as Date, 
                        COUNT(OrderID) as TotalOrders, 
                        SUM(TotalAmount) as TotalSales 
                    FROM Orders 
                    WHERE Status = 'Paid' 
                    AND DATE(OrderDate) = CURDATE()
                    GROUP BY DATE(OrderDate)";

                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStats();
            LoadDailyReport();
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
