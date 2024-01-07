using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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
            try
            {
                Logger.WriteLine("Installing Extras");
                Logger.WriteLine("─────────────────");

                if (InstallOptions.InstallWallpaper)
                {
                    frm.InstallerProgress = "Installing extras: Wallpapers";

                    if (InstallWallpapers())
                        Logger.WriteLine("InstallWallpapers() succeeded.");
                }
                if (InstallOptions.InstallASDF)
                {
                    frm.InstallerProgress = "Installing extras: AccentColorizer";

                    Installasdf();

                    if (!Variables.RestartRequired)
                    {
                        Interaction.Shell(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizer.exe"), AppWinStyle.Hide);
                        Interaction.Shell(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizerEleven.exe"), AppWinStyle.Hide);
                    }
                }
                if (InstallOptions.InstallGadgets)
                {
                    frm.InstallerProgress = "Installing extras: Gadgets";

                    InstallGadgets();
                    Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras", "GadgetPack"), false);
                }
                if (InstallOptions.InstallShell)
                {
                    frm.InstallerProgress = "Installing extras: Enhanced context menu";

                    InstallShell();

                    Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras", "Nilesoft"), false);
                    Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras", "NilesoftArm64"), false);
                }
                if (InstallOptions.userAvatars)
                {
                    frm.InstallerProgress = "Installing extras: User avatars";

                    InstallUserAvatars();
                    Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras", "userAV"), false);
                }
                Logger.WriteLine("Extras.Install() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Extras.Install() failed", ex);
                return false;
            }
        }

        public static bool Uninstall()
        {
            try
            {
                for (int i = 0; i < UninstallOptions.uninstExtrasList.Count; i++)
                {
                    if (UninstallOptions.uninstExtrasList[i] == "wallpapersNode")
                        UninstallWallpapers();
                    if (UninstallOptions.uninstExtrasList[i] == "asdfNode")
                        UninstallAsdf();
                    if (UninstallOptions.uninstExtrasList[i] == "useravNode")
                        UninstallUserAv();
                    if (UninstallOptions.uninstExtrasList[i] == "shellNode")
                        UninstallShell();
                    if (UninstallOptions.uninstExtrasList[i] == "gadgetsNode")
                        UninstallGadgets();
                }
                Logger.WriteLine("Extras.Uninstall() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Extras.Uninstall() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// installs wallpapers
        /// </summary>
        private static bool InstallWallpapers()
        {
            try
            {
                UninstallWallpapers();

                string path = Path.Combine(Variables.r11Folder, "extras", "wallpapers");
                Helper.SafeDirectoryDeletion(path, false);

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "wallpapers");

                DirectoryInfo walldir = new(Path.Combine(Variables.r11Folder, "extras", "wallpapers"));
                try
                {
                    Directory.CreateDirectory(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                    Logger.WriteLine("Created " + Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"));
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error creating " + Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), ex);
                    return false;
                }
                var files = walldir.GetFiles("*.*");
                for (var i = 0; i < files.Length; i++)
                {
                    File.Copy(files[i].FullName, Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified", files[i].Name), true);
                }
                Helper.SafeDirectoryDeletion(path, false);

                Logger.WriteLine("InstallWallpapers() succeeded");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("InstallWallpapers() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// installs asdf
        /// </summary>
        private static bool Installasdf()
        {
            try
            {
                UninstallAsdf();
                Helper.SvExtract("extras.7z", "extras", "AccentColorizer");
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

                Logger.WriteLine("Installasdf() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                UninstallAsdf();
                Logger.WriteLine("Installasdf() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// installs gadgets
        /// </summary>
        private static void InstallGadgets()
        {
            try
            {
                UninstallGadgets();
                Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras", "GadgetPack"), false);
                string gadgetsnewfolder = Path.Combine(Variables.r11Folder, "extras", "GadgetPack", "Gadgets");

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "GadgetPack");

                ProcessStartInfo gad = new()
                {
                    FileName = Path.Combine(Variables.sys32Folder, "msiexec.exe"),
                    WindowStyle = ProcessWindowStyle.Normal,
                    Arguments = "/i " + Path.Combine(Variables.r11Folder, "extras", "GadgetPack", "Install.msi") + " /quiet /passive"
                };
                var vcproc = Process.Start(gad);
                vcproc.WaitForExit();

                // Copy contents to %localappdata%\Microsoft\Windows Sidebar\Gadgets
                string localAppDataGadgetsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows Sidebar", "Gadgets");
                if (!Directory.Exists(localAppDataGadgetsFolder))
                {
                    Directory.CreateDirectory(localAppDataGadgetsFolder);
                }

                // Copy contents
                string[] gadgetFiles = Directory.GetFiles(gadgetsnewfolder);
                foreach (string gadgetFile in gadgetFiles)
                {
                    string destinationFile = Path.Combine(localAppDataGadgetsFolder, Path.GetFileName(gadgetFile));
                    File.Copy(gadgetFile, destinationFile, true);
                }

                Logger.WriteLine("InstallGadgets() succeeded.");
            }
            catch (Exception ex)
            {
                UninstallGadgets();
                Logger.WriteLine("InstallGadgets() failed", ex);
            }
        }

        /// <summary>
        /// installs nilesoft shell
        /// </summary>
        private static bool InstallShell()
        {
            try
            {
                UninstallShell();
                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "Nilesoft");
                Helper.SvExtract("extras.7z", "extras", "NilesoftArm64");

                string s = "";
                if (IsArm64()) s = "Arm64";
                Directory.Move(Path.Combine(Variables.r11Folder, "extras", "nilesoft" + s), Path.Combine(Variables.Windir, "nilesoft"));
                ProcessStartInfo shlinfo2 = new()
                {
                    FileName = Path.Combine(Variables.Windir, "nilesoft", "shell.exe"),
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = " -r"
                };
                int num = InstallOptions.CMenuStyle;
                if (num == 1 || num == 2)
                {
					string text = (string)Properties.Resources.ResourceManager.GetObject("config" + num);
					File.WriteAllText(Path.Combine(Variables.Windir, "nilesoft", "shell.nss"), text);
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

                Logger.WriteLine("InstallShell() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                UninstallShell();
                Logger.WriteLine("InstallShell() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// installs User Avatars
        /// </summary>
        private static bool InstallUserAvatars()
        {
            try
            {
                UninstallUserAv();
                Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras", "userAV"), false);

                // extract the 7z
                Helper.SvExtract("extras.7z", "extras", "userAV");

                Directory.CreateDirectory(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"));

                DirectoryInfo info = new DirectoryInfo(Path.Combine(Variables.r11Folder, "extras", "UserAV"));
                for (int i = 0; i < info.GetFiles().Length; i++)
                {
                    File.Copy(Path.Combine(Variables.r11Folder, "extras", "userAV", info.GetFiles("*.*")[i].Name),
                              Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures", info.GetFiles("*.*")[i].Name), true);
                }

                Logger.WriteLine("InstallUserAvatars() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                UninstallUserAv();
                Logger.WriteLine("InstallUserAvatars() failed", ex);
                return false;
            }
        }

        private static bool UninstallWallpapers()
        {
            try
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
                            Helper.SafeFileDeletion(files[j]);
                        }
                    }
                    if (Directory.GetFiles(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")).Length == 0)
                    {
                        Helper.SafeDirectoryDeletion(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified"), false);
                    }
                }

                Logger.WriteLine("UninstallWallpapers() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("UninstallWallpapers() failed", ex);
                return false;
            }
        }
        private static bool UninstallAsdf()
        {
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
            {
                Helper.KillProcess("AccentColorizer.exe");
                Helper.KillProcess("AccentColorizerEleven.exe");
                Helper.DeleteTask("asdf");

                string[] fils = { "asdf.lnk", "asdf11.lnk", "Accentcolorizer.lnk", "Accentcolorizer11.lnk" };
                for (int i = 0; i < fils.Length; i++)
                {
                    string path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", fils[i]);
                    Helper.SafeFileDeletion(path);
                }
                try
                {
                    Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer"), false);
                }
                catch { return false; }

                string epath = Path.Combine(Variables.r11Folder, "extras");
                if (Directory.GetDirectories(epath).Length == 0
                    && Directory.GetFiles(epath).Length == 0)
                {
                    Helper.SafeDirectoryDeletion(epath, false);
                }

                Logger.WriteLine("UninstallAsdf() succeeded.");
                return true;
            }
            return true;
        }
        private static bool UninstallGadgets()
        {
            try
            {
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

                Logger.WriteLine("UninstallGadgets() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("UninstallGadgets() failed", ex);
                return false;
            }
        }
        private static bool UninstallShell()
        {
            try
            {
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
                    Helper.KillProcess("AcrylicMenusLoader.exe");

                    string path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk");
                    if (!Helper.SafeFileDeletion(path))
                        return false;

                    // restore win11 menus
                    Process.Start(Path.Combine(Variables.sys32Folder, "reg.exe"), " delete \"HKCU\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\" /f");

                    if (!Helper.SafeDirectoryDeletion(Path.Combine(Variables.Windir, "nilesoft"), false))
                        return false;

                    if (!Variables.RestartRequired && Variables.IsUninstall)
                    {
                        Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im explorer.exe", AppWinStyle.Hide, true);
                        Interaction.Shell(Path.Combine(Variables.Windir, "explorer.exe"), AppWinStyle.NormalFocus);
                        Thread.Sleep(3000);
                    }
                }
                Logger.WriteLine("UninstallShell() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("UninstallShell() failed.", ex);
                return false;
            }
        }
        private static bool UninstallUserAv()
        {
            try
            {
                Helper.SafeDirectoryDeletion(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"), false);
                Logger.WriteLine("UninstallUserAv() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("UninstallUserAv() failed", ex);
                return false;
            }
        }
    }
}
