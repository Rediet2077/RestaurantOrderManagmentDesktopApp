using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace RestaurantDesktopApp
{
    public static class UIHelper
    {
        // Premium Color Palette
        public static Color PrimaryColor = Color.FromArgb(41, 128, 185);    // Modern Blue
        public static Color SecondaryColor = Color.FromArgb(52, 152, 219);  // Light Blue
        public static Color DarkSidebar = Color.FromArgb(28, 40, 51);      // Deep Slate
        public static Color SuccessColor = Color.FromArgb(39, 174, 96);    // Emerald
        public static Color DangerColor = Color.FromArgb(231, 76, 60);     // Alizarin Red
        public static Color AccentColor = Color.FromArgb(155, 89, 182);    // Amethyst
        public static Color BackgroundColor = Color.FromArgb(244, 247, 249); // Clean White/Gray
        public static Color SidebarColor = Color.FromArgb(44, 62, 80);     // Wet Asphalt
        public static Color ControlColor = Color.FromArgb(52, 73, 94);    // Gray Blue

        // Dark Mode Colors
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
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    csvData += col.HeaderText + ",";
                }
                csvData += "\r\n";

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
            RestaurantName = "BEST RESTAURANTS";
            Currency = "Birr (ETB)";
            IsDarkMode = false;

            _ = Task.Run(async () =>
            {
                try
                {
                    var settings = await ApiClient.GetSettingsAsync();
                    if (settings != null)
                    {
                        RestaurantName = settings.RestaurantName;
                        Currency = settings.Currency;
                        IsDarkMode = (settings.DarkMode == "True");
                    }
                }
                catch { }
            });
        }

        private static ConcurrentDictionary<string, Image> _imageCache = new();

        public static async Task LoadImageAsync(PictureBox pic, string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            if (_imageCache.TryGetValue(url, out Image? cachedImg))
            {
                pic.Image = cachedImg;
                return;
            }

            try
            {
                if (url.StartsWith("http"))
                {
                    using var client = new System.Net.Http.HttpClient();
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                    var bytes = await client.GetByteArrayAsync(url);
                    using var ms = new System.IO.MemoryStream(bytes);
                    using var rawImg = Image.FromStream(ms);
                    
                    // Create a copy that doesn't depend on the memory stream
                    var img = new Bitmap(rawImg);
                    _imageCache[url] = img;
                    
                    if (pic.InvokeRequired)
                        pic.Invoke(new Action(() => { pic.Image = img; pic.Refresh(); pic.Update(); }));
                    else
                    {
                        pic.Image = img;
                        pic.Refresh();
                        pic.Update();
                    }
                }
                else
                {
                    string fullPath = System.IO.Path.Combine(Application.StartupPath, url.Replace("/", "\\"));
                    if (System.IO.File.Exists(fullPath))
                    {
                        var img = Image.FromFile(fullPath);
                        _imageCache[url] = img;
                        pic.Image = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Image load error for {url}: {ex.Message}");
                if (pic.InvokeRequired)
                    pic.Invoke(new Action(() => pic.BackColor = Color.FromArgb(235, 235, 235)));
                else
                    pic.BackColor = Color.FromArgb(235, 235, 235);
            }
        }
    }
}
