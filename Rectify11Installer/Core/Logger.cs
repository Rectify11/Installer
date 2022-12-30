using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
#nullable enable

namespace Rectify11Installer.Core
{
	public class Logger
    {
        private static string Text = "";
        private static FileStream? fs;
        private static bool StartText = false;
        public static void WriteLine(string s)
        {
            lock (Text)
            {
                try
                {
                    if (!File.Exists(Path.Combine(Variables.r11Folder, "installer.log")))
                    {
                        File.WriteAllText(Path.Combine(Variables.r11Folder, "installer.log"), "");
                    }
                    Text += s + "\n";

                    if (fs == null)
                    {
                        fs = new FileStream("installer.log", FileMode.Create, FileAccess.Write);
                        if (!StartText)
                        {
                            Text = "=========================\nSTART: " + DateTime.Now.ToString() + "\n=========================\n" + Text;
                            StartText = true;
                        }
                    }


                    fs.Seek(0, SeekOrigin.End);
                    byte[] bt = Encoding.ASCII.GetBytes(Text);
                    fs.Write(bt, 0, bt.Length);
                    fs.Flush();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A error occured while trying to write to the log file. It is safe to ignore this message.\n" + ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void CloseLog()
        {
            if (fs != null)
                fs.Close();
        }

        public static void Warn(string v)
        {
            WriteLine("[WARNING] " + v);
        }
    }
}
