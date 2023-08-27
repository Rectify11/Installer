using Microsoft.Win32;
using Rectify11Installer.Controls;
using Rectify11Installer.Core;
using System;
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
			try
			{
				if (InstallStatus.IsRectify11Installed)
				{
					cmbInstall.Text = resources.GetString("modifyTitle");
					cmbInstall.Note = resources.GetString("modifyNote");
				}
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
			catch { }
			try
			{
				var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11", false);
				if (key != null)
				{
					var build = key.GetValue("OSVersion");
					Version ver = Version.Parse(build.ToString());
					if (build != null)
					{
						if (Environment.OSVersion.Version.Build > ver.Build || Win32.NativeMethods.GetUbr() > ver.Revision)
						{
                            cmbInstall.Text = resources.GetString("updateTitle");
                            cmbInstall.Note = resources.GetString("updateNote");
                        }
					}
				}
			}
			catch { }
		}
	}
}
