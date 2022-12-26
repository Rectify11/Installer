using KPreisser.UI;
using Microsoft.Win32;
using System.Text;

namespace Rectify11Installer.Core
{
	public class Helper
	{
		#region Public Methods
		public static bool CheckIfUpdatesPending()
		{
			if (RebootRequired())
			{
				TaskDialog.Show(text: "You cannot install Rectify11 as Windows Updates are pending.",
					instruction: "Compatibility Error",
					title: "Rectify11 Setup",
					buttons: TaskDialogButtons.OK,
					icon: TaskDialogStandardIcon.SecurityErrorRedBar);

				return false;
			}
			else
			{
				return true;
			}
		}
		public static bool UpdateIRectify11()
		{
			if (InstallOptions.iconsList.Contains("themeNode"))
			{
				InstallOptions.InstallThemes = true;
			}
			else
			{
				InstallOptions.InstallThemes = false;
			}

			if (InstallOptions.iconsList.Contains("epNode"))
			{
				InstallOptions.InstallEP = true;
			}
			else
			{
				InstallOptions.InstallEP = false;
			}

			if (InstallOptions.iconsList.Contains("winverNode"))
			{
				InstallOptions.InstallWinver = true;
			}
			else
			{
				InstallOptions.InstallWinver = false;
			}

			if (InstallOptions.iconsList.Contains("shellNode"))
			{
				InstallOptions.InstallShell = true;
			}
			else
			{
				InstallOptions.InstallShell = false;
			}

			if (InstallOptions.iconsList.Contains("wallpapersNode"))
			{
				InstallOptions.InstallWallpaper = true;
			}
			else
			{
				InstallOptions.InstallWallpaper = false;
			}

			if (InstallOptions.iconsList.Contains("asdfNode"))
			{
				InstallOptions.InstallASDF = true;
			}
			else
			{
				InstallOptions.InstallASDF = false;
			}

			return true;
		}
		public static bool FinalizeIRectify11()
		{
			if (InstallOptions.iconsList.Contains("themeNode"))
			{
				InstallOptions.iconsList.Remove("themeNode");
			}

			if (InstallOptions.iconsList.Contains("epNode"))
			{
				InstallOptions.iconsList.Remove("epNode");
			}

			if (InstallOptions.iconsList.Contains("winverNode"))
			{
				InstallOptions.iconsList.Remove("winverNode");
			}

			if (InstallOptions.iconsList.Contains("shellNode"))
			{
				InstallOptions.iconsList.Remove("shellNode");
			}

			if (InstallOptions.iconsList.Contains("wallpapersNode"))
			{
				InstallOptions.iconsList.Remove("wallpapersNode");
			}

			return true;
		}
		public static StringBuilder FinalText()
		{
			System.ComponentModel.ComponentResourceManager resources = new SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
			StringBuilder ok = new StringBuilder();
			ok.AppendLine();
			ok.AppendLine();
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
}
