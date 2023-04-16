using System;

namespace Rectify11Installer.Pages
{
	public partial class DebugPage : WizardPage
	{
		public DebugPage()
		{
			InitializeComponent();
		}
		private void CheckBox1_Click(object sender, EventArgs e)
		{
			Core.Variables.skipUpdateCheck = checkBox1.Checked;
		}
	}
}
