using System;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    /// <summary>
    /// Holds the current logged-in user information, retrieved from the API.
    /// </summary>
    public class AppUser
    {
        public int UserID { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Token { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }

    internal static class Program
    {
        public static bool IsLoggedIn = false;
        public static AppUser? CurrentUser = null;

        [STAThread]
        static void Main()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                UIHelper.LoadGlobalSettings();
                Application.Run(new LandingForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show("CRITICAL ERROR: " + ex.Message + "\n" + ex.StackTrace, "Startup Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}