using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RestaurantDesktopApp
{
    public static class UIHelper
    {
        // Premium Color Palette
        public static Color PrimaryColor = Color.FromArgb(44, 62, 80);    // Deep Navy
        public static Color AccentColor = Color.FromArgb(41, 128, 185);   // Bright Blue
        public static Color SuccessColor = Color.FromArgb(39, 174, 96);  // Emerald
        public static Color WarningColor = Color.FromArgb(243, 156, 18); // Orange
        public static Color DangerColor = Color.FromArgb(192, 57, 43);   // Soft Red
        public static Color BackgroundColor = Color.FromArgb(236, 240, 241); // Off-White
        public static Color SidebarColor = Color.FromArgb(33, 47, 61);    // Darker Navy
        public static Color ControlColor = Color.FromArgb(52, 73, 94);   // Gray Blue

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public static void SetRoundedRegion(Control ctrl, int radius)
        {
            ctrl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width, ctrl.Height, radius, radius));
        }

        public static void ApplyModernButton(Button btn, Color hoverColor)
        {
            Color originalColor = btn.BackColor;
            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = originalColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        public static string GetGreeting()
        {
            int hour = DateTime.Now.Hour;
            if (hour < 12) return "Good Morning";
            if (hour < 17) return "Good Afternoon";
            return "Good Evening";
        }
    }
}
