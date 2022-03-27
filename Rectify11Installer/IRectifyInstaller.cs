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

    public interface IRectifyInstaller
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
        void CompleteInstaller(RectifyInstallerCompleteInstallerEnum type, string ErrorDescription = "");
    }
    public enum RectifyInstallerCompleteInstallerEnum
    {
        Success,
        Fail
    }

    //
    //
    //   RectifyInstaller implementation
    //
    //

    internal class RectifyInstaller : IRectifyInstaller
    {
        private readonly frmWizard Wizard;
        private readonly ProgressPage ProgressPage;
        internal RectifyInstaller(frmWizard wizard, ProgressPage pg)
        {
            this.Wizard = wizard;
            this.ProgressPage = pg;
        }

        public void CompleteInstaller(RectifyInstallerCompleteInstallerEnum type, string ErrorDescription = "")
        {
            throw new NotImplementedException();
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
}
