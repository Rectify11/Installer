using System;
using System.Runtime.InteropServices;

namespace Rectify11Installer.Win32
{
    public class NativeMethods
    {
        #region P/Invoke
        [DllImport("user32.dll")]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool revert);

		[DllImport("user32.dll")]
		public static extern int EnableMenuItem(IntPtr hMenu, int IDEnableItem, int enable);

		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        internal unsafe static extern IntPtr CreateDIBSection(IntPtr hdc, BITMAPINFO pbmi, uint iUsage, out int* ppvBits, IntPtr hSection, uint dwOffset);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject")]
        internal static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        internal extern static int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string pszText, int iCharCount, uint flags, ref RECT rect, ref DTTOPTS poptions);

        [DllImport("gdi32.dll")]
        internal static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
       
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);
        #endregion
        #region Flags
        public const int SC_CLOSE = 0xF060;
        public const int MF_BYCOMMAND = 0;
        public const int MF_ENABLED = 0;
        public const int MF_GRAYED = 1;
        public const int WP_CAPTION = 1;
        public const int CS_ACTIVE = 1;
        [StructLayout(LayoutKind.Sequential)]
        public class BITMAPINFO
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct DTTOPTS
        {
            public int dwSize;
            public int dwFlags;
            public int crText;
            public int crBorder;
            public int crShadow;
            public int iTextShadowType;
            public int ptShadowOffsetX;
            public int ptShadowOffsetY;
            public int iBorderSize;
            public int iFontPropId;
            public int iColorPropId;
            public int iStateId;
            public bool fApplyOverlay;
            public int iGlowSize;
            public IntPtr pfnDrawTextCallback;
            public IntPtr lParam;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;      // width of left border that retains its size
            public int cxRightWidth;     // width of right border that retains its size
            public int cyTopHeight;      // height of top border that retains its size
            public int cyBottomHeight;   // height of bottom border that retains its size
        };
        #endregion
        #region Public Methods
        public static bool SetCloseButton(frmWizard frm, bool enable)
		{
			IntPtr hMenu = NativeMethods.GetSystemMenu(frm.Handle, false);
			if (hMenu != IntPtr.Zero)
			{
				NativeMethods.EnableMenuItem(hMenu,
											 NativeMethods.SC_CLOSE,
											 NativeMethods.MF_BYCOMMAND | (enable ? NativeMethods.MF_ENABLED : NativeMethods.MF_GRAYED));
				return true;
			}
			return false;
        }
        #endregion
    }
}
