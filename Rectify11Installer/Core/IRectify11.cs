using Rectify11Installer.Pages;
using System.Collections.Generic;

namespace Rectify11Installer.Core
{
	public class RectifyPages
	{
		public static WelcomePage WelcomePage = new WelcomePage();
		public static EulaPage EulaPage = new EulaPage();
		public static InstallOptnsPage InstallOptnsPage;
		public static ThemeChoicePage ThemeChoicePage = new ThemeChoicePage();
		public static EPPage EPPage = new EPPage();
		public static InstallConfirmation InstallConfirmation = new InstallConfirmation();
		public static ProgressPage ProgressPage;
	}
	public class InstallOptions
	{
		public static bool InstallEP;
		public static bool InstallASDF;
		public static bool InstallWallpaper;
		public static bool InstallWinver;
		public static bool InstallThemes;
		public static bool InstallShell;
		public static bool InstallIcons;
		public static List<string> iconsList = new List<string>();
	}
	public class Package
	{
		public string Path { get; set; }
		public PackageArch Arch { get; set; }
		public Package(string Path, PackageArch Arch)
		{
			this.Path = Path;
			this.Arch = Arch;
		}
	}
	public enum PackageArch
	{
		Amd64,
		Wow64
	}
}
