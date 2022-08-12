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
            if (Environment.OSVersion.Version.Build >= 10240)
                Theme.InitTheme();
            Theme.LoadTheme();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("kr");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentUICulture;
            Application.Run(new frmWizard());
        }
    }
}
