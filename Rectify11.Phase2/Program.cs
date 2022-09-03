using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                        Console.WriteLine(newval);
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
