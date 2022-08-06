using Rectify11Installer.Controls;

namespace Rectify11Installer.Pages
{
    public partial class WelcomePage : WizardPage
    {
        public FakeCommandLink InstallButton
        {
            get => cmbInstall;
            set => cmbInstall = value;
        }
        public FakeCommandLink UninstallButton
        {
            get => cmbUninstall;
            set => cmbUninstall = value;
        }
        public WelcomePage()
        {
            InitializeComponent();
        }
    }
}
