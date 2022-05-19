using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class RebootPage : WizardPage
    {
        public RebootPage()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value -= 1;
            if (progressBar1.Value <= 0)
            {
                winuiButton1_Click(this, new EventArgs());
                timer1.Stop();
            }
        }

        private void winuiButton1_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "-r -t 0");
        }
    }
}
