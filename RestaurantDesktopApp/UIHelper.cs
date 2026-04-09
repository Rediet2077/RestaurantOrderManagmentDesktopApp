using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RestaurantDesktopApp
{
    public static class UIHelper
    {
        // Premium Color Palette
        // Premium Color Palette - Refined
        public static Color PrimaryColor = Color.FromArgb(41, 128, 185);    // Modern Blue
        public static Color SecondaryColor = Color.FromArgb(52, 152, 219);  // Light Blue
        public static Color DarkSidebar = Color.FromArgb(28, 40, 51);      // Deep Slate
        public static Color SuccessColor = Color.FromArgb(39, 174, 96);    // Emerald
        public static Color DangerColor = Color.FromArgb(231, 76, 60);     // Alizarin Red
        public static Color AccentColor = Color.FromArgb(155, 89, 182);    // Amethyst
        public static Color BackgroundColor = Color.FromArgb(244, 247, 249); // Clean White/Gray
        public static Color SidebarColor = Color.FromArgb(44, 62, 80);     // Wet Asphalt
        public static Color ControlColor = Color.FromArgb(52, 73, 94);    // Gray Blue

        // Dark Mode Colors - Refined
        public static Color DarkBg = Color.FromArgb(18, 24, 31);
        public static Color DarkControl = Color.FromArgb(33, 47, 60);
        public static Color DarkText = Color.FromArgb(235, 237, 239);
        public static Color DarkAccent = Color.FromArgb(46, 64, 83);

        public static bool IsDarkMode = false;
        public static string RestaurantName = "LAUNCH";
        public static string Currency = "Birr (ETB)";

        public static string GetCurrencySymbol()
        {
            if (string.IsNullOrEmpty(Currency)) return "Birr";
            return Currency.Split(' ')[0];
        }

        public static void ApplyTheme(Form form)
        {
            form.BackColor = IsDarkMode ? DarkBg : BackgroundColor;
            form.ForeColor = IsDarkMode ? DarkText : PrimaryColor;

            foreach (Control ctrl in form.Controls)
            {
                UpdateControlTheme(ctrl);
            }
        }

        private static void UpdateControlTheme(Control ctrl)
        {
            if (ctrl is Panel p)
            {
                if (p.Name.Contains("Sidebar") || p.Name.Contains("side"))
                    p.BackColor = IsDarkMode ? DarkSidebar : SidebarColor;
                else if (p.Name.Contains("card"))
                    p.BackColor = IsDarkMode ? DarkControl : Color.White;
                else
                    p.BackColor = IsDarkMode ? DarkBg : BackgroundColor;
            }
            else if (ctrl is Button btn)
            {
                if (btn.BackColor == ControlColor || btn.BackColor == DarkControl)
                    btn.BackColor = IsDarkMode ? DarkControl : ControlColor;
                btn.ForeColor = Color.White;
            }
            else if (ctrl is Label lbl)
            {
                lbl.ForeColor = IsDarkMode ? DarkText : PrimaryColor;
            }
            else if (ctrl is TextBox txt)
            {
                txt.BackColor = IsDarkMode ? DarkControl : Color.White;
                txt.ForeColor = IsDarkMode ? DarkText : PrimaryColor;
            }
            else if (ctrl is DataGridView dgv)
            {
                dgv.BackgroundColor = IsDarkMode ? DarkControl : Color.White;
                dgv.DefaultCellStyle.BackColor = IsDarkMode ? DarkBg : Color.White;
                dgv.DefaultCellStyle.ForeColor = IsDarkMode ? DarkText : Color.Black;
                dgv.ColumnHeadersDefaultCellStyle.BackColor = IsDarkMode ? DarkSidebar : Color.LightGray;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = IsDarkMode ? DarkText : Color.Black;
                dgv.EnableHeadersVisualStyles = false;
            }

            foreach (Control child in ctrl.Controls)
            {
                UpdateControlTheme(child);
            }
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public static void SetRoundedRegion(Control ctrl, int radius)
        {
            if (ctrl.Width > 0 && ctrl.Height > 0)
                ctrl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width, ctrl.Height, radius, radius));
        }

        public static void ApplyModernButton(Button btn, Color hoverColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Font = new Font("Segoe UI Semibold", 11);
            
            if (btn.BackColor == SystemColors.Control || btn.BackColor == Color.Transparent)
                btn.BackColor = IsDarkMode ? DarkControl : ControlColor;

            Color originalColor = btn.BackColor;

            btn.MouseEnter += (s, e) => {
                btn.BackColor = hoverColor;
                btn.ForeColor = Color.White;
                // Subtle lift effect
                btn.Padding = new Padding(5, 0, 0, 0); 
            };

            btn.MouseLeave += (s, e) => {
                btn.BackColor = originalColor;
                btn.Padding = new Padding(0);
            };
        }

        public static void DrawGradient(Graphics g, Rectangle rect, Color startColor, Color endColor, float angle = 45F)
        {
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, startColor, endColor, angle))
            {
                g.FillRectangle(brush, rect);
            }
        }

        public static void ApplySidebarStyle(Panel panel)
        {
            panel.Paint += (s, e) => {
                Color c1 = IsDarkMode ? DarkSidebar : SidebarColor;
                Color c2 = IsDarkMode ? Color.Black : Color.FromArgb(33, 47, 61);
                DrawGradient(e.Graphics, panel.ClientRectangle, c1, c2, 135F);
            };
        }

        public static void ApplyGlassEffect(Control ctrl)
        {
            ctrl.BackColor = Color.FromArgb(180, ctrl.BackColor);
        }

        public static void ShowToast(string message, bool isError = false)
        {
            ToastNotification toast = new ToastNotification(message, isError);
            toast.Show();
        }

        public static void ExportToCSV(DataGridView dgv, string fileName)
        {
            try
            {
                if (dgv.Rows.Count == 0) return;

                string csvData = "";
                // Add header
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    csvData += col.HeaderText + ",";
                }
                csvData += "\r\n";

                // Add rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            csvData += cell.Value?.ToString().Replace(",", " ") + ",";
                        }
                        csvData += "\r\n";
                    }
                }

                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".csv");
                System.IO.File.WriteAllText(path, csvData);
                ShowToast("Exported to Desktop successfully!");
            }
            catch (Exception ex)
            {
                ShowToast("Export failed: " + ex.Message, true);
            }
        }

        public static string GetGreeting()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 12) return "Good Morning";
            if (hour < 17) return "Good Afternoon";
            return "Good Evening";
        }

        public static void LoadGlobalSettings()
        {
            try
            {
                using (var con = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;user=root;password=;database=RestaurantDB"))
                {
                    con.Open();
                    var cmd = new MySql.Data.MySqlClient.MySqlCommand("SELECT * FROM AppSettings", con);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string key = dr["SettingKey"]?.ToString() ?? "";
                            string val = dr["SettingValue"]?.ToString() ?? "";
                            if (key == "RestaurantName") RestaurantName = val;
                            else if (key == "Currency") Currency = val;
                            else if (key == "DarkMode") IsDarkMode = (val == "True");
                        }
                    }

                    if (string.IsNullOrEmpty(RestaurantName) || RestaurantName.ToUpper().Contains("GASTRO"))
                    {
                        RestaurantName = "LAUNCH";
                        try {
                            var updateCmd = new MySql.Data.MySqlClient.MySqlCommand("UPDATE AppSettings SET SettingValue='LAUNCH' WHERE SettingKey='RestaurantName'", con);
                            updateCmd.ExecuteNonQuery();
                        } catch { }
                    }

                    if (string.IsNullOrEmpty(Currency) || Currency.Contains("$") || Currency.Contains("Br (ETB)"))
                    {
                        Currency = "Birr (ETB)";
                        try {
                            var updateCmd = new MySql.Data.MySqlClient.MySqlCommand("UPDATE AppSettings SET SettingValue='Birr (ETB)' WHERE SettingKey='Currency'", con);
                            updateCmd.ExecuteNonQuery();
                        } catch { }
                    }
                }
            }
            catch { /* Fallback to defaults */ }
        }
    }
}
