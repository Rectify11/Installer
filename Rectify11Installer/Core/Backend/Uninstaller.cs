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
                await Task.Run(() => Icons.Uninstall());
                Variables.RestartRequired = true;
                Console.WriteLine("══════════════════════════════════════════════");
            }
            if (UninstallOptions.UninstallThemes)
            {
                Logger.WriteLine("Uninstalling themes");
                Logger.WriteLine("───────────────────");
                frm.InstallerProgress = "Uninstalling themes";
                await Task.Run(() => Themes.Uninstall());
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
                            File.Move(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf.lnk"), Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
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
                            File.Move(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "programs", "startup", "asdf11.lnk"), Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
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
