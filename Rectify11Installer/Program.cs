using KPreisser.UI;
using Rectify11Installer.Core;
using Rectify11Installer.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Threading;
using System.Windows.Forms;

namespace Rectify11Installer
{
	internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			using var mutex = new Mutex(false, "Rectify11Setup");
			bool isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero);
			if (isAnotherInstanceOpen) return;
			if (Environment.OSVersion.Version.Build <= 21343)
			{
				if (args.Length != 0 && args[0].ToLower() == "--allow")
				{ }
				else
				{
					bool yes = false;
					TaskDialog td = new();
					td.Page.Text = "Windows 10 build 21343 or higher is recommended in order to install Rectify11.";
					td.Page.Instruction = "Compatibility Error";
					td.Page.Title = "Rectify11 Setup";
					td.Page.StandardButtons = TaskDialogButtons.OK;
					td.Page.Icon = TaskDialogStandardIcon.SecurityErrorRedBar;
					td.Page.EnableHyperlinks = true;
					TaskDialogExpander tde = new();
					tde.Text = "<a href=\"link1\">Run anyway (not recommended)</a>";
					tde.Expanded = false;
					tde.CollapsedButtonText = "More information";
					tde.ExpandedButtonText = "Less information";
					td.Page.HyperlinkClicked += (s, e) =>
					{
						yes = true;
						td.Close();
					};
					td.Page.Expander = tde;
					td.Show();
					if (!yes) return;
				}
			}
			ProfileOptimization.SetProfileRoot(Path.Combine(Path.GetTempPath(), "Rectify11"));
			ProfileOptimization.StartProfile("Startup.Profile");
			if (Environment.OSVersion.Version.Build >= 10240)
			{
				Theme.InitTheme();
				if ((Environment.OSVersion.Version.Build >= 17763) && (Environment.OSVersion.Version.Build < 18362))
				{
					DarkMode.AllowDarkModeForApp(true);
				}
				else if (Environment.OSVersion.Version.Build >= 18362)
				{
					DarkMode.SetPreferredAppMode(DarkMode.PreferredAppMode.AllowDark);
				}
			}
			if (!Directory.Exists(Variables.r11Folder))
			{
				Directory.CreateDirectory(Variables.r11Folder);
			}

			if ((!File.Exists(Path.Combine(Variables.r11Folder, "Dark.msstyles"))) && (!File.Exists(Path.Combine(Variables.r11Folder, "light.msstyles"))))
			{
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Dark.msstyles"), Properties.Resources.Dark);
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "light.msstyles"), Properties.Resources.light);
			}
			Theme.LoadTheme();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ko");
			Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentUICulture;
			Application.Run(new FrmWizard());
			mutex.ReleaseMutex();
		}
	}
}