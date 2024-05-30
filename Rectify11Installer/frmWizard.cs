using Microsoft.Win32;
using Rectify11Installer.Core;
using Rectify11Installer.Pages;
using Rectify11Installer.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rectify11Installer
{
    public sealed partial class FrmWizard : Form
    {
        #region Variables
        public int _timerFrames;
        public int _timerFramesTmp;
        private bool _isWelcomePage = true;
        private bool _acknowledged;
        private bool _idleInit;
        private int _clicks;
        public string InstallerProgress
        {
            get => progressLabel.Text;
            set
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate () { InstallerProgress = value; });
                }
                else
                {
                    progressLabel.Text = value;
                }
            }
        }
        public Image UpdateSideImage
        {
            get => sideImage.Image;
            set => sideImage.Image = value;
        }
        public bool ShowRebootButton
        {
            get => tableLayoutPanel2.Visible;
            set
            {
                nextButton.Visible = false;
                progressLabel.Location = new Point(progressLabel.Location.X, progressLabel.Location.Y - 30);
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 30);
                cancelButton.ButtonText = "Finish";
                cancelButton.Click -= CancelButton_Click;
                tableLayoutPanel2.Visible = value;
                if (!ThemeUtil.IsUsingDarkMode)
                {
                    DarkMode.UpdateFrame(this, true);
                }
            }
        }
        public EventHandler SetRebootHandler
        {
            set => cancelButton.Click += value;
        }
        #endregion
        #region Main
        public FrmWizard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.CacheText, true);
            if (System.Globalization.CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
            {
                RightToLeftLayout = true;
                RightToLeft = RightToLeft.Yes;
            }
            InitializeComponent();
            DarkMode.RefreshTitleBarColor(Handle);
            if (ThemeUtil.IsUsingDarkMode) DarkMode.UpdateFrame(this, true);
            Shown += FrmWizard_Shown;
            FormClosing += FrmWizard_FormClosing;
            Application.Idle += Application_Idle;
            Navigate(RectifyPages.WelcomePage);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (_idleInit) return;

            // initialize here because it needs instance
            RectifyPages.InstallOptnsPage = new InstallOptnsPage(this);
            RectifyPages.InstallConfirmation = new InstallConfirmation(this);
            RectifyPages.ProgressPage = new ProgressPage(this);

            RectifyPages.WelcomePage.InstallButton.Click += InstallButton_Click;
            RectifyPages.WelcomePage.UninstallButton.Click += UninstallButton_Click;
            nextButton.Click += NextButton_Click;
            navBackButton.Click += BackButton_Click;
            cancelButton.Click += CancelButton_Click;
            versionLabel.Text += ProductVersion;
            ThemeUtil.OnThemeChanged += SystemEvents_UserPreferenceChanged;
            _idleInit = true;
        }

        private void FrmWizard_Shown(object sender, EventArgs e)
        {
            if (ThemeUtil.IsUsingDarkMode)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
                headerText.ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.White;
                ForeColor = Color.Black;
                headerText.ForeColor = Color.Black;
                if (Helper.CheckUBR())
                {
                    tableLayoutPanel1.BackColor = Color.White;
                    tableLayoutPanel2.BackColor = Color.White;
                    headerText.ForeColor = Color.White;
                }
            }

            RectifyPages.WelcomePage.UninstallButton.Enabled = InstallStatus.IsRectify11Installed;
            if (InstallStatus.IsRectify11Installed) _acknowledged = true;
        }
        #endregion
        #region Navigation
        private WizardPage CurrentPage;
        private void Navigate(WizardPage page)
        {
            CurrentPage = page;
            headerText.Text = page.WizardHeader;
            sideImage.Image = page.SideImage;
            tableLayoutPanel1.Visible = page.HeaderVisible;
            tableLayoutPanel2.Visible = page.FooterVisible;
            navPane.Controls.Clear();
            navPane.Controls.Add(page);
            if (!sideImage.Enabled)
                sideImage.Enabled = true;
            if (!ThemeUtil.IsUsingDarkMode)
            {
                DarkMode.UpdateFrame(this, page.UpdateFrame);
            }
            _isWelcomePage = page.IsWelcomePage;
            nextButton.Enabled = page.NextButtonEnabled;
            nextButton.ButtonText = page.NextButtonText;
            NavigationHelper.InvokeOnNavigate((object)page, null);
        }
        #endregion
        #region Private Methods
        private void Timer1_Tick(object sender, EventArgs e)
        {
            _timerFramesTmp++;
            if (_timerFramesTmp == _timerFrames)
            {
                _timerFrames = 0;
                _timerFramesTmp = 0;
                sideImage.Enabled = false;
                timer.Stop();
            }
        }
        private void CancelButton_Click(object sender, EventArgs e) => Application.Exit();
        private void FrmWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Variables.IsUninstall) { }
            else if (!Variables.isInstall)
            {
                if (MessageBox.Show(Strings.Rectify11.exitText, Strings.Rectify11.Title, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) e.Cancel = true;
            }
            else if (e.CloseReason == CloseReason.UserClosing) e.Cancel = true;
            ThemeUtil.OnThemeChanged -= SystemEvents_UserPreferenceChanged;
        }
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (CurrentPage == RectifyPages.DefenderPage)
            {
                if (!_acknowledged) Navigate(RectifyPages.EulaPage);
                else Navigate(RectifyPages.InstallOptnsPage);
            }
            else if (CurrentPage == RectifyPages.EulaPage)
            {
                if (!_acknowledged)
                {
                    _acknowledged = true;
                }
                Navigate(RectifyPages.InstallOptnsPage);
            }
            else if (CurrentPage == RectifyPages.InstallOptnsPage)
            {
                ExtrasOptions.UpdateIRectify11();
                if (InstallOptions.InstallThemes)
                {
                    Navigate(RectifyPages.ThemeChoicePage);
                }
                else if (InstallOptions.InstallShell)
                {
                    Navigate(RectifyPages.CMenuPage);
                }
                else
                {
                    Navigate(RectifyPages.InstallConfirmation);
                }
            }
            else if (CurrentPage == RectifyPages.ThemeChoicePage)
            {
                if (InstallOptions.InstallShell)
                {
                    Navigate(RectifyPages.CMenuPage);
                }
                else
                {
                    Navigate(RectifyPages.InstallConfirmation);
                }
            }
            else if (CurrentPage == RectifyPages.CMenuPage)
            {
                Navigate(RectifyPages.InstallConfirmation);
            }
            else if (CurrentPage == RectifyPages.InstallConfirmation)
            {
                Navigate(RectifyPages.ProgressPage);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (CurrentPage == RectifyPages.DefenderPage)
            {
                Navigate(RectifyPages.WelcomePage);
            }
            else if (CurrentPage == RectifyPages.EulaPage)
            {
                Navigate(RectifyPages.DefenderPage);
            }
            else if (CurrentPage == RectifyPages.InstallOptnsPage)
            {
                Navigate(RectifyPages.WelcomePage);
            }
            else if (CurrentPage == RectifyPages.ThemeChoicePage)
            {
                Navigate(RectifyPages.InstallOptnsPage);
            }
            else if (CurrentPage == RectifyPages.DebugPage)
            {
                Navigate(RectifyPages.WelcomePage);
            }
            else if (CurrentPage == RectifyPages.CMenuPage)
            {
                if (InstallOptions.InstallThemes)
                {
                    Navigate(RectifyPages.ThemeChoicePage);
                }
                else
                {
                    Navigate(RectifyPages.InstallOptnsPage);
                }
            }
            else if (CurrentPage == RectifyPages.InstallConfirmation)
            {
                if (InstallOptions.InstallShell)
                {
                    Navigate(RectifyPages.CMenuPage);
                }
                else if (InstallOptions.InstallThemes)
                {
                    Navigate(RectifyPages.ThemeChoicePage);
                }
                else
                {
                    Navigate(RectifyPages.InstallOptnsPage);
                }
            }
        }

        private void InstallButton_Click(object sender, EventArgs e)
        {
            if (Helper.CheckIfUpdatesPending())
            {
                Navigate(RectifyPages.DefenderPage);
            }
        }

        private void UninstallButton_Click(object sender, EventArgs e)
        {
            if (Helper.CheckIfUpdatesPending())
            {
                if (MessageBox.Show(Strings.Rectify11.uninstallConfirmText, Strings.Rectify11.uninstallTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Variables.IsUninstall = true;
                    Navigate(RectifyPages.ProgressPage);
                }
            }
        }

        private void VersionLabel_Click(object sender, EventArgs e)
        {
            _clicks++;
            if (_clicks != 2) return;
            _clicks = 0;
            Navigate(RectifyPages.DebugPage);
        }
        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
        }
        private void SystemEvents_UserPreferenceChanged(object sender, EventArgs e)
        {
            UserPreferenceChangedEventArgs ev = (UserPreferenceChangedEventArgs)e;
            if (ev.Category == UserPreferenceCategory.General)
            {
                DarkMode.RefreshTitleBarColor(Handle);
                if (ThemeUtil.IsUsingDarkMode)
                {
                    BackColor = Color.Black;
                    ForeColor = Color.White;
                    headerText.ForeColor = Color.White;
                }
                else
                {
                    headerText.ForeColor = Color.Black;
                    BackColor = Color.White;
                    ForeColor = Color.Black;
                }
                if (_isWelcomePage && !ThemeUtil.IsUsingDarkMode)
                {
                    DarkMode.UpdateFrame(this, false);
                }
                else if (Variables.isInstall && !ThemeUtil.IsUsingDarkMode)
                {
                    DarkMode.UpdateFrame(this, false);
                }
                else
                {
                    DarkMode.UpdateFrame(this, true);
                }
                Invalidate(true);
                Update();
            }
        }
        #endregion
    }
}
