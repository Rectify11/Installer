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
    public partial class WizardPage : UserControl
    {
        public string WizardTopText { get; set; } = "Wizard Page";
        public bool WizardShowTitle { get; set; } = true;
        public WizardPage()
        {
            InitializeComponent();
        }
    }
}
