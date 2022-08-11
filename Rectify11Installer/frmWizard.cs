using Microsoft.Win32;
using Rectify11Installer.Core;
using Rectify11Installer.Pages;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rectify11Installer
{
    public partial class frmWizard : Form
    {
        public static bool IsItemsSelected;
        public frmWizard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            if (Theme.IsUsingDarkMode)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.White;
                ForeColor = Color.Black;
            }
            // initialize installoptonspage here because it needs 
            // current instance to change button state.
            RectifyPages.InstallOptnsPage = new InstallOptnsPage(this);

            wlcmPage.Controls.Add(RectifyPages.WelcomePage);
            eulPage.Controls.Add(RectifyPages.EulaPage);
            installPage.Controls.Add(RectifyPages.InstallOptnsPage);
            themePage.Controls.Add(RectifyPages.ThemeChoicePage);
            epPage.Controls.Add(RectifyPages.EPPage);
            progressPage.Controls.Add(RectifyPages.ProgressPage);
            summaryPage.Controls.Add(RectifyPages.InstallConfirmation);
            RectifyPages.WelcomePage.InstallButton.Click += InstallButton_Click;
            RectifyPages.WelcomePage.UninstallButton.Click += UninstallButton_Click;
            nextButton.Click += NextButton_Click;
            navBackButton.Click += BackButton_Click;
            cancelButton.Click += CancelButton_Click;
            versionLabel.Text = versionLabel.Text + ProductVersion;
            Navigate(RectifyPages.WelcomePage);
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
            RectifyPages.WelcomePage.UninstallButton.Enabled = Properties.Settings.Default.IsInstalled;
            /*
            MessageBox.Show(Properties.Settings.Default.IsInstalled.ToString());
            Properties.Settings.Default.IsInstalled = true;
            Properties.Settings.Default.Save();
            */
        }

        #region Navigation
        private void Navigate(WizardPage page)
        {
            headerText.Text = page.WizardHeader;
            sideImage.BackgroundImage = page.SideImage;
            if (page == RectifyPages.WelcomePage)
            {
                navPane.SelectedTab = wlcmPage;
                tableLayoutPanel1.Visible = false;
                tableLayoutPanel2.Visible = false;
            }
            else if (page == RectifyPages.EulaPage)
            {
                tableLayoutPanel1.Visible = true;
                tableLayoutPanel2.Visible = true;
                nextButton.ButtonText = Strings.Rectify11.buttonAgree;
                nextButton.Enabled = true;
                navPane.SelectedTab = eulPage;
            }
            else if (page == RectifyPages.InstallOptnsPage)
            {
                nextButton.ButtonText = Strings.Rectify11.buttonNext;
                if (!IsItemsSelected)
                    nextButton.Enabled = false;
                navPane.SelectedTab = installPage;
            }
            else if (page == RectifyPages.ThemeChoicePage)
            {
                navPane.SelectedTab = themePage;
            }
            else if (page == RectifyPages.EPPage)
            {
                navPane.SelectedTab = epPage;
            }
            else if (page == RectifyPages.InstallConfirmation)
            {
                RectifyPages.InstallConfirmation.Summary = Strings.Rectify11.summaryItems;
                StringBuilder ok = new StringBuilder();
                ok.AppendLine();
                ok.AppendLine();
                if (InstallOptions.iconsList.Contains("epNode"))
                {
                    InstallOptions.InstallEP = true;
                    ok.AppendLine("Install ExplorerPatcher");
                }
                if (InstallOptions.iconsList.Contains("winverNode"))
                {
                    InstallOptions.InstallWinver = true;
                    ok.AppendLine("Install Winver");
                }
                if (InstallOptions.iconsList.Contains("shellNode"))
                {
                    InstallOptions.InstallShell = true;
                    ok.AppendLine("Install Shell");
                }
                if (InstallOptions.iconsList.Contains("wallpapersNode"))
                {
                    InstallOptions.InstallWallpaper = true;
                    ok.AppendLine("Install Wallpapers");
                }
                if (InstallOptions.iconsList.Contains("themeNode"))
                {
                    InstallOptions.InstallWallpaper = true;
                    ok.AppendLine("Install Themes");
                }
                RectifyPages.InstallConfirmation.Summary += ok.ToString();
                nextButton.ButtonText = Strings.Rectify11.buttonInstall;
                navPane.SelectedTab = summaryPage;
            }
            else if (page == RectifyPages.ProgressPage)
            {
                navPane.SelectedTab = progressPage;
            }
        }
        #endregion
        #region Private Methods
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (navPane.SelectedTab == eulPage)
                Navigate(RectifyPages.InstallOptnsPage);
            else if (navPane.SelectedTab == installPage)
                Navigate(RectifyPages.ThemeChoicePage);
            else if (navPane.SelectedTab == themePage)
                Navigate(RectifyPages.EPPage);
            else if (navPane.SelectedTab == epPage)
                Navigate(RectifyPages.InstallConfirmation);
            else if (navPane.SelectedTab == summaryPage)
                Navigate(RectifyPages.ProgressPage);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (navPane.SelectedTab == eulPage)
                Navigate(RectifyPages.WelcomePage);
            else if (navPane.SelectedTab == installPage)
                Navigate(RectifyPages.EulaPage);
            else if (navPane.SelectedTab == themePage)
                Navigate(RectifyPages.InstallOptnsPage);
            else if (navPane.SelectedTab == epPage)
                Navigate(RectifyPages.ThemeChoicePage);
            else if (navPane.SelectedTab == summaryPage)
                Navigate(RectifyPages.EPPage);
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (Helper.CheckIfUpdatesPending())
                Navigate(RectifyPages.EulaPage);
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (Helper.CheckIfUpdatesPending())
            {
                //Navigate(UninstallConfirmPage);
            }
        }
        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            switch (e.Category)
            {
                case UserPreferenceCategory.General:
                    {
                        Theme.InitTheme();
                        if (Theme.IsUsingDarkMode)
                        {
                            BackColor = Color.Black;
                            ForeColor = Color.White;
                        }
                        else
                        {
                            BackColor = Color.White;
                            ForeColor = Color.Black;
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}
