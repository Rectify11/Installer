using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Rectify11Installer.Win32;

namespace Rectify11Installer.Win32
{
    public class DarkMode
    {
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        internal static extern int SetPreferredAppMode(PreferredAppMode appMode);
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        internal static extern int AllowDarkModeForApp(bool allow);
        [DllImport("uxtheme.dll", EntryPoint = "#133")]
        internal static extern int AllowDarkModeForWindow(IntPtr handle, bool allow);
        [DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WINDOWCOMPOSITIONATTRIBDATA data);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);
        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, WINDOWCOMPOSITIONATTRIB dwAttribute, ref int pvAttribute, int cbAttribute);

        public static void RefreshTitleBarColor(IntPtr hWnd)
        {
            if (Environment.OSVersion.Version.Build < 18362)
                SetProp(hWnd, "UseImmersiveDarkModeColors", new IntPtr(Theme.IsUsingDarkMode ? 1 : 0));
            else
            {
                int size = Marshal.SizeOf(Theme.IsUsingDarkMode);
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(Theme.IsUsingDarkMode, ptr, false);
                WINDOWCOMPOSITIONATTRIBDATA data = new WINDOWCOMPOSITIONATTRIBDATA
                {
                    Attrib = WINDOWCOMPOSITIONATTRIB.WCA_USEDARKMODECOLORS,
                    pvData = ptr,
                    cbData = size
                };
                SetWindowCompositionAttribute(hWnd, ref data);
            }
        }
        public static void UpdateFrame(frmWizard frm, bool yes)
        {
            bool extend = Theme.IsUsingDarkMode;

            if (Environment.OSVersion.Version.Build >= 22523)
            {
                int micaValue = 0x02;
                DwmSetWindowAttribute(frm.Handle, WINDOWCOMPOSITIONATTRIB.DWMWA_SYSTEMBACKDROP_TYPE, ref micaValue, Marshal.SizeOf(typeof(int)));
            }

            else
            {
                int trueValue = 0x01;
                DwmSetWindowAttribute(frm.Handle, WINDOWCOMPOSITIONATTRIB.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
            }
            bool DarkMode = Theme.IsUsingDarkMode;
            NativeMethods.MARGINS m = new NativeMethods.MARGINS();

            if (DarkMode)
            {
                m.cyTopHeight = frm.Height;
            }
            else
            {
                m.cyTopHeight = frm.tableLayoutPanel1.Height + 1;
                m.cyBottomHeight = frm.tableLayoutPanel2.Height + 5;
            }
            if (yes)
                NativeMethods.DwmExtendFrameIntoClientArea(frm.Handle, ref m);
            else
            {
                NativeMethods.MARGINS mar = new NativeMethods.MARGINS
                {
                    cxLeftWidth = 0, cxRightWidth = 0, cyBottomHeight = 0, cyTopHeight = 0
                };
                NativeMethods.DwmExtendFrameIntoClientArea(frm.Handle, ref mar);
            }
        }
        internal enum PreferredAppMode
        {
            Default,
            AllowDark,
            ForceDark,
            ForceLight,
            Max
        }
        public enum WINDOWCOMPOSITIONATTRIB
        {
            WCA_UNDEFINED = 0,
            WCA_NCRENDERING_ENABLED = 1,
            WCA_NCRENDERING_POLICY = 2,
            WCA_TRANSITIONS_FORCEDISABLED = 3,
            WCA_ALLOW_NCPAINT = 4,
            WCA_CAPTION_BUTTON_BOUNDS = 5,
            WCA_NONCLIENT_RTL_LAYOUT = 6,
            WCA_FORCE_ICONIC_REPRESENTATION = 7,
            WCA_EXTENDED_FRAME_BOUNDS = 8,
            WCA_HAS_ICONIC_BITMAP = 9,
            WCA_THEME_ATTRIBUTES = 10,
            WCA_NCRENDERING_EXILED = 11,
            WCA_NCADORNMENTINFO = 12,
            WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,
            WCA_VIDEO_OVERLAY_ACTIVE = 14,
            WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15,
            WCA_DISALLOW_PEEK = 16,
            WCA_CLOAK = 17,
            WCA_CLOAKED = 18,
            WCA_ACCENT_POLICY = 19,
            WCA_FREEZE_REPRESENTATION = 20,
            WCA_EVER_UNCLOAKED = 21,
            WCA_VISUAL_OWNER = 22,
            WCA_HOLOGRAPHIC = 23,
            WCA_EXCLUDED_FROM_DDA = 24,
            WCA_PASSIVEUPDATEMODE = 25,
            WCA_USEDARKMODECOLORS = 26,
            WCA_LAST = 27,
            DWMWA_MICA_EFFECT = 1029,
            DWMWA_SYSTEMBACKDROP_TYPE = 38
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWCOMPOSITIONATTRIBDATA
        {
            public WINDOWCOMPOSITIONATTRIB Attrib;
            public IntPtr pvData;
            public int cbData;
        };
    }
}
