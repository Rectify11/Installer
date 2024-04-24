using System;
using System.Runtime.InteropServices;

namespace WinUIForms.Helpers;

public class DWMUtil
{
    internal struct MARGINS
    {
        /// <summary>
        /// Width of the left border that retains its size.
        /// </summary>
        public int cxLeftWidth;

        /// <summary>
        /// Width of the right border that retains its size.
        /// </summary>
        public int cxRightWidth;

        /// <summary>
        /// Height of the top border that retains its size.
        /// </summary>
        public int cyTopHeight;

        /// <summary>
        /// Height of the bottom border that retains its size.
        /// </summary>
        public int cyBottomHeight;

        public MARGINS(int cxLeftWidth, int cxRightWidth, int cyTopHeight, int cyBottomHeight)
        {
            this.cxLeftWidth = cxLeftWidth;
            this.cxRightWidth = cxRightWidth;
            this.cyTopHeight = cyTopHeight;
            this.cyBottomHeight = cyBottomHeight;
        }
        public MARGINS(int cxWidth, int cxHeight)
        {
            cxLeftWidth = cxWidth;
            cxRightWidth = cxWidth;
            cyTopHeight = cxHeight;
            cyBottomHeight = cxHeight;
        }
        public MARGINS(int cxGlobal)
        {
            cxLeftWidth = cxGlobal;
            cxRightWidth = cxGlobal;
            cyTopHeight = cxGlobal;
            cyBottomHeight = cxGlobal;
        }
    }

    internal enum DWMWINDOWATTRIBUTE
    {
        DWMWA_NCRENDERING_ENABLED,
        DWMWA_NCRENDERING_POLICY,
        DWMWA_TRANSITIONS_FORCEDISABLED,
        DWMWA_ALLOW_NCPAINT,
        DWMWA_CAPTION_BUTTON_BOUNDS,
        DWMWA_NONCLIENT_RTL_LAYOUT,
        DWMWA_FORCE_ICONIC_REPRESENTATION,
        DWMWA_FLIP3D_POLICY,
        DWMWA_EXTENDED_FRAME_BOUNDS,
        DWMWA_HAS_ICONIC_BITMAP,
        DWMWA_DISALLOW_PEEK,
        DWMWA_EXCLUDED_FROM_PEEK,
        DWMWA_CLOAK,
        DWMWA_CLOAKED,
        DWMWA_FREEZE_REPRESENTATION,
        DWMWA_PASSIVE_UPDATE_MODE,
        DWMWA_USE_HOSTBACKDROPBRUSH,
        DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        DWMWA_WINDOW_CORNER_PREFERENCE = 33,
        DWMWA_BORDER_COLOR,
        DWMWA_CAPTION_COLOR,
        DWMWA_TEXT_COLOR,
        DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
        DWMWA_SYSTEMBACKDROP_TYPE,
        DWMWA_LAST
    };

    public enum SystemBackdrop
    {
        Automatic = 0,
        None = 1,
        MicaRegular = 2,
        AcrylicRegular = 3,
        MicaAlt = 4
    };

    [DllImport("dwmapi.dll", PreserveSig = true)]
    internal static extern int DwmSetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE attr, ref int attrValue, int attrSize);

    [DllImport("dwmapi.dll", PreserveSig = true)]
    internal static extern void DwmExtendFrameIntoClientArea(nint hWnd, MARGINS margins);

    public static void SetSystemBackdrop(nint hwnd, SystemBackdrop bdrop, bool ShowInClientArea = true)
    {
        if (ShowInClientArea)
            DwmExtendFrameIntoClientArea(hwnd, new(-1));
        else
            DwmExtendFrameIntoClientArea(hwnd, new(0));

        int bdropi = (int)bdrop;
        DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, ref bdropi, sizeof(int));
    }

    public static void EnableImmersiveDarkMode(nint hwnd, bool enable)
    {
        int enablei = enable ? 1 : 0;
        DwmSetWindowAttribute(hwnd, DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ref enablei, sizeof(int));
    }
}
