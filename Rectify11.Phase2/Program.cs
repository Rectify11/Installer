using Microsoft.VisualBasic;
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
            Interaction.Shell("cmd.exe /c takeown /F " + Variables.sysresdir, AppWinStyle.Hide, true);
            Interaction.Shell("cmd.exe /c icacls " + Variables.sysresdir + " /grant Everyone:(F) /c", AppWinStyle.Hide, true);
            foreach (string file in r11dir)
            {
                foreach (string regFile in pendingFiles)
                {
                    if (regFile.Contains(Path.GetFileName(file)))
                    {
                        string newval = "";
                        if (regFile.Contains("%lang%"))
                        {
                            newval = regFile.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
                        }
                        else if (regFile.Contains("mun"))
                        {
                            newval = regFile.Replace(@"%sysresdir%", Variables.sysresdir);
                        }
                        else if (regFile.Contains("%basebrdlang%"))
                        {
                            newval = regFile.Replace(@"%basebrdlang%", Path.Combine(Variables.brandingFolder, "Basebrd", CultureInfo.CurrentUICulture.Name));
                        }
                        else if (regFile.Contains("%winlang%"))
                        {
                            newval = regFile.Replace(@"%winlang%", Path.Combine(Variables.windir, CultureInfo.CurrentUICulture.Name));
                        }

                        string extension = Path.GetExtension(Path.GetFileNameWithoutExtension(newval)) + Path.GetExtension(newval);
                        string final = Path.GetFileName(newval.Replace(extension, "") + "_original" + extension);
                        Console.WriteLine();
                        Console.WriteLine(newval);
                        Console.Write("Original name: ");
                        Console.WriteLine(Path.GetFileName(newval));
                        Console.Write("New name: ");
                        Console.WriteLine(final);
                        Console.Write("Final path: ");
                        Console.WriteLine(Path.Combine(Path.GetDirectoryName(newval), final));

                        Interaction.Shell("cmd.exe /c takeown /F " + newval, AppWinStyle.Hide, true);
                        Interaction.Shell("cmd.exe /c icacls " + newval + " /grant SYSTEM:(F)", AppWinStyle.Hide, true);
                        File.Move(newval, Path.Combine(Path.GetDirectoryName(newval), final));
                        File.Copy(file, newval, true);
                        Interaction.Shell("cmd.exe /c icacls " + newval + " /reset", AppWinStyle.Hide, true);
                        Interaction.Shell("cmd.exe /c icacls " + newval + " /inheritance:d", AppWinStyle.Hide, true);
                        Interaction.Shell("cmd.exe /c icacls " + newval + " /setowner \"NT Service\\TrustedInstaller\"", AppWinStyle.Hide, true);
                        MoveFileEx(Path.Combine(Path.GetDirectoryName(newval), final), null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                }
            }


        }
    }
    public class Variables
    {
        public static string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        public static string r11Folder = System.IO.Path.Combine(windir, "Rectify11");
        public static string r11Files = System.IO.Path.Combine(r11Folder, "files");
        public static string sysresdir = System.IO.Path.Combine(windir, "SystemResources");
        public static string sys32Folder = Environment.SystemDirectory;
        public static string brandingFolder = System.IO.Path.Combine(windir, "Branding");
    }
}
