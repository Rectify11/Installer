using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win32UIDemo.Pages
{
    public partial class WelcomePage : WizardPage
    {
        public CommandLinkButton InstallButton
        {
            get
            {
                return cmbInstallNOW;
            }
            set
            {
                cmbInstallNOW = value;
            }
        }
        public CommandLinkButton UninstallButton
        {
            get
            {
                return cmbUninstall;
            }
            set
            {
                cmbUninstall = value;
            }
        }
        public WelcomePage()
        {
            InitializeComponent();
        }
    }
}
