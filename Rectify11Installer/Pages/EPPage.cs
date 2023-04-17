using System;

namespace Rectify11Installer.Pages
{
	public partial class EPPage : WizardPage
	{

		public EPPage()
		{
			InitializeComponent();
		}

		private void w10StartImg_Click(object sender, EventArgs e)
		{
			w10StartRad.Checked = true;
		}

		private void w11StartImg_Click(object sender, EventArgs e)
		{
			w11StartRad.Checked = true;
		}
	}
}
