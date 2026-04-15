using System;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    public class MockUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    internal static class Program
    {
        public static bool IsLoggedIn = false;
        public static MockUser CurrentUser = null;
        public static System.Collections.Generic.List<MockUser> RegisteredUsers = new System.Collections.Generic.List<MockUser>();

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            UIHelper.LoadGlobalSettings();
            Application.Run(new LandingForm());
        }
    }
}