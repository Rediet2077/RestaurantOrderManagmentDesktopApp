using System;
using System.Windows.Forms;

namespace RestaurantDesktopApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            UIHelper.LoadGlobalSettings();
            Application.Run(new LandingForm());
        }
    }
}