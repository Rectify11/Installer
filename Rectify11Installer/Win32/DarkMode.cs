using Rectify11Installer.Core;
using System;
using System.Runtime.InteropServices;

namespace Rectify11Installer.Win32
{
    public class DarkMode
    {
        #region P/Invoke
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        internal static extern int SetPreferredAppMode(PreferredAppMode appMode);
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        internal static extern int AllowDarkModeForApp(bool allow);
        [DllImport("uxtheme.dll", EntryPoint = "#133")]
        internal static extern int AllowDarkModeForWindow(IntPtr handle, bool allow);
        [DllImport("user32.dll")]
        public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WINDOWCOMPOSITIONATTRIBDATA data);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);
        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, DWMATTRIB dwAttribute, ref int pvAttribute, int cbAttribute);
        #endregion
        #region Flags
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
            // ...
            WCA_USEDARKMODECOLORS = 26,
            // ...
        };
        public enum DWMATTRIB
        {
            DWMWA_SYSTEMBACKDROP_TYPE = 38,
            DWMWA_MICA_EFFECT = 1029
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWCOMPOSITIONATTRIBDATA
        {
            public WINDOWCOMPOSITIONATTRIB Attrib;
            public IntPtr pvData;
            public int cbData;
        };
        #endregion
        #region Public Methods
        public static void RefreshTitleBarColor(IntPtr hWnd)
        {
            if (Environment.OSVersion.Version.Build < 18362)
            {
                SetProp(hWnd, "UseImmersiveDarkModeColors", new IntPtr(Theme.IsUsingDarkMode ? 1 : 0));
            }
            else
            {
                var size = Marshal.SizeOf(Theme.IsUsingDarkMode);
                var ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(Theme.IsUsingDarkMode, ptr, false);
                var data = new WINDOWCOMPOSITIONATTRIBDATA
                {
                    Attrib = WINDOWCOMPOSITIONATTRIB.WCA_USEDARKMODECOLORS,
                    pvData = ptr,
                    cbData = size
                };
                SetWindowCompositionAttribute(hWnd, ref data);
            }
        }
        public static void UpdateFrame(FrmWizard frm, bool yes)
        {
            var extend = Theme.IsUsingDarkMode;

            if (Environment.OSVersion.Version.Build >= 22523)
            {
                var micaValue = 0x02;
                var tabbedvalue = 0x04;
                if (extend)
                {
                    DwmSetWindowAttribute(frm.Handle, DWMATTRIB.DWMWA_SYSTEMBACKDROP_TYPE, ref micaValue, Marshal.SizeOf(typeof(int)));
                }
                else
                {
                    DwmSetWindowAttribute(frm.Handle, DWMATTRIB.DWMWA_SYSTEMBACKDROP_TYPE, ref tabbedvalue, Marshal.SizeOf(typeof(int)));
                }
            }

            else
            {
                var trueValue = 0x01;
                DwmSetWindowAttribute(frm.Handle, DWMATTRIB.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
            }
            var DarkMode = Theme.IsUsingDarkMode;
            var m = new NativeMethods.MARGINS();

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
            {
                if ((NativeMethods.GetUbr() != -1
                     && NativeMethods.GetUbr() >= 51
                     && Environment.OSVersion.Version.Build == 22000)
                    || Environment.OSVersion.Version.Build is > 22000 or < 21996)
                {
                    NativeMethods.DwmExtendFrameIntoClientArea(frm.Handle, ref m);
                }
            }
            else
            {
                NativeMethods.MARGINS mar = new()
                {
                    cxLeftWidth = 0,
                    cxRightWidth = 0,
                    cyBottomHeight = 0,
                    cyTopHeight = 0
                };
                if ((NativeMethods.GetUbr() != -1
                     && NativeMethods.GetUbr() >= 51
                     && Environment.OSVersion.Version.Build == 22000)
                    || Environment.OSVersion.Version.Build is > 22000 or < 21996)
                {
                    NativeMethods.DwmExtendFrameIntoClientArea(frm.Handle, ref mar);
                }
            }
        }
        #endregion
    }
}
