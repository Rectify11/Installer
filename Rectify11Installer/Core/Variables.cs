using System;

namespace Rectify11Installer.Core
{
	/// <summary>
	/// Static variables
	/// </summary>
	public class Variables
	{
		//TODO: For v4 add iso patching


		public static string Windir
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            }
		}
		public static string r11Folder
		{
			get
			{
				return System.IO.Path.Combine(Windir, "Rectify11");
            }
		}
		public static string r11Files
		{
			get
			{
				return System.IO.Path.Combine(r11Folder, "files");
            }
		}
		public static string sys32Folder
		{
			get
			{
				return Environment.SystemDirectory;
            }
		}
		public static string sysWOWFolder
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
            }
		}
		public static string sysresdir
		{
			get
			{
				return System.IO.Path.Combine(Windir, "SystemResources");
            }
		}
		public static string BrandingFolder
		{
			get
			{
				return System.IO.Path.Combine(Windir, "Branding");
            }
		}
		public static string progfiles
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }
		}
		public static string progfiles86
		{
			get
			{
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            }
		}
		public static string diag
		{
			get
			{
				return System.IO.Path.Combine(Windir, "diagnostics", "system");
            }
		}
		public static string winSxS
		{
			get
			{
                return System.IO.Path.Combine(Windir, "WinSxS");
            }
		}
		public static bool isInstall { get; set; } = false;
	}
}