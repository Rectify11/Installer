using Rectify11Installer.Controls;

namespace Rectify11Installer.Pages
{
    public partial class WelcomePage : WizardPage
    {
        public FakeCommandLink InstallButton
        {
            get
            {
                return cmbInstallNOW;
            }
            set
            {
                cmbInstallNOW = value;
            }
        }
        public FakeCommandLink UninstallButton
        {
            get
            {
                return cmbUninstall;
            }
            set
            {
                cmbUninstall = value;
            }
        }
        public WelcomePage()
        {
            InitializeComponent();
        }
    }
}
