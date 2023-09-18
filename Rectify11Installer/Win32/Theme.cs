using libmsstyle;
using Microsoft.Win32;
using Rectify11Installer.Controls;
using System;
using System.IO;
using System.Linq;

namespace Rectify11Installer.Core
{
    public class Theme
    {
        #region Variables
        public static VisualStyle DarkStyle = new();
        public static VisualStyle LightStyle = new();
        public static bool IsUsingDarkMode { get; private set; }
        public static event EventHandler OnThemeChanged;
        #endregion
        #region Public Methods
        public static void InitTheme()
        {
            IsUsingDarkMode = (int)Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize")?.GetValue("AppsUseLightTheme") <= 0;
            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        }

        public static void LoadTheme()
        {
            DarkStyle.Load(Path.Combine(Path.GetTempPath(), "Dark.msstyles"));
            LightStyle.Load(Path.Combine(Path.GetTempPath(), "light.msstyles"));
        }

        public static StylePart GetNavArrowPart(VisualStyle v, NavigationButtonType type)
        {
            return (from classes in v.Classes.Values
                    where classes.ClassName == "Navigation"
                    let partStr = type switch
                    {
                        NavigationButtonType.Forward => "FORWARDBUTTON",
                        NavigationButtonType.Backward => "BACKBUTTON",
                        NavigationButtonType.Menu => "MENUBUTTON",
                        _ => throw new NotImplementedException()
                    }
                    from parts in classes.Parts
                    where parts.Value.PartName == partStr
                    select parts.Value).FirstOrDefault();
        }
        public static StylePart GetCommandLinkPart(VisualStyle v)
        {
            return (from classes in v.Classes.Values
                    where classes.ClassName == "Button"
                    from parts in classes.Parts
                    where parts.Value.PartName == "COMMANDLINK"
                    select parts.Value).FirstOrDefault();
        }
        public static StylePart GetGroupBox(VisualStyle v)
        {
            return (from classes in v.Classes.Values
                    where classes.ClassName == "Button"
                    from parts in classes.Parts
                    where parts.Value.PartName == "GROUPBOX"
                    select parts.Value).FirstOrDefault();
        }
        public static StylePart GetCommandLinkGlyphPart(VisualStyle v)
        {
            return (from classes in v.Classes.Values
                    where classes.ClassName == "Button"
                    from parts in classes.Parts
                    where parts.Value.PartName == "COMMANDLINKGLYPH"
                    select parts.Value).FirstOrDefault();
        }
        public static StylePart GetButtonPart(VisualStyle v)
        {
            return (from classes in v.Classes.Values
                    where classes.ClassName == "Button"
                    from parts in classes.Parts
                    where parts.Value.PartName == "PUSHBUTTON"
                    select parts.Value).FirstOrDefault();
        }
        #endregion
        #region Private Methods
        private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.General)
            {
                InitTheme();
                OnThemeChanged?.Invoke(sender, e);
            }
        }
    }
    #endregion
}