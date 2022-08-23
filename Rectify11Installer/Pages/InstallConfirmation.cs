using Rectify11Installer.Core;
using System.Text;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class InstallConfirmation : WizardPage
    {
        public string Summary
        {
            get { return summaryItems.Text; }
            set { summaryItems.Text = value; }
        }
        public InstallConfirmation()
        {
            InitializeComponent();
        }
    }
}
