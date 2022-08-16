using libmsstyle;
using Microsoft.Win32;
using Rectify11Installer.Controls;
using Rectify11Installer.Core;
using System;
using System.IO;

namespace Rectify11Installer
{
    public class Theme
    {
        public static VisualStyle DarkStyle = new VisualStyle();
        public static VisualStyle LightStyle = new VisualStyle();
        public static bool IsUsingDarkMode { get; private set; }
        public static void InitTheme()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
            var registryValueObject = key.GetValue("AppsUseLightTheme");
            if (registryValueObject == null)
            {  }
            var registryValue = (int)registryValueObject;
            IsUsingDarkMode = registryValue <= 0;
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }
        public static event EventHandler OnThemeChanged;

        public static void LoadTheme()
        {
            DarkStyle.Load(Path.Combine(Variables.r11Folder, "Dark.msstyles"));
            LightStyle.Load(Path.Combine(Variables.r11Folder, "light.msstyles"));
        }

        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            switch (e.Category)
            {
                case UserPreferenceCategory.General:
                    if (OnThemeChanged != null)
                    {
                        InitTheme();
                        OnThemeChanged.Invoke(sender, e);
                    }
                    break;
            }
        }
        
        public static StylePart GetNavArrowPart(VisualStyle v, NavigationButtonType type)
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
        public static StylePart GetCommandLinkPart(VisualStyle v)
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
        public static StylePart GetGroupBox(VisualStyle v)
        {
            foreach (var classes in v.Classes.Values)
            {
                if (classes.ClassName == "Button")
                {
                    foreach (var parts in classes.Parts)
                    {
                        if (parts.Value.PartName == "GROUPBOX")
                        {
                            return parts.Value;
                        }
                    }
                }
            }

            return null;
        }
        public static StylePart GetCommandLinkGlyphPart(VisualStyle v)
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