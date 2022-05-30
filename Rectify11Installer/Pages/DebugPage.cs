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
    public partial class DebugPage : WizardPage
    {
        public DebugPage()
        {
            InitializeComponent();
        }

        private void navigationButton3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(navigationButton3, new Point(10,10));
        }
    }
}
