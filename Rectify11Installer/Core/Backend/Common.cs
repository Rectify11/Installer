using KPreisser.UI;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Rectify11Installer.Core
{
    internal class Common
    {
        #region Public Methods
        /// <summary>
        /// writes all the needed files
        /// </summary>
        /// <param name="icons">indicates whether icon only files are written</param>
        /// <param name="themes">indicates whether theme only files are written</param>
        public static bool WriteFiles(bool icons, bool themes)
        {
            try
            {
                 if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "RectifyStart.exe"), Properties.Resources.RectifyStart, Helper.OperationType.Write))
                     return false;
                if (icons)
                {
                    if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "NSudoL.exe"), Properties.Resources.NSudoL, Helper.OperationType.Write))
                        return false;
                    if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2, Helper.OperationType.Write))
                        return false;
                }
                if (themes)
                {
                   if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "themes.7z"), Properties.Resources.themes, Helper.OperationType.Write))
                        return false;
                }
                if (!themes && !icons)
                {
                    if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "7za.exe"), Properties.Resources._7za, Helper.OperationType.Write))
                        return false;
                     if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files, Helper.OperationType.Write))
                         return false;
                     if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "extras.7z"), Properties.Resources.extras, Helper.OperationType.Write))
                         return false;
                    if (!Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"), Properties.Resources.ResourceHacker, Helper.OperationType.Write))
                        return false;
                }

                Logger.WriteLine("WriteFiles() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("WriteFiles() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// creates backup and temp folder
        /// </summary>
        public static bool CreateDirs()
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
                    Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup"));
                else
                    Logger.WriteLine(Path.Combine(Variables.r11Folder, "Backup") + " already exists.");

                Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Tmp"), false);
                Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp"));
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("CreateDirs() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// Copies installer to rectify11 folder, add entry to uninstall apps list
        /// </summary>
        public static bool CreateUninstall()
        {
            try
            {

                // backup
                // fails anyways if you use uninstaller.exe
                Helper.SafeFileOperation(Assembly.GetExecutingAssembly().Location, Path.Combine(Variables.r11Folder, "Uninstall.exe"), Helper.OperationType.Copy);
                Logger.WriteLine("Installer copied to " + Path.Combine(Variables.r11Folder, "Uninstall.exe"));

                var r11key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true)?.CreateSubKey("Rectify11", true);
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
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.Warn("CreateUninstall() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// installs runtimes and shows a warning message if the installation of runtimes fails.
        /// </summary>
        public static bool InstallRuntimes()
        {
            try
            {
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "vcredist.exe"));
                Logger.WriteLine("Extracting vcredist.exe from extras.7z");
                Helper.SvExtract(true, "extras.7z", "vcredist.exe");

                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "core31.exe"));
                Logger.WriteLine("Extracting core31.exe from extras.7z");
                Helper.SvExtract(true, "extras.7z", "core31.exe");

                // vcredist
                Logger.WriteLine("Executing vcredist.exe with arguments /install /quiet /norestart");
                ProcessStartInfo vcinfo = new()
                {
                    FileName = Path.Combine(Variables.r11Folder, "vcredist.exe"),
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = " /install /quiet /norestart"
                };
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
                if (vcproc.ExitCode == 0) Variables.RestartRequired = true;

                // .net core 
                Logger.WriteLine("Executing core31.exe with arguments /install /quiet /norestart");
                ProcessStartInfo core3info = new()
                {
                    FileName = Path.Combine(Variables.r11Folder, "core31.exe"),
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = " /install /quiet /norestart"
                };

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
                if (core3proc.ExitCode == 0) Variables.RestartRequired = true;
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("InstallRuntimes() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// show warning message of failed runtime installation
        /// </summary>
        /// <param name="app">the app which failed</param>
        /// <param name="info">info about the app</param>
        /// <param name="link">link to download the app</param>
        public static void RuntimeInstallError(string app, string info, string link)
        {
            TaskDialog td = new();
            td.Page.Text = Rectify11Installer.Strings.Rectify11.rtInstallFailPart1 + app + Rectify11Installer.Strings.Rectify11.rtInstallFailPart2;
            td.Page.Instruction = Rectify11Installer.Strings.Rectify11.rtInstallFailInstruc;
            td.Page.Title = Rectify11Installer.Strings.Rectify11.Title;
            td.Page.StandardButtons = TaskDialogButtons.OK;
            td.Page.Icon = TaskDialogStandardIcon.SecurityWarningYellowBar;
            td.Page.EnableHyperlinks = true;
            TaskDialogExpander tde = new();
            tde.Text = info + " \nDownload from <a href=\"" + link + "\">here</a>";
            tde.Expanded = false;
            tde.ExpandFooterArea = true;
            tde.CollapsedButtonText = Rectify11Installer.Strings.Rectify11.moreInfo;
            tde.ExpandedButtonText = Rectify11Installer.Strings.Rectify11.lessInfo;
            td.Page.Expander = tde;
            td.Show();
        }

        /// <summary>
        /// cleans up files
        /// </summary>
        public static bool Cleanup()
        {
            try
            {
                Logger.WriteLine("Cleaning up");
                Logger.WriteLine("───────────");

                // we dont care about returned value
                Helper.SafeDirectoryDeletion(Variables.r11Files, false);
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "files.7z"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "extras.7z"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "vcredist.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "extras", "vcredist.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "core31.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "extras", "core31.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "newfiles.txt"));
                Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "themes"), false);
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "themes.7z"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "7za.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "NSudoL.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "ResourceHacker.ini"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"));

				if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras")))
                {
                    if (Directory.GetDirectories(Path.Combine(Variables.r11Folder, "extras")).Length == 0)
                    {
                        Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras"), false);
                    }
                }
                Logger.WriteLine("Cleanup() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Warn("Cleanup() failed", ex);
                return false;
            }
        }
        #endregion
    }
}
