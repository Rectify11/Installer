using KPreisser.UI;
using Rectify11Installer.Core;
using Rectify11Installer.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Rectify11Installer.Pages
{
	public partial class DefenderPage : WizardPage
	{
		#region Classes
		#endregion
		#region Public methods
		public DefenderPage()
		{
			InitializeComponent();
            this.Page = Rectify11Installer.Core.TabPages.defenderPage;
        }
		#endregion
	}
}
