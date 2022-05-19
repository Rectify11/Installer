using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
    public partial class FormBackground : Form
    {
        FrmWizard Wizard;
        public FormBackground(bool setupMode)
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Wizard = new FrmWizard(setupMode);
            this.BackgroundImage = Properties.Resources.rectify;
        }

        private void FormBackground_Shown(object sender, EventArgs e)
        {
            Wizard.TopMost = true;
            Wizard.FormClosing += Wizard_FormClosing;
            Wizard.Show();
        }

        private void Wizard_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Close();
        }
    }
}
