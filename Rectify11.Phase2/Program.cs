using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11.Phase2
{
    class Program
    {
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
                    if (String.Equals(Path.GetFileName(file), regFile))
                    {
                        Console.WriteLine(file);
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
        public static string sys32Folder = Environment.SystemDirectory;
        public static string brandingFolder = System.IO.Path.Combine(windir, "Branding");
    }
}
