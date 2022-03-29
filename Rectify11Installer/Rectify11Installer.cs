using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer
{
    public class RectifyInstaller : IRectifyInstaller
    {
        private IRectifyInstallerWizard? _Wizard;
        public void Install()
        {
            if (_Wizard == null)
            {
                throw new Exception("SetParentWizard() in IRectifyInstaller was not called!");
            }

            _Wizard.SetProgressText("Installing SecureUXTheme");

            Thread.Sleep(1000);
            _Wizard.CompleteInstaller(RectifyInstallerWizardCompleteInstallerEnum.Fail, "not implemented!");
        }

        public void SetParentWizard(IRectifyInstallerWizard wiz)
        {
            _Wizard = wiz;
        }
    }
}
