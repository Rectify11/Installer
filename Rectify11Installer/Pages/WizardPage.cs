using System.Drawing;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class WizardPage : UserControl
    {
        public string WizardHeader { get; set; }
        public Image SideImage { get; set; }
        public WizardPage()
        {
            InitializeComponent();
        }
    }
}
