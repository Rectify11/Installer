using Rectify11Installer.Core;
using Rectify11Installer.Win32;
using System;
using System.Globalization;
using System.IO;
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
            {
                Theme.InitTheme();
                if (Environment.OSVersion.Version.Build >= 17763)
                {
                    DarkMode.AllowDarkModeForApp(true);
                }
                else if(Environment.OSVersion.Version.Build >= 18362)
                {
                    DarkMode.SetPreferredAppMode(DarkMode.PreferredAppMode.AllowDark);
                }
            }
            if (!Directory.Exists(Variables.r11Folder))
                Directory.CreateDirectory(Variables.r11Folder);
            if ((!File.Exists(Path.Combine(Variables.r11Folder, "Dark.msstyles"))) && (!File.Exists(Path.Combine(Variables.r11Folder, "light.msstyles"))))
            {
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Dark.msstyles"), Properties.Resources.Dark);
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "light.msstyles"), Properties.Resources.light);
            }
            Theme.LoadTheme();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh");
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentUICulture;
            Application.Run(new frmWizard());
        }
    }
}
