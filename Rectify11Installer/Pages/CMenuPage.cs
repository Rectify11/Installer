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
			for (int i=1; i<=5; i++)
            {
				Controls.DarkAwareRadioButton rad = this.Controls.Find("Rad" + i.ToString(), true).FirstOrDefault() as Controls.DarkAwareRadioButton;
                rad.CheckedChanged += Rad_CheckedChanged;
			}
			setImageAndType();
		}

        private void Rad_CheckedChanged(object sender, EventArgs e)
        {
			setImageAndType();
		}

        private int getCheckedButton()
        {
			int j=1;
			for (int i=1; i<=5; i++)
            {
				Controls.DarkAwareRadioButton rad = this.Controls.Find("Rad" + i.ToString(), true).FirstOrDefault() as Controls.DarkAwareRadioButton;
				if (rad.Checked)
                {
					j = i;
					break;
                }
            }
			return j;
        }


        private void setImageAndType()
        {
			string a = "L";
			if (Theme.IsUsingDarkMode)
            {
				a = "D";
            }
			PrevImg.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject("_"+getCheckedButton().ToString()+a);
			InstallOptions.CMenuStyle = getCheckedButton();
        }
	}
}
