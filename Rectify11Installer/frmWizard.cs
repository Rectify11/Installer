using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer
{
    public partial class frmWizard : Form
    {
        public frmWizard()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            if (Theme.IsUsingDarkMode)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
                welcomePage.BackColor = Color.Black;
                welcomePage.ForeColor = Color.White;
                button1.ForeColor = Color.Black;
                button2.ForeColor = Color.Black;
                eulaPage.BackColor = Color.Black;
                eulaPage.ForeColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            navPane.SelectedTab = eulaPage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            navPane.SelectedTab = welcomePage;
        }
    }
}
