using libmsstyle;
using Microsoft.Win32;
using Rectify11Installer.Controls;
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
            DarkStyle.Load(Application.StartupPath + "\\files\\Dark.msstyles");
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

        public static StylePart? GetButtonPart(VisualStyle v)
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

        public static StylePart? GetCommandLinkPart(VisualStyle v)
        {
            foreach (var classes in v.Classes.Values)
            {
                if (classes.ClassName == "Button")
                {
                    foreach (var parts in classes.Parts)
                    {
                        if (parts.Value.PartName == "COMMANDLINK")
                        {
                            return parts.Value;
                        }
                    }
                }
            }

            return null;
        }

        public static StylePart? GetCommandLinkGlyphPart(VisualStyle v)
        {
            foreach (var classes in v.Classes.Values)
            {
                if (classes.ClassName == "Button")
                {
                    foreach (var parts in classes.Parts)
                    {
                        if (parts.Value.PartName == "COMMANDLINKGLYPH")
                        {
                            return parts.Value;
                        }
                    }
                }
            }

            return null;
        }

        public static StylePart? GetNavArrowPart(VisualStyle v, NavigationButtonType type)
        {
            foreach (var classes in v.Classes.Values)
            {
                if (classes.ClassName == "Navigation")
                {
                    string PartStr = "";
                    switch (type)
                    {
                        case NavigationButtonType.Forward:
                            PartStr = "FORWARDBUTTON";
                            break;
                        case NavigationButtonType.Backward:
                            PartStr = "BACKBUTTON";
                            break;
                        case NavigationButtonType.Menu:
                            PartStr = "MENUBUTTON";
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    foreach (var parts in classes.Parts)
                    {
                        if (parts.Value.PartName == PartStr)
                        {
                            return parts.Value;
                        }
                    }
                }
            }

            return null;
        }

        public static StylePart? GetProgressbarBG(VisualStyle v)
        {
            foreach (var classes in v.Classes.Values)
            {
                if (classes.ClassName == "Progress")
                {
                    foreach (var parts in classes.Parts)
                    {
                        if (parts.Value.PartName == "BAR")
                        {
                            return parts.Value;
                        }
                    }
                }
            }

            return null;
        }

        public static StylePart? GetProgressbarFill(VisualStyle v)
        {
            foreach (var classes in v.Classes.Values)
            {
                if (classes.ClassName == "Progress")
                {
                    foreach (var parts in classes.Parts)
                    {
                        if (parts.Value.PartName == "FILL")
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