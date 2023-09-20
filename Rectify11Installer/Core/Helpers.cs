using KPreisser.UI;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Pages;
using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Rectify11Installer.Core
{
    public class Helper
    {
        #region Public Methods
        public static bool CheckIfUpdatesPending()
        {
            if (Variables.skipUpdateCheck) return true;
            if (!RebootRequired()) return true;

            TaskDialog.Show(text: Strings.Rectify11.updatePending,
                instruction: "Compatibility Error",
                title: Strings.Rectify11.Title,
                buttons: TaskDialogButtons.OK,
                icon: TaskDialogStandardIcon.SecurityErrorRedBar);

            return false;
        }
        public static bool SvExtract(string file, string path)
        {
            Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                         " x -o" + Path.Combine(Variables.r11Folder, path) +
                         " " + Path.Combine(Variables.r11Folder, file), AppWinStyle.Hide, true);
            return true;
        }

        public static bool SvExtract(string file, string path, string folder)
        {
            Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -y " + Path.Combine(Variables.r11Folder, file)
                        + " -o\"" + Path.Combine(Variables.r11Folder, path) + "\""
                        + " " + folder, AppWinStyle.Hide, true);
            return true;
        }

        public static bool SvExtract(bool isf, string file, string exe)
        {
            Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                    " e -o" + Variables.r11Folder + " "
                    + Path.Combine(Variables.r11Folder, file) + " " + exe, AppWinStyle.Hide, true);
            return true;
        }

        public static StringBuilder FinalText()
        {
            var finalstr = new StringBuilder();
            finalstr.AppendLine();
            finalstr.AppendLine();
            if (Variables.InstallIcons)
                finalstr.AppendLine(Strings.Rectify11.installIcons);

            if (InstallOptions.iconsList.Contains("themeNode"))
                finalstr.AppendLine(Strings.Rectify11.installThemes);

            if (InstallOptions.iconsList.Contains("asdfNode"))
                finalstr.AppendLine(Strings.Rectify11.installASDF);

            if (InstallOptions.iconsList.Contains("shellNode"))
                finalstr.AppendLine(Strings.Rectify11.installShell);

            if (InstallOptions.iconsList.Contains("gadgetsNode"))
                finalstr.AppendLine(Strings.Rectify11.installGadgets);

            if (InstallOptions.iconsList.Contains("wallpapersNode"))
                finalstr.AppendLine(Strings.Rectify11.installWallpapers);

            if (InstallOptions.iconsList.Contains("useravNode"))
                finalstr.AppendLine(Strings.Rectify11.installUserAV);

            return finalstr;
        }

        public static bool CheckUBR()
        {
            return ((NativeMethods.GetUbr() != -1
                    && NativeMethods.GetUbr() < 51
                    && Environment.OSVersion.Version.Build == 22000)
                    || Environment.OSVersion.Version.Build is < 22000 and >= 21996);
        }

        public static bool CheckIfUpdate()
        {
            try
            {
                // more priority
                var build = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Rectify11", false)?.GetValue("Build");
                if (build != null && int.Parse(build.ToString()) < Assembly.GetEntryAssembly().GetName().Version.Build)
                    return true;

                var r11 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11", false)?.GetValue("OSVersion");
                Version ver = Version.Parse(r11.ToString());
                if (Environment.OSVersion.Version.Build > ver.Build || NativeMethods.GetUbr() > ver.Revision)
                {
                    Variables.WindowsUpdate = true;
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
        public static void UpdateSideImageOptns(string name, FrmWizard _frmWizard)
        {
            switch (name)
            {
                case "shellNode":
                    _frmWizard.UpdateSideImage = Properties.Resources.menus;
                    if (Theme.IsUsingDarkMode) _frmWizard.UpdateSideImage = Properties.Resources.menusD;
                    break;
                case "gadgetsNode":
                    _frmWizard.UpdateSideImage = Properties.Resources.gadgets;
                    break;
                case "asdfNode":
                    _frmWizard.UpdateSideImage = Properties.Resources.asdf;
                    break;
                case "wallpapersNode":
                    _frmWizard.UpdateSideImage = Properties.Resources.wallpapers;
                    break;
                case "useravNode":
                    _frmWizard.UpdateSideImage = Properties.Resources.avatars;
                    break;
                case "soundNode":
                    _frmWizard.UpdateSideImage = Properties.Resources.sound; ;
                    break;
                // disabled until i will make them work
                // case "sysiconsNode":
                //     _frmWizard.UpdateSideImage = global::Rectify11Installer.Properties.Resources.iconnewtree;
                //     break;
                // case "themesNode":
                //     _frmWizard.UpdateSideImage = global::Rectify11Installer.Properties.Resources.theme;
                //     break;
                default:
                    _frmWizard.UpdateSideImage = Properties.Resources.installoptns;
                    break;
            }
        }

        public static string FixString(string path, bool x86)
        {
            if (path.Contains("mun"))
            {
                return path.Replace(@"%sysresdir%", Variables.sysresdir);
            }
            else if (path.Contains("%sys32%"))
            {
                if (x86)
                {
                    return path.Replace(@"%sys32%", Variables.sysWOWFolder);
                }
                else
                {
                    return path.Replace(@"%sys32%", Variables.sys32Folder);
                }
            }
            else if (path.Contains("%lang%"))
            {
                return path.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
            }
            else if (path.Contains("%en-US%"))
            {
                return path.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
            }
            else if (path.Contains("%windirLang%"))
            {
                return path.Replace(@"%windirLang%", Path.Combine(Variables.Windir, CultureInfo.CurrentUICulture.Name));
            }
            else if (path.Contains("%windirEn-US%"))
            {
                return path.Replace(@"%windirEn-US%", Path.Combine(Variables.Windir, "en-US"));
            }
            else if (path.Contains("%branding%"))
            {
                return path.Replace(@"%branding%", Variables.BrandingFolder);
            }
            else if (path.Contains("%prog%"))
            {
                if (x86)
                {
                    return path.Replace(@"%prog%", Variables.progfiles86);
                }
                else
                {
                    return path.Replace(@"%prog%", Variables.progfiles);
                }
            }
            else if (path.Contains("%windir%"))
            {
                return path.Replace(@"%windir%", Variables.Windir);
            }
            else if (path.Contains("%diag%"))
            {
                return path.Replace("%diag%", Variables.diag);
            }
            return path;
        }


        #endregion
        #region Private Methods
        private static bool RebootRequired()
        {
            using var auKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired");
            using var cbsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending");
            return (auKey != null || cbsKey != null);
        }
        #endregion
    }

    public class InstallStatus
    {
        #region Public Methods
        public static bool IsRectify11Installed
        {
            get => (int?)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11")?.GetValue("IsInstalled") == 1;
            set => Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11")?.SetValue("IsInstalled", value ? 1 : 0);
        }
        public static string InstalledVersion
        {
            get => (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11")?.GetValue("Version");
            set => Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11")?.SetValue("Version", value);
        }
        #endregion
    }
    public class NavigationHelper
    {
        public static event EventHandler OnNavigate;
        public static void InvokeOnNavigate(object sender, EventArgs e)
            => OnNavigate?.Invoke(sender, e);
    }

    #region Pages
    public class RectifyPages
    {
        public static WelcomePage WelcomePage = new();
        public static EulaPage EulaPage = new();
        public static InstallOptnsPage InstallOptnsPage;
        public static ThemeChoicePage ThemeChoicePage = new();
        public static CMenuPage CMenuPage = new();
        public static InstallConfirmation InstallConfirmation;
        public static ProgressPage ProgressPage;
        public static DebugPage DebugPage = new();
        public static UninstallPage UninstallPage;
    }
    public class TabPages
    {
        public static Controls.DarkAwareTabPage eulPage;
        public static Controls.DarkAwareTabPage installPage;
        public static Controls.DarkAwareTabPage themePage;
        public static Controls.DarkAwareTabPage cmenupage;
        public static Controls.DarkAwareTabPage debPage;
        public static Controls.DarkAwareTabPage uninstPage;
        public static Controls.DarkAwareTabPage progressPage;
        public static Controls.DarkAwareTabPage summaryPage;
        public static Controls.DarkAwareTabPage wlcmPage;
    }
    public class InstallOptions
    {
        public static bool InstallEP { get; set; }
        public static bool InstallASDF { get; set; }
        public static bool InstallWallpaper { get; set; }
        public static bool InstallWinver { get; set; }
        public static bool InstallGadgets { get; set; }
        public static bool InstallThemes { get; set; }
        public static bool ThemeDark { get; set; }
        public static bool ThemeBlack { get; set; }
        public static bool ThemeLight { get; set; }
        public static bool InstallShell { get; set; }
        public static bool InstallSounds { get; set; }
        public static bool SkipMFE { get; set; }
        public static bool TabbedNotMica { get; set; }
        public static bool userAvatars { get; set; }
        public static int CMenuStyle = 1;
        public static List<string> iconsList = new();
        public static bool InstallExtras()
        {
            return InstallEP
                   || InstallASDF
                   || InstallWallpaper
                   || InstallGadgets
                   || InstallWinver
                   || InstallShell
                   || InstallSounds
                   || userAvatars;
        }
    }
    public class UninstallOptions
    {
        // does nothing
        public static List<string> uninstDummylist = new();

        public static List<string> uninstIconsList = new();
        public static List<string> uninstExtrasList = new();
        public static bool UninstallThemes { get; set; }
    }
    #endregion

    public class Logger
    {
        #region Variables
        private static string Text =
            "═════════════════════════════\nSTART: "
            + DateTime.Now.ToString(CultureInfo.CurrentCulture)
            + "\nRectify11 Version: " + Assembly.GetExecutingAssembly().GetName().Version
            + "\n═════════════════════════════\n";
        #endregion
        #region Public Methods
        public static void WriteLine(string s)
            => Text += s + "\n";

        public static void WriteLine(string s, Exception ex)
            => Text += s + ". " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine;

        public static void CommitLog()
            => File.WriteAllText(System.IO.Path.Combine(Variables.r11Folder, "installer.log"), Text);

        public static void Warn(string v)
            => WriteLine("[WARNING] " + v);

        public static void Warn(string v, Exception ex)
            => WriteLine("[WARNING] " + v, ex);

        public static void LogFile(string file)
            => WriteLine("Wrote " + file);

        public static void LogFile(string file, Exception ex)
            => WriteLine("Error while writing " + file + ". " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);

        #endregion
    }
}
