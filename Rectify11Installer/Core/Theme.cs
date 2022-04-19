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
        public static event EventHandler? OnThemeChanged;

        static Theme()
        {
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            switch (e.Category)
            {
                case UserPreferenceCategory.General:
                    if (OnThemeChanged != null)
                        OnThemeChanged.Invoke(sender, e);
                    break;
            }
        }
    }
}
