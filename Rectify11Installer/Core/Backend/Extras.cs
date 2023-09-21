using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using static Rectify11Installer.Win32.NativeMethods;
using static System.Environment;

namespace Rectify11Installer.Core
{
    internal class Extras
    {
        public static bool Install(FrmWizard frm)
        {
            Logger.WriteLine("Installing Extras");
            Logger.WriteLine("─────────────────");
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras")))
            {
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizer.exe", AppWinStyle.Hide, true);
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizerEleven.exe", AppWinStyle.Hide, true);
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AcrylicMenusLoader.exe", AppWinStyle.Hide, true);
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras"), true);
                    Logger.WriteLine(Path.Combine(Variables.r11Folder, "extras") + " exists. Deleting it.");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error deleting " + Path.Combine(Variables.r11Folder, "extras"), ex);
                }
            }
            Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "extras"));

            if (InstallOptions.InstallWallpaper)
            {
                frm.InstallerProgress = "Installing extras: Wallpapers";
                string path = Path.Combine(Variables.r11Folder, "extras", "wallpapers");
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "wallpapers");

                if (!InstallWallpapers())
                {
                    Logger.WriteLine("InstallWallpapers() failed.");
                    return false;
                }
                Logger.WriteLine("InstallWallpapers() succeeded.");
                Directory.Delete(path, true);
            }
            if (InstallOptions.InstallASDF)
            {
                frm.InstallerProgress = "Installing extras: AccentColorizer";
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
                {
                    Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizer.exe", AppWinStyle.Hide, true);
                    Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizerEleven.exe", AppWinStyle.Hide, true);
                    Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn asdf", AppWinStyle.Hide);

                    string path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "asdf.lnk");
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); }
                        catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())); }
                    }

                    path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "asdf11.lnk");
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); }
                        catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())); }
                    }

                    path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "Accentcolorizer.lnk");
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); }
                        catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())); }
                    }

                    path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "Accentcolorizer11.lnk");
                    if (File.Exists(path))
                    {
                        try { File.Delete(path); }
                        catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())); }
                    }

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

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "AccentColorizer");

                Installasdf();
                Logger.WriteLine("Installasdf() succeeded.");
                if (!Variables.RestartRequired)
                {
                    Interaction.Shell(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizer.exe"), AppWinStyle.Hide);
                    Interaction.Shell(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizerEleven.exe"), AppWinStyle.Hide);
                }
            }
            if (InstallOptions.InstallGadgets)
            {
                frm.InstallerProgress = "Installing extras: Gadgets";
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "GadgetPack")))
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "GadgetPack"), true);
                }

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "GadgetPack");

                InstallGadgets();
                Logger.WriteLine("InstallGadgets() succeeded.");
                Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "GadgetPack"), true);
            }
            if (InstallOptions.InstallShell)
            {
                frm.InstallerProgress = "Installing extras: Enhanced context menu";

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "Nilesoft");
                Helper.SvExtract("extras.7z", "extras", "NilesoftArm64");

                InstallShell();
                Logger.WriteLine("InstallShell() succeeded.");
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "Nilesoft"), true);
                }
                catch { }
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "NilesoftArm64"), true);
                }
                catch { }
            }
            if (InstallOptions.userAvatars)
            {
                frm.InstallerProgress = "Installing extras: User avatars";
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "userAV")))
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "userAV"), true);
                }

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "userAV");

                InstallUserAvatars();
                Logger.WriteLine("InstallUserAvatars() succeeded.");
                Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "userAV"), true);
            }
            Logger.WriteLine("InstallExtras() succeeded.");
            Logger.WriteLine("══════════════════════════════════════════════");
            return true;
        }

        /// <summary>
        /// installs wallpapers
        /// </summary>
        private static bool InstallWallpapers()
        {
            DirectoryInfo walldir = new(Path.Combine(Variables.r11Folder, "extras", "wallpapers"));
            if (!Directory.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                    Logger.WriteLine("Created " + Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error creating " + Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified") + ". " + ex.Message + NewLine + ex.StackTrace + NewLine);
                    return false;
                }
            }
            var files = walldir.GetFiles("*.*");
            for (var i = 0; i < files.Length; i++)
            {
                File.Copy(files[i].FullName, Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified", files[i].Name), true);
            }
            return true;
        }

        /// <summary>
        /// installs asdf
        /// </summary>
        private static void Installasdf()
        {
            //Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn asdf /xml " + Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "asdf.xml"), AppWinStyle.Hide);
            using ShellLink shortcut = new();
            shortcut.Target = Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizer.exe");
            shortcut.WorkingDirectory = @"%windir%\Rectify11\extras\AccentColorizer";
            shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
            shortcut.Save(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "Accentcolorizer.lnk"));

            using ShellLink asdf11 = new();
            asdf11.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
            asdf11.WorkingDirectory = @"%windir%\Rectify11\extras\AccentColorizer";
            asdf11.Target = Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizerEleven.exe");
            asdf11.Save(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "Accentcolorizer11.lnk"));
        }

        /// <summary>
        /// installs gadgets
        /// </summary>
        private static void InstallGadgets()
        {
            // what????
            //Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn gadgets /xml " + Path.Combine(Variables.r11Folder, "extras", "GadgetPack", "gadget.xml"), AppWinStyle.Hide);
            if (File.Exists(Path.Combine(Variables.progfiles, "Windows Sidebar", "sidebar.exe")))
            {
                ProcessStartInfo uns = new()
                {
                    FileName = Path.Combine(Variables.sys32Folder, "msiexec.exe"),
                    WindowStyle = ProcessWindowStyle.Normal,
                    Arguments = "/X{A84C39EA-54FE-4CED-B464-97DA9201EB33} /qn"
                };
                var gaduns = Process.Start(uns);
                gaduns.WaitForExit();
            }
            ProcessStartInfo gad = new()
            {
                FileName = Path.Combine(Variables.sys32Folder, "msiexec.exe"),
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = "/i " + Path.Combine(Variables.r11Folder, "extras", "GadgetPack", "Install.msi") + " /quiet /passive"
            };
            var vcproc = Process.Start(gad);
            vcproc.WaitForExit();
        }

        /// <summary>
        /// installs nilesoft shell
        /// </summary>
        private static bool InstallShell()
        {
            string s = "";
            if (IsArm64()) s = "Arm64";
            if (Directory.Exists(Path.Combine(Variables.Windir, "nilesoft")))
            {
                if (File.Exists(Path.Combine(Variables.Windir, "nilesoft", "shell.exe")))
                {
                    ProcessStartInfo shlinfo3 = new()
                    {
                        FileName = Path.Combine(Variables.Windir, "nilesoft", "shell.exe"),
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Arguments = " -u"
                    };
                    try
                    {
                        var shlInstproc2 = Process.Start(shlinfo3);
                        shlInstproc2.WaitForExit();
                    }
                    catch { }
                }
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AcrylicMenusLoader.exe", AppWinStyle.Hide, true);
                if (File.Exists(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk")))
                {
                    File.Delete(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk"));
                }

                // gonna give it a random folder name
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
            }
            Directory.Move(Path.Combine(Variables.r11Folder, "extras", "nilesoft" + s), Path.Combine(Variables.Windir, "nilesoft"));
            ProcessStartInfo shlinfo2 = new()
            {
                FileName = Path.Combine(Variables.Windir, "nilesoft", "shell.exe"),
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = " -r"
            };
            int num = InstallOptions.CMenuStyle;
            string text = (string)Properties.Resources.ResourceManager.GetObject("config" + num);
            File.WriteAllText(Path.Combine(Variables.Windir, "nilesoft", "shell.nss"), text);
            if (num == 1 || num == 2)
            {
                var shlInstproc2 = Process.Start(shlinfo2);
                shlInstproc2.WaitForExit();
            }
            if (num == 3 || num == 4)
            {
                Process.Start(Path.Combine(Variables.sys32Folder, "reg.exe"), " add \"HKCU\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32\" /f /ve");
            }
            if (num == 4)
            {
                using ShellLink shortcut = new();
                shortcut.Target = Path.Combine(Variables.Windir, "nilesoft", "AcrylicMenus", "AcrylicMenusLoader.exe");
                shortcut.WorkingDirectory = @"%windir%\nilesoft\AcrylicMenus";
                shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
                shortcut.Save(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk"));
            }
            if (!Variables.RestartRequired)
            {
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im explorer.exe", AppWinStyle.Hide, true);
                Interaction.Shell(Path.Combine(Variables.Windir, "explorer.exe"), AppWinStyle.NormalFocus);
                Thread.Sleep(3000);
                if (num == 4) Process.Start(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk"));
            }
            return true;
        }

        /// <summary>
        /// installs User Avatars
        /// </summary>
        private static void InstallUserAvatars()
        {
            if (Directory.Exists(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures")))
            {
                Directory.Delete(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"), true);
            }
            Directory.CreateDirectory(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"));

            DirectoryInfo info = new DirectoryInfo(Path.Combine(Variables.r11Folder, "extras", "UserAV"));
            for (int i = 0; i < info.GetFiles().Length; i++)
            {
                File.Copy(Path.Combine(Variables.r11Folder, "extras", "userAV", info.GetFiles("*.*")[i].Name),
                          Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures", info.GetFiles("*.*")[i].Name), true);
            }
        }
    }
}
