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

namespace Rectify11ControlCenter
{
    public static class Controls
    {
        //PreviewPane
        public static string osV = "OS Version: Windows NT Build " + Environment.OSVersion.Version.ToString();
        public static string userN = "Username: " + System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        public static string CumterName = "PC Name: " + Environment.MachineName;
        public static string theme = "Theme: " + "Not implemented";
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
