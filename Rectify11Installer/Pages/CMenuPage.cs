using System;
using System.Linq;

namespace Rectify11Installer.Pages
{
	public partial class CMenuPage : WizardPage
	{

		public CMenuPage()
		{
			InitializeComponent();
		}
		private int getCheckedButton()
        {
			int j=0;
			for (int i=1; i<=5; i++)
            {
				Controls.DarkAwareRadioButton checkedrad = this.Controls.Find("Rad" + i.ToString(), true).FirstOrDefault() as Controls.DarkAwareRadioButton; 
				if (checkedrad.Checked)
                {
					j = i;
					break;
                }
            }
			return j;
        }

		private void setImageAndBool()
        {
            switch (getCheckedButton())
			{
				case 1:{
						//WIP
						break;
					}

				case 2:{
						//WIP
						break;
					}

				case 3:{
						//WIP
						break;
                    }
				case 4:{
						//WIP
						break;
                    }
				case 5:{
						//WIP
						break;
                    }
			}
        }
	}
}
