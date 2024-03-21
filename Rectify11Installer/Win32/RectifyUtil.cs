using System;
using System.Runtime.InteropServices;

namespace Rectify11Installer.Win32
{
    // Helpers for installing themetool. The implementation of these functions can be found in rectify11cpl
    [Guid("9CD66066-9784-4DA6-A27A-D322FC96D02E")]
    [ComImport]
    public class CRectifyUtil
    {
        
    }

    [Guid("A7BCDC3B-C5A2-44BB-B8EC-560B24ACAAD8")]
    public interface IRectifyUtil
    {
        public int GetMicaSettings(ref bool enabled, ref bool tabbed);
        public int SetMicaForEveryoneEnabled(ref bool enabled, ref bool tabbed);
        public int GetCurrentMenuIndex(ref int index);
        public int SetCurrentMenuByIndex(ref int index);

        public int ApplyTheme(string themeName);
        public int InstallThemeTool();
        public int UninstallThemeTool();
    }

    public static class RectifyThemeUtil
    {
        private static IRectifyUtil utility = null;
        public static IRectifyUtil Utility
        {
            get
            {
                if (utility == null)
                {
                    Init();
                }

                return utility;
            }
        }
        public static void Init()
        {
            if (utility == null)
            {
                utility = (IRectifyUtil)new CRectifyUtil();
            }
        }
    }
}
