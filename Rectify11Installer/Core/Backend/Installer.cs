using Microsoft.Win32;
using Rectify11Installer.Win32;
using System;
using System.IO;
using static Rectify11Installer.Win32.NativeMethods;

namespace Rectify11Installer.Core
{
    public class Installer
    {
        #region Public Methods
        public bool Install(FrmWizard frm)
        {
            frm.InstallerProgress = Rectify11Installer.Strings.Rectify11.preparingInstall;
            Logger.WriteLine("Preparing Installation");
            Logger.WriteLine("──────────────────────");

            Directory.CreateDirectory(Variables.r11Folder);

            // goofy fix
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)
                ?.CreateSubKey("Rectify11", true)
                ?.DeleteValue("x86PendingFiles", false);

            if (!Common.WriteFiles(false, false))
                return false;

            if (!Common.CreateDirs())
                return false;

            if (Variables.CreateRestorePoint)
            {
                try
                {
                    frm.InstallerProgress = Rectify11Installer.Strings.Rectify11.creatingRestorePoint;
                    CreateSystemRestorePoint();
                }
                catch (Exception ex)
                {
                    Logger.Warn("Error creating a restore point", ex);
                }
            }

            // runtimes
            frm.InstallerProgress = Rectify11Installer.Strings.Rectify11.installingRt;
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

            // extract files, delete if folder exists
            frm.InstallerProgress = "Extracting files...";
            Helper.SafeDirectoryDeletion(Path.Combine(Variables.r11Folder, "files"), false);
             if (!Helper.SafeFileOperation(
                 Path.Combine(Variables.r11Folder, "files.7z"),
                 Properties.Resources.files,
                 Helper.OperationType.Write))
                 return false;

            // extract the 7z
            Helper.SvExtract("files.7z", "files");

            // theme
            if (InstallOptions.InstallThemes)
            {
                frm.InstallerProgress = Rectify11Installer.Strings.Rectify11.installingThemes;
                if (!Themes.Install()) return false;
            }

            // extras
            if (InstallOptions.InstallExtras())
            {
                frm.InstallerProgress = Rectify11Installer.Strings.Rectify11.installingExtras;
                if (!Extras.Install(frm)) return false;
            }

            // Icons
            if (InstallOptions.iconsList.Count > 0)
            {
                if (!Icons.Install(frm)) return false;
            }

            // copy duires if any icons were patched or if themes was selected (required for r11cpl)
           if (InstallOptions.iconsList.Count > 0 || InstallOptions.InstallThemes)
            {
                File.Copy(Path.Combine(Variables.r11Files, "duires.dll"), Path.Combine(Variables.sys32Folder, "duires.dll"), true);
            }

            InstallChangelogApp();

            frm.InstallerProgress = Rectify11Installer.Strings.Rectify11.creatingUninstaller;
            Common.CreateUninstall();

			InstallStatus.IsRectify11Installed = true;
            Logger.WriteLine("══════════════════════════════════════════════");

            // cleanup
            frm.InstallerProgress = Rectify11Installer.Strings.Rectify11.cleaningUp;
            Common.Cleanup();
            Logger.CommitLog();
            return true;
        }

        private void InstallChangelogApp()
        {
            // Register RectifyStart.exe to run on startup
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            key?.SetValue("RectifyStart", Path.Combine(Variables.r11Folder, "RectifyStart.exe"), RegistryValueKind.String);
            key.Close();
        }
        #endregion
    }
}
