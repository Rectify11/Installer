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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

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
                TaskDialogPage pg = new TaskDialogPage();
                pg.Heading = text;
                pg.Text = header;
                pg.Buttons = [TaskDialogButton.OK];
                pg.Icon = TaskDialogIcon.ShieldWarningYellowBar;
                pg.EnableLinks = true;

                TaskDialogExpander tde = new();
                tde.Text = "<a href=\"link1\">Run anyway (not recommended)</a>";
                tde.Expanded = false;
                tde.CollapsedButtonText = Strings.Rectify11.moreInfo;
                tde.ExpandedButtonText = Strings.Rectify11.lessInfo;

                pg.Expander = tde;

                pg.LinkClicked += delegate (object s, TaskDialogLinkClickedEventArgs e)
                {
                    if (e.LinkHref == "link1")
                    {
                        pg.BoundDialog.Close();
                    }
                };

                if (TaskDialog.ShowDialog(pg) == TaskDialogButton.OK)
                    return false;
                return true;
            }
            else
            {
                TaskDialogPage pg = new();
                pg.Text = header;
                pg.Heading = text;
                pg.Buttons = [TaskDialogButton.OK];
                pg.Icon = TaskDialogIcon.ShieldErrorRedBar;
                TaskDialog.ShowDialog(pg);
                return false;
            }
        }
    }
}
