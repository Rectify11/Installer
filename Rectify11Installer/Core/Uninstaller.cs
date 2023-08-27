using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Rectify11Installer.Win32.NativeMethods;

namespace Rectify11Installer.Core
{
    public class Uninstaller
    {
        public async Task<bool> Uninstall(FrmWizard frm)
        {
            if (UninstallOptions.uninstIconsList.Count > 0)
            {
                Logger.WriteLine("Uninstalling icons");
                Logger.WriteLine("──────────────────");
                frm.InstallerProgress = "Uninstalling icons";
                await Task.Run(() => UninstallIcons());
                Variables.RestartRequired = true;
                Console.WriteLine("══════════════════════════════════════════════");
            }
            if (UninstallOptions.UninstallThemes)
            {
                Logger.WriteLine("Uninstalling themes");
                Logger.WriteLine("───────────────────");
                frm.InstallerProgress = "Uninstalling themes";
                await Task.Run(() => UninstallThemes());
                Variables.RestartRequired = true;
                Console.WriteLine("══════════════════════════════════════════════");
            }
            if (UninstallOptions.uninstExtrasList.Count > 0)
            {
                Logger.WriteLine("Uninstalling extras");
                Logger.WriteLine("───────────────────");
                frm.InstallerProgress = "Uninstalling Extras";
                await Task.Run(() => UninstallExtras());
                Console.WriteLine("══════════════════════════════════════════════");
            }
            // cleanup
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "Tmp"), true);
                }
                catch { }
            }
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
            {
                if (Directory.GetFiles(Path.Combine(Variables.r11Folder, "Backup")).Length == 0
                    && Directory.GetDirectories(Path.Combine(Variables.r11Folder, "Backup")).Length == 0)
                {
                    try
                    {
                        Directory.Delete(Path.Combine(Variables.r11Folder, "Backup"), true);
                    }
                    catch { }
                }
            }
            Logger.CommitLog();
            // complete uninstall
            if (Variables.CompleteUninstall)
            {
                var files = Directory.GetFiles(Variables.r11Folder);
                for (int j = 0; j < files.Length; j++)
                {
                    try
                    {
                        File.Delete(files[j]);
                    }
                    catch
                    {
                        string name = Path.GetRandomFileName();
                        File.Move(files[j], Path.Combine(Path.GetTempPath(), name));
                        MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                }
                if (Directory.GetFiles(Variables.r11Folder).Length == 0 && Directory.GetDirectories(Variables.r11Folder).Length == 0)
                {
                    Directory.Delete(Variables.r11Folder, true);
                }
                using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true);
                reg.DeleteSubKey("Rectify11", false);
                using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
                key.DeleteSubKey("Rectify11", false);
            }
            if (!Variables.RestartRequired) frm.InstallerProgress = "Done, you can close this window";
            return true;
        }
        private async Task<bool> UninstallIcons()
        {
            using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", true);
            File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2);
            Logger.WriteLine("Wrote Rectify11.Phase2.exe");
            File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun);
            Logger.WriteLine("Wrote aRun.exe");
            try
            {
                reg.SetValue("UninstallFiles", UninstallOptions.uninstIconsList.ToArray());
                if (!Variables.Phase2Skip)
                {
                    Logger.WriteLine("Executed Rectify11.Phase2.exe");
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun.exe")
                    + " /EXEFilename " + '"' + Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe") + '"'
                    + " /CommandLine " + "\'" + "/uninstall" + "\'"
                    + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true));
                }
            }
            catch { }
            try
            {
                File.Delete(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"));
            }
            catch
            {
                string name = Path.GetRandomFileName();
                File.Move(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Path.Combine(Path.GetTempPath(), name));
                MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
            }
            try
            {
                File.Delete(Path.Combine(Variables.r11Folder, "aRun.exe"));
            }
            catch
            {
                string name = Path.GetRandomFileName();
                File.Move(Path.Combine(Variables.r11Folder, "aRun.exe"), Path.Combine(Path.GetTempPath(), name));
                MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
            }
            return true;
        }
        private async Task<bool> UninstallThemes()
        {
            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im micaforeveryone.exe", AppWinStyle.Hide, true));
            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im micafix.exe", AppWinStyle.Hide, true));
            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im explorerframe.exe", AppWinStyle.Hide, true));
            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn mfe", AppWinStyle.Hide));
            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn micafix", AppWinStyle.Hide));
            var s = Properties.Resources.secureux_x64;
            var dll = Properties.Resources.ThemeDll_x64;
            if (NativeMethods.IsArm64())
            {
                s = Properties.Resources.secureux_arm64;
                dll = Properties.Resources.secureux_arm64;
            }

            try
            {
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"), s);
                Logger.WriteLine("Wrote SecureUXHelper.exe");
            }
            catch { }
            try
            {
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "ThemeDll.dll"), dll);
                Logger.WriteLine("Wrote ThemeDll.dll");
            }
            catch { }
            if (Theme.IsUsingDarkMode)
            {
                await Task.Run(() => Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "dark.theme")));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " apply " + '"' + "Windows (dark)" + '"', AppWinStyle.Hide, true));
            }
            else
            {
                await Task.Run(() => Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "aero.theme")));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " apply " + '"' + "Windows (light)" + '"', AppWinStyle.Hide, true));
            }
            if (Directory.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")))
            {
                List<string> wallpapers = new List<string>
                    {
                        "cosmic.png",
                        "img0.png",
                        "img19.png",
                        "metal.png"
                    };
                for (int i = 0; i < wallpapers.Count; i++)
                {
                    if (File.Exists(Path.Combine(Variables.Windir, "web", "Wallpaper", "Rectified", wallpapers[i])))
                    {
                        try
                        {
                            File.Delete(Path.Combine(Variables.Windir, "web", "Wallpaper", "Rectified", wallpapers[i]));
                        }
                        catch
                        {
                            string name = Path.GetRandomFileName();
                            File.Move(Path.Combine(Variables.Windir, "web", "Wallpaper", "Rectified", wallpapers[i]), Path.Combine(Path.GetTempPath(), name));
                            MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        Logger.WriteLine("Deleted " + Path.Combine(Variables.Windir, "web", "Wallpaper", "Rectified", wallpapers[i]));
                    }
                }
                if (Directory.GetFiles(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")).Length == 0)
                {
                    try
                    {
                        Directory.Delete(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), true);
                    }
                    catch
                    {
                        string name = Path.GetRandomFileName();
                        Directory.Move(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), Path.Combine(Path.GetTempPath(), name));
                        MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    Logger.WriteLine("Deleted " + Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                }
            }
            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " uninstall", AppWinStyle.Hide, true));
            if (File.Exists(Path.Combine(Variables.Windir, "Themetool.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.Windir, "Themetool.exe"));
                }
                catch
                {
                    string name = Path.GetRandomFileName();
                    File.Move(Path.Combine(Variables.Windir, "Themetool.exe"), Path.Combine(Path.GetTempPath(), name));
                    MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                Logger.WriteLine("Deleted " + Path.Combine(Variables.Windir, "Themetool.exe"));

            }
            if (Directory.Exists(Path.Combine(Variables.Windir, "cursors", "WindowsRectified")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "cursors", "WindowsRectified"), true);
                }
                catch
                {
                    string name = Path.GetTempPath();
                    Directory.Move(Path.Combine(Variables.Windir, "cursors", "WindowsRectified"), Path.Combine(Path.GetTempPath(), name));
                    var fil = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                    for (int i = 0; i < fil.Length; i++)
                    {
                        MoveFileEx(fil[i], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                Logger.WriteLine("Deleted " + Path.Combine(Variables.Windir, "cursors", "WindowsRectified"));
            }
            if (Directory.Exists(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark"), true);
                }
                catch
                {
                    string name = Path.GetTempPath();
                    Directory.Move(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark"), Path.Combine(Path.GetTempPath(), name));
                    var fil = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                    for (int i = 0; i < fil.Length; i++)
                    {
                        MoveFileEx(fil[i], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                Logger.WriteLine("Deleted " + Path.Combine(Variables.Windir, "cursors", "WindowsRectified"));
            }
            List<string> themefiles = new List<string>
                {
                    "black.theme",
                    "darkcolorized.theme",
                    "darkrectified.theme",
                    "lightrectified.theme"
                };
            for (int i = 0; i < themefiles.Count; i++)
            {
                if (File.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i])))
                {
                    try
                    {
                        File.Delete(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i]));
                    }
                    catch
                    {
                        string name = Path.GetRandomFileName();
                        File.Move(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i]), Path.Combine(Path.GetTempPath(), name));
                        MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    Logger.WriteLine("Deleted " + Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i]));
                }
            }
            if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified")))
            {
                string name = Path.GetRandomFileName();
                Directory.Move(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified"), Path.Combine(Path.GetTempPath(), name));
                if (Directory.Exists(Path.Combine(Path.GetTempPath(), name, "Shell")))
                {
                    try
                    {
                        Directory.Delete(Path.Combine(Path.GetTempPath(), name, "Shell"), true);
                    }
                    catch { }
                }
                var files = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                for (int j = 0; j < files.Length; j++)
                {
                    MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                Logger.WriteLine("Deleted msstyle");
            }
            try
            {
                File.Delete(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"));
            }
            catch
            {
                MoveFileEx(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
            }

            try
            {
                File.Delete(Path.Combine(Variables.r11Folder, "ThemeDll.dll"));
            }
            catch
            {
                MoveFileEx(Path.Combine(Variables.r11Folder, "ThemeDll.dll"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
            }

            // waow
            if (Directory.Exists(Path.Combine(Variables.Windir, "MicaForEveryone")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "MicaForEveryone"), true);
                }
                catch
                {
                    string name = Path.GetRandomFileName();
                    Directory.Move(Path.Combine(Variables.Windir, "MicaForEveryone"), Path.Combine(Path.GetTempPath(), name));
                }
                Logger.WriteLine("Deleted MicaForEveryone");
            }
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
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Rectify11 Control Center.lnk")))
            {
                try
                {
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Rectify11 Control Center.lnk"));
                }
                catch
                {
                    MoveFileEx(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Rectify11 Control Center.lnk"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
            }
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Rectify11 Control Center.lnk")))
            {
                try
                {
                    File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Rectify11 Control Center.lnk"));
                }
                catch { MoveFileEx(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Rectify11 Control Center.lnk"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT); }
            }
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
                    var fil = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                    for (int i = 0; i < fil.Length; i++)
                    {
                        MoveFileEx(fil[i], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
            }
            Logger.WriteLine("Deleted Rectify11 Control Center");
            return true;
        }
        private async Task<bool> UninstallExtras()
        {
            for (int i = 0; i < UninstallOptions.uninstExtrasList.Count; i++)
            {
                if (UninstallOptions.uninstExtrasList[i] == "wallpapersNode")
                {
                    if (Directory.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")))
                    {
                        List<string> wallpapers = new List<string>
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
                            {
                                try
                                {
                                    File.Delete(files[j]);
                                }
                                catch
                                {
                                    MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                                }
                            }
                        }
                        if (Directory.GetFiles(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")).Length == 0)
                        {
                            try
                            {
                                Directory.Delete(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), true);
                            }
                            catch
                            {
                                string name = Path.GetRandomFileName();
                                Directory.Move(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), Path.Combine(Path.GetTempPath(), name));
                                MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                        }
                    }
                    Logger.WriteLine("Uninstalled wallpapers");
                }
                if (UninstallOptions.uninstExtrasList[i] == "asdfNode")
                {
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizer.exe", AppWinStyle.Hide, true));
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizerEleven.exe", AppWinStyle.Hide, true));
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn asdf", AppWinStyle.Hide));
                    if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf.lnk")))
                    {
                        try
                        {
                            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf.lnk"));
                        }
                        catch
                        {
                            File.Move(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf.lnk"), Path.Combine(Path.GetTempPath(), Path.GetTempFileName()));
                        }
                    }
                    if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf11.lnk")))
                    {
                        try
                        {
                            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf11.lnk"));
                        }
                        catch
                        {
                            File.Move(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf11.lnk"), Path.Combine(Path.GetTempPath(), Path.GetTempFileName()));
                        }
                    }
                    if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
                    {
                        try
                        {
                            Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer"), true);
                        }
                        catch
                        {
                            string name = Path.GetRandomFileName();
                            Directory.Move(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer"), Path.Combine(Path.GetTempPath(), name));
                            var files = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                            for (int j = 0; j < files.Length; j++)
                            {
                                MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                            MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                    }
                    // will fail anyways if the folder isnt empty
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras"), true);
                    Logger.WriteLine("Uninstalled asdf");
                }
                if (UninstallOptions.uninstExtrasList[i] == "useravNode")
                {
                    if (Directory.Exists(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures")))
                    {
                        try
                        {
                            Directory.Delete(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"), true);
                        }
                        catch
                        {
                            string name = Path.GetRandomFileName();
                            Directory.Move(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"), Path.Combine(Path.GetTempPath(), name));
                            var fil = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                            for (int j = 0; j < fil.Length; j++)
                            {
                                MoveFileEx(fil[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                            MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                    }
                    Logger.WriteLine("Uninstalled user avatars");
                }
                if (UninstallOptions.uninstExtrasList[i] == "shellNode")
                {
                    if (Directory.Exists(Path.Combine(Variables.Windir, "nilesoft")))
                    {
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AcrylicMenusLoader.exe", AppWinStyle.Hide, true));
                        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk")))
                        {
                            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk"));
                        }
                        ProcessStartInfo shlinfo2 = new()
                        {
                            FileName = Path.Combine(Variables.Windir, "nilesoft", "shell.exe"),
                            WindowStyle = ProcessWindowStyle.Hidden,
                            Arguments = " -u"
                        };
                        try
                        {
                            if (File.Exists(Path.Combine(Variables.Windir, "nilesoft", "shell.dll")))
                            {
                                var shlInstproc2 = Process.Start(shlinfo2);
                                shlInstproc2.WaitForExit();
                            }
                        }
                        catch { }
                        await Task.Run(() => Process.Start(Path.Combine(Variables.sys32Folder, "reg.exe"), " delete \"HKCU\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\" /f"));
                        string name = Path.GetRandomFileName();
                        Directory.Move(Path.Combine(Variables.Windir, "nilesoft"), Path.Combine(Path.GetTempPath(), name));
                        var files = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                        for (int j = 0; j < files.Length; j++)
                        {
                            try
                            {
                                File.Delete(files[j]);
                            }
                            catch
                            {
                                MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                        }
                        var dir = Directory.GetDirectories(Path.Combine(Path.GetTempPath(), name));
                        for (int j = 0; j < dir.Length; j++)
                        {
                            var fil = Directory.GetFiles(dir[j]);
                            for (int k = 0; k < fil.Length; k++)
                            {
                                try
                                {
                                    File.Delete(fil[k]);
                                }
                                catch
                                {
                                    MoveFileEx(fil[k], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                                }
                            }
                            try
                            {
                                Directory.Delete(dir[j], true);
                            }
                            catch
                            {
                                MoveFileEx(dir[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                        }
                        MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        if (!Variables.RestartRequired)
                        {
                            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im explorer.exe", AppWinStyle.Hide, true));
                            await Task.Run(() => Interaction.Shell(Path.Combine(Variables.Windir, "explorer.exe"), AppWinStyle.NormalFocus));
                            Thread.Sleep(3000);
                        }
                        Logger.WriteLine("Uninstalled shell");
                    }
                }
                if (UninstallOptions.uninstExtrasList[i] == "gadgetsNode")
                {
                    ProcessStartInfo gad = new()
                    {
                        FileName = Path.Combine(Variables.sys32Folder, "msiexec.exe"),
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = "/X{A84C39EA-54FE-4CED-B464-97DA9201EB33} /qn"
                    };
                    var vcproc = Process.Start(gad);
                    vcproc.WaitForExit();
                    Logger.WriteLine("Uninstalled shell");
                }
            }
            return true;
        }
    }
}
