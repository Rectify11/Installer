using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using static System.Environment;

namespace Rectify11Installer.Core
{
    internal class Icons
    {
        #region Variables
        private enum PatchType
        {
            General = 0,
            Mui,
            Troubleshooter,
            x86
        }
        #endregion
        public static bool Install(FrmWizard frm)
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
            Helper.SvExtract("files.7z", "files");
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
                        if (!MatchAndApplyRule(patch[i]))
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

            if (!WritePendingFiles(fileList, x86List))
            {
                Logger.WriteLine("WritePendingFiles() failed");
                return false;
            }
            Logger.WriteLine("WritePendingFiles() succeeded");

            if (!Common.WriteFiles(true, false))
            {
                Logger.WriteLine("WriteFiles() failed");
                return false;
            }
            Logger.WriteLine("WriteFiles() succeeded");

            frm.InstallerProgress = "Replacing files";

            // runs only if SSText3D.scr is selected
            if (InstallOptions.iconsList.Contains("SSText3D.scr"))
            {
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "screensaver.reg"), AppWinStyle.Hide);
                Logger.WriteLine("screensaver.reg succeeded");
            }

            // runs only if any one of mmcbase.dll.mun, mmc.exe.mui or mmcndmgr.dll.mun is selected
            if (InstallOptions.iconsList.Contains("mmcbase.dll.mun")
                || InstallOptions.iconsList.Contains("mmc.exe.mui")
                || InstallOptions.iconsList.Contains("mmcndmgr.dll.mun"))
            {
                if (!MMCHelper.PatchAll())
                {
                    Logger.WriteLine("MmcHelper.PatchAll() failed");
                    return false;
                }
                Logger.WriteLine("MmcHelper.PatchAll() succeeded");
            }

            if (InstallOptions.iconsList.Contains("odbcad32.exe"))
            {
                if (!FixOdbc())
                {
                    Logger.Warn("FixOdbc() failed");
                }
                else
                {
                    Logger.WriteLine("FixOdbc() succeeded");
                }
            }
            // phase 2
            Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun.exe")
                + " /EXEFilename " + '"' + Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe") + '"'
                + " /CommandLine " + "\'" + "/install" + "\'"
                + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true);

            // reg files for various file extensions
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "icons.reg"), AppWinStyle.Hide);
            Logger.WriteLine("icons.reg succeeded");

            Variables.RestartRequired = true;
            return true;
        }

        public static bool Uninstall()
        {
            Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2, Helper.OperationType.Write);
            Helper.SafeFileOperation(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun, Helper.OperationType.Write);
            try
            {
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)
                    ?.CreateSubKey("Rectify11", true)
                    ?.SetValue("UninstallFiles", UninstallOptions.uninstIconsList.ToArray());

                if (!Variables.Phase2Skip)
                {
                    Logger.WriteLine("Executed Rectify11.Phase2.exe");
                    Helper.RunAsTI(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), "/uninstall");
                }
            }
            catch { }

            Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"));
            Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "aRun.exe"));
            return true;
        }

        /// <summary>
        /// fixes 32-bit odbc shortcut icon
        /// </summary>
        private static bool FixOdbc()
        {
            var filename = string.Empty;
            var admintools = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Administrative Tools");
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
        /// sets required registry values for phase 2
        /// </summary>
        /// <param name="fileList">normal files list</param>
        /// <param name="x86List">32-bit files list</param>
        private static bool WritePendingFiles(List<string> fileList, List<string> x86List)
        {
            using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", true);
            if (reg == null) return false;
            try
            {
                SafeWriteReg(reg, "PendingFiles", fileList.ToArray(), true);
                if (x86List.Count != 0)
                    SafeWriteReg(reg, "x86PendingFiles", x86List.ToArray(), true);

                SafeWriteReg(reg, "Language", CultureInfo.CurrentUICulture.Name, false);
                SafeWriteReg(reg, "Version", Assembly.GetEntryAssembly()?.GetName().Version, false);
                SafeWriteReg(reg, "WindowsUpdate", Variables.WindowsUpdate ? 1 : 0, false);
                using var ubrReg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", false);
                string build = OSVersion.Version.Major + "." + OSVersion.Version.Minor + "." + OSVersion.Version.Build + "." + ubrReg.GetValue("UBR").ToString();
                SafeWriteReg(reg, "OSVersion", build, false);
            }
            catch
            {
                return false;
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
            if (!File.Exists(file)) return false;
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

            if (string.IsNullOrWhiteSpace(name)) return false;

            if (type == PatchType.Troubleshooter)
            {
                if (!Directory.Exists(backupfolder)) Directory.CreateDirectory(backupfolder);
                if (!Directory.Exists(tempfolder)) Directory.CreateDirectory(tempfolder);
            }

            //File.Copy(file, Path.Combine(backupfolder, name));
            File.Copy(file, Path.Combine(tempfolder, name), true);

            var filename = name + ".res";
            var masks = patch.mask;
            string filepath;
            if (type == PatchType.Troubleshooter)
                filepath = Path.Combine(Variables.r11Files, "Diag");
            else
                filepath = Variables.r11Files;

            if (patch.mask.Contains("|"))
            {
                if (!string.IsNullOrWhiteSpace(patch.Ignore) 
                    && ((!string.IsNullOrWhiteSpace(patch.MinVersion) && OSVersion.Version.Build <= int.Parse(patch.MinVersion)) || (!string.IsNullOrWhiteSpace(patch.MaxVersion) && OSVersion.Version.Build >= int.Parse(patch.MaxVersion))))
                {
                    masks = masks.Replace(patch.Ignore, "");
                }
                var str = masks.Split('|');
                for (var i = 0; i < str.Length; i++)
                {
                    PatchCmd(type, filename, name, tempfolder, filepath, str[i]);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(patch.Ignore) && ((!string.IsNullOrWhiteSpace(patch.MinVersion) && OSVersion.Version.Build <= int.Parse(patch.MinVersion)) || (!string.IsNullOrWhiteSpace(patch.MaxVersion) && OSVersion.Version.Build >= int.Parse(patch.MaxVersion))))
                {
                    masks = masks.Replace(patch.Ignore, "");
                }
                PatchCmd(type, filename, name, tempfolder, filepath, masks);
            }
            return true;
        }

        /// <summary>
        /// Replaces the path and patches the file accordingly.
        /// </summary>
        /// <param name="patch">Xml element containing all the info</param>
        private static bool MatchAndApplyRule(PatchesPatch patch)
        {
            string newhardlink = Helper.FixString(patch.HardlinkTarget, false);
            if (newhardlink.Contains(".mui"))
            {
                if (!Patch(newhardlink, patch, PatchType.Mui)) return false;
            }
            else if (patch.HardlinkTarget.Contains("%diag%"))
            {
                if (!Patch(newhardlink, patch, PatchType.Troubleshooter)) return false;
            }
            else
            {
                if (!Patch(newhardlink, patch, PatchType.General)) return false;
            }

            if (!string.IsNullOrWhiteSpace(patch.x86))
            {
                newhardlink = Helper.FixString(patch.HardlinkTarget, true);
                if (!Patch(newhardlink, patch, PatchType.x86)) return false;
            }
            return true;
        }

        #region Internal functions
        private static void PatchCmd(PatchType type, string filename, string name, string tempfolder, string filepath, string mask)
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
                " -mask " + mask, AppWinStyle.Hide, true);
            }
            Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
            " -open " + Path.Combine(tempfolder, name) +
            " -save " + Path.Combine(tempfolder, name) +
            " -action " + "addskip" +
            " -resource " + Path.Combine(filepath, filename) +
            " -mask " + mask, AppWinStyle.Hide, true);
        }

        private static bool SafeWriteReg(RegistryKey key, string name, object value, bool hardError)
        {
            try
            {
                key?.SetValue(name, value);
                Logger.WriteLine("Wrote to " + name);
                return true;
            }
            catch (Exception ex)
            {
                if (hardError)
                {
                    Logger.WriteLine("Error writing to " + name, ex);
                    throw new Exception("error");
                }
                else
                {
                    Logger.Warn("Error writing to " + name, ex);
                    return true;
                }
            }
        }
        #endregion
    }
}
