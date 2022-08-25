using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
    public class Installer
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);
        [Flags]
        enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 0x00000001,
            MOVEFILE_COPY_ALLOWED = 0x00000002,
            MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
            MOVEFILE_WRITE_THROUGH = 0x00000008,
            MOVEFILE_CREATE_HARDLINK = 0x00000010,
            MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
        }
        // eh two functions whatever cant be bothered to make it single.
        private static void PatchMui(string file, PatchesPatch patch)
        {
            if (File.Exists(file))
            {
                if (!Directory.Exists(Path.Combine(Variables.r11Folder, "backup", patch.Mui)))
                {
                    Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "backup", patch.Mui));
                    Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp", patch.Mui));
                }
                if (!File.Exists(Path.Combine(Variables.r11Folder, "backup", patch.Mui, patch.Mui)))
                {
                    File.Copy(file, Path.Combine(Variables.r11Folder, "backup", patch.Mui, patch.Mui));
                    File.Copy(file, Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui));
                    string filename = patch.Mui + ".res";
                    if (patch.mask.Contains("|"))
                    {
                        string[] str = patch.mask.Split('|');
                        foreach (string mask in str)
                        {
                            Interaction.Shell(Path.Combine(Variables.r11Files, "ResourceHacker.exe") +
                                " -open " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -save " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -action " + "addskip" +
                                " -resource " + Path.Combine(Variables.r11Files, filename) +
                                " -mask " + mask, AppWinStyle.Hide, true, -1);
                        }
                    }
                    else
                    {
                        Interaction.Shell(Path.Combine(Variables.r11Files, "ResourceHacker.exe") +
                                " -open " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -save " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -action " + "addskip" +
                                " -resource " + Path.Combine(Variables.r11Files, filename) +
                                " -mask " + patch.mask, AppWinStyle.Hide, true, -1);
                    }
                }
            }
        }
        private static void PatchMun(string file, PatchesPatch patch)
        {
            if (File.Exists(file))
            {
                if (!Directory.Exists(Path.Combine(Variables.r11Folder, "backup", patch.Mui)))
                {
                    Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "backup", patch.Mui));
                    Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp", patch.Mui));
                }
                if (!File.Exists(Path.Combine(Variables.r11Folder, "backup", patch.Mui, patch.Mui)))
                {
                    File.Copy(file, Path.Combine(Variables.r11Folder, "backup", patch.Mui, patch.Mui));
                    File.Copy(file, Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui));
                    string filename = patch.Mui + ".res";
                    if (patch.mask.Contains("|"))
                    {
                        string[] str = patch.mask.Split('|');
                        foreach (string mask in str)
                        {
                            Interaction.Shell(Path.Combine(Variables.r11Files, "ResourceHacker.exe") +
                                " -open " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -save " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -action " + "delete" +
                                " -mask " + mask, AppWinStyle.Hide, true);

                            Interaction.Shell(Path.Combine(Variables.r11Files, "ResourceHacker.exe") +
                                " -open " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -save " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -action " + "addskip" +
                                " -resource " + Path.Combine(Variables.r11Files, filename) +
                                " -mask " + mask, AppWinStyle.Hide, true);
                        }
                    }
                    else
                    {
                        Interaction.Shell(Path.Combine(Variables.r11Files, "ResourceHacker.exe") +
                                " -open " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -save " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -action " + "delete" +
                                " -mask " + patch.mask, AppWinStyle.Hide, true);
                        Interaction.Shell(Path.Combine(Variables.r11Files, "ResourceHacker.exe") +
                                " -open " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -save " + Path.Combine(Variables.r11Folder, "Tmp", patch.Mui, patch.Mui) +
                                " -action " + "addskip" +
                                " -resource " + Path.Combine(Variables.r11Files, filename) +
                                " -mask " + patch.mask, AppWinStyle.Hide, true);
                    }
                }
            }
        }

        public async Task<bool> Install(frmWizard frm)
        {
            if (!File.Exists(Path.Combine(Variables.r11Folder, "PaExec64.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "PaExec64.exe"), Properties.Resources.PsExec64);

            if (!File.Exists(Path.Combine(Variables.r11Folder, "7za.exe")))
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "7za.exe"), Properties.Resources._7za);

            if (!File.Exists(Path.Combine(Variables.r11Folder, "files.7z")))
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files7z);

            if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
                Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup"));

            if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
                Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp"));

            File.Copy(Path.Combine(Application.StartupPath, "Rectify11Installer.exe"), Path.Combine(Variables.r11Folder, "Uninstall.exe"), true);

            if (!Directory.Exists(Path.Combine(Variables.r11Folder, "files")))
            {
                frm.InstallerProgress = "Extracting files...";
                Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
                    " x -o" + Variables.r11Folder +
                    " " + Path.Combine(Variables.r11Folder, "files.7z"), AppWinStyle.Hide, true, -1);
            }
            if (InstallOptions.iconsList.Count > 0)
            {

                // Get all patches
                Patches patches = PatchesParser.GetAll();
                PatchesPatch[] ok = patches.Items;
                decimal i = 0;
                string newhardlink;
                foreach (PatchesPatch patch in ok)
                {
                    foreach (string items in InstallOptions.iconsList)
                    {
                        if (items == patch.Mui)
                        {
                            decimal number = Math.Round((i / InstallOptions.iconsList.Count) * 100m);
                            frm.InstallerProgress = "Patching " + patch.Mui + " (" + number + "%)";
                            i++;
                            if (patch.HardlinkTarget.Contains("%lang%"))
                            {
                                newhardlink = patch.HardlinkTarget.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentCulture.Name));
                                Installer.PatchMui(newhardlink, patch);
                            }
                            else if (patch.HardlinkTarget.Contains("mun"))
                            {
                                Installer.PatchMun(patch.HardlinkTarget, patch);
                            }
                            else if (patch.HardlinkTarget.Contains("%basebrdlang%"))
                            {
                                newhardlink = patch.HardlinkTarget.Replace(@"%basebrdlang%", Path.Combine(Variables.brandingFolder, "Basebrd", CultureInfo.CurrentCulture.Name));
                                Installer.PatchMui(newhardlink, patch);
                            }
                            else if (patch.HardlinkTarget.Contains("%winlang%"))
                            {
                                newhardlink = patch.HardlinkTarget.Replace(@"%winlang%", Path.Combine(Variables.windir, CultureInfo.CurrentCulture.Name));
                                Installer.PatchMui(newhardlink, patch);
                            }
                        }
                    }
                }
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
        private static void TakeFullOwnership(string file)
        {
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "takeown.exe") + " /F " + file, AppWinStyle.Hide, true, -1);
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "icacls.exe") + " " + file + " /grant Users:(F)", AppWinStyle.Hide, true, -1);
            Interaction.Shell(Path.Combine(Variables.sys32Folder, "icacls.exe") + " " + file + " /grant Administrators:(F)", AppWinStyle.Hide, true, -1);
        }
    }
}
