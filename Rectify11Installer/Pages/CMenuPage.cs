using System;
using System.Drawing;
using System.Linq;
using Rectify11Installer.Core;
namespace Rectify11Installer.Pages
{
	public partial class CMenuPage : WizardPage
	{
		public CMenuPage()
		{
			InitializeComponent();
			Rad1.Checked = true;
			for(int i=1; i<=4; i++)
            {
				Controls.DarkAwareRadioButton rad = getButton(i);
				rad.CheckedChanged += CMenuPage_CheckedChanged;
            }
			
		}

        private void CMenuPage_CheckedChanged(object sender, EventArgs e)
        {
			if (((Controls.DarkAwareRadioButton)sender).Checked) 
			{
				getButton(Int32.Parse(((Controls.DarkAwareRadioButton)sender).Name.Replace("Rad","")));
			}

        }

        private Controls.DarkAwareRadioButton getButton(int num)
        {
			Controls.DarkAwareRadioButton rad = this.Controls.Find("Rad" + num.ToString(), true).FirstOrDefault() as Controls.DarkAwareRadioButton;
			if (rad.Checked)
			{
				string a = "L";
				if (Theme.IsUsingDarkMode) a = "D";
				PrevImg.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + num + a);
				InstallOptions.CMenuStyle = num;
			}
			return rad;
		}
	}
}
