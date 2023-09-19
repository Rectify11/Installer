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
        /// <summary>
        /// writes all the needed files
        /// </summary>
        /// <param name="icons">indicates whether icon only files are written</param>
        /// <param name="themes">indicates whether theme only files are written</param>
        public static bool WriteFiles(bool icons, bool themes)
        {
            if (icons)
            {
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun, OperationType.Write))
                    return false;
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2, OperationType.Write))
                    return false;
            }
            if (themes)
            {
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "themes.7z"), Properties.Resources.themes, OperationType.Write))
                    return false;

                var s = NativeMethods.IsArm64() ? Properties.Resources.secureux_arm64 : Properties.Resources.secureux_x64;
                var dll = NativeMethods.IsArm64() ? Properties.Resources.ThemeDll_arm64 : Properties.Resources.ThemeDll_x64;

                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "SecureUXHelper.exe"), s, OperationType.Write))
                    return false;
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "ThemeDll.dll"), dll, OperationType.Write))
                    return false;
            }
            if (!themes && !icons)
            {
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "7za.exe"), Properties.Resources._7za, OperationType.Write))
                    return false;
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files7z, OperationType.Write))
                    return false;
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "extras.7z"), Properties.Resources.extras, OperationType.Write))
                    return false;
                if (!SafeFileOperation(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"), Properties.Resources.ResourceHacker, OperationType.Write))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// creates backup and temp folder
        /// </summary>
        public static bool CreateDirs()
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
                catch
                {
                    string name = Path.GetTempFileName();
                    string tmpPath = Path.Combine(Path.GetTempPath(), name);
                    Directory.Move(Path.Combine(Variables.r11Folder, "Tmp"), tmpPath);
                    var files = Directory.GetFiles(tmpPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        try
                        {
                            File.Delete(files[i]);
                        }
                        catch
                        {
                            NativeMethods.MoveFileEx(files[i], null, NativeMethods.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                    }
                    NativeMethods.MoveFileEx(tmpPath, null, NativeMethods.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
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
        /// Copies installer to rectify11 folder, add entry to uninstall apps list
        /// </summary>
        public static bool CreateUninstall()
        {
            // backup
            if (!SafeFileOperation(Assembly.GetExecutingAssembly().Location, Path.Combine(Variables.r11Folder, "Uninstall.exe"), OperationType.Copy))
                return false;
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

        /// <summary>
        /// installs runtimes and shows a warning message if the installation of runtimes fails.
        /// </summary>
        public static bool InstallRuntimes()
        {
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "vcredist.exe"));
            Logger.WriteLine("Extracting vcredist.exe from extras.7z");
            Helper.SvExtract(true, "extras.7z", "vcredist.exe");

            SafeFileDeletion(Path.Combine(Variables.r11Folder, "core31.exe"));
            Logger.WriteLine("Extracting core31.exe from extras.7z");
            Helper.SvExtract(true, "extras.7z", "core31.exe");

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
                if (vcproc.ExitCode == 0) Variables.RestartRequired = true;
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
                if (core3proc.ExitCode == 0) Variables.RestartRequired = true;
            }
            catch
            {
                return false;
            }
            return true;
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
        /// cleans up files
        /// </summary>
        public static bool Cleanup()
        {
            // we dont care about returned value
            SafeDirectoryDeletion(Variables.r11Files);
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "files.7z"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "extras.7z"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "vcredist.exe"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "extras", "vcredist.exe"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "core31.exe"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "extras", "core31.exe"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "newfiles.txt"));
            SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "themes"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "themes.7z"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "7za.exe"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "aRun.exe"));
            SafeFileDeletion(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"));
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras")))
            {
                if (Directory.GetDirectories(Path.Combine(Variables.r11Folder, "extras")).Length == 0)
                {
                    SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "extras"));
                }
            }
            return true;
        }

        private enum OperationType
        {
            Write = 0,
            Copy
        }
        private static bool SafeFileDeletion(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    {
                        string name = Path.GetTempFileName();
                        string tmpPath = Path.Combine(Path.GetTempPath(), name);
                        File.Move(path, tmpPath);
                        NativeMethods.MoveFileEx(tmpPath, null, NativeMethods.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static bool SafeFileOperation(string path, object file, OperationType ot)
        {
            // whatever
            try
            {
                if (ot == OperationType.Write)
                {
                    if (!SafeFileDeletion(path)) return false;
                    File.WriteAllBytes(path, (byte[])file);
                    Logger.LogFile(Path.GetFileName(path));
                }
                else if (ot == OperationType.Copy)
                {
                    if (!SafeFileDeletion((string)file)) return false;
                    File.Copy(path, (string)file, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ot == OperationType.Write)
                    Logger.LogFile(Path.GetFileName(path), ex);
                return false;
            }
        }
        private static bool SafeDirectoryDeletion(string path)
        {
            // simply
            try
            {
                if (Directory.Exists(path))
                {
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch
                    {
                        string name = Path.GetTempFileName();
                        string tmpPath = Path.Combine(Path.GetTempPath(), name);
                        Directory.Move(path, tmpPath);
                        var dirs = Directory.GetDirectories(tmpPath);
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            var chldFiles = Directory.GetFiles(dirs[i]);
                            for (int j = 0; j < chldFiles.Length; j++)
                            {
                                try
                                {
                                    File.Delete(chldFiles[j]);
                                }
                                catch
                                {
                                    NativeMethods.MoveFileEx(chldFiles[j], null, NativeMethods.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                                }
                            }
                            NativeMethods.MoveFileEx(dirs[i], null, NativeMethods.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                        }
                        var files = Directory.GetFiles(tmpPath);
                        for (int i = 0; i < files.Length; i++)
                        {
                            try
                            {
                                File.Delete(files[i]);
                            }
                            catch
                            {
                                NativeMethods.MoveFileEx(files[i], null, NativeMethods.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                            }
                        }
                        NativeMethods.MoveFileEx(tmpPath, null, NativeMethods.MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
