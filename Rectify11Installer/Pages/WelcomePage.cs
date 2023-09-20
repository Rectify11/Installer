using Rectify11Installer.Controls;
using Rectify11Installer.Core;

namespace Rectify11Installer.Pages
{
	public partial class WelcomePage : WizardPage
	{
		public FakeCommandLink InstallButton
		{
			get { return cmbInstall; }
			set { cmbInstall = value; }
		}
		public FakeCommandLink UninstallButton
		{
			get { return cmbUninstall; }
			set { cmbUninstall = value; }
		}
		public WelcomePage()
		{
			InitializeComponent();
            // update
            if (InstallStatus.IsRectify11Installed)
            {
                cmbInstall.Text = Strings.Rectify11.modifyTitle;
                cmbInstall.Note = Strings.Rectify11.modifyNote;
            }
            if (Helper.CheckIfUpdate())
			{
				cmbInstall.Text = Strings.Rectify11.updateTitle;
				cmbInstall.Note = Strings.Rectify11.updateNote;
			}
        }
	}
}
