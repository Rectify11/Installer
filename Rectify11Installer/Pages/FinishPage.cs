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
    public partial class FinishPage : WizardPage
    {
        public Label MainText
        {
            get
            {
                return lblText;
            }
            set
            {
                lblText = value;
            }
        }
        public FinishPage()
        {
            InitializeComponent();
        }
    }
}
