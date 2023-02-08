using KPreisser.UI;
using Microsoft.Win32;
using System;
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
			if (RebootRequired())
			{
				TaskDialog.Show(text: "You cannot install Rectify11 as Windows Updates are pending.",
					instruction: "Compatibility Error",
					title: "Rectify11 Setup",
					buttons: TaskDialogButtons.OK,
					icon: TaskDialogStandardIcon.SecurityErrorRedBar);
				return false;
			}
			return true;
		}
		public static bool UpdateIRectify11()
		{
			InstallOptions.InstallThemes = InstallOptions.iconsList.Contains("themeNode");
			InstallOptions.InstallEP = InstallOptions.iconsList.Contains("epNode");
			InstallOptions.InstallWinver = InstallOptions.iconsList.Contains("winverNode");
			InstallOptions.InstallShell = InstallOptions.iconsList.Contains("shellNode");
			InstallOptions.InstallWallpaper = InstallOptions.iconsList.Contains("wallpapersNode");
			InstallOptions.InstallASDF = InstallOptions.iconsList.Contains("asdfNode");
			return true;
		}
		public static bool FinalizeIRectify11()
		{
			InstallOptions.iconsList.Remove("themeNode");
			InstallOptions.iconsList.Remove("epNode");
			InstallOptions.iconsList.Remove("winverNode");
			InstallOptions.iconsList.Remove("shellNode");
			InstallOptions.iconsList.Remove("wallpapersNode");
			InstallOptions.iconsList.Remove("asdfNode");
			return true;
		}
		public static StringBuilder FinalText()
		{
			System.ComponentModel.ComponentResourceManager resources = new SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
			StringBuilder ok = new StringBuilder();
			ok.AppendLine();
			ok.AppendLine();
			FinalizeIRectify11();
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
			
			if(InstallOptions.InstallASDF)
			{
				ok.AppendLine(resources.GetString("installASDF"));
			}

			if (InstallOptions.InstallShell)
			{
				ok.AppendLine(resources.GetString("installShell"));
			}

			if (InstallOptions.InstallWallpaper)
			{
				ok.AppendLine(resources.GetString("installWallpapers"));
			}

			return ok;
		}
		#endregion
		#region Private Methods
		private static bool RebootRequired()
		{
			using RegistryKey auKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired");
			using RegistryKey cbsKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending");
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
				using RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11");
				if (key == null)
				{
					return false;
				}

				var b = key.GetValue("IsInstalled");
				if (b == null)
				{
					return false;
				}

				var value = (int)b;
				return value == 1;
			}
			set
			{
				using RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11");
				key.SetValue("IsInstalled", value ? 1 : 0);
			}
		}
		public static string InstalledVersion
		{
			get
			{
				using RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11");
				if (key == null)
				{
					return null;
				}

				var b = key.GetValue("Version");
				if (b == null)
				{
					return null;
				}

				var value = (string)b;
				return value;
			}
			set
			{
				using RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11");
				key.SetValue("Version", value);
			}
		}
		#endregion
	}

	public class Logger
	{
		#region Variables
		private static string Text =
			"═════════════════════════════\nSTART: "
			+ DateTime.Now.ToString()
			+ "\nRectify11 Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString()
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
		#endregion
	}
}
