using Microsoft.Win32;
using Rectify11Installer.Win32;
using System.IO;
using static Rectify11Installer.Win32.NativeMethods;

namespace Rectify11Installer.Core
{
    public class Installer
    {
        #region Public Methods
        public bool Install(FrmWizard frm)
        {
            frm.InstallerProgress = "Preparing Installation";
            Logger.WriteLine("Preparing Installation");
            Logger.WriteLine("──────────────────────");

            if (!Directory.Exists(Variables.r11Folder))
                Directory.CreateDirectory(Variables.r11Folder);

            // goofy fix
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)
                ?.CreateSubKey("Rectify11", true)
                ?.DeleteValue("x86PendingFiles", false);

            if (!Common.WriteFiles(false, false))
                return false;

            if (!Common.CreateDirs())
                return false;

            try
            {
                frm.InstallerProgress = "Begin creating a restore point";
                CreateSystemRestorePoint(false);
            }
            catch
            {
                Logger.Warn("Error creating a restore point.");
            }

            // runtimes
            frm.InstallerProgress = "Installing runtimes";
            Common.InstallRuntimes();
            if (Variables.vcRedist && Variables.core31)
            {
                Logger.WriteLine("InstallRuntimes() succeeded.");
            }
            else if (!Variables.vcRedist)
            {
                Logger.Warn("vcredist.exe installation failed.");
                Common.RuntimeInstallError("Visual C++ Runtime", "Visual C++ Runtime is used for MicaForEveryone and AccentColorizer.", "https://aka.ms/vs/17/release/vc_redist.x64.exe");
            }
            else if (!Variables.core31)
            {
                Logger.Warn("core31.exe installation failed.");
                Common.RuntimeInstallError(".NET Core 3.1", ".NET Core 3.1 is used for MicaForEveryone.", "https://dotnet.microsoft.com/en-us/download/dotnet/3.1");
            }
            Logger.WriteLine("══════════════════════════════════════════════");

            // some random issue where the installer's frame gets extended
            if (!Theme.IsUsingDarkMode) DarkMode.UpdateFrame(frm, false);


            // theme
            if (InstallOptions.InstallThemes)
            {
                frm.InstallerProgress = "Installing Themes";
                if (!Themes.Install()) return false;
            }

            // extras
            if (InstallOptions.InstallExtras())
            {
                frm.InstallerProgress = "Installing extras";
                if (!Extras.Install(frm)) return false;
            }

            // Icons
            if (InstallOptions.iconsList.Count > 0)
            {
                if (!Icons.Install(frm)) return false;
            }

            frm.InstallerProgress = "Creating uninstaller";
            Common.CreateUninstall();

            InstallStatus.IsRectify11Installed = true;
            Logger.WriteLine("══════════════════════════════════════════════");

            try
            {
                frm.InstallerProgress = "End creating a restore point";
                CreateSystemRestorePoint(true);
            }
            catch
            {
                //ignored
            }

            // cleanup
            frm.InstallerProgress = "Cleaning up...";
            Common.Cleanup();
            Logger.CommitLog();
            return true;
        }
        #endregion
    }
}
