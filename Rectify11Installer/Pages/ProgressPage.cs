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
			new("Rectify11 has changed everything", "We have modernized icons across the whole system, resulting in a more consistent and fluent user experience.", Properties.Resources.iconnewtree),
			new("Did you know?", "Rectify11 has better Win32 DPI support because we scale controls correctly.", Properties.Resources.dpi),
			new("Rectify11 has a more consistent theme.", "We have tried our best to replicate WinUI controls in our themes and the dark theme is just amazing.", Properties.Resources.themepage),
			new("Rectify11 has better performance", "We strongly value performance. In future releases, you will be able to choose things that you want to debloat in your system.", Properties.Resources.perf),
			new("Rectified Control Panel", "We improved many details in the control panel, such as modernizing old visuals and adding back removed items", Properties.Resources.cp),
			new("Need help or technical support?", "You can ask us anything on our official Discord server. The link is on the GitHub page, where you have downloaded the installer.", Properties.Resources.discord),
			new("Thank you!", "We appreciate your support, thank you for installing Rectify11.", Properties.Resources.cool)
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
			progressText.Text = "Restarting your PC";
			if (Variables.IsUninstall)
			{
				progressInfo.Text = "Rectify11 has finished uninstalling. Your device needs to restart in order to complete the uninstallation";
			}
			else
			{
				progressInfo.Text = "Rectify11 has finished installing. Your device needs to restart in order to complete the installation";
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
						TaskDialog.Show(text: "Rectify11 setup encountered an error, for more information, see the log in " + Path.Combine(Variables.r11Folder, "installer.log") + ", and report it to rectify11 development server",
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
						frmwiz.InstallerProgress = "Done, you can close this window";
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
							TaskDialog.Show(text: "Rectify11 setup encountered an error, for more information, see the log in " + Path.Combine(Variables.r11Folder, "installer.log") + ", and report it to rectify11 development server",
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
							TaskDialog.Show(text: "Rectify11 setup encountered an error, for more information, see the log in " + Path.Combine(Variables.r11Folder, "installer.log") + ", and report it to rectify11 development server",
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
						frmwiz.InstallerProgress = "Done, you can close this window";
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
				progressText.Text = "Thanks for using Rectify11";
				progressInfo.Text = "Uninstallation will be done in a few moments.";

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
