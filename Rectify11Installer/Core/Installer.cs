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

            // goofy fix
            using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", true);
            reg.DeleteValue("x86PendingFiles", false);
            reg.Dispose();

            if (!await Task.Run(() => WriteFiles(false, false)))
            {
                Logger.WriteLine("WriteFiles() failed.");
                return false;
            }
            Logger.WriteLine("WriteFiles() succeeded.");

            if (!await Task.Run(() => CreateDirs()))
            {
                Logger.WriteLine("CreateDirs() failed.");
                return false;
            }
            Logger.WriteLine("CreateDirs() succeeded.");

            // backup
            try
            {
                frm.InstallerProgress = "Creating uninstaller";
                await Task.Run(() => File.Copy(Assembly.GetExecutingAssembly().Location, Path.Combine(Variables.r11Folder, "Uninstall.exe"), true));
                Logger.WriteLine("Installer copied to " + Path.Combine(Variables.r11Folder, "Uninstall.exe"));
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error while copying installer", ex);
            }
            // create restore point
            frm.InstallerProgress = "Creating a restore point";
            await Task.Run(() => NativeMethods.CreateSystemRestorePoint());

            frm.InstallerProgress = "Installing runtimes";
            if (!await Task.Run(InstallRuntimes))
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
                RuntimeInstallError("Visual C++ Runtime", "Visual C++ Runtime is used for MicaForEveryone and AccentColorizer.", "https://aka.ms/vs/17/release/vc_redist.x64.exe");
            }
            else if (!Variables.core31)
            {
                Logger.Warn("core31.exe installation failed.");
                RuntimeInstallError(".NET Core 3.1", ".NET Core 3.1 is used for MicaForEveryone.", "https://dotnet.microsoft.com/en-us/download/dotnet/3.1");
            }
            Logger.WriteLine("══════════════════════════════════════════════");
            if (!Theme.IsUsingDarkMode)
            {
                DarkMode.UpdateFrame(frm, false);
            }
            // theme
            if (InstallOptions.InstallThemes)
            {
                frm.InstallerProgress = "Installing Themes";
                Logger.WriteLine("Installing Themes");
                Logger.WriteLine("─────────────────");
                if (!await Task.Run(() => WriteFiles(false, true)))
                {
                    Logger.WriteLine("WriteFiles() failed.");
                    return false;
                }
                Logger.WriteLine("WriteFiles() succeeded.");
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im micaforeveryone.exe", AppWinStyle.Hide, true));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im micafix.exe", AppWinStyle.Hide, true));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im explorerframe.exe", AppWinStyle.Hide, true));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /end /tn mfe", AppWinStyle.Hide));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /end /tn micafix", AppWinStyle.Hide));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn mfe", AppWinStyle.Hide));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn micafix", AppWinStyle.Hide));
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "themes")))
                {
                    try
                    {
                        Logger.WriteLine(Path.Combine(Variables.r11Folder, "themes") + " exists. Deleting it.");
                        await Task.Run(() => Directory.Delete(Path.Combine(Variables.r11Folder, "themes"), true));
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine("Deleting " + Path.Combine(Variables.r11Folder, "themes") + " failed. ", ex);
                    }
                }
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -o" + Path.Combine(Variables.r11Folder, "themes") +
                        " " + Path.Combine(Variables.r11Folder, "themes.7z"), AppWinStyle.Hide, true));
                Logger.WriteLine("Extracted themes.7z");
                if (!await Task.Run(() => InstallThemes()))
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
                            try
                            {
                                await Task.Run(() => Directory.Delete(Path.Combine(Variables.Windir, "MicaForEveryone"), true));
                            }
                            catch
                            {
                                // tf you doing
                                if (Directory.Exists(Path.Combine(Path.GetTempPath(), "MicaForEveryone")))
                                {
                                    try
                                    {
                                        Directory.Delete(Path.Combine(Path.GetTempPath(), "MicaForEveryone"), true);
                                    }
                                    catch { }
                                }
                                Directory.Move(Path.Combine(Variables.Windir, "MicaForEveryone"), Path.Combine(Path.GetTempPath(), "MicaForEveryone"));
                            }
                        }
                        await Task.Run(() => Directory.Move(Path.Combine(Variables.r11Folder, "Themes", "MicaForEveryone"), Path.Combine(Variables.Windir, "MicaForEveryone")));
                        await Task.Run(() => InstallMfe());
                        Logger.WriteLine("InstallMfe() succeeded.");
                    }
                }
                catch
                {
                    Logger.WriteLine("InstallMfe() failed.");
                }
                try
                {
                    await Task.Run(() => Installr11cpl());
                    Logger.WriteLine("Installr11cpl() succeeded.");
                }
                catch
                {
                    Logger.WriteLine("Installr11cpl() failed.");
                }
                Variables.RestartRequired = true;
                Logger.WriteLine("InstallThemes() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
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
                /*
				await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
						" x -o" + Path.Combine(Variables.r11Folder, "extras") +
						" " + Path.Combine(Variables.r11Folder, "extras.7z"), AppWinStyle.Hide, true));
				*/

                Logger.WriteLine("Extracted extras.7z");

                if (InstallOptions.InstallWallpaper)
                {
                    frm.InstallerProgress = "Installing extras: Wallpapers";

                    if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "wallpapers")))
                    {
                        Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "wallpapers"), true);
                    }
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -y " + Path.Combine(Variables.r11Folder, "extras.7z")
                        + " -o\"" + Path.Combine(Variables.r11Folder, "extras") + "\""
                        + " wallpapers", AppWinStyle.Hide, true));
                    if (!await Task.Run(() => InstallWallpapers()))
                    {
                        Logger.WriteLine("InstallWallpapers() failed.");
                        return false;
                    }
                    Logger.WriteLine("InstallWallpapers() succeeded.");
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "wallpapers"), true);
                }
                if (InstallOptions.InstallASDF)
                {
                    frm.InstallerProgress = "Installing extras: AccentColorizer";
                    if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
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
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -y " + Path.Combine(Variables.r11Folder, "extras.7z")
                        + " -o\"" + Path.Combine(Variables.r11Folder, "extras") + "\""
                        + " AccentColorizer", AppWinStyle.Hide, true));
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
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -y " + Path.Combine(Variables.r11Folder, "extras.7z")
                        + " -o\"" + Path.Combine(Variables.r11Folder, "extras") + "\""
                        + " GadgetPack", AppWinStyle.Hide, true));
                    await Task.Run(() => InstallGadgets());
                    Logger.WriteLine("InstallGadgets() succeeded.");
                    Directory.Delete(Path.Combine(Variables.r11Folder, "extras", "GadgetPack"), true);
                }
                if (InstallOptions.InstallShell)
                {
                    frm.InstallerProgress = "Installing extras: Enhanced context menu";
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -y " + Path.Combine(Variables.r11Folder, "extras.7z")
                        + " -o\"" + Path.Combine(Variables.r11Folder, "extras") + "\""
                        + " Nilesoft", AppWinStyle.Hide, true));

                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -y " + Path.Combine(Variables.r11Folder, "extras.7z")
                        + " -o\"" + Path.Combine(Variables.r11Folder, "extras") + "\""
                        + " NilesoftArm64", AppWinStyle.Hide, true));
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
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -y " + Path.Combine(Variables.r11Folder, "extras.7z")
                        + " -o\"" + Path.Combine(Variables.r11Folder, "extras") + "\""
                        + " userAV", AppWinStyle.Hide, true));
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
                    LogFile("files.7z", false, null);
                }
                catch (Exception ex)
                {
                    LogFile("files.7z", true, ex);
                    return false;
                }
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                        " x -o" + Path.Combine(Variables.r11Folder, "files") +
                        " " + Path.Combine(Variables.r11Folder, "files.7z"), AppWinStyle.Hide, true));
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

                if (!await Task.Run(() => WriteFiles(true, false)))
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

            if (!await Task.Run(() => AddToControlPanel()))
            {
                Logger.WriteLine("AddToControlPanel() failed");
                return false;
            }
            Logger.WriteLine("AddToControlPanel() succeeded");

            InstallStatus.IsRectify11Installed = true;

            Logger.WriteLine("══════════════════════════════════════════════");

            // cleanup
            frm.InstallerProgress = "Cleaning up...";
            Logger.WriteLine("Cleaning up");
            Logger.WriteLine("───────────");
            if (!await Task.Run(() => Cleanup()))
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
        /// installs themes
        /// </summary>
        private bool InstallThemes()
        {
            DirectoryInfo cursors = new(Path.Combine(Variables.r11Folder, "themes", "cursors"));
            var curdir = cursors.GetDirectories("*", SearchOption.TopDirectoryOnly);
            DirectoryInfo themedir = new(Path.Combine(Variables.r11Folder, "themes", "themes"));
            var msstyleDirList = themedir.GetDirectories("*", SearchOption.TopDirectoryOnly);
            var themefiles = themedir.GetFiles("*.theme");

            if (Directory.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")))
            {
                try
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
                                // idk why it would fail to delete a fucking png
                                if (File.Exists(Path.Combine(Path.GetTempPath(), Path.GetFileName(files[j]))))
                                {
                                    File.Delete(Path.Combine(Path.GetTempPath(), Path.GetFileName(files[j])));
                                }
                                File.Move(files[j], Path.Combine(Path.GetTempPath(), Path.GetFileName(files[j])));
                                MoveFileEx(Path.Combine(Path.GetTempPath(), Path.GetFileName(files[j])), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                        }
                    }
                    Logger.WriteLine("Deleted old wallpapers");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error deleting old wallpapers" + ex.Message + NewLine + ex.StackTrace + NewLine);
                }
            }
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
                Logger.WriteLine("Error copying wallpapers. " + ex.Message + NewLine + ex.StackTrace + NewLine);
            }

            // todo: remove r11cp
            File.Copy(Path.Combine(Variables.r11Folder, "themes", "ThemeTool.exe"), Path.Combine(Variables.Windir, "ThemeTool.exe"), true);
            Logger.WriteLine("Copied Themetool.");
            Interaction.Shell(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe") + " install", AppWinStyle.Hide, true);
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Folder, "themes", "Themes.reg"), AppWinStyle.Hide);

            for (var i = 0; i < curdir.Length; i++)
            {
                if (Directory.Exists(Path.Combine(Variables.Windir, "cursors", curdir[i].Name)))
                {
                    try
                    {
                        Directory.Delete(Path.Combine(Variables.Windir, "cursors", curdir[i].Name), true);
                        Logger.WriteLine("Deleted existing cursor directory " + Path.Combine(Variables.Windir, "cursors", curdir[i].Name));
                    }
                    catch
                    {
                        if (Directory.Exists(Path.Combine(Path.GetTempPath(), curdir[i].Name)))
                        {
                            Directory.Delete(Path.Combine(Path.GetTempPath(), curdir[i].Name), true);
                        }
                        Directory.Move(Path.Combine(Variables.Windir, "cursors", curdir[i].Name), Path.Combine(Path.GetTempPath(), curdir[i].Name));
                        var files = Directory.GetFiles(Path.Combine(Path.GetTempPath(), curdir[i].Name));
                        for (int j = 0; j < files.Length; j++)
                        {
                            MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        Logger.WriteLine("Moved " + Path.Combine(Variables.Windir, "cursors", curdir[i].Name) + " to " + Path.Combine(Path.GetTempPath(), curdir[i].Name));
                    }
                }
                try
                {
                    Directory.Move(curdir[i].FullName, Path.Combine(Variables.Windir, "cursors", curdir[i].Name));
                    Logger.WriteLine("Copied " + curdir[i].Name + " cursors");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error copying " + curdir[i].Name + ". " + ex.Message + NewLine + ex.StackTrace + NewLine);
                    return false;
                }
            }
            for (var i = 0; i < themefiles.Length; i++)
            {
                // why would it fail
                File.Copy(themefiles[i].FullName, Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i].Name), true);
            }
            File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun1.exe"), Properties.Resources.AdvancedRun);
            for (var i = 0; i < msstyleDirList.Length; i++)
            {
                if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name)))
                {
                    try
                    {
                        string name = Path.GetRandomFileName();
                        Directory.Move(Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name), Path.Combine(Path.GetTempPath(), name));
                        Directory.Delete(Path.Combine(Path.GetTempPath(), name, "Shell"), true);
                        var files = Directory.GetFiles(Path.Combine(Path.GetTempPath(), name));
                        for (int j = 0; j < files.Length; j++)
                        {
                            MoveFileEx(files[j], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        MoveFileEx(Path.Combine(Path.GetTempPath(), name), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        Logger.WriteLine(Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name) + " exists. Deleting it.");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLine("Error deleting " + Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name) + ex.Message + NewLine + ex.StackTrace + NewLine);
                        return false;
                    }
                }
                try
                {
                    Directory.Move(msstyleDirList[i].FullName, Path.Combine(Variables.Windir, "Resources", "Themes", msstyleDirList[i].Name));
                    Logger.WriteLine("Copied " + msstyleDirList[i].Name + " directory.");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error copying " + msstyleDirList[i].Name + ". " + ex.Message + NewLine + ex.StackTrace + NewLine);
                    return false;
                }
            }
            File.Delete(Path.Combine(Variables.r11Folder, "aRun1.exe"));
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
            shortcut.Save(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "asdf.lnk"));

            using ShellLink asdf11 = new();
            asdf11.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
            asdf11.WorkingDirectory = @"%windir%\Rectify11\extras\AccentColorizer";
            asdf11.Target = Path.Combine(Variables.r11Folder, "extras", "AccentColorizer", "AccentColorizerEleven.exe");
            asdf11.Save(Path.Combine(GetFolderPath(SpecialFolder.CommonStartMenu), "programs", "startup", "asdf11.lnk"));
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
            if (!Directory.Exists(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures")))
            {
                Directory.CreateDirectory(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"));
            }
            else
            {
                Directory.Delete(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures"), true);
            }

            DirectoryInfo info = new DirectoryInfo(Path.Combine(Variables.r11Folder, "extras", "UserAV"));
            for (int i = 0; i < info.GetFiles().Length; i++)
            {
                File.Copy(Path.Combine(Variables.r11Folder, "extras", "userAV", info.GetFiles("*.*")[i].Name),
                          Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures", info.GetFiles("*.*")[i].Name), true);
            }
        }

        /// <summary>
        /// installs sounds
        /// </summary>
        private void InstallSounds()
        {
            if (!File.Exists(Path.Combine(Variables.Windir, "Media", "ClassicSoundsService.exe")))
            {
                File.Move(Path.Combine(Variables.r11Folder, "extras", "ClassicSoundsService.exe"), Path.Combine(Variables.Windir, "media", "ClassicSoundsService.exe"));
            }
            if (!Directory.Exists(Path.Combine(Variables.Windir, "Media", "dm")))
            {
                Directory.CreateDirectory(Path.Combine(Variables.Windir, "Media", "dm"));
            }
            Interaction.Shell(Path.Combine(Variables.Windir, "Media", "ClassicSoundsService.exe") + " +", AppWinStyle.NormalFocus, true);
            DirectoryInfo sndir = new DirectoryInfo(Path.Combine(Variables.r11Folder, "extras", "Media"));
            DirectoryInfo snDMdir = new DirectoryInfo(Path.Combine(Variables.r11Folder, "extras", "Media", "dm"));
            File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun2.exe"), Properties.Resources.AdvancedRun);

            Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun2.exe") + " /EXEFilename " + '"' +
                Path.Combine(Variables.Windir, "Media", "ClassicSoundsService.exe") + '"' + " /CommandLine " + "\'" + " +" + "\'" + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true);
            for (int i = 0; i < sndir.GetFiles().Length; i++)
            {
                Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun2.exe")
                        + " /EXEFilename " + '"' + Path.Combine(Variables.sys32Folder, "cmd.exe") + '"'
                        + " /CommandLine " + "\'" + "/c copy " + '"' + sndir.GetFiles()[i].FullName + '"' + " "
                        + Path.Combine(Variables.Windir, "Media") + " /y" + "\'"
                        + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true);
            }
            for (int i = 0; i < snDMdir.GetFiles().Length; i++)
            {
                Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun2.exe")
                        + " /EXEFilename " + '"' + Path.Combine(Variables.sys32Folder, "cmd.exe") + '"'
                        + " /CommandLine " + "\'" + "/c copy " + '"' + snDMdir.GetFiles()[i].FullName + '"' + " "
                        + Path.Combine(Variables.Windir, "Media", "dm") + " /y" + "\'"
                        + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true);
            }
            File.Delete(Path.Combine(Variables.r11Folder, "aRun2.exe"));
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Folder, "extras", "Sound.reg"), AppWinStyle.Hide);
        }
        /// <summary>
		/// installs control center
		/// </summary>
		private void Installr11cpl()
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
        private void InstallMfe()
        {
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /create /tn mfe /xml " + Path.Combine(Variables.Windir, "MicaForEveryone", "XML", "mfe.xml"), AppWinStyle.Hide);
            if (Directory.Exists(Path.Combine(GetEnvironmentVariable("localappdata") ?? string.Empty, "Mica For Everyone")))
            {
                Directory.Delete(Path.Combine(GetEnvironmentVariable("localappdata") ?? string.Empty, "Mica For Everyone"), true);
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

        private void LogFile(string file, bool error, Exception ex)
        {
            if (error)
            {
                if (ex != null)
                {
                    Logger.WriteLine("Error while writing " + file + ". " + ex.Message + NewLine + ex.StackTrace + NewLine);
                }
                else
                {
                    Logger.WriteLine("Error while writing " + file + ". (No exception information)");
                }
            }
            else
            {
                Logger.WriteLine("Wrote " + file);
            }
        }

        /// <summary>
        /// writes all the needed files
        /// </summary>
        /// <param name="icons">indicates whether icon only files are written</param>
        /// <param name="themes">indicates whether theme only files are written</param>
        private bool WriteFiles(bool icons, bool themes)
        {
            if (icons)
            {
                if (!File.Exists(Path.Combine(Variables.r11Folder, "aRun.exe")))
                {
                    try
                    {
                        File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun);
                        LogFile("aRun.exe", false, null);
                    }
                    catch (Exception ex)
                    {
                        LogFile("aRun.exe", true, ex);
                        return false;
                    }
                }
                try
                {
                    File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2);
                    LogFile("Rectify11.Phase2.exe", false, null);
                }
                catch (Exception ex)
                {
                    LogFile("Rectify11.Phase2.exe", true, ex);
                    return false;
                }
            }
            if (themes)
            {
                try
                {
                    File.WriteAllBytes(Path.Combine(Variables.r11Folder, "themes.7z"), Properties.Resources.themes);
                    LogFile("themes.7z", false, null);
                }
                catch (Exception ex)
                {
                    LogFile("themes.7z", true, ex);
                    return false;
                }

                var s = Properties.Resources.secureux_x64;
                var dll = Properties.Resources.ThemeDll_x64;
                if (NativeMethods.IsArm64())
                {
                    s = Properties.Resources.secureux_arm64;
                    dll = Properties.Resources.ThemeDll_arm64;
                }
                try
                {
                    File.WriteAllBytes(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"), s);
                    File.WriteAllBytes(Path.Combine(Variables.r11Folder, "ThemeDll.dll"), dll);
                    LogFile("SecureUXHelper.exe", false, null);
                }
                catch (Exception ex)
                {
                    LogFile("SecureUXHelper.exe", true, ex);
                    return false;
                }
            }
            if (!themes && !icons)
            {
                if (!File.Exists(Path.Combine(Variables.r11Folder, "7za.exe")))
                {
                    try
                    {
                        File.WriteAllBytes(Path.Combine(Variables.r11Folder, "7za.exe"), Properties.Resources._7za);
                        LogFile("7za.exe", false, null);
                    }
                    catch (Exception ex)
                    {
                        LogFile("7za.exe", true, ex);
                        return false;
                    }
                }
                try
                {
                    File.WriteAllBytes(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files7z);
                    LogFile("files.7z", false, null);
                }
                catch (Exception ex)
                {
                    LogFile("files.7z", true, ex);
                    return false;
                }
                try
                {
                    File.WriteAllBytes(Path.Combine(Variables.r11Folder, "extras.7z"), Properties.Resources.extras);
                    LogFile("extras.7z", false, null);
                }
                catch (Exception ex)
                {
                    LogFile("extras.7z", true, ex);
                    return false;
                }
                if (!File.Exists(Path.Combine(Variables.r11Folder, "ResourceHacker.exe")))
                {
                    try
                    {
                        File.WriteAllBytes(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"), Properties.Resources.ResourceHacker);
                        LogFile("ResourceHacker.exe", false, null);
                    }
                    catch (Exception ex)
                    {
                        LogFile("ResourceHacker.exe", true, ex);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// creates backup and temp folder
        /// </summary>
        private bool CreateDirs()
        {
            if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup"));
                    Logger.WriteLine("Created " + Path.Combine(Variables.r11Folder, "Backup"));
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error creating " + Path.Combine(Variables.r11Folder, "Backup"), ex);
                    return false;
                }
            }
            else
            {
                Logger.WriteLine(Path.Combine(Variables.r11Folder, "Backup") + " already exists.");
            }

            if (Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
            {
                Logger.WriteLine(Path.Combine(Variables.r11Folder, "Tmp") + " exists. Deleting it.");
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "Tmp"), true);
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Error deleting " + Path.Combine(Variables.r11Folder, "Tmp"), ex);
                    return false;
                }
            }
            try
            {
                Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp"));
                Logger.WriteLine("Created " + Path.Combine(Variables.r11Folder, "Tmp"));
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Error creating " + Path.Combine(Variables.r11Folder, "Tmp"), ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// installs runtimes and shows a warning message if the installation of runtimes fails.
        /// </summary>
        private bool InstallRuntimes()
        {
            if (!File.Exists(Path.Combine(Variables.r11Folder, "vcredist.exe")))
            {
                Logger.WriteLine("Extracting vcredist.exe from extras.7z");
                Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                    " e -o" + Variables.r11Folder + " "
                    + Path.Combine(Variables.r11Folder, "extras.7z") + " vcredist.exe", AppWinStyle.Hide, true);
            }
            if (!File.Exists(Path.Combine(Variables.r11Folder, "core31.exe")))
            {
                Logger.WriteLine("Extracting core31.exe from extras.7z");
                Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                  " e -o" + Variables.r11Folder + " " + Path.Combine(Variables.r11Folder, "extras.7z") +
                  " core31.exe", AppWinStyle.Hide, true);
            }
            Logger.WriteLine("Executing vcredist.exe with arguments /install /quiet /norestart");
            ProcessStartInfo vcinfo = new()
            {
                FileName = Path.Combine(Variables.r11Folder, "vcredist.exe"),
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = " /install /quiet /norestart"
            };
            try
            {
                var vcproc = Process.Start(vcinfo);
                if (vcproc == null) return false;
                vcproc.WaitForExit();
                if (!vcproc.HasExited) return false;
                Logger.WriteLine("vcredist.exe exited with error code " + vcproc.ExitCode.ToString());
                if (vcproc.ExitCode != 0 && vcproc.ExitCode != 1638 && vcproc.ExitCode != 3010)
                {
                    Variables.vcRedist = false;
                }
                else Variables.vcRedist = true;
            }
            catch
            {
                return false;
            }
            Logger.WriteLine("Executing core31.exe with arguments /install /quiet /norestart");
            ProcessStartInfo core3info = new()
            {
                FileName = Path.Combine(Variables.r11Folder, "core31.exe"),
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = " /install /quiet /norestart"
            };
            try
            {
                var core3proc = Process.Start(core3info);
                if (core3proc == null) return false;
                core3proc.WaitForExit();
                if (!core3proc.HasExited) return false;
                Logger.WriteLine("core31.exe exited with error code " + core3proc.ExitCode.ToString());
                if (core3proc.ExitCode != 0 && core3proc.ExitCode != 1638 && core3proc.ExitCode != 3010)
                {
                    Variables.core31 = false;
                }
                else Variables.core31 = true;
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void RuntimeInstallError(string app, string info, string link)
        {
            TaskDialog td = new();
            td.Page.Text = "Installation of " + app + " has failed. You need to install it manually.";
            td.Page.Instruction = "Runtime installation error";
            td.Page.Title = "Rectify11 Setup";
            td.Page.StandardButtons = TaskDialogButtons.OK;
            td.Page.Icon = TaskDialogStandardIcon.SecurityWarningYellowBar;
            td.Page.EnableHyperlinks = true;
            TaskDialogExpander tde = new();
            tde.Text = info + " \nDownload from <a href=\"" + link + "\">here</a>";
            tde.Expanded = false;
            tde.ExpandFooterArea = true;
            tde.CollapsedButtonText = "More information";
            tde.ExpandedButtonText = "Less information";
            td.Page.Expander = tde;
            td.Show();
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
        /// Adds installer entry to control panel uninstall apps list
        /// </summary>
        /// <returns>true if writing to registry was successful, otherwise false</returns>
        private bool AddToControlPanel()
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
            if (key == null) return false;
            var r11key = key.CreateSubKey("Rectify11", true);
            if (r11key != null)
            {
                r11key.SetValue("DisplayName", "Rectify11", RegistryValueKind.String);
                r11key.SetValue("DisplayVersion", Assembly.GetEntryAssembly()?.GetName().Version.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("DisplayIcon", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
                r11key.SetValue("InstallLocation", Variables.r11Folder, RegistryValueKind.String);
                r11key.SetValue("UninstallString", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
                r11key.SetValue("ModifyPath", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
                r11key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                r11key.SetValue("VersionMajor", Assembly.GetEntryAssembly()?.GetName().Version.Major.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("VersionMinor", Assembly.GetEntryAssembly()?.GetName().Version.Minor.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("Build", Assembly.GetEntryAssembly()?.GetName().Version.Build.ToString() ?? string.Empty, RegistryValueKind.String);
                r11key.SetValue("Publisher", "The Rectify11 Team", RegistryValueKind.String);
                r11key.SetValue("URLInfoAbout", "https://rectify11.net/", RegistryValueKind.String);
                key.Close();
                return true;
            }
            key.Close();
            return false;
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

        /// <summary>
        /// cleans up files
        /// </summary>
        public static bool Cleanup()
        {
            if (Directory.Exists(Variables.r11Files))
            {
                try
                {
                    Directory.Delete(Variables.r11Files, true);
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Variables.r11Files, ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "files.7z")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "files.7z"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "files.7z"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "extras.7z")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "extras.7z"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "extras.7z"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "vcredist.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "vcredist.exe"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "vcredist.exe"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "extras", "vcredist.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "extras", "vcredist.exe"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "extras", "vcredist.exe"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "core31.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "core31.exe"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "core31.exe"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "extras", "core31.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "extras", "core31.exe"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "extras", "core31.exe"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "newfiles.txt")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "newfiles.txt"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "newfiles.txt"), ex);
                }
            }
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "themes")))
            {
                try
                {
                    Directory.Delete(Path.Combine(Variables.r11Folder, "themes"), true);
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "themes"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "themes.7z")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "themes.7z"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "themes.7z"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "7za.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "7za.exe"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "7za.exe"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "aRun.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "aRun.exe"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "aRun.exe"), ex);
                }
            }
            if (File.Exists(Path.Combine(Variables.r11Folder, "ResourceHacker.exe")))
            {
                try
                {
                    File.Delete(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"));
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "aRun.exe"), ex);
                }
            }
            try
            {
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras")))
                {
                    if (Directory.GetDirectories(Path.Combine(Variables.r11Folder, "extras")).Length == 0)
                    {
                        try
                        {
                            Directory.Delete(Path.Combine(Variables.r11Folder, "extras"), true);
                        }
                        catch (Exception ex)
                        {
                            Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "extras"), ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warn("Error deleting " + Path.Combine(Variables.r11Folder, "extras"), ex);
            }
            return true;
        }
    }
    #endregion
}
