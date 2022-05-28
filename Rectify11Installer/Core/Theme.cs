using libmsstyle;
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
        public static VisualStyle DarkStyle = new VisualStyle();
        public static VisualStyle LightStyle = new VisualStyle();
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


        public static void LoadTheme()
        {
            DarkStyle.Load(Application.StartupPath+"\\files\\Dark.msstyles");
            LightStyle.Load(Application.StartupPath + "\\files\\light.msstyles");
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

        public static StylePart GetButtonPart(VisualStyle v)
        {
            foreach (var classes in v.Classes.Values)
            {
                if (classes.ClassName == "Button")
                {
                    foreach (var parts in classes.Parts)
                    {
                        if (parts.Value.PartName == "PUSHBUTTON")
                        {
                            return parts.Value;
                        }
                    }
                }
            }

            return null;
        }

        
    }
}
