using Rectify11Installer.Controls;
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
    public partial class ProgressPage : WizardPage
    {
        public Label CurrentProgressText
        {
            get
            {
                return lblCurrent;
            }
            set
            {
                lblCurrent = value;
            }
        }
        public CustomProgressBar ProgressBarDef
        {
            get
            {
                return ProgressBar;
            }
            set
            {
                ProgressBar = value;
            }
        }
        public ProgressPage()
        {
            InitializeComponent();
        }
    }
}
