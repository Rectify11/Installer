using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Rectify11Installer.Win32.NativeMethods;

namespace Rectify11Installer.Core
{
    public class Uninstaller
    {
        public async Task<bool> Uninstall(FrmWizard frm)
        {
            if (UninstallOptions.uninstIconsList.Count > 0)
            {
                using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", true);
                frm.InstallerProgress = "Phase2";
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2);
                File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun);
                try
                {
                    reg.SetValue("UninstallFiles", UninstallOptions.uninstIconsList.ToArray());
                    if (!Variables.Phase2Skip)
                    {
                        await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun.exe")
                        + " /EXEFilename " + '"' + Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe") + '"'
                        + " /CommandLine " + "\'" + "/uninstall" + "\'"
                        + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true));
                    }
                }
                catch { }
                var oldFiles = Directory.GetFiles(Path.Combine(Variables.Windir, "Temp"), "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < oldFiles.Length; i++)
                {
                    MoveFileEx(oldFiles[i], null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                File.Delete(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"));
                File.Delete(Path.Combine(Variables.r11Folder, "aRun.exe"));
            }
            if (UninstallOptions.UninstallThemes)
            {
                frm.InstallerProgress = "Uninstalling themes";
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im micaforeveryone.exe", AppWinStyle.Hide, true));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im micafix.exe", AppWinStyle.Hide, true));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "taskkill.exe") + " /f /im explorerframe.exe", AppWinStyle.Hide, true));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn mfe", AppWinStyle.Hide));
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "schtasks.exe") + " /delete /f /tn micafix", AppWinStyle.Hide));
                var s = Properties.Resources.SecureUxHelper_x64;
                if (NativeMethods.IsArm64()) s = Properties.Resources.SecureUxHelper_arm64;
                try
                {
                    File.WriteAllBytes(Path.Combine(Variables.Windir, "SecureUXHelper.exe"), s);
                }
                catch { }
                if (Theme.IsUsingDarkMode)
                {
                    await Task.Run(() => Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "dark.theme")));
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.Windir, "SecureUXHelper.exe") + " apply " + '"' + "Windows (dark)" + '"', AppWinStyle.Hide, true));
                }
                else
                {
                    await Task.Run(() => Process.Start(Path.Combine(Variables.Windir, "Resources", "Themes", "aero.theme")));
                    await Task.Run(() => Interaction.Shell(Path.Combine(Variables.Windir, "SecureUXHelper.exe") + " apply " + '"' + "Windows (light)" + '"', AppWinStyle.Hide, true));
                }
                if (Directory.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified")))
                { 
                    List<string> wallpapers = new List<string>
                    {
                        "cosmic.png",
                        "img0.png",
                        "img19.png",
                        "metal.png"
                    };
                    for (int i = 0; i < wallpapers.Count; i++)
                    {
                        if (File.Exists(Path.Combine(Variables.Windir, "web", "Wallpaper" , "Rectified", wallpapers[i])))
                        {
                            File.Delete(Path.Combine(Variables.Windir, "web", "Wallpaper", "Rectified", wallpapers[i]));
                        }
                    }
                }
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.Windir, "SecureUXHelper.exe") + " uninstall", AppWinStyle.Hide, true));
                if (File.Exists(Path.Combine(Variables.Windir, "Themetool.exe")))
                {
                    File.Delete(Path.Combine(Variables.Windir, "Themetool.exe"));
                }
                if (Directory.Exists(Path.Combine(Variables.Windir, "cursors", "WindowsRectified")))
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "cursors", "WindowsRectified"), true);
                }
                if (Directory.Exists(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark")))
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "cursors", "WindowsRectifiedDark"), true);
                }
                List<string> themefiles = new List<string>
                {
                    "black.theme",
                    "darkcolorized.theme",
                    "darkrectified.theme",
                    "lightrectified.theme"
                };
                for (int i = 0; i < themefiles.Count; i++)
                {
                    if (File.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i])))
                    {
                        File.Delete(Path.Combine(Variables.Windir, "Resources", "Themes", themefiles[i]));
                    }
                }
                if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified")))
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Shell"), true);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Aero.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Black.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "Dark.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified", "DarkColorized.msstyles"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    MoveFileEx(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified"), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                }
                File.Delete(Path.Combine(Variables.Windir, "SecureUXHelper.exe"));

                // waow
                if (Directory.Exists(Path.Combine(Variables.Windir, "MicaForEveryone")))
                {
                    Directory.Delete(Path.Combine(Variables.Windir, "MicaForEveryone"));
                }
			    var key = Registry.ClassesRoot.OpenSubKey(@"CLSID", true);
                key.DeleteSubKeyTree("{959E11F4-0A48-49cf-8416-FF9BC49D9656}", false);
                key.Dispose();
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace", true);
                key.DeleteSubKeyTree("{959E11F4-0A48-49cf-8416-FF9BC49D9656}", false);
                key.Dispose();
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\FontSubstitutes");
                key.SetValue("MS Shell Dlg 2", "Tahoma");
                key.SetValue("MS Shell Dlg", "Microsoft Sans Serif");
                key.Dispose();
                key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop\WindowMetrics");
                key.SetValue("MenuHeight", "-285");
                key.SetValue("MenuWidth", "-285");
                key.Dispose();
            }
            if (UninstallOptions.uninstExtrasList.Count > 0)
            {

            }
            frm.InstallerProgress = "Done, you can close this window";
            return true;
        }
    }
}
