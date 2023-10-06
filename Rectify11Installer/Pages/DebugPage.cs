using System;
using System.Reflection;

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
		private void WinUIButton1_Click(object sender, System.EventArgs e)
		{
			Type type = Type.GetType(textBox2.Text);
			object o = Activator.CreateInstance(type);
			MethodInfo method = type.GetMethod(textBox1.Text);
			method.Invoke(o, null);
		}

	}
}
