using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static Rectify11Installer.Win32.NativeMethods;

namespace Rectify11Installer.Core
{
    internal class Themes
    {
        /// <summary>
        /// themes installation logic
        /// </summary>
        /// <returns>true if succeeds, else returns false</returns>
        public static bool Install()
        {
            try
            {
                Logger.WriteLine("Installing Themes");
                Logger.WriteLine("─────────────────");
                if (!Common.WriteFiles(false, true))
                    return false;


                if (Directory.Exists(Path.Combine(Variables.r11Folder, "themes")))
                {
                    Logger.WriteLine(Path.Combine(Variables.r11Folder, "themes") + " exists. Deleting it.");
                    if (!Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "themes"), false))
                    {
                        return false;
                    }
                }

                // extract the 7z
                Helper.SvExtract("themes.7z", "themes");

                // Install/update r11cpl first to make RectifyUtil class work
                try
                {
                    InstallR11Cpl();
                    Logger.WriteLine("Installr11cpl() succeeded.");
                }
                catch (Exception ex)
                {
                    Logger.Warn("Installr11cpl() failed", ex);
                    return false;
                }

                if (!InstallThemes())
                    return false;

                try
                {
                    if (!InstallOptions.SkipMFE)
                    {
                        InstallMfe();
                        Logger.WriteLine("InstallMfe() succeeded.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn("InstallMfe() failed", ex);
                }

                // mmc dpi fix
                Process.Start(Path.Combine(Variables.sys32Folder, "reg.exe"), @" ADD HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\SideBySide /v PreferExternalManifest /t REG_DWORD /d 1 /f");
                Helper.SvExtract(true, "themes.7z", "mmc.exe.manifest");
                Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "mmc.exe.manifest"), Path.Combine(Variables.sys32Folder, "mmc.exe.manifest"), Helper.OperationType.Copy);
                File.Delete(Path.Combine(Variables.r11Folder, "mmc.exe.manifest"));

                try
                {
                    ApplyScheme();
                    Logger.WriteLine("ApplyScheme() succeeded.");
                }
                catch (Exception ex)
                {
                    Logger.Warn("ApplyScheme() failed", ex);
                }

                Variables.RestartRequired = true;
                Logger.WriteLine("Themes.Install() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Themes.Install() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// themes uninstallation logic
        /// </summary>
        /// <returns>true if succeeds, else returns false</returns>
        public static bool Uninstall()
        {
            try
            {
                string mode = Theme.IsUsingDarkMode ? "dark.theme" : "aero.theme";
                if (File.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", mode)))
                    Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", mode));
                string theme = Theme.IsUsingDarkMode ? "Windows (dark)" : "Windows (light)";
                RectifyThemeUtil.Utility.ApplyTheme(theme);

                UninstallThemeWallpapers();

                nint hr = RectifyThemeUtil.Utility.UninstallThemeTool();
                if (hr != 0)
                {
                    Logger.WriteLine("FAILED TO REMOVE THEMETOOL: " + hr);
                }
                Helper.SafeFileDeletion(Path.Combine(Variables.Windir, "Themetool.exe"));

                UninstallCursors();

                UninstallMsstyles();

                // Remove leftovers from previous versions
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
                    Logger.WriteLine("Removed registry entries.");
                }
                catch { }

                UninstallR11Cpl();
                Logger.WriteLine("Deleted Rectify11 Control Center.");

                Process.Start(Path.Combine(Variables.sys32Folder, "reg.exe"), @" ADD HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\SideBySide /v PreferExternalManifest /t REG_DWORD /d 0 /f");
                Helper.SafeFileDeletion(Path.Combine(Variables.sys32Folder, "mmc.exe.manifest"));

                Logger.WriteLine("Themes.Uninstall() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Themes.Uninstall() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// installs themes
        /// </summary>
        private static bool InstallThemes()
        {
            try
            {
                InstallThemeWallpapers();

                Helper.SafeFileOperation(
                    Path.Combine(Variables.r11Folder, "themes", "ThemeTool.exe"),
                    Path.Combine(Variables.Windir, "ThemeTool.exe"),
                    Helper.OperationType.Copy);

                Logger.WriteLine("Copied Themetool.");
                nint hr = 0;
                try
                {
                    hr = RectifyThemeUtil.Utility.InstallThemeTool();
                }
                catch (Exception ex)
                {
                    UninstallThemeWallpapers();

                    MessageBox.Show("Failed to install UxTheme patcher: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteLine("InstallThemes() failed as SecureUXHelper is missing (most likely deleted by antivirus. Please exclude the C:\\Windows\\Rectify11 folder from your anti-virus.");

                    return false;
                }

                if (hr != 0)
                {
                    MessageBox.Show("Failed to install UxTheme patcher. HRESULT is " + hr, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.WriteLine("Failed to install UxTheme patcher. HRESULT is " + hr);
                }
                Helper.ImportReg(Path.Combine(Variables.r11Folder, "themes", "Themes.reg"));

                InstallCursors();
                InstallMsstyles();
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("InstallThemes() failed", ex);
                UninstallThemeWallpapers();
                UninstallCursors();
                UninstallMsstyles();
                Helper.SafeFileDeletion(Path.Combine(Variables.Windir, "ThemeTool.exe"));

                nint hr = 0;
                try
                {
                    hr = RectifyThemeUtil.Utility.UninstallThemeTool();
                }
                catch (Exception ex2)
                {
                    Logger.WriteLine("Failed to uninstall themetool: ", ex2);
                }

                if (hr != 0)
                {
                    Logger.WriteLine("Failed to uninstall themetool. HRESULT is " + hr);
                }
                return false;
            }
        }

        /// <summary>
        /// installs control center
        /// </summary>
        public static void InstallR11Cpl()
        {
            UninstallR11Cpl();

            // Delete r11 control center
            Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"), false);
            Helper.SafeFileDeletion(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs"));

            // install new
            string cplPath = Path.Combine(Variables.r11Folder, "Rectify11CPL", "Rectify11CPL.dll");

            //create files
            Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Rectify11CPL"));

            File.WriteAllBytes(cplPath, Properties.Resources.Rectify11CPL);

            // write SecureUxTheme helper dll
            if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "Rectify11CPL", "ThemeDLL.dll"), Properties.Resources.ThemeDLL, Helper.OperationType.Write))
                return;

            // create shortcut
            using ShellLink shortcut = new();
            shortcut.Target = Path.Combine(Variables.sys32Folder, "control.exe");
            shortcut.Arguments = "/name Rectify11.SettingsCPL";
            shortcut.WorkingDirectory = @"%windir%\system32";
            shortcut.IconPath = Path.Combine(Variables.r11Folder, "Rectify11CPL", "Rectify11CPL.dll");
            shortcut.IconIndex = 0;
            shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;

            string startmenu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs");
            Directory.CreateDirectory(startmenu);
            try
            {
                shortcut.Save(Path.Combine(startmenu, "Rectify11 Control Center.lnk"));
            }
            catch (Exception ex)
            {
                Logger.Warn("Error while saving shortcut: " + ex);
            }
            shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Rectify11 Control Center.lnk"));

            // register CPL
            var proc = new Process();
            proc.StartInfo.FileName = "regsvr32.exe";
            proc.StartInfo.Arguments = "/s \"" + cplPath + "\"";
            proc.Start();
            proc.WaitForExit();

            if (proc.ExitCode != 0)
            {
                Logger.WriteLine("Error while registering CPL: " + proc.ExitCode);
            }
        }
        /// <summary>
        /// uninstalls control center
        /// </summary>
        private static void UninstallR11Cpl()
        {
            string cplPath = Path.Combine(Variables.r11Folder, "Rectify11CPL", "Rectify11CPL.dll");
            string startmenuShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Rectify11 Control Center.lnk");
            string desktopShortcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Rectify11 Control Center.lnk");

            // delete shortcut
            Helper.SafeFileDeletion(startmenuShortcut);
            Helper.SafeFileDeletion(desktopShortcut);

            if (File.Exists(cplPath))
            {
                // unregister CPL
                var proc = new Process();
                proc.StartInfo.FileName = "regsvr32.exe";
                proc.StartInfo.Arguments = "/s /u \"" + cplPath + "\"";
                proc.Start();
                proc.WaitForExit();

                if (proc.ExitCode != 0)
                {
                    Logger.Warn("Error while unregistering CPL: " + proc.ExitCode);
                }
            }
            // nuke r11cp
            Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"), false);

            //delete folder
            Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Rectify11CPL"), false);
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

            // TODO: Use CRectifyUtil

            if (InstallOptions.ThemeLight) val = t + "lightrectified.conf";
            else if (InstallOptions.ThemeDark) val = t + "darkrectified.conf";
            else if (InstallOptions.ThemePDark) val = t + "darkrectified.conf";
            else if (InstallOptions.ThemeBlack)
            {
                val = t + "black.conf";
                string amdorarm = NativeMethods.IsArm64() ? "ARM" : "AMD";
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn mfefix /xml " + Path.Combine(Variables.Windir, "MicaForEveryone", "XML", "micafix" + amdorarm + "64.xml"), AppWinStyle.Hide, true);
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
            Directory.CreateDirectory(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
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
                Logger.WriteLine("Error copying wallpapers", ex);
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
                    Logger.WriteLine("Deleted old wallpapers.");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error deleting old wallpapers", ex);
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
                    Logger.WriteLine("Error copying " + curdir[i].Name, ex);
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
                "lightrectified.theme",
                "darkpartial.theme"
            };
            try
            {
                for (int i = 0; i < themefiles.Count; i++)
                {
                    Helper.SafeFileDeletion(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i]));
                }
                Logger.WriteLine("Deleted themes.");
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
                Logger.WriteLine("Error deleting " + Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified"), ex);
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

            for (var i = 0; i < msstyleDirList.Length; i++)
            {
                try
                {
                    if (Environment.OSVersion.Version.Build >= 22543
                        && !msstyleDirList[i].Name.Contains("Legacy"))
                    {
                        Directory.Move(msstyleDirList[i].FullName, Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name));
                        Logger.WriteLine("Copied " + msstyleDirList[i].Name + " directory.");
                    }
                    else if (Environment.OSVersion.Version.Build < 22543)
                    {
                        Directory.Move(msstyleDirList[i].FullName, Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name.Replace("Legacy", "")));
                        Logger.WriteLine("Copied " + msstyleDirList[i].Name + " directory.");
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error copying " + msstyleDirList[i].Name, ex);
                    return false;
                }
            }
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
                Helper.DeleteTask("mfefix");
                if (!Helper.SafeDirectoryDeletion(Path.Combine(Variables.Windir, "MicaForEveryone"), false))
                {
                    Logger.WriteLine("Deleting " + Path.Combine(Variables.Windir, "MicaForEveryone") + " failed. ");
                    return false;
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Applies theme, and schedules removal of 
        /// </summary>
        private static void ApplyScheme()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", true);

            var config = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Rectify11", true);
            if (key != null && config != null)
            {
                // The goal here is to apply the theme and visual style. For some reason, running the .theme
                // file only applies cursors, wallpaper, etc but does not change the visual style. Because of that,
                // RectifyStart applies it on the next boot after theme patcher is installed. It is ran on the next boot
                // to ensure that the UxTheme patcher is running.
                if (InstallOptions.ThemeLight)
                {
                    Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "lightrectified.theme"));
                    config.SetValue("ApplyThemeOnNextRun", "Rectify11 light theme");
                }
                else if (InstallOptions.ThemeDark)
                {
                    Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "darkrectified.theme"));
                    config.SetValue("ApplyThemeOnNextRun", "Rectify11 dark theme");
                }
                else if (InstallOptions.ThemePDark)
                {
                    Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "darkpartial.theme"));
                    config.SetValue("ApplyThemeOnNextRun", "Rectify11 partial dark theme");
                }
                else
                {
                    Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "black.theme"));
                    config.SetValue("ApplyThemeOnNextRun", "Rectify11 Dark theme with Mica");
                }

                config.Close();
                key.SetValue("DeleteJunk", "rmdir /s /q " + Path.Combine(Environment.SpecialFolder.LocalApplicationData.ToString(), "junk"), RegistryValueKind.String);
                key.Close();
            }
        }
        #endregion
    }
}
