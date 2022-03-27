using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer
{
    public class Theme
    {
        public static bool IsUsingDarkMode
        {
            get
            {
                using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                var registryValueObject = key?.GetValue("AppsUseLightTheme");
                if (registryValueObject == null)
                {
                    return false;
                }
                var registryValue = (int)registryValueObject;

                return registryValue <= 0;
            }
        }
    }
}
