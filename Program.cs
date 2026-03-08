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
            Application.Run(new Menu_Form());
        }
    }
}