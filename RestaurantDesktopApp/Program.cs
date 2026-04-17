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
        public string Token { get; set; } = "";
    }

    internal static class Program
    {
        public static bool IsLoggedIn = false;
        public static AppUser? CurrentUser = null;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            UIHelper.LoadGlobalSettings();
            Application.Run(new LandingForm());
        }
    }
}