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
            nextButton.Click += NextButton_Click;
            navBackButton.Click += BackButton_Click;
            cancelButton.Click += CancelButton_Click;
            versionLabel.Text = versionLabel.Text + ProductVersion;
            SetUp();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            //todo
        }
        private void SetUp()
        {
            if (navPane.SelectedTab == wlcmPage)
            {
                tableLayoutPanel1.Visible = false;
                tableLayoutPanel2.Visible = false;
                sideImage.BackgroundImage = Properties.Resources.install;
            }
            else if (navPane.SelectedTab == eulPage)
            {
                tableLayoutPanel1.Visible = true;
                tableLayoutPanel2.Visible = true;
                headerText.Text = Strings.Rectify11.eulaPageHeader;
                sideImage.BackgroundImage = Properties.Resources.eula;
                nextButton.ButtonText = Strings.Rectify11.buttonAgree;
            }
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (navPane.SelectedTab == eulPage)
            {
                navPane.SelectedTab = wlcmPage;
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
            SetUp();
        }
    }
}
