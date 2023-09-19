using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Rectify11Installer.Win32;
using static System.Environment;
using KPreisser.UI;
using static Rectify11Installer.Win32.NativeMethods;
using System.Threading;

namespace Rectify11Installer.Core
{
    public class Installer
    {
        #region Variables
        private string newhardlink;
        private enum PatchType
        {
            General = 0,
            Mui,
            Troubleshooter,
            x86
        }
        #endregion
        #region Public Methods
        public async Task<bool> Install(FrmWizard frm)
        {
            Logger.WriteLine("Preparing Installation");
            Logger.WriteLine("──────────────────────");

            if (!Directory.Exists(Variables.r11Folder)) 
                Directory.CreateDirectory(Variables.r11Folder);

            // goofy fix
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)
                ?.CreateSubKey("Rectify11", true)
                ?.DeleteValue("x86PendingFiles", false); 

            if (!Common.WriteFiles(false, false))
            {
                Logger.WriteLine("WriteFiles() failed.");
                return false;
            }
            Logger.WriteLine("WriteFiles() succeeded.");

            if (!Common.CreateDirs())
            {
                Logger.WriteLine("CreateDirs() failed.");
                return false;
            }
            Logger.WriteLine("CreateDirs() succeeded.");

            try
            {
                // create restore point
                frm.InstallerProgress = "Begin creating a restore point";
                CreateSystemRestorePoint(false);
            }
            catch
            {
                Logger.Warn("Error creating a restore point.");
            }

            // runtimes
            frm.InstallerProgress = "Installing runtimes";
            if (!Common.InstallRuntimes())
            {
                Logger.WriteLine("InstallRuntimes() failed.");
                return false;
            }
            if (Variables.vcRedist && Variables.core31)
            {
                Logger.WriteLine("InstallRuntimes() succeeded.");
            }
            else if (!Variables.vcRedist)
            {
                Logger.Warn("vcredist.exe installation failed.");
                Common.RuntimeInstallError("Visual C++ Runtime", "Visual C++ Runtime is used for MicaForEveryone and AccentColorizer.", "https://aka.ms/vs/17/release/vc_redist.x64.exe");
            }
            else if (!Variables.core31)
            {
                Logger.Warn("core31.exe installation failed.");
                Common.RuntimeInstallError(".NET Core 3.1", ".NET Core 3.1 is used for MicaForEveryone.", "https://dotnet.microsoft.com/en-us/download/dotnet/3.1");
            }
            Logger.WriteLine("══════════════════════════════════════════════");

            // some random issue where the installer's frame gets extended
            if (!Theme.IsUsingDarkMode) DarkMode.UpdateFrame(frm, false);


            // theme
            if (InstallOptions.InstallThemes)
            {
                frm.InstallerProgress = "Installing Themes";
                if (!Themes.Install()) return false;
            }

            // extras
            if (InstallOptions.InstallExtras())
            {
                frm.InstallerProgress = "Installing extras";
                Logger.WriteLine("Installing Extras");
                Logger.WriteLine("─────────────────");
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras")))
                {
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizer.exe", AppWinStyle.Hide, true));
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizerEleven.exe", AppWinStyle.Hide, true));
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AcrylicMenusLoader.exe", AppWinStyle.Hide, true));
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
                    await Task.Run(() => Helper.SvExtract("extras.7z", "extras", "wallpapers"));

                    if (!await Task.Run(() => InstallWallpapers()))
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
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizer.exe", AppWinStyle.Hide, true));
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AccentColorizerEleven.exe", AppWinStyle.Hide, true));
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn asdf", AppWinStyle.Hide));

                        string path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "asdf.lnk");
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); }
                            catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetTempFileName())); }
                        }

                        path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "asdf11.lnk");
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); }
                            catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetTempFileName())); }
                        }

                        path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "Accentcolorizer.lnk");
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); }
                            catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetTempFileName())); }
                        }

                        path = Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "Accentcolorizer11.lnk");
                        if (File.Exists(path))
                        {
                            try { File.Delete(path); }
                            catch { File.Move(path, Path.Combine(Path.GetTempPath(), Path.GetTempFileName())); }
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
                    await Task.Run(() => Helper.SvExtract("extras.7z", "extras", "AccentColorizer"));

                    await Task.Run(() => Installasdf());
                    Logger.WriteLine("Installasdf() succeeded.");
                    if (!Variables.RestartRequired)
                    {
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizer.exe"), AppWinStyle.Hide));
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizerEleven.exe"), AppWinStyle.Hide));
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
                    await Task.Run(() => Helper.SvExtract("extras.7z", "extras", "GadgetPack"));

                    await Task.Run(() => InstallGadgets());
                    Logger.WriteLine("InstallGadgets() succeeded.");
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "GadgetPack"), true);
                }
                if (InstallOptions.InstallShell)
                {
                    frm.InstallerProgress = "Installing extras: Enhanced context menu";

                    // extract the 7z
                    await Task.Run(() => Helper.SvExtract("extras.7z", "extras", "Nilesoft"));
                    await Task.Run(() => Helper.SvExtract("extras.7z", "extras", "NilesoftArm64"));

                    await Task.Run(InstallShell);
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
                    await Task.Run(() => Helper.SvExtract("extras.7z", "extras", "userAV"));

                    await Task.Run(() => InstallUserAvatars());
                    Logger.WriteLine("InstallUserAvatars() succeeded.");
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "userAV"), true);
                }
                /*
                if (InstallOptions.InstallSounds)
				{
					await Task.Run(() => InstallSounds());
					Logger.WriteLine("InstallSounds() succeeded.");
				}
				*/
                Logger.WriteLine("InstallExtras() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
            }

            // Icons
            if (InstallOptions.iconsList.Count > 0)
            {
                Logger.WriteLine("Installing icons");
                Logger.WriteLine("────────────────");
                // extract files, delete if folder exists
                frm.InstallerProgress = "Extracting files...";
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "files")))
                {
                    try
                    {
                        Directory.Delete(Path.Combine(Variables.r11Folder, "files"), true);
                        Logger.WriteLine(Path.Combine(Variables.r11Folder, "files") + " exists. Deleting it.");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine("Error deleting " + Path.Combine(Variables.r11Folder, "files"), ex);
                    }
                }
                try
                {
                    File.WriteAllBytes(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files7z);
                    Logger.LogFile("files.7z");
                }
                catch (Exception ex)
                {
                    Logger.LogFile("files.7z", ex);
                    return false;
                }

                // extract the 7z
                await Task.Run(() => Helper.SvExtract("files.7z", "files"));
                Logger.WriteLine("Extracted files.7z");

                // Get all patches
                var patches = PatchesParser.GetAll();
                var patch = patches.Items;
                decimal progress = 0;
                List<string> fileList = new();
                List<string> x86List = new();
                for (var i = 0; i < patch.Length; i++)
                {
                    for (var j = 0; j < InstallOptions.iconsList.Count; j++)
                    {
                        if (patch[i].Mui.Contains(InstallOptions.iconsList[j]))
                        {
                            var number = Math.Round((progress / InstallOptions.iconsList.Count) * 100m);
                            frm.InstallerProgress = "Patching " + patch[i].Mui + " (" + number + "%)";
                            if (!await Task.Run(() => MatchAndApplyRule(patch[i])))
                            {
                                Logger.Warn("MatchAndApplyRule() on " + patch[i].Mui + " failed");
                            }
                            else
                            {
                                fileList.Add(patch[i].HardlinkTarget);
                                if (!string.IsNullOrWhiteSpace(patch[i].x86))
                                {
                                    x86List.Add(patch[i].HardlinkTarget);
                                }
                            }
                            progress++;
                        }
                    }
                }
                Logger.WriteLine("MatchAndApplyRule() succeeded");

                if (!await Task.Run(() => WritePendingFiles(fileList, x86List)))
                {
                    Logger.WriteLine("WritePendingFiles() failed");
                    return false;
                }
                Logger.WriteLine("WritePendingFiles() succeeded");

                if (!await Task.Run(() => Common.WriteFiles(true, false)))
                {
                    Logger.WriteLine("WriteFiles() failed");
                    return false;
                }
                Logger.WriteLine("WriteFiles() succeeded");

                frm.InstallerProgress = "Replacing files";

                // runs only if SSText3D.scr is selected
                if (InstallOptions.iconsList.Contains("SSText3D.scr"))
                {
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "screensaver.reg"), AppWinStyle.Hide));
                    Logger.WriteLine("screensaver.reg succeeded");
                }

                // runs only if any one of mmcbase.dll.mun, mmc.exe.mui or mmcndmgr.dll.mun is selected
                if (InstallOptions.iconsList.Contains("mmcbase.dll.mun")
                    || InstallOptions.iconsList.Contains("mmc.exe.mui")
                    || InstallOptions.iconsList.Contains("mmcndmgr.dll.mun"))
                {
                    if (!await Task.Run(() => MMCHelper.PatchAll()))
                    {
                        Logger.WriteLine("MmcHelper.PatchAll() failed");
                        return false;
                    }
                    Logger.WriteLine("MmcHelper.PatchAll() succeeded");
                }

                if (InstallOptions.iconsList.Contains("odbcad32.exe"))
                {
                    if (!await Task.Run(() => FixOdbc()))
                    {
                        Logger.Warn("FixOdbc() failed");
                    }
                    else
                    {
                        Logger.WriteLine("FixOdbc() succeeded");
                    }
                }
                // phase 2
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun.exe")
                    + " /EXEFilename " + '"' + Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe") + '"'
                    + " /CommandLine " + "\'" + "/install" + "\'"
                    + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true));

                // reg files for various file extensions
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "icons.reg"), AppWinStyle.Hide));
                Logger.WriteLine("icons.reg succeeded");

                Variables.RestartRequired = true;
            }

            frm.InstallerProgress = "Creating uninstaller";
            if (!Common.CreateUninstall())
            {
                Logger.WriteLine("CreateUninstall() failed");
                return false;
            }
            Logger.WriteLine("CreateUninstall() succeeded");

            InstallStatus.IsRectify11Installed = true;
            Logger.WriteLine("══════════════════════════════════════════════");

            try
            {
                // create restore point
                frm.InstallerProgress = "End creating a restore point";
                await Task.Run(() => CreateSystemRestorePoint(true));
            }
            catch
            {
                //ignored
            }

            // cleanup
            frm.InstallerProgress = "Cleaning up...";
            Logger.WriteLine("Cleaning up");
            Logger.WriteLine("───────────");
            if (!await Task.Run(() => Common.Cleanup()))
            {
                Logger.WriteLine("Cleanup() failed");
                return false;
            }
            Logger.WriteLine("Cleanup() succeeded");
            Logger.WriteLine("══════════════════════════════════════════════");
            Logger.CommitLog();
            return true;
        }

        #endregion
        #region Private Methods

        /// <summary>
        /// fixes 32-bit odbc shortcut icon
        /// </summary>
        public bool FixOdbc()
        {
            var filename = string.Empty;
            var admintools = Path.Combine(Environment.GetFolderPath(SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Administrative Tools");
            var files = Directory.GetFiles(admintools);
            for (var i = 0; i < files.Length; i++)
            {
                if (!Path.GetFileName(files[i]).Contains("ODBC") ||
                    !Path.GetFileName(files[i])!.Contains("32")) continue;
                filename = Path.GetFileName(files[i]);
                File.Delete(files[i]);
            }
            try
            {
                using ShellLink shortcut = new();
                shortcut.Target = Path.Combine(Variables.sysWOWFolder, "odbcad32.exe");
                shortcut.WorkingDirectory = @"%windir%\system32";
                shortcut.IconPath = Path.Combine(Variables.sys32Folder, "odbcint.dll");
                shortcut.IconIndex = 0;
                shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
                if (filename != null) shortcut.Save(Path.Combine(admintools, filename));
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// installs wallpapers
        /// </summary>
        private bool InstallWallpapers()
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
        private void Installasdf()
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
        private void InstallGadgets()
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
        private async Task<bool> InstallShell()
        {
            string s = "";
            if (NativeMethods.IsArm64()) s = "Arm64";
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
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im AcrylicMenusLoader.exe", AppWinStyle.Hide, true));
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
                await Task.Run(() => Process.Start(Path.Combine(Variables.sys32Folder, "reg.exe"), " add \"HKCU\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32\" /f /ve"));
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
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im explorer.exe", AppWinStyle.Hide, true));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.Windir, "explorer.exe"), AppWinStyle.NormalFocus));
                Thread.Sleep(3000);
                if (num == 4) await Task.Run(() => Process.Start(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "acrylmenu.lnk")));
            }
            return true;
        }

        /// <summary>
        /// installs User Avatars
        /// </summary>
        private void InstallUserAvatars()
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

        /// <summary>
        /// sets required registry values for phase 2
        /// </summary>
        /// <param name="fileList">normal files list</param>
        /// <param name="x86List">32-bit files list</param>
        private bool WritePendingFiles(List<string> fileList, List<string> x86List)
        {
            using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", true);
            if (reg == null) return false;
            try
            {
                reg.SetValue("PendingFiles", fileList.ToArray());
                Logger.WriteLine("Wrote filelist to PendingFiles");
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error writing filelist to PendingFiles", ex);
                return false;
            }

            if (x86List.Count != 0)
            {
                try
                {
                    reg.SetValue("x86PendingFiles", x86List.ToArray());
                    Logger.WriteLine("Wrote x86list to x86PendingFiles");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error writing x86list to x86PendingFiles", ex);
                    return false;
                }
            }
            try
            {
                reg.SetValue("Language", CultureInfo.CurrentUICulture.Name);
                Logger.WriteLine("Wrote CurrentUICulture.Name to Language");
            }
            catch (Exception ex)
            {
                Logger.Warn("Error writing CurrentUICulture.Name to Language", ex);
            }
            try
            {
                reg.SetValue("Version", Assembly.GetEntryAssembly()?.GetName().Version);
                Logger.WriteLine("Wrote ProductVersion to Version");
            }
            catch (Exception ex)
            {
                Logger.Warn("Error writing ProductVersion to Version", ex);
            }


            try
            {
                reg?.SetValue("WindowsUpdate", Variables.WindowsUpdate ? 1 : 0);
                string sr = Variables.WindowsUpdate ? "1" : "0";
                Logger.WriteLine("Wrote " + sr + "to WindowsUpdate");
            }
            catch (Exception ex)
            {
                Logger.Warn("Error writing to WindowsUpdate", ex);
            }

            try
            {
                // mane fuck this shit
                using var ubrReg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
                reg.SetValue("OSVersion", OSVersion.Version.Major + "." + OSVersion.Version.Minor + "." + OSVersion.Version.Build + "." + ubrReg.GetValue("UBR").ToString());
                Logger.WriteLine("Wrote OSVersion");
            }
            catch (Exception ex)
            {
                Logger.Warn("Error writing OSVersion", ex);
            }
            return true;
        }

        /// <summary>
        /// Patches a specific file
        /// </summary>
        /// <param name="file">The file to be patched</param>
        /// <param name="patch">Xml element containing all the info</param>
        /// <param name="type">The type of the file to be patched.</param>
        private static bool Patch(string file, PatchesPatch patch, PatchType type)
        {
            if (File.Exists(file))
            {
                string name;
                string backupfolder;
                string tempfolder;
                if (type == PatchType.Troubleshooter)
                {
                    name = patch.Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
                    backupfolder = Path.Combine(Variables.r11Folder, "backup", "Diag");
                    tempfolder = Path.Combine(Variables.r11Folder, "Tmp", "Diag");
                }
                else if (type == PatchType.x86)
                {
                    var ext = Path.GetExtension(patch.Mui);
                    name = Path.GetFileNameWithoutExtension(patch.Mui) + "86" + ext;
                    backupfolder = Path.Combine(Variables.r11Folder, "backup");
                    tempfolder = Path.Combine(Variables.r11Folder, "Tmp");
                }
                else
                {
                    name = patch.Mui;
                    backupfolder = Path.Combine(Variables.r11Folder, "backup");
                    tempfolder = Path.Combine(Variables.r11Folder, "Tmp");
                }

                if (string.IsNullOrWhiteSpace(name))
                {
                    return false;
                }

                if (type == PatchType.Troubleshooter)
                {
                    if (!Directory.Exists(backupfolder))
                    {
                        Directory.CreateDirectory(backupfolder);
                    }
                    if (!Directory.Exists(tempfolder))
                    {
                        Directory.CreateDirectory(tempfolder);
                    }
                }

                //File.Copy(file, Path.Combine(backupfolder, name));
                File.Copy(file, Path.Combine(tempfolder, name), true);

                var filename = name + ".res";
                var masks = patch.mask;
                string filepath;
                if (type == PatchType.Troubleshooter)
                {
                    filepath = Path.Combine(Variables.r11Files, "Diag");
                }
                else
                {
                    filepath = Variables.r11Files;
                }

                if (patch.mask.Contains("|"))
                {
                    if (!string.IsNullOrWhiteSpace(patch.Ignore) && ((!string.IsNullOrWhiteSpace(patch.MinVersion) && OSVersion.Version.Build <= Int32.Parse(patch.MinVersion)) || (!string.IsNullOrWhiteSpace(patch.MaxVersion) && OSVersion.Version.Build >= Int32.Parse(patch.MaxVersion))))
                    {
                        masks = masks.Replace(patch.Ignore, "");
                    }
                    var str = masks.Split('|');
                    for (var i = 0; i < str.Length; i++)
                    {
                        if (type == PatchType.x86)
                        {
                            filename = Path.GetFileNameWithoutExtension(name).Remove(Path.GetFileNameWithoutExtension(name).Length - 2, 2) + Path.GetExtension(name) + ".res";
                        }
                        if (type != PatchType.Mui)
                        {
                            Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
                            " -open " + Path.Combine(tempfolder, name) +
                            " -save " + Path.Combine(tempfolder, name) +
                            " -action " + "delete" +
                            " -mask " + str[i], AppWinStyle.Hide, true);
                        }
                        Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
                        " -open " + Path.Combine(tempfolder, name) +
                        " -save " + Path.Combine(tempfolder, name) +
                        " -action " + "addskip" +
                        " -resource " + Path.Combine(filepath, filename) +
                        " -mask " + str[i], AppWinStyle.Hide, true);
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(patch.Ignore) && ((!string.IsNullOrWhiteSpace(patch.MinVersion) && OSVersion.Version.Build <= Int32.Parse(patch.MinVersion)) || (!string.IsNullOrWhiteSpace(patch.MaxVersion) && OSVersion.Version.Build >= Int32.Parse(patch.MaxVersion))))
                    {
                        masks = masks.Replace(patch.Ignore, "");
                    }
                    if (type == PatchType.x86)
                    {
                        filename = Path.GetFileNameWithoutExtension(name).Remove(Path.GetFileNameWithoutExtension(name).Length - 2, 2) + Path.GetExtension(name) + ".res";
                    }
                    if (type != PatchType.Mui)
                    {
                        Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
                             " -open " + Path.Combine(tempfolder, name) +
                             " -save " + Path.Combine(tempfolder, name) +
                             " -action " + "delete" +
                             " -mask " + masks, AppWinStyle.Hide, true);
                    }
                    Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
                            " -open " + Path.Combine(tempfolder, name) +
                            " -save " + Path.Combine(tempfolder, name) +
                            " -action " + "addskip" +
                            " -resource " + Path.Combine(filepath, filename) +
                            " -mask " + masks, AppWinStyle.Hide, true);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Replaces the path and patches the file accordingly.
        /// </summary>
        /// <param name="patch">Xml element containing all the info</param>
        private bool MatchAndApplyRule(PatchesPatch patch)
        {
            if (patch.HardlinkTarget.Contains("%sys32%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%sys32%", Variables.sys32Folder);
                if (!Patch(newhardlink, patch, PatchType.General))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%lang%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
                if (!Patch(newhardlink, patch, PatchType.Mui))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%en-US%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
                if (!Patch(newhardlink, patch, PatchType.Mui))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%windirLang%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%windirLang%", Path.Combine(Variables.Windir, CultureInfo.CurrentUICulture.Name));
                if (!Patch(newhardlink, patch, PatchType.Mui))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%windirEn-US%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%windirEn-US%", Path.Combine(Variables.Windir, "en-US"));
                if (!Patch(newhardlink, patch, PatchType.Mui))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("mun"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%sysresdir%", Variables.sysresdir);
                if (!Patch(newhardlink, patch, PatchType.General))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%branding%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%branding%", Variables.BrandingFolder);
                if (!Patch(newhardlink, patch, PatchType.General))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%prog%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%prog%", Variables.progfiles);
                if (!Patch(newhardlink, patch, PatchType.General))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%diag%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%diag%", Variables.diag);
                if (!Patch(newhardlink, patch, PatchType.Troubleshooter))
                {
                    return false;
                }
            }
            else if (patch.HardlinkTarget.Contains("%windir%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%windir%", Variables.Windir);
                if (!Patch(newhardlink, patch, PatchType.General))
                {
                    return false;
                }
            }
            if (!string.IsNullOrWhiteSpace(patch.x86))
            {
                if (patch.HardlinkTarget.Contains("%sys32%"))
                {
                    newhardlink = patch.HardlinkTarget.Replace(@"%sys32%", Variables.sysWOWFolder);
                    if (!Patch(newhardlink, patch, PatchType.x86))
                    {
                        return false;
                    }
                }
                else if (patch.HardlinkTarget.Contains("%prog%"))
                {
                    newhardlink = patch.HardlinkTarget.Replace(@"%prog%", Variables.progfiles86);
                    if (!Patch(newhardlink, patch, PatchType.x86))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    #endregion
}
