using System.Drawing;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
	public partial class WizardPage : UserControl
	{
		public string WizardHeader { get; set; }
		public Image SideImage { get; set; }
		public bool HeaderVisible { get; set; }
		public bool FooterVisible { get; set; }
		public TabPage Page { get; set; }
		public bool UpdateFrame { get; set; }
		public bool IsWelcomePage { get; set; }
		public bool NextButtonEnabled { get; set; }
		public string NextButtonText { get; set; }
		public WizardPage()
		{
			InitializeComponent();
		}
	}
}
