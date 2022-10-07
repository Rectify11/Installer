using System;

namespace Rectify11Installer.Core
{
    public class Variables
    {
        public static string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        public static string r11Folder = System.IO.Path.Combine(windir, "Rectify11");
        public static string r11Files = System.IO.Path.Combine(r11Folder, "files");
        public static string sys32Folder = Environment.SystemDirectory;
        public static string sysWOWFolder = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
        public static string sysresdir = System.IO.Path.Combine(windir, "SystemResources");
        public static string brandingFolder = System.IO.Path.Combine(windir, "Branding");
        public static string progfiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public static string progfiles86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        public static string diag = System.IO.Path.Combine(windir, "diagnostics", "system");
        public static string winSxS = System.IO.Path.Combine(windir, "WinSxS");
        public static bool isInstall = false;
	}
}