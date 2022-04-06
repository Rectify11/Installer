using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class InstalllOptnsPage : WizardPage
    {
        public bool ShouldInstallExplorerPatcher
        {
            get
            {
                return chkExploderPatcher.Checked;
            }
        }
        public InstalllOptnsPage()
        {
            InitializeComponent();
        }
    }
}
