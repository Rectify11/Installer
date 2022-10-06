using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
    public class Installer
    {
        #region Variables
        private string newhardlink;
        #endregion
        #region Public Methods
        public async Task<bool> Install(frmWizard frm)
        {
            /*
            if (!File.Exists(Path.Combine(Variables.r11Folder, "PaExec64.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "PaExec64.exe"), Properties.Resources.PsExec64);
            */
            if (!File.Exists(Path.Combine(Variables.r11Folder, "7za.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "7za.exe"), Properties.Resources._7za);

            if (!File.Exists(Path.Combine(Variables.r11Folder, "files.7z")))
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files7z);

            if (!File.Exists(Path.Combine(Variables.r11Folder, "ResourceHacker.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"), Properties.Resources.ResourceHacker);

            if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
                Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup"));

            if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
                Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp"));

            File.Copy(Path.Combine(Application.StartupPath, "Rectify11Installer.exe"), Path.Combine(Variables.r11Folder, "Uninstall.exe"), true);

            if (!Directory.Exists(Path.Combine(Variables.r11Folder, "files")))
            {
                frm.InstallerProgress = "Extracting files...";
                Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                    " x -o" + Path.Combine(Variables.r11Folder, "files") +
                    " " + Path.Combine(Variables.r11Folder, "files.7z"), AppWinStyle.Hide, true, -1);
            }
            if (InstallOptions.iconsList.Count > 0)
            {
                // Get all patches
                Patches patches = PatchesParser.GetAll();
                PatchesPatch[] ok = patches.Items;
                decimal i = 0;
                List<string> filelist = new List<string>();
                foreach (PatchesPatch patch in ok)
                {
                    foreach (string items in InstallOptions.iconsList)
                    {
                        if (patch.Mui.Contains(items))
                        {
                            decimal number = Math.Round((i / InstallOptions.iconsList.Count) * 100m);
                            frm.InstallerProgress = "Patching " + patch.Mui + " (" + number + "%)";
                            filelist.Add(patch.HardlinkTarget);
                            MatchAndApplyRule(patch);
                            i++;
                        }
                    }
                }
                var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true).CreateSubKey("Rectify11", true);
                if (reg != null)
                {
                    reg.SetValue("PendingFiles", filelist.ToArray());
                    reg.SetValue("Language", CultureInfo.CurrentUICulture.Name);
                    reg.SetValue("Version", new Label().ProductVersion);
                }
                reg.Close();
            }
            if (InstallOptions.InstallThemes)
            {
                frm.InstallerProgress = "Installing Themes";
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Files, "Extras", "UltraUXThemePatcher_4.3.4.exe"), AppWinStyle.NormalFocus, true));
            }
            AddToControlPanel();
            frm.InstallerProgress = "Cleaning up...";
            Directory.Delete(Variables.r11Files, true);
            File.Delete(Path.Combine(Variables.r11Folder, "files.7z"));
            frm.InstallerProgress = "Done";
            NativeMethods.SetCloseButton(frm, true);
            return true;
        }
        #endregion
        #region Private Methods
        private bool AddToControlPanel()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
            if (key != null)
            {
                var r11key = key.CreateSubKey("Rectify11", true);
                if (r11key != null)
                {
                    r11key.SetValue("DisplayName", "Rectify11", RegistryValueKind.String);
                    r11key.SetValue("DisplayVersion", Assembly.GetEntryAssembly().GetName().Version.ToString(), RegistryValueKind.String);
                    r11key.SetValue("DisplayIcon", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
                    r11key.SetValue("InstallLocation", Variables.r11Folder, RegistryValueKind.String);
                    r11key.SetValue("UninstallString", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
                    r11key.SetValue("ModifyPath", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
                    r11key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                    r11key.SetValue("VersionMajor", Assembly.GetEntryAssembly().GetName().Version.Major.ToString(), RegistryValueKind.String);
                    r11key.SetValue("VersionMinor", Assembly.GetEntryAssembly().GetName().Version.Minor.ToString(), RegistryValueKind.String);
                    r11key.SetValue("Publisher", "The Rectify11 Team", RegistryValueKind.String);
                    r11key.SetValue("URLInfoAbout", "https://rectify.vercel.app/", RegistryValueKind.String);
                    return true;
                }
                return false;
            }
            return false;
        }
        private enum PatchType
        {
            General = 0,
            Mui,
            Troubleshooter,
            x86

        }
        private static void Patch(string file, PatchesPatch patch, PatchType type)
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
                    string ext = Path.GetExtension(patch.Mui);
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
                    return;

                if (type == PatchType.Troubleshooter)
                {
                    if (!Directory.Exists(backupfolder))
                    {
                        Directory.CreateDirectory(backupfolder);
                        Directory.CreateDirectory(tempfolder);
                    }
                }
                if (!File.Exists(Path.Combine(backupfolder, name)))
                {
                    File.Copy(file, Path.Combine(backupfolder, name));
                    File.Copy(file, Path.Combine(tempfolder, name));
                }

                string filename = name + ".res";
                string filepath;
                if (type == PatchType.Troubleshooter)
                    filepath = Path.Combine(Variables.r11Files, "Diag");
                else
                    filepath = Variables.r11Files;

                if (patch.mask.Contains("|"))
                {
                    string[] str = patch.mask.Split('|');
                    for (int i = 0; i < str.Length; i++)
                    {
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
                    if (type != PatchType.Mui)
                    {
                        Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
                            " -open " + Path.Combine(tempfolder, name) +
                            " -save " + Path.Combine(tempfolder, name) +
                            " -action " + "delete" +
                            " -mask " + patch.mask, AppWinStyle.Hide, true);
                    }
                    Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
                            " -open " + Path.Combine(tempfolder, name) +
                            " -save " + Path.Combine(tempfolder, name) +
                            " -action " + "addskip" +
                            " -resource " + Path.Combine(filepath, filename) +
                            " -mask " + patch.mask, AppWinStyle.Hide, true);
                }
            }
        }
        private void MatchAndApplyRule(PatchesPatch patch)
        {
            if (patch.HardlinkTarget.Contains("%sys32%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%sys32%", Variables.sys32Folder);
                Installer.Patch(newhardlink, patch, PatchType.General);
            }
            else if (patch.HardlinkTarget.Contains("%lang%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
                Installer.Patch(newhardlink, patch, PatchType.Mui);
            }
            else if (patch.HardlinkTarget.Contains("%en-US%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
                Installer.Patch(newhardlink, patch, PatchType.Mui);
            }
            else if (patch.HardlinkTarget.Contains("mun"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%sysresdir%", Variables.sysresdir);
                Installer.Patch(newhardlink, patch, PatchType.General);
            }
            else if (patch.HardlinkTarget.Contains("%branding%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%branding%", Variables.brandingFolder);
                Installer.Patch(newhardlink, patch, PatchType.General);
            }
            else if (patch.HardlinkTarget.Contains("%prog%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%prog%", Variables.progfiles);
                Installer.Patch(newhardlink, patch, PatchType.General);
            }
            else if (patch.HardlinkTarget.Contains("%diag%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%diag%", Variables.diag);
                Installer.Patch(newhardlink, patch, PatchType.Troubleshooter);
            }
            else if (patch.HardlinkTarget.Contains("%windir%"))
            {
                newhardlink = patch.HardlinkTarget.Replace(@"%windir%", Variables.windir);
                Installer.Patch(newhardlink, patch, PatchType.General);
            }
            // only for comctl so idc
            else if (patch.HardlinkTarget.Contains("%winsxs%"))
            {
                // amd64 and x86 version same
                string[] ok = patch.HardlinkTarget.Split('\\');
                int[] amd64versionArray;
                string[] directories = Directory.GetDirectories(Variables.winSxS, ok[1], SearchOption.TopDirectoryOnly);
                var regex = new Regex(@"\d+(\.\d+)+");
                amd64versionArray = new int[directories.Length];
                for (int i = 0; i < directories.Length; i++)
                {
                    var match = regex.Match(directories[i]);
                    if (match.Success)
                    {
                        Version ver = Version.Parse(match.Value);
                        if (ver.Major == 6)
                        {
                            if (directories[i].Contains("amd64_"))
                            {
                                amd64versionArray[i] = ver.Revision;
                            }
                        }
                    }
                }
                int amd64largest = amd64versionArray.Max();
                string[] finaldirs = Directory.GetDirectories(Variables.winSxS, ok[1] + "_6.0." + Environment.OSVersion.Version.Build.ToString() + "." + amd64largest.ToString() + "_*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < finaldirs.Length; i++)
                {
                    if (finaldirs[i].Contains("amd64_"))
                    {
                        Installer.Patch(Path.Combine(finaldirs[i], "comctl32.dll"), patch, PatchType.General);
                    }
                    else if (finaldirs[i].Contains("x86_"))
                    {
                        Installer.Patch(Path.Combine(finaldirs[i], "comctl32.dll"), patch, PatchType.x86);
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(patch.x86))
            {
                if (patch.HardlinkTarget.Contains("%sys32%"))
                {
                    newhardlink = patch.HardlinkTarget.Replace(@"%sys32%", Variables.sysWOWFolder);
                    Installer.Patch(newhardlink, patch, PatchType.x86);
                }
                else if (patch.HardlinkTarget.Contains("%prog%"))
                {
                    newhardlink = patch.HardlinkTarget.Replace(@"%prog%", Variables.progfiles86);
                    Installer.Patch(newhardlink, patch, PatchType.x86);
                }
            }
        }
        #endregion
    }
}
