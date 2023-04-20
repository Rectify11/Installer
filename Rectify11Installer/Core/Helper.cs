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
			if (InstallOptions.iconsList.Contains("themeNode"))
			{
				ok.AppendLine(resources.GetString("installThemes"));
			}

			if (InstallOptions.iconsList.Contains("epNode"))
			{
				ok.AppendLine(resources.GetString("installEP"));
			}

			if (InstallOptions.iconsList.Contains("winverNode"))
			{
				ok.AppendLine(resources.GetString("installWinver"));
			}

			if (InstallOptions.iconsList.Contains("asdfNode"))
			{
				ok.AppendLine(resources.GetString("installASDF"));
			}

			if (InstallOptions.iconsList.Contains("shellNode"))
			{
				ok.AppendLine(resources.GetString("installShell"));
			}

			if (InstallOptions.iconsList.Contains("gadgetsNode"))
			{
				ok.AppendLine(resources.GetString("installGadgets"));
			}

			if (InstallOptions.iconsList.Contains("wallpapersNode"))
			{
				ok.AppendLine(resources.GetString("installWallpapers"));
			}

			if (InstallOptions.iconsList.Contains("useravNode"))
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
	public class NavigationHelper
	{
		public static event EventHandler OnNavigate;
		public static void InvokeOnNavigate(object sender, EventArgs e)
		{
			if (OnNavigate != null)
			{
				OnNavigate.Invoke(sender, e);
			}
		}
	}

	#region Pages
	public class RectifyPages
	{
		public static WelcomePage WelcomePage = new();
		public static EulaPage EulaPage = new();
		public static InstallOptnsPage InstallOptnsPage;
		public static ThemeChoicePage ThemeChoicePage = new();
		public static CMenuPage CMenuPage = new();
		public static EPPage EPPage = new();
		public static InstallConfirmation InstallConfirmation;
		public static ProgressPage ProgressPage;
		public static Experimental ExperimentalPage = new();
		public static DebugPage DebugPage = new();
	}
	public class TabPages
	{
		public static Controls.DarkAwareTabPage installPage;
		public static Controls.DarkAwareTabPage themePage;
		public static Controls.DarkAwareTabPage cmenupage;
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
		public static bool InstallEP { get; set; }
		public static bool InstallASDF { get; set; }
		public static bool InstallWallpaper { get; set; }
		public static bool InstallWinver { get; set; }
		public static bool InstallGadgets { get; set; }
		public static bool InstallThemes { get; set; }
		public static bool ThemeDark { get; set; }
		public static bool ThemeBlack { get; set; }
		public static bool ThemeLight { get; set; }
		public static bool InstallShell { get; set; }
		public static bool InstallIcons { get; set; }
		public static bool InstallSounds { get; set; }
		public static bool SkipMFE { get; set; }
		public static bool TabbedNotMica { get; set; }
		public static bool userAvatars { get; set; }
		public static int CMenuStyle = 1;
		public static List<string> iconsList = new();
		public static bool InstallExtras()
		{
			return InstallEP
				   || InstallASDF
				   || InstallWallpaper
				   || InstallGadgets
				   || InstallWinver
				   || InstallShell
				   || InstallSounds
				   || userAvatars;
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
