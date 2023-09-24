using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static Rectify11Installer.Win32.NativeMethods;

namespace Rectify11Installer.Core
{
    internal class Themes
    {
        public static bool Install()
        {
            Logger.WriteLine("Installing Themes");
            Logger.WriteLine("─────────────────");
            if (!Common.WriteFiles(false, true))
            {
                Logger.WriteLine("WriteFiles() failed.");
                return false;
            }
            Logger.WriteLine("WriteFiles() succeeded.");


            if (Directory.Exists(Path.Combine(Variables.r11Folder, "themes")))
            {
                Logger.WriteLine(Path.Combine(Variables.r11Folder, "themes") + " exists. Deleting it.");
                if (!Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "themes"), false))
                {
                    Logger.WriteLine("Deleting " + Path.Combine(Variables.r11Folder, "themes") + " failed. ");
                    return false;
                }
            }

            // extract the 7z
            Helper.SvExtract("themes.7z", "themes");

            Logger.WriteLine("Extracted themes.7z");
            if (!InstallThemes())
            {
                Logger.WriteLine("InstallThemes() failed.");
                return false;
            }
            try
            {
                if (!InstallOptions.SkipMFE)
                {

                    InstallMfe();
                    Logger.WriteLine("InstallMfe() succeeded.");
                }
            }
            catch
            {
                Logger.WriteLine("InstallMfe() failed.");
            }
            try
            {
                Installr11cpl();
                Logger.WriteLine("Installr11cpl() succeeded.");
            }
            catch
            {
                Logger.WriteLine("Installr11cpl() failed.");
            }
            Variables.RestartRequired = true;
            Logger.WriteLine("InstallThemes() succeeded.");
            Logger.WriteLine("══════════════════════════════════════════════");
            return true;
        }
        public static bool Uninstall()
        {
            var s = IsArm64() ? Properties.Resources.secureux_arm64 : Properties.Resources.secureux_x64;
            var dll = IsArm64() ? Properties.Resources.ThemeDll_arm64 : Properties.Resources.ThemeDll_x64;

            if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"), s, Helper.OperationType.Write))
                return false;
            if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "ThemeDll.dll"), dll, Helper.OperationType.Write))
                return false;

            string mode = Theme.IsUsingDarkMode ? "dark.theme" : "aero.theme";
            Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", mode));
            string theme = Theme.IsUsingDarkMode ? "Windows (dark)" : "Windows (light)";
            Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " apply " + '"' + theme + '"', AppWinStyle.Hide, true);

            UninstallThemeWallpapers();

            Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " uninstall", AppWinStyle.Hide, true);
            Helper.SafeFileDeletion(Path.Combine(Variables.Windir, "Themetool.exe"));
            Logger.WriteLine("Deleted " + Path.Combine(Variables.Windir, "Themetool.exe"));

            UninstallCursors();

            UninstallMsstyles();

            Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"));
            Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "ThemeDll.dll"));

            UninstallMfe();

            try
            {
                var key = Registry.ClassesRoot.OpenSubKey(@"CLSID", true);
                key.DeleteSubKeyTree("{959E11F4-0A48-49cf-8416-FF9BC49D9656}", false);
                key.Dispose();
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace", true);
                key.DeleteSubKeyTree("{959E11F4-0A48-49cf-8416-FF9BC49D9656}", false);
                key.Dispose();
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes", true);
                key.SetValue("MS Shell Dlg 2", "Tahoma");
                key.SetValue("MS Shell Dlg", "Microsoft Sans Serif");
                key.Dispose();
                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop\WindowMetrics", true);
                key.SetValue("MenuHeight", "-285");
                key.SetValue("MenuWidth", "-285");
                key.Dispose();
                Logger.WriteLine("Remove registry entries");
            }
            catch { }

            // nuke r11cp
            Helper.SafeFileDeletion(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Rectify11 Control Center.lnk"));
            Helper.SafeFileDeletion(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Rectify11 Control Center.lnk"));
            Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"), false);

            Logger.WriteLine("Deleted Rectify11 Control Center");
            return true;
        }

        /// <summary>
        /// installs themes
        /// </summary>
        private static bool InstallThemes()
        {
            InstallThemeWallpapers();

            // todo: remove r11cp
            File.Copy(Path.Combine(Variables.r11Folder, "themes", "ThemeTool.exe"), Path.Combine(Variables.Windir, "ThemeTool.exe"), true);
            Logger.WriteLine("Copied Themetool.");
            Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " install", AppWinStyle.Hide, true);
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Folder, "themes", "Themes.reg"), AppWinStyle.Hide, true);

            InstallCursors();
            InstallMsstyles();
            return true;
        }


        /// <summary>
        /// installs control center
        /// </summary>
        private static void Installr11cpl()
        {
            Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"), false);
            Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"));
            File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter", "Rectify11ControlCenter.exe"), Properties.Resources.Rectify11ControlCenter);
            using ShellLink shortcut = new();
            shortcut.Target = Path.Combine(Variables.r11Folder, "Rectify11ControlCenter", "Rectify11ControlCenter.exe");
            shortcut.WorkingDirectory = @"%windir%\Rectify11\Rectify11ControlCenter";
            shortcut.IconPath = Path.Combine(Variables.r11Folder, "Rectify11ControlCenter", "Rectify11ControlCenter.exe");
            shortcut.IconIndex = 0;
            shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
            shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Rectify11 Control Center.lnk"));
            shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Rectify11 Control Center.lnk"));
            shortcut.Dispose();
        }

        /// <summary>
        /// installs mfe
        /// </summary>
        private static void InstallMfe()
        {
            UninstallMfe();
            Directory.Move(Path.Combine(Variables.r11Folder, "Themes", "MicaForEveryone"), Path.Combine(Variables.Windir, "MicaForEveryone"));
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn mfe /xml " + Path.Combine(Variables.Windir, "MicaForEveryone", "XML", "mfe.xml"), AppWinStyle.Hide, true);

            string path = Path.Combine(Environment.GetEnvironmentVariable("localappdata"), "Mica For Everyone");
            Helper.SafeDirectoryDeletion(path, false);
            string t = InstallOptions.TabbedNotMica ? "T" : "";
            string val = "";
            if (InstallOptions.ThemeLight) val = t + "lightrectified.conf";
            else if (InstallOptions.ThemeDark) val = t + "darkrectified.conf";
            else if (InstallOptions.ThemeBlack)
            {
                val = t + "black.conf";
                string amdorarm = NativeMethods.IsArm64() ? "ARM" : "AMD";
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn micafix /xml " + Path.Combine(Variables.Windir, "MicaForEveryone", "XML", "micafix" + amdorarm + "64.xml"), AppWinStyle.Hide, true);
            }

            if (!string.IsNullOrWhiteSpace(val))
            {
                File.Copy(Path.Combine(Variables.Windir, "MicaForEveryone", "CONF", val),
                    Path.Combine(Variables.Windir, "MicaForEveryone", "MicaForEveryone.conf"), true);
            }
        }

        #region Internal
        private static bool InstallThemeWallpapers()
        {
            UninstallThemeWallpapers();
            if (!Directory.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")))
            {
                Directory.CreateDirectory(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
            }
            try
            {
                var files = Directory.GetFiles(Path.Combine(Variables.r11Folder, "themes", "wallpapers"));
                for (int j = 0; j < files.Length; j++)
                {
                    File.Copy(files[j], Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified", Path.GetFileName(files[j])), true);
                }
                Logger.WriteLine("Copied wallpapers to " + Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error copying wallpapers. " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
            }
            return true;
        }
        private static bool UninstallThemeWallpapers()
        {
            if (Directory.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")))
            {
                try
                {
                    List<string> wallpapers = new()
                    {
                        "cosmic.png",
                        "img0.png",
                        "img19.png",
                        "metal.png"
                    };
                    string path = Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified");
                    var files = Directory.GetFiles(path);
                    for (int j = 0; j < files.Length; j++)
                    {
                        if (!wallpapers.Contains(Path.GetFileName(files[j])))
                            Helper.SafeFileDeletion(files[j]);
                    }
                    if (Directory.GetFiles(path).Length == 0)
                    {
                        Helper.SafeDirectoryDeletion(path, false);
                        Logger.WriteLine("Deleted " + path);
                    }
                    Logger.WriteLine("Deleted old wallpapers");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error deleting old wallpapers" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                }
            }
            return true;
        }
        private static bool InstallCursors()
        {
            UninstallCursors();
            var curdir = new DirectoryInfo(Path.Combine(Variables.r11Folder, "themes", "cursors"))
                .GetDirectories("*", SearchOption.TopDirectoryOnly);

            for (var i = 0; i < curdir.Length; i++)
            {
                try
                {
                    Directory.Move(curdir[i].FullName, Path.Combine(Variables.Windir, "cursors", curdir[i].Name));
                    Logger.WriteLine("Copied " + curdir[i].Name + " cursors");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error copying " + curdir[i].Name + ". " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                    return false;
                }
            }
            return true;
        }
        private static bool UninstallCursors()
        {
            var dirs = Directory.GetDirectories(Path.Combine(Variables.Windir, "cursors"), "WindowsRectified*");
            for (int i = 0; i < dirs.Length; i++)
            {
                try
                {
                    Helper.SafeDirectoryDeletion(dirs[i], false);
                    Logger.WriteLine("Deleted existing cursor directory " + dirs[i]);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        private static bool UninstallMsstyles()
        {
            // .theme files
            List<string> themefiles = new()
            {
                "black.theme",
                "darkcolorized.theme",
                "darkrectified.theme",
                "lightrectified.theme"
            };
            try
            {
                for (int i = 0; i < themefiles.Count; i++)
                {
                    Helper.SafeFileDeletion(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i]));
                }
                Logger.WriteLine("Deleted themes");
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error deleting themes", ex);
            }

            // msstyles
            try
            {
                Helper.SafeDirectoryDeletion(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified"), false);
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error deleting " + Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified") + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                return false;
            }
            return true;
        }
        private static bool InstallMsstyles()
        {
            UninstallMsstyles();
            DirectoryInfo themedir = new(Path.Combine(Variables.r11Folder, "themes", "themes"));
            var msstyleDirList = themedir.GetDirectories("*", SearchOption.TopDirectoryOnly);
            var themefiles = themedir.GetFiles("*.theme");

            for (var i = 0; i < themefiles.Length; i++)
            {
                // why would it fail
                File.Copy(themefiles[i].FullName, Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i].Name), true);
            }

            File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun1.exe"), Properties.Resources.AdvancedRun);
            for (var i = 0; i < msstyleDirList.Length; i++)
            {
                try
                {
                    Directory.Move(msstyleDirList[i].FullName, Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name));
                    Logger.WriteLine("Copied " + msstyleDirList[i].Name + " directory.");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error copying " + msstyleDirList[i].Name + ". " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
                    return false;
                }
            }
            File.Delete(Path.Combine(Variables.r11Folder, "aRun1.exe"));
            return true;
        }
        private static bool UninstallMfe()
        {
            try
            {
                Helper.KillProcess("micaforeveryone.exe");
                Helper.KillProcess("micafix.exe");
                Helper.KillProcess("explorerframe.exe");
                Helper.DeleteTask("mfe");
                Helper.DeleteTask("micafix");
                if (!Helper.SafeDirectoryDeletion(Path.Combine(Variables.Windir, "MicaForEveryone"), false))
                {
                    Logger.WriteLine("Deleting " + Path.Combine(Variables.Windir, "MicaForEveryone") + " failed. ");
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        #endregion
    }
}
