using Microsoft.Win32;
using Rectify11Installer.Controls;
using System.IO;
using System.Reflection;

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
            System.ComponentModel.ComponentResourceManager resources = new global::Rectify11Installer.Core.SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));

            // update
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Rectify11", false);
            if (key != null)
            {
                var build = key.GetValue("Build");
                if (build != null && int.Parse(build.ToString()) < Assembly.GetEntryAssembly().GetName().Version.Build)
                {
					cmbInstall.Text = resources.GetString("updateTitle");
                    cmbInstall.Note = resources.GetString("updateNote");

                }
            }
            key.Dispose();
        }
	}
}
