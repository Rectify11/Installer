using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
	public partial class ProgressPage : WizardPage
	{
		#region Variables
		private frmWizard frmwiz;
		private Timer timer2;
		private int duration = 30;
		private int CurrentTextIndex = -1;
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
		public ProgressPage(frmWizard frm)
		{
			InitializeComponent();
			timer2 = new()
			{
				Interval = 1000
			};
			timer2.Tick += Timer2_Tick;
			frmwiz = frm;
		}
		public void StartReset()
		{
			timer1.Stop();
			progressText.Text = "Restarting your PC";
			progressInfo.Text = "Rectify11 has finished patching your system. Your PC needs to restart in order to apply the changes, it will automatically restart in " + duration.ToString()+ " seconds";
			frmwiz.InstallerProgress = "Restarting in " + duration.ToString() + " seconds";
			frmwiz.UpdateSideImage = global::Rectify11Installer.Properties.Resources.incomplete;
			timer2.Start();
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
		private void rebootButton_Click(object sender, EventArgs e)
		{
			timer2.Stop();
			frmwiz.InstallerProgress = "Restarting...";
			ClearIconCache();
			Win32.NativeMethods.Reboot();
		}
		private void Timer2_Tick(object sender, EventArgs e)
		{
			duration -= 1;
			frmwiz.InstallerProgress = "Restarting in " + duration.ToString() + " seconds";
			if (duration == 0)
			{
				timer2.Stop();
				frmwiz.InstallerProgress = "Restarting...";
				ClearIconCache();
				Win32.NativeMethods.Reboot();
			}
		}
		private void ClearIconCache()
		{
			Interaction.Shell("taskkill.exe /f /im explorer.exe", AppWinStyle.Hide, true);
			try
			{
				DirectoryInfo di = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "microsoft", "windows", "explorer"));
				FileInfo[] files = di.GetFiles("*.db");

				for(int i = 0; i < files.Length; i++)
				{
					files[i].Attributes = FileAttributes.Normal;
					File.Delete(files[i].FullName);
				}
			}
			catch
			{
				MessageBox.Show("deleting icon cache failed");
			}
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
			NextText();
		}
		private static InstallerTexts[] Rectify11InstallerTexts =
		{
			new InstallerTexts("Did you know that...", "Rectify11 has better Win32 DPI support because we scale controls correctly.", Properties.Resources.dpi),
			new InstallerTexts("Rectify11 has a better Theme", "We have tried our best to replicate WinUI controls in our themes, and the dark theme is just amazing.", Properties.Resources.theme),
			new InstallerTexts("Rectify11 has better Performance", "We strongly value performance. You can choose things that you want to debloat in your system.", Properties.Resources.perf),
			new InstallerTexts("Rectify11 has changed everything", "We have changed many icons in many different DLL's, resulting in a more consistent operating system.", Properties.Resources.ep),
			new InstallerTexts("Rectified Control Panel", "We changed many details in the control panel, such as removing old gradients and adding back removed items", Properties.Resources.cp),
			new InstallerTexts("Thank you!", "The team appreciates your support, thank you for installing Rectify11.", Properties.Resources.install)
		};
		private void NextText()
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
		#endregion
	}
}
