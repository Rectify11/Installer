using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace Rectify11.Phase2
{
    class Program
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName,
          MoveFileFlags dwFlags);

        [Flags]
        enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 0x00000001,
            MOVEFILE_COPY_ALLOWED = 0x00000002,
            MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
            MOVEFILE_WRITE_THROUGH = 0x00000008,
            MOVEFILE_CREATE_HARDLINK = 0x00000010,
            MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
        }
        static void Main(string[] args)
        {
            string[] pendingFiles = {};
            var r11dir = Directory.GetFiles(Path.Combine(Variables.r11Folder, "Tmp"));
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true).CreateSubKey("Rectify11", false);
            if (reg != null)
                pendingFiles = (string[])reg.GetValue("PendingFiles");
            reg.Close();
            foreach (string file in r11dir)
            {
                foreach (string regFile in pendingFiles)
                {
                    if (regFile.Contains(Path.GetFileName(file)))
                    {
						// TrustedInstaller part
						if (args[0] == "/T")
						{
							string newval = "";
							if (regFile.Contains("mun"))
								newval = regFile.Replace(@"%sysresdir%", Variables.sysresdir);
							Console.WriteLine();
							Console.WriteLine(newval);
							Console.Write("Final path: ");
							Console.WriteLine(Path.Combine(Variables.r11Folder, "Backup"));
							File.Move(newval, Path.Combine(Variables.r11Folder, "Backup", Path.GetFileName(newval)));
							File.Copy(file, newval, true);
						}
                    }
                }
            }
        }
    }
    public class Variables
    {
        public static string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        public static string r11Folder = Path.Combine(windir, "Rectify11");
        public static string r11Files = Path.Combine(r11Folder, "files");
        public static string sysresdir = Path.Combine(windir, "SystemResources");
        public static string sys32Folder = Environment.SystemDirectory;
        public static string brandingFolder = Path.Combine(windir, "Branding");
    }
}
