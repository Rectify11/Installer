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
        private void CheckBox2_Click(object sender, EventArgs e)
        {
            Core.Variables.Phase2Skip = checkBox2.Checked;
        }
    }
}
