using KPreisser.UI;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using static System.Environment;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

        /// <summary>
        /// icon installation logic
        /// </summary>
        /// <param name="frm">FrmWizard instance</param>
        /// <returns>true if succeeds, else returns false</returns>
        public static bool Install(FrmWizard frm)
        {
            try
            {
                Logger.WriteLine("Installing icons");
                Logger.WriteLine("────────────────");

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
                Logger.WriteLine("MatchAndApplyRule() succeeded.");

                if (!WritePendingFiles(fileList, x86List))
                    return false;

                if (!Common.WriteFiles(true, false))
                    return false;

                frm.InstallerProgress = "Replacing files";

                // runs only if SSText3D.scr is selected
                if (InstallOptions.iconsList.Contains("SSText3D.scr"))
                {
                    Helper.ImportReg(Path.Combine(Variables.r11Files, "screensaver.reg"));
                }

				// runs only if mmc.exe.mui is selected
				if (InstallOptions.iconsList.Contains("mmc.exe.mui"))
                {
					if (!MMCHelper.PatchAll())
                        return false;
				}

				if (InstallOptions.iconsList.Contains("odbcad32.exe"))
                    FixOdbc();

                // phase 2
                Helper.RunAsTI(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), "/install");

                // reg files for various file extensions
                Helper.ImportReg(Path.Combine(Variables.r11Files, "icons.reg"));

                ClearIconCache();

				Variables.RestartRequired = true;
                Logger.WriteLine("Icons.Install() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
                return true;
            }
            catch (Exception ex)
            {
                // rollback
                // ikr amazing
                Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Tmp"), false);
                Logger.WriteLine("Icons.Install() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// icon uninstallation logic
        /// </summary>
        /// <returns>true if succeeds, else returns false</returns>
        public static bool Uninstall()
        {
            try
            {
                Common.WriteFiles(true, false);
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)
                    ?.CreateSubKey("Rectify11", true)
                    ?.SetValue("UninstallFiles", UninstallOptions.uninstIconsList.ToArray());

                if (!Variables.Phase2Skip)
                {
                    Logger.WriteLine("Executed Rectify11.Phase2.exe");
                    Helper.RunAsTI(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), "/uninstall");
                }

                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"));
                Helper.SafeFileDeletion(Path.Combine(Variables.r11Folder, "NSudoL.exe"));

                Logger.WriteLine("Icons.Uninstall() succeeded.");
                Logger.WriteLine("══════════════════════════════════════════════");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Icons.Uninstall() failed", ex);
                return false;
            }
        }

        /// <summary>
        /// fixes 32-bit odbc shortcut icon
        /// </summary>
        private static bool FixOdbc()
        {
            try
            {
                var filename = string.Empty;
                var admintools = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), "Microsoft", "Windows", "Start Menu", "Programs", "Administrative Tools");
                if (!Directory.Exists(admintools)) return false;

                var files = Directory.GetFiles(admintools);
                for (var i = 0; i < files.Length; i++)
                {
                    if (!Path.GetFileName(files[i]).Contains("ODBC") ||
                        !Path.GetFileName(files[i])!.Contains("32")) continue;
                    filename = Path.GetFileName(files[i]);
                    File.Delete(files[i]);
                }
                using ShellLink shortcut = new();
                shortcut.Target = Path.Combine(Variables.sysWOWFolder, "odbcad32.exe");
                shortcut.WorkingDirectory = @"%windir%\system32";
                shortcut.IconPath = Path.Combine(Variables.sys32Folder, "odbcint.dll");
                shortcut.IconIndex = 0;
                shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
                if (filename != null) shortcut.Save(Path.Combine(admintools, filename));

                Logger.WriteLine("FixOdbc() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Warn("FixOdbc() failed", ex);
                return false;
            }
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

                Logger.WriteLine("WritePendingFiles() succeeded.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine("WritePendingFiles() failed", ex);
                return false;
            }
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
                Directory.CreateDirectory(backupfolder);
                Directory.CreateDirectory(tempfolder);
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

		/// <summary>
		/// clears *.db cache files
		/// </summary>
		private static void ClearIconCache()
		{
			try
			{
				DirectoryInfo di = new(Path.Combine(GetFolderPath(SpecialFolder.LocalApplicationData), "microsoft", "windows", "explorer"));
				var files = di.GetFiles("*.db");

				for (var i = 0; i < files.Length; i++)
				{
					files[i].Attributes = FileAttributes.Normal;
					if (File.Exists(files[i].FullName))
					{
						Helper.SafeFileDeletion(files[i].FullName);
					}
				}
				var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", true);
				key?.SetValue("ResetIconCache", Path.Combine(Variables.sys32Folder, "ie4uinit.exe") + " -show", RegistryValueKind.String);
				key.Close();
			}
			catch (Exception ex)
			{
                Logger.Warn("Clearing icon cache failed", ex);
			}
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
