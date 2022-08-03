using Rectify11Installer.Pages;
using System;
using System.Drawing;
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
                wlcmPage.BackColor = Color.Black;
                wlcmPage.ForeColor = Color.White;
                eulPage.BackColor = Color.Black;
                eulPage.ForeColor = Color.White;
            }
            wlcmPage.Controls.Add(WelcomePage);
            eulPage.Controls.Add(EulaPage);
            WelcomePage.InstallButton.Click += InstallButton_Click;
            WelcomePage.UninstallButton.Click += UninstallButton_Click;

            SetImages();
        }
        

        void SetImages()
        {
            if (navPane.SelectedTab == wlcmPage)
            {
                sideImage.BackgroundImage = Properties.Resources.install;
            }
            else if (navPane.SelectedTab == eulPage)
            {
                sideImage.BackgroundImage = Properties.Resources.eula;
            }
        }
        private void InstallButton_Click(object sender, EventArgs e)
        {/*
            if (CheckIfUpdatesPending())
            {*/
            navPane.SelectedTab = eulPage;
            //}

        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {/*
            if (CheckIfUpdatesPending())
            {
                Navigate(UninstallConfirmPage);
            }*/
        }

        private void navPane_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetImages();
        }
    }
}
