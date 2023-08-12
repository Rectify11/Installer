using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", true);
                frm.InstallerProgress = "Phase2";
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2);
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun);
                try
                {
                    reg.SetValue("UninstallFiles", UninstallOptions.uninstIconsList.ToArray());
                    if (!Variables.Phase2Skip)
                    {
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
                    MoveFileEx(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "aRun.exe"));
                }
                catch
                {
                    MoveFileEx(Path.Combine(Variables.r11Folder, "aRun.exe"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);

                }
            }
            if (UninstallOptions.UninstallThemes)
            {
                frm.InstallerProgress = "Uninstalling themes";
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
                                MoveFileEx(Path.Combine(Variables.Windir, "web", "Wallpaper", "Rectified", wallpapers[i]), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                        }
                    }
                    if (Directory.GetFiles(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")).Length == 0)
                    {
                        try
                        {
                            Directory.Delete(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                        }
                        catch
                        {
                            MoveFileEx(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                    }
                }
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " uninstall", AppWinStyle.Hide, true));
                if (File.Exists(Path.Combine(Variables.Windir, "Themetool.exe")))
                {
                    try
                    {
                        File.Delete(Path.Combine(Variables.Windir, "Themetool.exe"));
                    }
                    catch { MoveFileEx(Path.Combine(Variables.Windir, "Themetool.exe"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT); }
                }
                if (Directory.Exists(Path.Combine(Variables.Windir, "cursors", "WindowsRectified")))
                {
                    try
                    {
                        Directory.Delete(Path.Combine(Variables.Windir, "cursors", "WindowsRectified"), true);
                    }
                    catch
                    {
                        var fil = Directory.GetFiles(Path.Combine(Variables.Windir, "cursors", "WindowsRectified"));
                        for (int i = 0; i<fil.Length; i++)
                        {
                            MoveFileEx(fil[i], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        MoveFileEx(Path.Combine(Variables.Windir, "cursors", "WindowsRectified"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                }
                if (Directory.Exists(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark")))
                {
                    try
                    {
                        Directory.Delete(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark"), true);
                    }
                    catch
                    {
                        var fil = Directory.GetFiles(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark"));
                        for (int i = 0; i < fil.Length; i++)
                        {
                            MoveFileEx(fil[i], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        MoveFileEx(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
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
                            MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i]), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                    }
                }
                if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified")))
                {
                    if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Shell")))
                    {
                        Directory.Delete(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Shell"), true);
                    }
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Aero.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Black.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Dark.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "DarkColorized.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"));
                }
                catch
                {
                    MoveFileEx(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }

                // waow
                if (Directory.Exists(Path.Combine(Variables.Windir, "MicaForEveryone")))
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "MicaForEveryone"), true);
                }
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
                        var fil = Directory.GetFiles(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"));
                        for (int i = 0; i < fil.Length; i++)
                        {
                            MoveFileEx(fil[i], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        MoveFileEx(Path.Combine(Variables.r11Folder, "Rectify11ControlCenter"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                }
            }
            if (UninstallOptions.uninstExtrasList.Count > 0)
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
                                //MessageBox.Show(Path.GetFileName(files[j]));
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
                                    Directory.Delete(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                                }
                                catch
                                {
                                    MoveFileEx(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                                }
                            }
                        }
                    }
                    if (UninstallOptions.uninstExtrasList[i] == "asdfNode")
                    {
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizer.exe", AppWinStyle.Hide, true));
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizerEleven.exe", AppWinStyle.Hide, true));
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /end /tn asdf", AppWinStyle.Hide));
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn asdf", AppWinStyle.Hide));
                        if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
                        {
                            // idk File.Delete cant nuke it
                            var files = Directory.GetFiles(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer"));
                            for (int j = 0; j < files.Length; j++)
                            {
                                MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                            MoveFileEx(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        // will fail anyways if the folder isnt empty
                        MoveFileEx(Path.Combine(Variables.r11Folder, "extras"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
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
                                var fil = Directory.GetFiles(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"));
                                for (int j = 0; j < fil.Length; j++)
                                {
                                    MoveFileEx(fil[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                                }
                                MoveFileEx(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                        }
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
                                var shlInstproc2 = Process.Start(shlinfo2);
                                shlInstproc2.WaitForExit();
                            }
                            catch { }
                            await Task.Run(() => Process.Start(Path.Combine(Variables.sys32Folder, "reg.exe"), " delete \"HKCU\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\" /f"));
                            var files = Directory.GetFiles(Path.Combine(Variables.Windir, "nilesoft"));
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
                            var dir = Directory.GetDirectories(Path.Combine(Variables.Windir, "nilesoft"));
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
                            }
                            MoveFileEx(Path.Combine(Variables.Windir, "nilesoft"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
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
                    }
                }
            }
            // cleanup
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "Tmp"));
                }
                catch { }
            }
            frm.InstallerProgress = "Done, you can close this window";
            return true;
        }
    }
}
