using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Core
{
    public class Logger
    {
        private static string Text = "";
        private static FileStream? fs;
        public static void WriteLine(string s)
        {
            lock (Text)
            {
                if (!File.Exists("installer.log"))
                {
                    File.WriteAllText("installer.log", "");
                }
                Text += s + "\n";

                if (fs == null)
                {
                    fs = new FileStream("installer.log", FileMode.Create, FileAccess.Write);
                }
            
                byte[] bt = Encoding.ASCII.GetBytes(Text);
                fs.Write(bt, 0, bt.Length);
                fs.Flush();
            }
        }
    }
}
