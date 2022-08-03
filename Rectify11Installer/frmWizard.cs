using Rectify11Installer.Pages;
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
        WelcomePage WelcomePage = new WelcomePage();
        EulaPage EulaPage = new EulaPage();
        public frmWizard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            if (Theme.IsUsingDarkMode)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
                welcomePage.BackColor = Color.Black;
                welcomePage.ForeColor = Color.White;
                eulaPage.BackColor = Color.Black;
                eulaPage.ForeColor = Color.White;
            }
            welcomePage.Controls.Add(WelcomePage);
            eulaPage.Controls.Add(EulaPage);
            WelcomePage.InstallButton.Click += InstallButton_Click;
            WelcomePage.UninstallButton.Click += UninstallButton_Click;
        }
        private void InstallButton_Click(object sender, EventArgs e)
        {/*
            if (CheckIfUpdatesPending())
            {*/
            navPane.SelectedTab = eulaPage;
            //}

        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {/*
            if (CheckIfUpdatesPending())
            {
                Navigate(UninstallConfirmPage);
            }*/
        }
    }
}
