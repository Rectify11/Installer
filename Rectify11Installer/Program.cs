using KPreisser.UI;
using Rectify11Installer.Core;
using Rectify11Installer.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Threading;
using System.Windows.Forms;

namespace Rectify11Installer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            // check if another instance is running
            using var mutex = new Mutex(false, "Rectify11Setup");
            bool isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero);
            if (isAnotherInstanceOpen)
            {
                MessageBox.Show("Another instance of the rectify11 setup is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool skipVersionCheck = false;
            foreach (var arg in args)
            {
                if (arg == "--allow")
                {
                    skipVersionCheck = true;
                }
            }

            if (!skipVersionCheck)
            {
                // Check if OS is older than 1903
                if (Environment.OSVersion.Version.Build <= 18362)
                {
                    ShowCompatibilityMessage(false, Strings.Rectify11.compatErrorInstruc, Strings.Rectify11.compatErrorText);
                    return;
                }

                // Check if OS is Windows 1903 - Windows 11 build 21343
                // 21343 is the first known build of Windows 11

                if ((Environment.OSVersion.Version.Build >= 18362) && (Environment.OSVersion.Version.Build < 21343))
                {
                    if (!ShowCompatibilityMessage(true, Strings.Rectify11.compatWarnText, Strings.Rectify11.compatWarnInstruc))
                        return;
                }

                // Windows 11 builds greater than 25977 have known issues
                if (Environment.OSVersion.Version.Build >= 25977)
                {
                    if (!ShowCompatibilityMessage(true, Strings.Rectify11.compatWarnText, Strings.Rectify11.tooNewBuild))
                        return;
                }
            }

            if (Environment.OSVersion.Version.Build >= 10240)
            {
                Theme.InitTheme();
                if ((Environment.OSVersion.Version.Build >= 17763) && (Environment.OSVersion.Version.Build < 18362))
                {
                    DarkMode.AllowDarkModeForApp(true);
                }
                else if (Environment.OSVersion.Version.Build >= 18362)
                {
                    DarkMode.SetPreferredAppMode(DarkMode.PreferredAppMode.AllowDark);
                }
            }

            // Load the Rectify11 theme to skin the installer with it
            File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "Dark.msstyles"), Properties.Resources.Dark);
            File.WriteAllBytes(Path.Combine(Path.GetTempPath(), "light.msstyles"), Properties.Resources.light);
            Theme.LoadTheme();

            // Optimize the installer
            ProfileOptimization.SetProfileRoot(Path.Combine(Path.GetTempPath(), "Rectify11"));
            ProfileOptimization.StartProfile("Startup.Profile");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
            Application.Run(new FrmWizard());

            // Release mutex once installer exists
            mutex.ReleaseMutex();
        }

        private static bool ShowCompatibilityMessage(bool CanAllow, string header, string text)
        {
            if (CanAllow)
            {
                // in this case, we can skip the warning
                bool yes = false;
                TaskDialog td = new();
                td.Page.Text = header;
                td.Page.Instruction = text;
                td.Page.Title = Strings.Rectify11.Title;
                td.Page.StandardButtons = TaskDialogButtons.OK;
                td.Page.Icon = TaskDialogStandardIcon.SecurityWarningYellowBar;
                td.Page.EnableHyperlinks = true;
                TaskDialogExpander tde = new();
                tde.Text = "<a href=\"link1\">Run anyway (not recommended)</a>";
                tde.Expanded = false;
                tde.CollapsedButtonText = Strings.Rectify11.moreInfo;
                tde.ExpandedButtonText = Strings.Rectify11.lessInfo;
                td.Page.HyperlinkClicked += (s, e) =>
                {
                    yes = true;
                    td.Close();
                };
                td.Page.Expander = tde;
                td.Show();
                return yes;
            }
            else
            {
                TaskDialog td = new();
                td.Page.Text = header;
                td.Page.Instruction = text;
                td.Page.Title = Strings.Rectify11.Title;
                td.Page.StandardButtons = TaskDialogButtons.OK;
                td.Page.Icon = TaskDialogStandardIcon.SecurityErrorRedBar;
                td.Page.EnableHyperlinks = false;
                td.Show();
                return false;
            }
        }
    }
}
