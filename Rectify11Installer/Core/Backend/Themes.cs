using Microsoft.VisualBasic;
using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
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

            Helper.KillProcess("micaforeveryone.exe");
            Helper.KillProcess("micafix.exe");
            Helper.KillProcess("explorerframe.exe");
            Helper.DeleteTask("mfe");
            Helper.DeleteTask("micafix");

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
                    if (Directory.Exists(Path.Combine(Variables.Windir, "MicaForEveryone")))
                    {
                        if (!Helper.SafeDirectoryDeletion(Path.Combine(Variables.Windir, "MicaForEveryone"), false))
                        {
                            Logger.WriteLine("Deleting " + Path.Combine(Variables.Windir, "MicaForEveryone") + " failed. ");
                            return false;
                        }
                    }
                    Directory.Move(Path.Combine(Variables.r11Folder, "Themes", "MicaForEveryone"), Path.Combine(Variables.Windir, "MicaForEveryone"));
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
        /// <summary>
        /// installs themes
        /// </summary>
        private static bool InstallThemes()
        {
            var curdir = new DirectoryInfo(Path.Combine(Variables.r11Folder, "themes", "cursors"))
                .GetDirectories("*", SearchOption.TopDirectoryOnly);

            InstallThemeWallpapers();

            // todo: remove r11cp
            File.Copy(Path.Combine(Variables.r11Folder, "themes", "ThemeTool.exe"), Path.Combine(Variables.Windir, "ThemeTool.exe"), true);
            Logger.WriteLine("Copied Themetool.");
            Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " install", AppWinStyle.Hide, true);
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Folder, "themes", "Themes.reg"), AppWinStyle.Hide);

            InstallCursors(curdir);
            InstallMsstyles();
            return true;
        }


        /// <summary>
        /// installs control center
        /// </summary>
        private static void Installr11cpl()
        {
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"), true);
                }
                catch
                {
                    string name = Path.GetRandomFileName();
                    Directory.Move(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"), Path.Combine(Path.GetTempPath(), name));
                    var files = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                    for (int j = 0; j < files.Length; j++)
                    {
                        MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
            }
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
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn mfe /xml " + Path.Combine(Variables.Windir, "MicaForEveryone", "XML", "mfe.xml"), AppWinStyle.Hide);
            if (Directory.Exists(Path.Combine(Environment.GetEnvironmentVariable("localappdata") ?? string.Empty, "Mica For Everyone")))
            {
                Directory.Delete(Path.Combine(Environment.GetEnvironmentVariable("localappdata") ?? string.Empty, "Mica For Everyone"), true);
            }
            string t = "";
            if (InstallOptions.TabbedNotMica) t = "T";
            if (InstallOptions.ThemeLight)
            {
                File.Copy(Path.Combine(Variables.Windir, "MicaForEveryone", "CONF", t + "lightrectified.conf"), Path.Combine(Variables.Windir, "MicaForEveryone", "MicaForEveryone.conf"), true);
            }
            else if (InstallOptions.ThemeDark)
            {
                File.Copy(Path.Combine(Variables.Windir, "MicaForEveryone", "CONF", t + "darkrectified.conf"), Path.Combine(Variables.Windir, "MicaForEveryone", "MicaForEveryone.conf"), true);
            }
            else
            {
                File.Copy(Path.Combine(Variables.Windir, "MicaForEveryone", "CONF", t + "black.conf"), Path.Combine(Variables.Windir, "MicaForEveryone", "MicaForEveryone.conf"), true);
                string amdorarm = "AMD";
                if (NativeMethods.IsArm64()) amdorarm = "ARM";
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn micafix /xml " + Path.Combine(Variables.Windir, "MicaForEveryone", "XML", "micafix" + amdorarm + "64.xml"), AppWinStyle.Hide);
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
                    var files = Directory.GetFiles(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                    for (int j = 0; j < files.Length; j++)
                    {
                        if (!wallpapers.Contains(Path.GetFileName(files[j])))
                            Helper.SafeFileDeletion(files[j]);
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
        private static bool InstallCursors(DirectoryInfo[] curdir)
        {
            UninstallCursors();
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
                    Directory.Delete(dirs[i], true);
                    Logger.WriteLine("Deleted existing cursor directory " + dirs[i]);
                }
                catch
                {
                    Helper.SafeDirectoryDeletion(dirs[i], false);
                }
            }
            return true;
        }
        private static bool UninstallMsstyles()
        {
            if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified")))
            {
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
            return false;
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
        #endregion
    }
}
