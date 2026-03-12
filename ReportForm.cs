using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RestaurantDesktopApp
{
    public partial class ReportForm : Form
    {
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB");

        public ReportForm()
        {
            InitializeComponent();
            LoadDailyReport();
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

                if (dt.Rows.Count > 0)
                {
                    lblTotalSales.Text = Convert.ToDecimal(dt.Rows[0]["TotalSales"]).ToString("N2");
                }
                else
                {
                    lblTotalSales.Text = "0.00";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDailyReport();
        }
    }
}
