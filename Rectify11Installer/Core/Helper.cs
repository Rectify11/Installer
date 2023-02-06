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
}
