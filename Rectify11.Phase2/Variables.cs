using System;
using System.IO;

namespace Rectify11.Phase2
{
    public class Variables
    {
        public static string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        public static string r11Folder = Path.Combine(windir, "Rectify11");
        public static string r11Files = Path.Combine(r11Folder, "files");
        public static string sys32Folder = Environment.SystemDirectory;
        public static string sysWOWFolder = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
        public static string sysresdir = Path.Combine(windir, "SystemResources");
        public static string brandingFolder = Path.Combine(windir, "Branding");
        public static string progfiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public static string progfiles86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        public static string diag = Path.Combine(windir, "diagnostics", "system");
    }
}
