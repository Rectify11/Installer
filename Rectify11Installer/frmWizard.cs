using KPreisser.UI;
using Microsoft.Win32;
using Rectify11Installer.Core;
using Rectify11Installer.Pages;
using Rectify11Installer.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Rectify11Installer
{
	public partial class frmWizard : Form
	{
		#region Variables
		public static bool IsItemsSelected;
		private bool acknowledged = false;
		private bool idleinit = false;
		public string InstallerProgress
		{
			get { return progressLabel.Text; }
			set { progressLabel.Text = value; }
		}
		public Image UpdateSideImage
		{
			get { return sideImage.BackgroundImage; }
			set { sideImage.BackgroundImage = value; }
		}
		public bool ShowRebootButton
		{
			get { return tableLayoutPanel2.Visible; }
			set
			{
				nextButton.Visible = false;
				progressLabel.Location = new Point(progressLabel.Location.X, progressLabel.Location.Y - 30);
				pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 30);
				cancelButton.ButtonText = resources.GetString("buttonReboot");
				cancelButton.Click -= CancelButton_Click;
				tableLayoutPanel2.Visible = true;
				if (!Theme.IsUsingDarkMode)
				{
					DarkMode.UpdateFrame(this, true);
				}
			}
		}
		public EventHandler SetRebootHandler
		{
			set { cancelButton.Click += value; }
		}
		private System.ComponentModel.ComponentResourceManager resources = new SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
		#endregion
		#region Main
		public frmWizard()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			InitializeComponent();
			if (System.Globalization.CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft)
			{
				RightToLeftLayout = true;
				RightToLeft = RightToLeft.Yes;
			}
			DarkMode.RefreshTitleBarColor(Handle);
			if (Theme.IsUsingDarkMode)
			{
				DarkMode.UpdateFrame(this, true);
			}

			Navigate(RectifyPages.WelcomePage);
			Shown += FrmWizard_Shown;
			FormClosing += frmWizard_FormClosing;
			Application.Idle += Application_Idle;
		}

		private void Application_Idle(object sender, EventArgs e)
		{
			if (!idleinit)
			{
				// initialize installoptonspage here because it needs 
				// current instance to change button state.
				RectifyPages.InstallOptnsPage = new InstallOptnsPage(this);
				RectifyPages.ProgressPage = new ProgressPage(this);
				expPage.Controls.Add(RectifyPages.ExperimentalPage);
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
				SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
				idleinit = true;
			}
		}

		private void FrmWizard_Shown(object sender, EventArgs e)
		{
			if (Theme.IsUsingDarkMode)
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
				if ((Win32.NativeMethods.GetUbr() != -1
					&& Win32.NativeMethods.GetUbr() < 51
					&& Environment.OSVersion.Version.Build == 22000)
					|| (Environment.OSVersion.Version.Build < 22000
					&& Environment.OSVersion.Version.Build >= 21996))
				{
					tableLayoutPanel1.BackColor = Color.White;
					tableLayoutPanel2.BackColor = Color.White;
					headerText.ForeColor = Color.White;
				}
			}

			wlcmPage.Controls.Add(RectifyPages.WelcomePage);
			RectifyPages.WelcomePage.UninstallButton.Enabled = InstallStatus.IsRectify11Installed;
		}
		#endregion
		#region Navigation
		private async void Navigate(WizardPage page)
		{
			headerText.Text = page.WizardHeader;
			sideImage.BackgroundImage = page.SideImage;
			if (page == RectifyPages.WelcomePage)
			{
				navPane.SelectedTab = wlcmPage;
				tableLayoutPanel1.Visible = false;
				tableLayoutPanel2.Visible = false;
				if (!Theme.IsUsingDarkMode)
				{
					DarkMode.UpdateFrame(this, false);
				}
			}
			else if (page == RectifyPages.ExperimentalPage)
			{
				if (!Theme.IsUsingDarkMode)
				{
					DarkMode.UpdateFrame(this, true);
				}

				tableLayoutPanel1.Visible = true;
				tableLayoutPanel2.Visible = true;
				nextButton.ButtonText = resources.GetString("buttonNext");
				nextButton.Enabled = true;
				navPane.SelectedTab = expPage;
			}
			else if (page == RectifyPages.EulaPage)
			{
				if (acknowledged)
				{
					if (!Theme.IsUsingDarkMode)
					{
						DarkMode.UpdateFrame(this, true);
					}
				}
				tableLayoutPanel1.Visible = true;
				tableLayoutPanel2.Visible = true;
				nextButton.ButtonText = resources.GetString("buttonAgree");
				nextButton.Enabled = true;
				navPane.SelectedTab = eulPage;
			}
			else if (page == RectifyPages.InstallOptnsPage)
			{
				nextButton.ButtonText = resources.GetString("buttonNext");
				if (!IsItemsSelected)
				{
					nextButton.Enabled = false;
				}

				navPane.SelectedTab = installPage;
			}
			else if (page == RectifyPages.ThemeChoicePage)
			{
				nextButton.ButtonText = resources.GetString("buttonNext");
				navPane.SelectedTab = themePage;
			}
			else if (page == RectifyPages.EPPage)
			{
				nextButton.ButtonText = resources.GetString("buttonNext");
				navPane.SelectedTab = epPage;
			}
			else if (page == RectifyPages.InstallConfirmation)
			{
				RectifyPages.InstallConfirmation.Summary = resources.GetString("summaryItems");
				RectifyPages.InstallConfirmation.Summary += Helper.FinalText().ToString();
				nextButton.ButtonText = resources.GetString("buttonInstall");
				navPane.SelectedTab = summaryPage;
			}
			else if (page == RectifyPages.ProgressPage)
			{
				tableLayoutPanel1.Visible = false;
				tableLayoutPanel2.Visible = false;
				versionLabel.Visible = false;
				Helper.FinalizeIRectify11();
				if (!Theme.IsUsingDarkMode)
				{
					DarkMode.UpdateFrame(this, false);
				}

				pictureBox1.Visible = true;
				progressLabel.Visible = true;
				RectifyPages.ProgressPage.Start();
				NativeMethods.SetCloseButton(this, false);
				Variables.isInstall = true;
				navPane.SelectedTab = progressPage;
				Installer installer = new();
				if (!await installer.Install(this))
				{
					Logger.CommitLog();
					MessageBox.Show("Rectify11 setup encountered an error, for more information, see the log in " + Path.Combine(Variables.r11Folder + "installer.log") + ", and report it to rectify11 development server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Application.Exit();
				}
				else
				{
					Logger.CommitLog();
					RectifyPages.ProgressPage.StartReset();
				}
			}
		}
		#endregion
		#region Private Methods
		private void CancelButton_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		private void frmWizard_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!Variables.isInstall)
			{
				// will be replaced with taskdialog.
				DialogResult ok = MessageBox.Show(resources.GetString("exitText"), resources.GetString("Title"), MessageBoxButtons.YesNo, MessageBoxIcon.Information);
				if (ok == DialogResult.No)
				{
					e.Cancel = true;
				}
			}
			else if (Variables.isInstall)
			{
				if (e.CloseReason == CloseReason.UserClosing)
				{
					e.Cancel = true;
				}
			}
			SystemEvents.UserPreferenceChanged -= new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
		}
		private void NextButton_Click(object sender, EventArgs e)
		{
			if (navPane.SelectedTab == expPage)
			{
				if (!acknowledged)
				{
					acknowledged = true;
				}
				Navigate(RectifyPages.EulaPage);
			}
			else if (navPane.SelectedTab == eulPage)
			{
				Navigate(RectifyPages.InstallOptnsPage);
			}
			else if (navPane.SelectedTab == installPage)
			{
				Helper.UpdateIRectify11();
				//MessageBox.Show("EP " + InstallOptions.InstallEP + "\nASDF " + InstallOptions.InstallASDF + "\nWallpapers " + InstallOptions.InstallWallpaper + "\nWinver " + InstallOptions.InstallWinver + "\nShell " + InstallOptions.InstallShell);
				if (InstallOptions.InstallThemes)
				{
					Navigate(RectifyPages.ThemeChoicePage);
				}
				else if (InstallOptions.InstallEP)
				{
					Navigate(RectifyPages.EPPage);
				}
				else
				{
					Navigate(RectifyPages.InstallConfirmation);
				}
			}
			else if (navPane.SelectedTab == themePage)
			{
				if (InstallOptions.InstallEP)
				{
					Navigate(RectifyPages.EPPage);
				}
				else
				{
					Navigate(RectifyPages.InstallConfirmation);
				}
			}
			else if (navPane.SelectedTab == epPage)
			{
				Navigate(RectifyPages.InstallConfirmation);
			}
			else if (navPane.SelectedTab == summaryPage)
			{
				Navigate(RectifyPages.ProgressPage);
			}
		}

		private void BackButton_Click(object sender, EventArgs e)
		{
			if (navPane.SelectedTab == expPage)
			{
				Navigate(RectifyPages.WelcomePage);
			}
			else if (navPane.SelectedTab == eulPage)
			{
				Navigate(RectifyPages.WelcomePage);
			}
			else if (navPane.SelectedTab == installPage)
			{
				Navigate(RectifyPages.EulaPage);
			}
			else if (navPane.SelectedTab == themePage)
			{
				Navigate(RectifyPages.InstallOptnsPage);
			}
			else if (navPane.SelectedTab == epPage)
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
			else if (navPane.SelectedTab == summaryPage)
			{
				if (InstallOptions.InstallEP)
				{
					Navigate(RectifyPages.EPPage);
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
				if (!acknowledged)
				{
					Navigate(RectifyPages.ExperimentalPage);
				}
				else
				{
					Navigate(RectifyPages.EulaPage);
				}
			}
		}

		private void UninstallButton_Click(object sender, EventArgs e)
		{
			if (Helper.CheckIfUpdatesPending())
			{
				TaskDialog.Show(text: "Uninstalling Rectify11 is not yet supported. You can try to run sfc /scannow to revert icon changes.",
				instruction: "Incompleted Software",
				title: "Rectify11 Setup",
				buttons: TaskDialogButtons.OK,
				icon: TaskDialogStandardIcon.SecurityErrorRedBar);
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
						DarkMode.RefreshTitleBarColor(Handle);
						if (Theme.IsUsingDarkMode)
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
						Invalidate(true);
						Update();
					}
					break;
			}
		}
		#endregion
	}
}
