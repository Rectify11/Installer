using Microsoft.Win32;
using System;
using System.IO;

namespace Rectify11Installer.Core
{
    public class Uninstaller
    {
        public bool Uninstall(FrmWizard frm)
        {
            if (UninstallOptions.uninstIconsList.Count > 0)
            {
                Logger.WriteLine("Uninstalling icons");
                Logger.WriteLine("──────────────────");
                frm.InstallerProgress = "Uninstalling icons";
                Icons.Uninstall();
                Variables.RestartRequired = true;
                Console.WriteLine("══════════════════════════════════════════════");
            }
            if (UninstallOptions.UninstallThemes)
            {
                Logger.WriteLine("Uninstalling themes");
                Logger.WriteLine("───────────────────");
                frm.InstallerProgress = "Uninstalling themes";
                Themes.Uninstall();
                Variables.RestartRequired = true;
                Console.WriteLine("══════════════════════════════════════════════");
            }
            if (UninstallOptions.uninstExtrasList.Count > 0)
            {
                Logger.WriteLine("Uninstalling extras");
                Logger.WriteLine("───────────────────");
                frm.InstallerProgress = "Uninstalling Extras";
                Extras.Uninstall();
                Console.WriteLine("══════════════════════════════════════════════");
            }
            // cleanup
            Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Tmp"), false);
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
            {
                if (Directory.GetFiles(Path.Combine(Variables.r11Folder, "Backup")).Length == 0
                    && Directory.GetDirectories(Path.Combine(Variables.r11Folder, "Backup")).Length == 0)
                {
                    Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "Backup"), false);
                }
            }
            Logger.CommitLog();
            // complete uninstall
            if (Variables.CompleteUninstall)
            {
                var files = Directory.GetFiles(Variables.r11Folder);
                for (int j = 0; j < files.Length; j++)
                {
                    Helper.SafeFileDeletion(files[j]);
                }
                if (Directory.GetFiles(Variables.r11Folder).Length == 0 
                    && Directory.GetDirectories(Variables.r11Folder).Length == 0)
                {
                    Helper.SafeDirectoryDeletion(Variables.r11Folder, false);
                }
                using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true);
                reg.DeleteSubKey("Rectify11", false);
                using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
                key.DeleteSubKey("Rectify11", false);
            }
            if (!Variables.RestartRequired) frm.InstallerProgress = "Done, you can close this window";
            return true;
        }
    }
}
