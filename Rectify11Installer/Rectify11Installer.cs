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

            _Wizard.SetProgressText("Copying files");
            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);
            Directory.CreateDirectory("tmp");

            File.Copy(@"C:\windows\systemresources\shell32.dll.mun", "tmp/shell32.dll.mun");
            File.Copy(@"C:\windows\systemresources\imageres.dll.mun", "tmp/imageres.dll.mun");



            //_Wizard.CompleteInstaller(RectifyInstallerWizardCompleteInstallerEnum.Fail, "not implemented!");
        }

        public void SetParentWizard(IRectifyInstallerWizard wiz)
        {
            _Wizard = wiz;
        }
    }
}
