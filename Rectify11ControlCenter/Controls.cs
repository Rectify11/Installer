using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
namespace Rectify11ControlCenter
{
    public static class Controls
    {
        //PreviewPane
        public static string osV = "OS: " + Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment.OperatingSystem + " NT Build " + Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment.OperatingSystemVersion;
        public static string userN = "Username: " + Environment.UserName;
        public static string CumterName = "PC Name: " + Environment.MachineName;
        public static string theme()
        {
            string pathTheme = "";
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes", false);
            pathTheme = regKey.GetValue("CurrentTheme").ToString();
            regKey.Close();
            if (File.Exists(pathTheme))
            {
                var MyIni = new IniFile(pathTheme);
                string themename = MyIni.Read("DisplayName", "Theme");
                return "Theme: " + themename;
            }
            else
            {
                string s = "Theme: Not Implemented";
                return s;
            }
        }
        public static string r11Version = "Rectify11 Version: " + Assembly.GetEntryAssembly().GetName().Version.ToString();
        public static Image deskimg()
        {
            string pathWallpaper = "";
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
            pathWallpaper = regKey.GetValue("WallPaper").ToString();
            regKey.Close();
            Image image = Image.FromFile(pathWallpaper);
            return image;
        }

    }
}
