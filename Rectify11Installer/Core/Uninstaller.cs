using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Rectify11Installer.Core
{
    public class Uninstaller
    {
        public async Task<bool> Uninstall(FrmWizard frm)
        {
            using var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", true);
            frm.InstallerProgress = "Phase2";
            try
            {
                reg.SetValue("UninstallFiles", InstallOptions.uninstIconsList.ToArray());
                await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun.exe")
                + " /EXEFilename " + '"' + Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe") + '"'
                + " /CommandLine " + "\'" + "/uninstall" + "\'"
                + " /WaitProcess 1 /RunAs 8 /Run", AppWinStyle.NormalFocus, true));

            }
            catch (Exception ex) { }
            return true;
        }
    }
}
