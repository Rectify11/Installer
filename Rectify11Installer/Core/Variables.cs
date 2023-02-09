using System;

namespace Rectify11Installer.Core
{
	/// <summary>
	/// Static variables
	/// </summary>
	public class Variables
	{
		//TODO: For v4 add iso patching

		public static string Windir => Environment.GetFolderPath(Environment.SpecialFolder.Windows);

		public static string r11Folder => System.IO.Path.Combine(Windir, "Rectify11");

		public static string r11Files => System.IO.Path.Combine(r11Folder, "files");

		public static string sys32Folder => Environment.SystemDirectory;

		public static string sysWOWFolder => Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);

		public static string sysresdir => System.IO.Path.Combine(Windir, "SystemResources");

		public static string BrandingFolder => System.IO.Path.Combine(Windir, "Branding");

		public static string progfiles => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

		public static string progfiles86 => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

		public static string diag => System.IO.Path.Combine(Windir, "diagnostics", "system");

		public static string winSxS => System.IO.Path.Combine(Windir, "WinSxS");

		public static bool isInstall { get; set; } = false;

		public static bool skipUpdateCheck { get; set; } = false;

		public static bool IsItemsSelected { get; set; }
	}
}