using KPreisser.UI;
using Microsoft.Win32;
using Rectify11Installer.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Rectify11Installer.Core
{
	public class Helper
	{
		#region Public Methods
		public static bool CheckIfUpdatesPending()
		{
			if (Variables.skipUpdateCheck)
			{
				return true;
			}

			if (!RebootRequired()) return true;
			TaskDialog.Show(text: "You cannot install Rectify11 as Windows Updates are pending. Please reboot your system.",
				instruction: "Compatibility Error",
				title: "Rectify11 Setup",
				buttons: TaskDialogButtons.OK,
				icon: TaskDialogStandardIcon.SecurityErrorRedBar);
			return false;
		}

		public static StringBuilder FinalText()
		{
			System.ComponentModel.ComponentResourceManager resources = new SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
			var ok = new StringBuilder();
			ok.AppendLine();
			ok.AppendLine();
			ExtrasOptions.FinalizeIRectify11();
			if (InstallOptions.iconsList.Count > 0)
			{
				ok.AppendLine(resources.GetString("installIcons"));
			}
			if (InstallOptions.InstallThemes)
			{
				ok.AppendLine(resources.GetString("installThemes"));
			}

			if (InstallOptions.InstallEP)
			{
				ok.AppendLine(resources.GetString("installEP"));
			}

			if (InstallOptions.InstallWinver)
			{
				ok.AppendLine(resources.GetString("installWinver"));
			}

			if (InstallOptions.InstallASDF)
			{
				ok.AppendLine(resources.GetString("installASDF"));
			}

			if (InstallOptions.InstallShell)
			{
				ok.AppendLine(resources.GetString("installShell"));
			}

			if (InstallOptions.InstallGadgets)
			{
				ok.AppendLine(resources.GetString("installGadgets"));
			}

			if (InstallOptions.InstallWallpaper)
			{
				ok.AppendLine(resources.GetString("installWallpapers"));
			}

			if (InstallOptions.userAvatars)
			{
				ok.AppendLine(resources.GetString("userAvatars"));
			}
			return ok;
		}
		#endregion
		#region Private Methods
		private static bool RebootRequired()
		{
			using var auKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired");
			using var cbsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending");
			return (auKey != null || cbsKey != null);
		}
		#endregion
	}

	public class InstallStatus
	{
		#region Public Methods
		public static bool IsRectify11Installed
		{
			get
			{
				using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11");
				var b = key?.GetValue("IsInstalled");
				var value = (int?)b;
				return value == 1;
			}
			set
			{
				using var key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11");
				key?.SetValue("IsInstalled", value ? 1 : 0);
			}
		}
		public static string InstalledVersion
		{
			get
			{
				using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11");
				var b = key?.GetValue("Version");
				var value = (string)b;
				return value;
			}
			set
			{
				using var key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11");
				key?.SetValue("Version", value);
			}
		}
		#endregion
	}

	#region Pages
	public class RectifyPages
	{
		public static WelcomePage WelcomePage = new();
		public static EulaPage EulaPage = new();
		public static InstallOptnsPage InstallOptnsPage;
		public static ThemeChoicePage ThemeChoicePage = new();
		public static EPPage EPPage = new();
		public static InstallConfirmation InstallConfirmation = new();
		public static ProgressPage ProgressPage;
		public static Experimental ExperimentalPage = new();
		public static DebugPage DebugPage = new();
	}
	public class TabPages
	{
		public static Controls.DarkAwareTabPage installPage;
		public static Controls.DarkAwareTabPage themePage;
		public static Controls.DarkAwareTabPage epPage;
		public static Controls.DarkAwareTabPage summaryPage;
		public static Controls.DarkAwareTabPage progressPage;
		public static Controls.DarkAwareTabPage rebootPage;
		public static Controls.DarkAwareTabPage wlcmPage;
		public static Controls.DarkAwareTabPage eulPage;
		public static Controls.DarkAwareTabPage expPage;
		public static Controls.DarkAwareTabPage debPage;
	}
	public class InstallOptions
	{
		public static bool InstallEP;
		public static bool InstallASDF;
		public static bool InstallWallpaper;
		public static bool InstallWinver;
		public static bool InstallGadgets;
		public static bool InstallThemes;
		public static bool ThemeDark;
		public static bool ThemeBlack;
		public static bool ThemeLight;
		public static bool InstallShell;
		public static bool InstallIcons;
		public static bool SkipMFE;
		public static bool TabbedNotMica;
		public static bool userAvatars;
		public static List<string> iconsList = new();
		public static bool InstallExtras()
		{
			return InstallEP
				   || InstallASDF
				   || InstallWallpaper
				   || InstallGadgets
				   || InstallWinver
				   || InstallShell;
		}
	}
	#endregion

	public class Logger
	{
		#region Variables
		private static string Text =
			"═════════════════════════════\nSTART: "
			+ DateTime.Now.ToString(CultureInfo.CurrentCulture)
			+ "\nRectify11 Version: " + Assembly.GetExecutingAssembly().GetName().Version
			+ "\n═════════════════════════════\n";
		#endregion
		#region Public Methods
		public static void WriteLine(string s)
		{
			Text += s + "\n";
		}
		public static void WriteLine(string s, Exception ex)
		{
			Text += s + ". " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine;
		}
		public static void CommitLog()
		{
			System.IO.File.WriteAllText(System.IO.Path.Combine(Variables.r11Folder, "installer.log"), Text);
		}

		public static void Warn(string v)
		{
			WriteLine("[WARNING] " + v);
		}
		public static void Warn(string v, Exception ex)
		{
			WriteLine("[WARNING] " + v, ex);
		}

		public static void LogFile(string file, bool error, Exception ex)
		{
			if (error)
			{
				if (ex != null)
				{
					Logger.WriteLine("Error while writing " + file + ". " + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine);
				}
				else
				{
					Logger.WriteLine("Error while writing " + file + ". (No exception information)");
				}
			}
			else
			{
				Logger.WriteLine("Wrote " + file);
			}
		}

		#endregion
	}
}
