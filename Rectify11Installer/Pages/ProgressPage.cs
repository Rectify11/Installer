using KPreisser.UI;
using Rectify11Installer.Core;
using Rectify11Installer.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Rectify11Installer.Pages
{
	public partial class ProgressPage : WizardPage
	{
		#region Variables
		private FrmWizard frmwiz;
		private int CurrentTextIndex = -1;
		private static readonly InstallerTexts[] Rectify11InstallerTexts =
		{
			new(Rectify11Installer.Strings.Rectify11.changedEverythingTitle, Rectify11Installer.Strings.Rectify11.changedEverythingText, Properties.Resources.iconnewtree),
			new(Rectify11Installer.Strings.Rectify11.betterDpiTitle, Rectify11Installer.Strings.Rectify11.betterDpiText, Properties.Resources.dpi),
			new(Rectify11Installer.Strings.Rectify11.betterThemeTitle, Rectify11Installer.Strings.Rectify11.betterThemeText, Properties.Resources.themepage),
			new(Rectify11Installer.Strings.Rectify11.betterPerfTitle, Rectify11Installer.Strings.Rectify11.betterPerfText, Properties.Resources.perf),
			new(Rectify11Installer.Strings.Rectify11.betterCplTitle, Rectify11Installer.Strings.Rectify11.betterCplText, Properties.Resources.cp),
			new(Rectify11Installer.Strings.Rectify11.supportTitle, Rectify11Installer.Strings.Rectify11.supportText, Properties.Resources.discord),
			new(Rectify11Installer.Strings.Rectify11.thankYouTitle, Rectify11Installer.Strings.Rectify11.thankYouText, Properties.Resources.cool)
		};
		#endregion
		#region Classes

		private class InstallerTexts
		{
			public string Title { get; set; }
			public string Description { get; set; }
			public Bitmap Side { get; set; }

			public InstallerTexts(string Title, string Description, Bitmap image)
			{
				this.Title = Title;
				this.Description = Description;
				Side = image;
			}
		}
		#endregion
		#region Public methods
		public ProgressPage(FrmWizard frm)
		{
			InitializeComponent();
			frmwiz = frm;
			NavigationHelper.OnNavigate += NavigationHelper_OnNavigate;
		}

		public void StartReset()
		{
			timer1.Stop();
			progressText.Text = Rectify11Installer.Strings.Rectify11.restartingYourPC;
			if (Variables.IsUninstall)
			{
				progressInfo.Text = Rectify11Installer.Strings.Rectify11.uninstallFinishedPrompt;
			}
			else
			{
				progressInfo.Text = Rectify11Installer.Strings.Rectify11.installFinishedPrompt;
			}
			frmwiz.InstallerProgress = "";
			frmwiz.UpdateSideImage = Properties.Resources.done;
			r1.Visible = r2.Visible = true;
			frmwiz.ShowRebootButton = true;
			frmwiz.SetRebootHandler = rebootButton_Click;
		}
		public void Start()
		{
			timer1.Start();
			NextText();
		}
		#endregion
		#region Private Methods

		private async void NavigationHelper_OnNavigate(object sender, EventArgs e)
		{
			if ((WizardPage)sender == RectifyPages.ProgressPage)
			{
				if (Variables.IsUninstall)
				{
					if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified")))
						UninstallOptions.UninstallThemes = true;

					if (Directory.Exists(Path.Combine(Variables.Windir, "nilesoft")))
						UninstallOptions.uninstExtrasList.Add("shellNode");

					if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
						UninstallOptions.uninstExtrasList.Add("asdfNode");

					if (File.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified", "img41.jpg")))
						UninstallOptions.uninstExtrasList.Add("wallpapersNode");

					if (Directory.Exists(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures")))
						UninstallOptions.uninstExtrasList.Add("useravNode");

					if (File.Exists(Path.Combine(Variables.progfiles, "Windows Sidebar", "sidebar.exe")))
						UninstallOptions.uninstExtrasList.Add("gadgetsNode");
					var list = PatchesParser.GetAll();
					var patch = list.Items;
					string path = Path.Combine(Variables.r11Folder, "backup");
					for (var i = 0; i < patch.Length; i++)
					{
						if (!patch[i].HardlinkTarget.Contains("%diag%"))
						{
							var newpath = Helper.FixString(patch[i].HardlinkTarget, !string.IsNullOrWhiteSpace(patch[i].x86));
							if (File.Exists(newpath))
							{
								if (newpath.Contains(".mun"))
								{
									if (File.Exists(Path.Combine(path, patch[i].Mui)))
									{
										UninstallOptions.uninstIconsList.Add(patch[i].Mui);
									}
								}
								else
								{
									if (File.Exists(Path.Combine(path, patch[i].Mui)))
									{
										UninstallOptions.uninstIconsList.Add(patch[i].Mui);
									}
								}
							}

						}
						if (patch[i].HardlinkTarget.Contains("%diag%"))
						{
							var name = patch[i].Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
							var newpath = Helper.FixString(patch[i].HardlinkTarget, false);
							if (File.Exists(newpath))
							{
								if (File.Exists(Path.Combine(path, "Diag", name)))
								{
									UninstallOptions.uninstIconsList.Add(patch[i].Mui);
								}
							}
						}
					}
					Variables.CompleteUninstall = true;
					frmwiz.versionLabel.Visible = false;
					ExtrasOptions.FinalizeIRectify11();
					frmwiz.pictureBox1.Visible = true;
					frmwiz.progressLabel.Visible = true;
					RectifyPages.ProgressPage.Start();
					NativeMethods.SetCloseButton(frmwiz, false);
					Uninstaller uninstaller = new();
					if (!await Task.Run(() => uninstaller.Uninstall(frmwiz)))
					{
						Common.Cleanup();
						Logger.CommitLog();
						TaskDialog.Show(text: Rectify11Installer.Strings.Rectify11.r11InstallErrorPart1 + Path.Combine(Variables.r11Folder, "installer.log") + Rectify11Installer.Strings.Rectify11.r11InstallErrorPart2,
							title: "Error",
							buttons: TaskDialogButtons.OK,
							icon: TaskDialogStandardIcon.Error);
						Application.Exit();
					}
					//Logger.CommitLog();
					if (Variables.RestartRequired)
					{
						NativeMethods.SetCloseButton(frmwiz, false);
						RectifyPages.ProgressPage.StartReset();
					}
					else
					{
						NativeMethods.SetCloseButton(frmwiz, true);
						timer1.Stop();
						frmwiz.InstallerProgress = Rectify11Installer.Strings.Rectify11.doneYouCanClose;
					}
				}
				else
				{
					frmwiz.versionLabel.Visible = false;
					ExtrasOptions.FinalizeIRectify11();
					frmwiz.pictureBox1.Visible = true;
					frmwiz.progressLabel.Visible = true;
					RectifyPages.ProgressPage.Start();
					NativeMethods.SetCloseButton(frmwiz, false);
					Variables.isInstall = true;
					if (Variables.RunUninstaller)
					{
						Uninstaller uninstaller = new();
						if (!await Task.Run(() => uninstaller.Uninstall(frmwiz)))
						{
							Common.Cleanup();
							Logger.CommitLog();
							TaskDialog.Show(text: Rectify11Installer.Strings.Rectify11.r11InstallErrorPart1 + Path.Combine(Variables.r11Folder, "installer.log") + Rectify11Installer.Strings.Rectify11.r11InstallErrorPart2,
								title: "Error",
								buttons: TaskDialogButtons.OK,
								icon: TaskDialogStandardIcon.Error);
							Application.Exit();
						}
						Logger.CommitLog();
					}
					if (Variables.RunInstaller)
					{
						Installer installer = new();
						if (!await Task.Run(() => installer.Install(frmwiz)))
						{
							Common.Cleanup();
							Logger.CommitLog();
							TaskDialog.Show(text: Rectify11Installer.Strings.Rectify11.r11InstallErrorPart1 + Path.Combine(Variables.r11Folder, "installer.log") + Rectify11Installer.Strings.Rectify11.r11InstallErrorPart2,
								title: "Error",
								buttons: TaskDialogButtons.OK,
								icon: TaskDialogStandardIcon.Error);
							Application.Exit();
						}
						Logger.CommitLog();
					}
					if (Variables.RestartRequired)
					{
						NativeMethods.SetCloseButton(frmwiz, false);
						RectifyPages.ProgressPage.StartReset();
					}
					else
					{
						NativeMethods.SetCloseButton(frmwiz, true);
						Variables.isInstall = false;
						Variables.IsUninstall = true;
						timer1.Stop();
						frmwiz.InstallerProgress = Rectify11Installer.Strings.Rectify11.doneYouCanClose;
					}
				}
			}
		}


		/// <summary>
		/// goes to the next text in the list to be shown
		/// </summary>
		private void NextText()
		{
			if (Variables.IsUninstall)
			{
				progressText.Text = Rectify11Installer.Strings.Rectify11.r11UninstallThanks;
				progressInfo.Text = Rectify11Installer.Strings.Rectify11.r11UninstallSubTitle;

			}
			else
			{
				CurrentTextIndex++;
				if (CurrentTextIndex >= Rectify11InstallerTexts.Length)
				{
					CurrentTextIndex = -1;
				}
				else
				{
					var t = Rectify11InstallerTexts[CurrentTextIndex];
					progressText.Text = t.Title;
					progressInfo.Text = t.Description;
					frmwiz.UpdateSideImage = t.Side;
				}
			}
		}
		/// <summary>
		/// routine to perform before restarting
		/// </summary>
		private void RestartRoutine()
		{
			//ApplyScheme();
			//ClearIconCache();
			Variables.isInstall = false;
			Variables.IsUninstall = true;
			if (r1.Checked)
			{
				NativeMethods.Reboot();
			}
			else if (r2.Checked)
				Application.Exit();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			NextText();
		}

		private async void rebootButton_Click(object sender, EventArgs e)
		{
			await Task.Run(() => RestartRoutine());
		}

		#endregion
	}
}
