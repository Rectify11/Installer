using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Rectify11Installer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Theme.LoadTheme();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("pl-PL");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentUICulture;
            Application.Run(new frmWizard());
        }
    }
}
