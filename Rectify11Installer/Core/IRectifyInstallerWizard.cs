using Rectify11Installer.Core;
using Rectify11Installer.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer
{
    //
    //
    //   IRectifyInstaller Interface
    //
    //

    public interface IRectifyInstallerWizard
    {
        /// <summary>
        /// Sets progress bar value
        /// </summary>
        /// <param name="val"></param>
        void SetProgress(int val);
        /// <summary>
        /// Sets the text by the progress bar
        /// </summary>
        /// <param name="text"></param>
        void SetProgressText(string text);
        /// <summary>
        /// Tell the installer that it's work is completed.
        /// </summary>
        void CompleteInstaller(RectifyInstallerWizardCompleteInstallerEnum type, bool IsInstalling, string ErrorDescription = "");
    }
    public enum RectifyInstallerWizardCompleteInstallerEnum
    {
        Success,
        Fail
    }

    //
    //
    //   RectifyInstallerWizard implementation
    //
    //

    internal class RectifyInstallerWizard : IRectifyInstallerWizard
    {
        private readonly FrmWizard Wizard;
        private readonly ProgressPage ProgressPage;
        private bool ShownCompletionPage = false;
        internal RectifyInstallerWizard(FrmWizard wizard, ProgressPage pg)
        {
            this.Wizard = wizard;
            this.ProgressPage = pg;
        }

        public void CompleteInstaller(RectifyInstallerWizardCompleteInstallerEnum type, bool IsInstalling, string ErrorDescription = "")
        {
            if (ShownCompletionPage)
                return;
            ShownCompletionPage = true;
            Logger.CloseLog();
            ProgressPage.Invoke((MethodInvoker)delegate ()
            {
                Wizard.Complete(type, IsInstalling, ErrorDescription);

            });    
        }

        public void SetProgress(int val)
        {
            ProgressPage.Invoke((MethodInvoker)delegate ()
            {
                ProgressPage.ProgressBarDef.Value = val;
            });
        }

        public void SetProgressText(string text)
        {
            ProgressPage.Invoke((MethodInvoker)delegate ()
            {
                ProgressPage.CurrentProgressText.Text = text;
            });
        }
    }

    //
    //
    //   RectifyInstaller Interface
    //
    //
    /// <summary>
    /// The class implementing this interface is what installs Rectify11
    /// </summary>
    public interface IRectifyInstaller
    {
        /// <summary>
        /// Install Rectify11
        /// </summary>
        /// <param name="options">Installer options</param>
        void Install(IRectifyInstalllerInstallOptions options);
        /// <summary>
        /// Uninstall Rectify11
        /// </summary>
        /// <param name="options">Installer options</param>
        void Uninstall(IRectifyInstalllerUninstallOptions options);
        /// <summary>
        /// Used for storing the IRectifyInstallerWizard instance
        /// </summary>
        /// <param name="wiz">The IRectifyInstallerWizard instance</param>
        void SetParentWizard(IRectifyInstallerWizard wiz);
    }

    public interface IRectifyInstalllerInstallOptions
    {
        public bool ShouldInstallExplorerPatcher { get; }
        public bool ShouldInstallThemes { get; }
        public bool ShouldInstallWallpaper { get; }
        public bool ShouldInstallWinver { get; }
        public bool DoSafeInstall { get; }
    }
    public class InstallerOptions : IRectifyInstalllerInstallOptions
    {
        public bool ShouldInstallExplorerPatcher { get; set; }

        public bool ShouldInstallThemes { get; set; }

        public bool ShouldInstallWallpaper { get; set; }

        public bool ShouldInstallWinver { get; set; }

        public bool DoSafeInstall { get; set; }
    }

    public interface IRectifyInstalllerUninstallOptions
    {
        public bool RemoveExplorerPatcher { get; }
        public bool RemoveThemesAndThemeTool { get; }
        public bool RestoreWallpapers { get; }
        public bool RestoreWinver { get; }
    }
}
