using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Vanara.Interop
{
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		internal const string DWMAPI = "dwmapi.dll";
		internal const string SHELL32 = "shell32.dll";
		internal const string USER32 = "user32.dll";

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr CreateDIBSection(
		  NativeMethods.SafeDCHandle hdc,
		  in NativeMethods.BITMAPINFO pbmi,
		  NativeMethods.DIBColorMode iUsage,
		  out IntPtr ppvBits,
		  [In, Optional] IntPtr hSection,
		  [In, Optional] int dwOffset);

		[SecurityCritical]
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(
		  IntPtr hWnd,
		  ref NativeMethods.Margins pMarInset);

		[SecurityCritical]
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmIsCompositionEnabled(ref int pfEnabled);

		[DllImport("gdi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BitBlt(
		  NativeMethods.SafeDCHandle hdc,
		  int nXDest,
		  int nYDest,
		  int nWidth,
		  int nHeight,
		  NativeMethods.SafeDCHandle hdcSrc,
		  int nXSrc,
		  int nYSrc,
		  NativeMethods.RasterOperationMode dwRop);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

		[SecurityCritical]
		[DllImport("gdi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteDC(IntPtr hdc);

		[DllImport("gdi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern IntPtr SelectObject(NativeMethods.SafeDCHandle hDC, IntPtr hObject);

		[DllImport("gdi32.dll", SetLastError = true)]
		public static extern NativeMethods.DCLayout SetLayout(
		  NativeMethods.SafeDCHandle hdc,
		  NativeMethods.DCLayout dwLayout);

		[DllImport("uxtheme.dll")]
		public static extern int CloseThemeData(IntPtr hTheme);

		[SecurityCritical]
		[DllImport("uxtheme.dll")]
		public static extern int DrawThemeBackground(
		  NativeMethods.SafeThemeHandle hTheme,
		  NativeMethods.SafeDCHandle hdc,
		  int iPartId,
		  int iStateId,
		  ref NativeMethods.RECT pRect,
		  NativeMethods.PRECT pClipRect);

		[SecurityCritical]
		[DllImport("uxtheme.dll")]
		public static extern int DrawThemeIcon(
		  NativeMethods.SafeThemeHandle hTheme,
		  NativeMethods.SafeDCHandle hdc,
		  int iPartId,
		  int iStateId,
		  ref NativeMethods.RECT pRect,
		  IntPtr himl,
		  int iImageIndex);

		[SecurityCritical]
		[DllImport("uxtheme.dll")]
		public static extern int DrawThemeParentBackground(
		  IntPtr hwnd,
		  NativeMethods.SafeDCHandle hdc,
		  NativeMethods.PRECT pRect);

		[SecurityCritical]
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		public static extern int DrawThemeTextEx(
		  NativeMethods.SafeThemeHandle hTheme,
		  NativeMethods.SafeDCHandle hdc,
		  int iPartId,
		  int iStateId,
		  string text,
		  int iCharCount,
		  TextFormatFlags dwFlags,
		  ref NativeMethods.RECT pRect,
		  ref NativeMethods.DrawThemeTextOptions pOptions);

		[SecurityCritical]
		[DllImport("uxtheme.dll")]
		public static extern int GetThemeMargins(
		  NativeMethods.SafeThemeHandle hTheme,
		  NativeMethods.SafeDCHandle hdc,
		  int iPartId,
		  int iStateId,
		  int iPropId,
		  IntPtr prc,
		  out NativeMethods.RECT pMargins);

		[SecurityCritical]
		[DllImport("uxtheme.dll")]
		public static extern int GetThemeTransitionDuration(
		  NativeMethods.SafeThemeHandle hTheme,
		  int iPartId,
		  int iStateIdFrom,
		  int iStateIdTo,
		  int iPropId,
		  out int pdwDuration);

		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		public static extern IntPtr OpenThemeData(IntPtr hWnd, string classList);

		public static int SetWindowThemeAttribute(
		  IWin32Window wnd,
		  NativeMethods.WindowThemeNonClientAttributes ncAttrs,
		  int ncAttrMasks = 2147483647)
		{
			var attributes = new NativeMethods.WTA_OPTIONS()
			{
				Flags = ncAttrs,
				Mask = ncAttrMasks == int.MaxValue ? (int)ncAttrs : ncAttrMasks
			};
			return NativeMethods.SetWindowThemeAttribute(wnd != null ? wnd.Handle : IntPtr.Zero, NativeMethods.WindowThemeAttributeType.NonClient, ref attributes, Marshal.SizeOf((object)attributes));
		}

		[SecurityCritical]
		[DllImport("uxtheme.dll")]
		private static extern int SetWindowThemeAttribute(
		  IntPtr hWnd,
		  NativeMethods.WindowThemeAttributeType wtype,
		  ref NativeMethods.WTA_OPTIONS attributes,
		  int size);

		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		public static extern int GetThemeFont(
		  NativeMethods.SafeThemeHandle hTheme,
		  NativeMethods.SafeDCHandle hdc,
		  int iPartId,
		  int iStateId,
		  int iPropId,
		  out NativeMethods.LOGFONT pFont);

		public enum BitmapCompressionMode : uint
		{
			BI_RGB,
			BI_RLE8,
			BI_RLE4,
			BI_BITFIELDS,
			BI_JPEG,
			BI_PNG,
		}

		public enum DIBColorMode
		{
			DIB_RGB_COLORS,
			DIB_PAL_COLORS,
		}

		public struct BITMAPINFO
		{
			public NativeMethods.BITMAPINFOHEADER bmiHeader;
			public BITMAPINFO(int width, int height, ushort bitCount = 32)
			  : this()
			{
				bmiHeader = new NativeMethods.BITMAPINFOHEADER(width, height, bitCount);
			}
		}

		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct BITMAPINFOHEADER
		{
			public uint biSize;
			public int biWidth;
			public int biHeight;
			public ushort biPlanes;
			public ushort biBitCount;
			public NativeMethods.BitmapCompressionMode biCompression;
			public uint biSizeImage;
			public int biXPelsPerMeter;
			public int biYPelsPerMeter;
			public uint biClrUsed;
			public uint biClrImportant;
			public static readonly NativeMethods.BITMAPINFOHEADER Default = new()
			{
				biSize = (uint)Marshal.SizeOf(typeof(NativeMethods.BITMAPINFOHEADER))
			};

			public BITMAPINFOHEADER(int width, int height, ushort bitCount = 32)
			  : this()
			{
				biSize = (uint)Marshal.SizeOf(typeof(NativeMethods.BITMAPINFO));
				biWidth = width;
				biHeight = height;
				biPlanes = 1;
				biBitCount = bitCount;
				biCompression = NativeMethods.BitmapCompressionMode.BI_RGB;
				biSizeImage = 0U;
			}
		}

		public struct RGBQUAD
		{
			public byte rgbBlue;
			public byte rgbGreen;
			public byte rgbRed;
			public byte rgbReserved;

			public Color Color
			{
				get => Color.FromArgb(rgbReserved, rgbRed, rgbGreen, rgbBlue);
				set
				{
					rgbReserved = value.A;
					rgbBlue = value.B;
					rgbGreen = value.G;
					rgbRed = value.R;
				}
			}
		}

		public enum BlurBehindFlags
		{
			Enable = 1,
			BlurRegion = 2,
			TransitionOnMaximized = 4,
		}

		public enum DWMWINDOWATTRIBUTE : uint
		{
			NCRenderingEnabled = 1,
			NCRenderingPolicy = 2,
			TransitionsForceDisabled = 3,
			AllowNCPaint = 4,
			CaptionButtonBounds = 5,
			NonClientRtlLayout = 6,
			ForceIconicRepresentation = 7,
			Flip3DPolicy = 8,
			ExtendedFrameBounds = 9,
			HasIconicBitmap = 10, // 0x0000000A
			DisallowPeek = 11, // 0x0000000B
			ExceludedFromPeek = 12, // 0x0000000C
			Cloak = 13, // 0x0000000D
			Cloaked = 14, // 0x0000000E
			FreezeRepresentation = 15, // 0x0000000F
		}

		public struct BlurBehind
		{
			private NativeMethods.BlurBehindFlags dwFlags;
			private readonly int fEnable;
			private IntPtr hRgnBlur;
			private int fTransitionOnMaximized;

			public BlurBehind(bool enabled)
			{
				fEnable = enabled ? 1 : 0;
				hRgnBlur = IntPtr.Zero;
				fTransitionOnMaximized = 0;
				dwFlags = NativeMethods.BlurBehindFlags.Enable;
			}

			public bool TransitionOnMaximized
			{
				get => fTransitionOnMaximized > 0;
				set
				{
					fTransitionOnMaximized = value ? 1 : 0;
					dwFlags |= NativeMethods.BlurBehindFlags.TransitionOnMaximized;
				}
			}

			public void SetRegion(Graphics graphics, Region region)
			{
				hRgnBlur = region.GetHrgn(graphics);
				dwFlags |= NativeMethods.BlurBehindFlags.BlurRegion;
			}
		}

		public struct Margins
		{
			public int Left;
			public int Right;
			public int Top;
			public int Bottom;
			public static readonly NativeMethods.Margins Empty = new(0);
			public static readonly NativeMethods.Margins Infinite = new(-1);

			public Margins(int left, int right, int top, int bottom)
			{
				Left = left;
				Right = right;
				Top = top;
				Bottom = bottom;
			}

			public Margins(int allMargins) => Left = Right = Top = Bottom = allMargins;

			public Margins(Padding padding)
			  : this(padding.Left, padding.Right, padding.Top, padding.Bottom)
			{
			}
		}

		public enum DCLayout
		{
			GDI_ERROR = -1, // 0xFFFFFFFF
			LAYOUT_RTL = 1,
			LAYOUT_BTT = 2,
			LAYOUT_VBH = 4,
			LAYOUT_BITMAPORIENTATIONPRESERVED = 8,
		}

		public enum RasterOperationMode
		{
			NOMIRRORBITMAP = -2147483648, // 0x80000000
			BLACKNESS = 66, // 0x00000042
			NOTSRCERASE = 1114278, // 0x001100A6
			NOTSRCCOPY = 3342344, // 0x00330008
			SRCERASE = 4457256, // 0x00440328
			DSTINVERT = 5570569, // 0x00550009
			PATINVERT = 5898313, // 0x005A0049
			SRCINVERT = 6684742, // 0x00660046
			SRCAND = 8913094, // 0x008800C6
			MERGEPAINT = 12255782, // 0x00BB0226
			MERGECOPY = 12583114, // 0x00C000CA
			SRCCOPY = 13369376, // 0x00CC0020
			SRCPAINT = 15597702, // 0x00EE0086
			PATCOPY = 15728673, // 0x00F00021
			PATPAINT = 16452105, // 0x00FB0A09
			WHITENESS = 16711778, // 0x00FF0062
			CAPTUREBLT = 1073741824, // 0x40000000
		}

		public enum LogFontCharSet : byte
		{
			ANSI_CHARSET = 0,
			DEFAULT_CHARSET = 1,
			SYMBOL_CHARSET = 2,
			MAC_CHARSET = 77, // 0x4D
			SHIFTJIS_CHARSET = 128, // 0x80
			HANGEUL_CHARSET = 129, // 0x81
			HANGUL_CHARSET = 129, // 0x81
			JOHAB_CHARSET = 130, // 0x82
			GB2312_CHARSET = 134, // 0x86
			CHINESEBIG5_CHARSET = 136, // 0x88
			GREEK_CHARSET = 161, // 0xA1
			TURKISH_CHARSET = 162, // 0xA2
			VIETNAMESE_CHARSET = 163, // 0xA3
			HEBREW_CHARSET = 177, // 0xB1
			ARABIC_CHARSET = 178, // 0xB2
			BALTIC_CHARSET = 186, // 0xBA
			RUSSIAN_CHARSET = 204, // 0xCC
			THAI_CHARSET = 222, // 0xDE
			EASTEUROPE_CHARSET = 238, // 0xEE
			OEM_CHARSET = 255, // 0xFF
		}

		public enum LogFontClippingPrecision : byte
		{
			CLIP_DEFAULT_PRECIS = 0,
			CLIP_CHARACTER_PRECIS = 1,
			CLIP_STROKE_PRECIS = 2,
			CLIP_MASK = 15, // 0x0F
			CLIP_LH_ANGLES = 16, // 0x10
			CLIP_TT_ALWAYS = 32, // 0x20
			CLIP_DFA_DISABLE = 64, // 0x40
			CLIP_EMBEDDED = 128, // 0x80
		}

		public enum LogFontFontFamily : byte
		{
			FF_DONTCARE = 0,
			FF_ROMAN = 16, // 0x10
			FF_SWISS = 32, // 0x20
			FF_MODERN = 48, // 0x30
			FF_SCRIPT = 64, // 0x40
			FF_DECORATIVE = 80, // 0x50
		}

		public enum LogFontOutputPrecision : byte
		{
			OUT_DEFAULT_PRECIS,
			OUT_STRING_PRECIS,
			OUT_CHARACTER_PRECIS,
			OUT_STROKE_PRECIS,
			OUT_TT_PRECIS,
			OUT_DEVICE_PRECIS,
			OUT_RASTER_PRECIS,
			OUT_TT_ONLY_PRECIS,
			OUT_OUTLINE_PRECIS,
			OUT_SCREEN_OUTLINE_PRECIS,
			OUT_PS_ONLY_PRECIS,
		}

		public enum LogFontOutputQuality : byte
		{
			DEFAULT_QUALITY,
			DRAFT_QUALITY,
			PROOF_QUALITY,
			NONANTIALIASED_QUALITY,
			ANTIALIASED_QUALITY,
			CLEARTYPE_QUALITY,
			CLEARTYPE_NATURAL_QUALITY,
		}

		public enum LogFontPitch : byte
		{
			DEFAULT_PITCH,
			FIXED_PITCH,
			VARIABLE_PITCH,
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct LOGFONT
		{
			public int lfHeight;
			public int lfWidth;
			public int lfEscapement;
			public int lfOrientation;
			public int lfWeight;
			public byte lfItalic;
			public byte lfUnderline;
			public byte lfStrikeOut;
			public NativeMethods.LogFontCharSet lfCharSet;
			public NativeMethods.LogFontOutputPrecision lfOutPrecision;
			public NativeMethods.LogFontClippingPrecision lfClipPrecision;
			public NativeMethods.LogFontOutputQuality lfQuality;
			public byte lfPitchAndFamily;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;

			public Font ToFont()
			{
				try
				{
					return Font.FromLogFont(this);
				}
				catch
				{
					return new Font(lfFaceName, lfHeight, FontStyle.Regular, GraphicsUnit.Display);
				}
			}
		}

		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;

			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public RECT(Rectangle r)
			  : this(r.Left, r.Top, r.Right, r.Bottom)
			{
			}

			public int X
			{
				get => Left;
				set
				{
					Right -= Left - value;
					Left = value;
				}
			}

			public int Y
			{
				get => Top;
				set
				{
					Bottom -= Top - value;
					Top = value;
				}
			}

			public int Height
			{
				get => Bottom - Top;
				set => Bottom = value + Top;
			}

			public int Width
			{
				get => Right - Left;
				set => Right = value + Left;
			}

			public static implicit operator Rectangle(NativeMethods.RECT r) => new(r.Left, r.Top, r.Width, r.Height);

			public static implicit operator NativeMethods.RECT(Rectangle r) => new(r);
		}

		[StructLayout(LayoutKind.Sequential)]
		public class PRECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;

			public PRECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public PRECT(Rectangle r)
			  : this(r.Left, r.Top, r.Right, r.Bottom)
			{
			}

			public int X
			{
				get => Left;
				set
				{
					Right -= Left - value;
					Left = value;
				}
			}

			public int Y
			{
				get => Top;
				set
				{
					Bottom -= Top - value;
					Top = value;
				}
			}

			public int Height
			{
				get => Bottom - Top;
				set => Bottom = value + Top;
			}

			public int Width
			{
				get => Right - Left;
				set => Right = value + Left;
			}

			public static implicit operator Rectangle(NativeMethods.PRECT r) => new(r.Left, r.Top, r.Width, r.Height);

			public static implicit operator NativeMethods.PRECT(Rectangle? r) => !r.HasValue ? null : new NativeMethods.PRECT(r.Value);

			public static implicit operator NativeMethods.PRECT(Rectangle r) => new(r);
		}

		public class SafeDCHandle : SafeHandle
		{
			public static readonly NativeMethods.SafeDCHandle Null = new(IntPtr.Zero);
			private readonly IDeviceContext idc;

			public SafeDCHandle(IntPtr hDC, bool ownsHandle = true)
			  : base(IntPtr.Zero, ownsHandle)
			{
				SetHandle(hDC);
			}

			public SafeDCHandle(IDeviceContext dc)
			  : base(IntPtr.Zero, true)
			{
				idc = dc != null ? dc : throw new ArgumentNullException(nameof(dc));
				SetHandle(dc.GetHdc());
			}

			public static NativeMethods.SafeDCHandle ScreenCompatibleDCHandle => new(NativeMethods.CreateCompatibleDC(IntPtr.Zero));

			public override bool IsInvalid => handle == IntPtr.Zero;

			public static implicit operator NativeMethods.SafeDCHandle(Graphics graphics) => new(graphics);

			public NativeMethods.SafeDCHandle GetCompatibleDCHandle() => new(NativeMethods.CreateCompatibleDC(handle));

			protected override bool ReleaseHandle()
			{
				if (idc == null)
				{
					return NativeMethods.DeleteDC(handle);
				}

				idc.ReleaseHdc();
				return true;
			}
		}

		public class SafeDCObjectHandle : SafeHandle
		{
			private readonly NativeMethods.SafeDCHandle hDC;
			private readonly IntPtr hOld;

			public SafeDCObjectHandle(NativeMethods.SafeDCHandle hdc, IntPtr hObj)
			  : base(IntPtr.Zero, true)
			{
				if (hdc == null || hdc.IsInvalid)
				{
					return;
				}

				hDC = hdc;
				hOld = NativeMethods.SelectObject(hdc, hObj);
				SetHandle(hObj);
			}

			public override bool IsInvalid => handle == IntPtr.Zero;

			protected override bool ReleaseHandle()
			{
				NativeMethods.SelectObject(hDC, hOld);
				return NativeMethods.DeleteObject(handle);
			}
		}

		internal enum ADLT
		{
			RECENT,
			FREQUENT,
		}

		internal enum Facility
		{
			Null = 0,
			Rpc = 1,
			Dispatch = 2,
			Storage = 3,
			Itf = 4,
			Win32 = 7,
			Windows = 8,
			Control = 10, // 0x0000000A
			Ese = 3678, // 0x00000E5E
		}

		internal enum FDAP : uint
		{
			BOTTOM,
			TOP,
		}

		internal enum FDEOR
		{
			DEFAULT,
			ACCEPT,
			REFUSE,
		}

		internal enum FDESVR
		{
			DEFAULT,
			ACCEPT,
			REFUSE,
		}

		[Flags]
		internal enum FOS : uint
		{
			ALLNONSTORAGEITEMS = 128, // 0x00000080
			ALLOWMULTISELECT = 512, // 0x00000200
			CREATEPROMPT = 8192, // 0x00002000
			DEFAULTNOMINIMODE = 536870912, // 0x20000000
			DONTADDTORECENT = 33554432, // 0x02000000
			FILEMUSTEXIST = 4096, // 0x00001000
			FORCEFILESYSTEM = 64, // 0x00000040
			FORCEPREVIEWPANEON = 1073741824, // 0x40000000
			FORCESHOWHIDDEN = 268435456, // 0x10000000
			HIDEMRUPLACES = 131072, // 0x00020000
			HIDEPINNEDPLACES = 262144, // 0x00040000
			NOCHANGEDIR = 8,
			NODEREFERENCELINKS = 1048576, // 0x00100000
			NOREADONLYRETURN = 32768, // 0x00008000
			NOTESTFILECREATE = 65536, // 0x00010000
			NOVALIDATE = 256, // 0x00000100
			OVERWRITEPROMPT = 2,
			PATHMUSTEXIST = 2048, // 0x00000800
			PICKFOLDERS = 32, // 0x00000020
			SHAREAWARE = 16384, // 0x00004000
			STRICTFILETYPES = 4,
		}

		internal enum KDC
		{
			FREQUENT = 1,
			RECENT = 2,
		}

		[Flags]
		internal enum SFGAO : uint
		{
			BROWSABLE = 134217728, // 0x08000000
			CANCOPY = 1,
			CANDELETE = 32, // 0x00000020
			CANLINK = 4,
			CANMONIKER = 4194304, // 0x00400000
			CANMOVE = 2,
			CANRENAME = 16, // 0x00000010
			CAPABILITYMASK = 375, // 0x00000177
			COMPRESSED = 67108864, // 0x04000000
			CONTENTSMASK = 2147483648, // 0x80000000
			DISPLAYATTRMASK = 1032192, // 0x000FC000
			DROPTARGET = 256, // 0x00000100
			ENCRYPTED = 8192, // 0x00002000
			FILESYSANCESTOR = 268435456, // 0x10000000
			FILESYSTEM = 1073741824, // 0x40000000
			FOLDER = 536870912, // 0x20000000
			GHOSTED = 32768, // 0x00008000
			HASPROPSHEET = 64, // 0x00000040
			HASSTORAGE = CANMONIKER, // 0x00400000
			HASSUBFOLDER = CONTENTSMASK, // 0x80000000
			HIDDEN = 524288, // 0x00080000
			ISSLOW = 16384, // 0x00004000
			LINK = 65536, // 0x00010000
			NEWCONTENT = 2097152, // 0x00200000
			NONENUMERATED = 1048576, // 0x00100000
			PKEYSFGAOMASK = 2164539392, // 0x81044000
			READONLY = 262144, // 0x00040000
			REMOVABLE = 33554432, // 0x02000000
			SHARE = 131072, // 0x00020000
			STORAGE = 8,
			STORAGEANCESTOR = 8388608, // 0x00800000
			STORAGECAPMASK = STORAGEANCESTOR | STORAGE | READONLY | LINK | HASSTORAGE | FOLDER | FILESYSTEM | FILESYSANCESTOR, // 0x70C50008
			STREAM = HASSTORAGE, // 0x00400000
			SYSTEM = 4096, // 0x00001000
			VALIDATE = 16777216, // 0x01000000
		}

		[Flags]
		internal enum SHCONTF
		{
			CHECKING_FOR_CHILDREN = 16, // 0x00000010
			ENABLE_ASYNC = 32768, // 0x00008000
			FASTITEMS = 8192, // 0x00002000
			FLATLIST = 16384, // 0x00004000
			FOLDERS = 32, // 0x00000020
			INCLUDEHIDDEN = 128, // 0x00000080
			INIT_ON_FIRST_NEXT = 256, // 0x00000100
			NAVIGATION_ENUM = 4096, // 0x00001000
			NETPRINTERSRCH = 512, // 0x00000200
			NONFOLDERS = 64, // 0x00000040
			SHAREABLE = 1024, // 0x00000400
			STORAGE = 2048, // 0x00000800
		}

		[Flags]
		internal enum SHGDN
		{
			SHGDN_FORADDRESSBAR = 16384, // 0x00004000
			SHGDN_FOREDITING = 4096, // 0x00001000
			SHGDN_FORPARSING = 32768, // 0x00008000
			SHGDN_INFOLDER = 1,
			SHGDN_NORMAL = 0,
		}

		[Flags]
		internal enum SICHINT : uint
		{
			ALLFIELDS = 2147483648, // 0x80000000
			CANONICAL = 268435456, // 0x10000000
			DISPLAY = 0,
			TEST_FILESYSPATH_IF_NOT_EQUAL = 536870912, // 0x20000000
		}

		internal enum SIGDN : uint
		{
			NORMALDISPLAY = 0,
			PARENTRELATIVEPARSING = 2147581953, // 0x80018001
			DESKTOPABSOLUTEPARSING = 2147647488, // 0x80028000
			PARENTRELATIVEEDITING = 2147684353, // 0x80031001
			DESKTOPABSOLUTEEDITING = 2147794944, // 0x8004C000
			FILESYSPATH = 2147844096, // 0x80058000
			URL = 2147909632, // 0x80068000
			PARENTRELATIVEFORADDRESSBAR = 2147991553, // 0x8007C001
			PARENTRELATIVE = 2148007937, // 0x80080001
			PARENTRELATIVEFORUI = 2148089857, // 0x80094001
		}

		[Flags]
		internal enum SLGP
		{
			RAWPATH = 4,
			SHORTPATH = 1,
			UNCPRIORITY = 2,
			RELATIVEPRIORITY = 8,
		}

		[Flags]
		internal enum STPF
		{
			NONE = 0,
			USEAPPPEEKALWAYS = 4,
			USEAPPPEEKWHENACTIVE = 8,
			USEAPPTHUMBNAILALWAYS = 1,
			USEAPPTHUMBNAILWHENACTIVE = 2,
		}

		[Flags]
		internal enum TBPF
		{
			ERROR = 4,
			INDETERMINATE = 1,
			NOPROGRESS = 0,
			NORMAL = 2,
			PAUSED = 8,
		}

		[Flags]
		internal enum THB : uint
		{
			BITMAP = 1,
			FLAGS = 8,
			ICON = 2,
			TOOLTIP = 4,
		}

		[Flags]
		internal enum THBF : uint
		{
			DISABLED = 1,
			DISMISSONCLICK = 2,
			ENABLED = 0,
			HIDDEN = 8,
			NOBACKGROUND = 4,
			NONINTERACTIVE = 16, // 0x00000010
		}

		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214F2-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IEnumIDList
		{
			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT Next(uint celt, out IntPtr rgelt, out int pceltFetched);

			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT Skip(uint celt);

			void Reset();

			[return: MarshalAs(UnmanagedType.Interface)]
			NativeMethods.IEnumIDList Clone();
		}

		[Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IFileDialog : NativeMethods.IModalWindow
		{
			[MethodImpl(MethodImplOptions.PreserveSig)]
			new NativeMethods.HRESULT Show(IntPtr parent);

			void SetFileTypes(uint cFileTypes, [MarshalAs(UnmanagedType.LPArray), In] NativeMethods.COMDLG_FILTERSPEC[] rgFilterSpec);

			void SetFileTypeIndex(uint iFileType);

			uint GetFileTypeIndex();

			uint Advise(NativeMethods.IFileDialogEvents pfde);

			void Unadvise(uint dwCookie);

			void SetOptions(NativeMethods.FOS fos);

			NativeMethods.FOS GetOptions();

			void SetDefaultFolder(NativeMethods.IShellItem psi);

			void SetFolder(NativeMethods.IShellItem psi);

			NativeMethods.IShellItem GetFolder();

			NativeMethods.IShellItem GetCurrentSelection();

			void SetFileName([MarshalAs(UnmanagedType.LPWStr)] string pszName);

			[return: MarshalAs(UnmanagedType.LPWStr)]
			string GetFileName();

			void SetTitle([MarshalAs(UnmanagedType.LPWStr)] string pszTitle);

			void SetOkButtonLabel([MarshalAs(UnmanagedType.LPWStr)] string pszText);

			void SetFileNameLabel([MarshalAs(UnmanagedType.LPWStr)] string pszLabel);

			NativeMethods.IShellItem GetResult();

			void AddPlace(NativeMethods.IShellItem psi, NativeMethods.FDAP alignment);

			void SetDefaultExtension([MarshalAs(UnmanagedType.LPWStr)] string pszDefaultExtension);

			void Close([MarshalAs(UnmanagedType.Error)] int hr);

			void SetClientGuid([In] ref Guid guid);

			void ClearClientData();

			void SetFilter([MarshalAs(UnmanagedType.Interface)] object pFilter);
		}

		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("973510DB-7D7F-452B-8975-74A85828D354")]
		[ComImport]
		internal interface IFileDialogEvents
		{
			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT OnFileOk(NativeMethods.IFileDialog pfd);

			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT OnFolderChanging(
			  NativeMethods.IFileDialog pfd,
			  NativeMethods.IShellItem psiFolder);

			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT OnFolderChange(NativeMethods.IFileDialog pfd);

			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT OnSelectionChange(NativeMethods.IFileDialog pfd);

			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT OnShareViolation(
			  NativeMethods.IFileDialog pfd,
			  NativeMethods.IShellItem psi,
			  out NativeMethods.FDESVR pResponse);

			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT OnTypeChange(NativeMethods.IFileDialog pfd);

			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT OnOverwrite(
			  NativeMethods.IFileDialog pfd,
			  NativeMethods.IShellItem psi,
			  out NativeMethods.FDEOR pResponse);
		}

		[Guid("b4db1657-70d7-485e-8e3e-6fcb5a5c1802")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IModalWindow
		{
			[MethodImpl(MethodImplOptions.PreserveSig)]
			NativeMethods.HRESULT Show(IntPtr parent);
		}

		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
		[ComImport]
		internal interface IShellItem
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			object BindToHandler(IBindCtx pbc, [In] ref Guid bhid, [In] ref Guid riid);

			NativeMethods.IShellItem GetParent();

			[return: MarshalAs(UnmanagedType.LPWStr)]
			string GetDisplayName(NativeMethods.SIGDN sigdnName);

			uint GetAttributes(NativeMethods.SFGAO sfgaoMask);

			int Compare(NativeMethods.IShellItem psi, NativeMethods.SICHINT hint);
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct COMDLG_FILTERSPEC
		{
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszName;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszSpec;
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct HRESULT
		{
			[FieldOffset(0)]
			private readonly uint _value;
			public static readonly NativeMethods.HRESULT COR_E_OBJECTDISPOSED;
			public static readonly NativeMethods.HRESULT DESTS_E_NO_MATCHING_ASSOC_HANDLER;
			public static readonly NativeMethods.HRESULT DISP_E_BADINDEX;
			public static readonly NativeMethods.HRESULT DISP_E_BADPARAMCOUNT;
			public static readonly NativeMethods.HRESULT DISP_E_EXCEPTION;
			public static readonly NativeMethods.HRESULT DISP_E_MEMBERNOTFOUND;
			public static readonly NativeMethods.HRESULT DISP_E_OVERFLOW;
			public static readonly NativeMethods.HRESULT DISP_E_PARAMNOTOPTIONAL;
			public static readonly NativeMethods.HRESULT DISP_E_TYPEMISMATCH;
			public static readonly NativeMethods.HRESULT DISP_E_UNKNOWNNAME;
			public static readonly NativeMethods.HRESULT E_ABORT;
			public static readonly NativeMethods.HRESULT E_ACCESSDENIED;
			public static readonly NativeMethods.HRESULT E_FAIL;
			public static readonly NativeMethods.HRESULT E_INVALIDARG;
			public static readonly NativeMethods.HRESULT E_NOINTERFACE;
			public static readonly NativeMethods.HRESULT E_NOTIMPL;
			public static readonly NativeMethods.HRESULT E_OUTOFMEMORY;
			public static readonly NativeMethods.HRESULT E_POINTER;
			public static readonly NativeMethods.HRESULT E_UNEXPECTED;
			public static readonly NativeMethods.HRESULT S_FALSE;
			public static readonly NativeMethods.HRESULT S_OK = new(0U);
			public static readonly NativeMethods.HRESULT SCRIPT_E_REPORTED;
			public static readonly NativeMethods.HRESULT STG_E_INVALIDFUNCTION;
			public static readonly NativeMethods.HRESULT WC_E_GREATERTHAN;
			public static readonly NativeMethods.HRESULT WC_E_SYNTAX;

			static HRESULT()
			{
				NativeMethods.HRESULT.S_FALSE = new NativeMethods.HRESULT(1U);
				NativeMethods.HRESULT.E_NOTIMPL = new NativeMethods.HRESULT(2147500033U);
				NativeMethods.HRESULT.E_NOINTERFACE = new NativeMethods.HRESULT(2147500034U);
				NativeMethods.HRESULT.E_POINTER = new NativeMethods.HRESULT(2147500035U);
				NativeMethods.HRESULT.E_ABORT = new NativeMethods.HRESULT(2147500036U);
				NativeMethods.HRESULT.E_FAIL = new NativeMethods.HRESULT(2147500037U);
				NativeMethods.HRESULT.E_UNEXPECTED = new NativeMethods.HRESULT(2147549183U);
				NativeMethods.HRESULT.DISP_E_MEMBERNOTFOUND = new NativeMethods.HRESULT(2147614723U);
				NativeMethods.HRESULT.DISP_E_TYPEMISMATCH = new NativeMethods.HRESULT(2147614725U);
				NativeMethods.HRESULT.DISP_E_UNKNOWNNAME = new NativeMethods.HRESULT(2147614726U);
				NativeMethods.HRESULT.DISP_E_EXCEPTION = new NativeMethods.HRESULT(2147614729U);
				NativeMethods.HRESULT.DISP_E_OVERFLOW = new NativeMethods.HRESULT(2147614730U);
				NativeMethods.HRESULT.DISP_E_BADINDEX = new NativeMethods.HRESULT(2147614731U);
				NativeMethods.HRESULT.DISP_E_BADPARAMCOUNT = new NativeMethods.HRESULT(2147614734U);
				NativeMethods.HRESULT.DISP_E_PARAMNOTOPTIONAL = new NativeMethods.HRESULT(2147614735U);
				NativeMethods.HRESULT.SCRIPT_E_REPORTED = new NativeMethods.HRESULT(2147614977U);
				NativeMethods.HRESULT.STG_E_INVALIDFUNCTION = new NativeMethods.HRESULT(2147680257U);
				NativeMethods.HRESULT.DESTS_E_NO_MATCHING_ASSOC_HANDLER = new NativeMethods.HRESULT(2147749635U);
				NativeMethods.HRESULT.E_ACCESSDENIED = new NativeMethods.HRESULT(2147942405U);
				NativeMethods.HRESULT.E_OUTOFMEMORY = new NativeMethods.HRESULT(2147942414U);
				NativeMethods.HRESULT.E_INVALIDARG = new NativeMethods.HRESULT(2147942487U);
				NativeMethods.HRESULT.COR_E_OBJECTDISPOSED = new NativeMethods.HRESULT(2148734498U);
				NativeMethods.HRESULT.WC_E_GREATERTHAN = new NativeMethods.HRESULT(3222072867U);
				NativeMethods.HRESULT.WC_E_SYNTAX = new NativeMethods.HRESULT(3222072877U);
			}

			public HRESULT(uint i) => _value = i;

			public override bool Equals(object obj)
			{
				try
				{
					return (int)((NativeMethods.HRESULT)obj)._value == (int)_value;
				}
				catch
				{
					return false;
				}
			}

			public static int GetCode(int error) => error & ushort.MaxValue;

			public Exception GetException() => GetException(null);

			[SecurityCritical]
			[SecuritySafeCritical]
			public Exception GetException(string message)
			{
				if (!Failed)
				{
					return null;
				}

				var exception = Marshal.GetExceptionForHR((int)_value, new IntPtr(-1));
				if (exception.GetType() == typeof(COMException))
				{
					if (Facility != NativeMethods.Facility.Win32)
					{
						return new COMException(message ?? exception.Message, (int)_value);
					}

					return string.IsNullOrEmpty(message) ? new Win32Exception(Code) : new Win32Exception(Code, message);
				}
				if (!string.IsNullOrEmpty(message))
				{
					var types = new Type[1] { typeof(string) };
					var constructor = exception.GetType().GetConstructor(types);
					if (null != constructor)
					{
						var parameters = new object[1]
						{
			   message
						};
						exception = constructor.Invoke(parameters) as Exception;
					}
				}
				return exception;
			}

			public static NativeMethods.Facility GetFacility(int errorCode) => (NativeMethods.Facility)(errorCode >> 16 & 8191);

			public override int GetHashCode() => _value.GetHashCode();

			public static NativeMethods.HRESULT Make(
			  bool severe,
			  NativeMethods.Facility facility,
			  int code)
			{
				return new NativeMethods.HRESULT((uint)((severe ? int.MinValue : 0) | (int)facility << 16 | code));
			}

			public static bool operator ==(NativeMethods.HRESULT hrLeft, NativeMethods.HRESULT hrRight) => (int)hrLeft._value == (int)hrRight._value;

			public static bool operator !=(NativeMethods.HRESULT hrLeft, NativeMethods.HRESULT hrRight) => !(hrLeft == hrRight);

			public void ThrowIfFailed() => ThrowIfFailed(null);

			[SecurityCritical]
			[SecuritySafeCritical]
			public void ThrowIfFailed(string message)
			{
				var exception = GetException(message);
				if (exception != null)
				{
					throw exception;
				}
			}

			public override string ToString()
			{
				foreach (var field in typeof(NativeMethods.HRESULT).GetFields(BindingFlags.Static | BindingFlags.Public))
				{
					if (field.FieldType == typeof(NativeMethods.HRESULT) && (NativeMethods.HRESULT)field.GetValue(null) == this)
					{
						return field.Name;
					}
				}
				if (Facility == NativeMethods.Facility.Win32)
				{
					foreach (var field in typeof(NativeMethods.Win32Error).GetFields(BindingFlags.Static | BindingFlags.Public))
					{
						if (field.FieldType == typeof(NativeMethods.Win32Error) && (NativeMethods.HRESULT)(NativeMethods.Win32Error)field.GetValue(null) == this)
						{
							return "HRESULT_FROM_WIN32(" + field.Name + ")";
						}
					}
				}
				return string.Format(CultureInfo.InvariantCulture, "0x{0:X8}", new object[1]
				{
		   _value
				});
			}

			public int Code => NativeMethods.HRESULT.GetCode((int)_value);

			public NativeMethods.Facility Facility => NativeMethods.HRESULT.GetFacility((int)_value);

			public bool Failed => _value < 0U;

			public bool Succeeded => _value >= 0U;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Unicode)]
		internal struct THUMBBUTTON
		{
			public const int THBN_CLICKED = 6144;
			public NativeMethods.THB dwMask;
			public uint iId;
			public uint iBitmap;
			public IntPtr hIcon;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szTip;
			public NativeMethods.THBF dwFlags;
			public static NativeMethods.THUMBBUTTON Default = new()
			{
				dwMask = NativeMethods.THB.FLAGS,
				dwFlags = NativeMethods.THBF.HIDDEN
			};
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct Win32Error
		{
			[FieldOffset(0)]
			private readonly int _value;
			public static readonly NativeMethods.Win32Error ERROR_ACCESS_DENIED;
			public static readonly NativeMethods.Win32Error ERROR_BAD_DEVICE;
			public static readonly NativeMethods.Win32Error ERROR_CANCELLED;
			public static readonly NativeMethods.Win32Error ERROR_FILE_NOT_FOUND;
			public static readonly NativeMethods.Win32Error ERROR_INSUFFICIENT_BUFFER;
			public static readonly NativeMethods.Win32Error ERROR_INVALID_DATATYPE;
			public static readonly NativeMethods.Win32Error ERROR_INVALID_FUNCTION;
			public static readonly NativeMethods.Win32Error ERROR_INVALID_HANDLE;
			public static readonly NativeMethods.Win32Error ERROR_INVALID_PARAMETER;
			public static readonly NativeMethods.Win32Error ERROR_INVALID_WINDOW_HANDLE;
			public static readonly NativeMethods.Win32Error ERROR_KEY_DELETED;
			public static readonly NativeMethods.Win32Error ERROR_NESTING_NOT_ALLOWED;
			public static readonly NativeMethods.Win32Error ERROR_NO_MATCH;
			public static readonly NativeMethods.Win32Error ERROR_NO_MORE_FILES;
			public static readonly NativeMethods.Win32Error ERROR_OUTOFMEMORY;
			public static readonly NativeMethods.Win32Error ERROR_PATH_NOT_FOUND;
			public static readonly NativeMethods.Win32Error ERROR_SHARING_VIOLATION;
			public static readonly NativeMethods.Win32Error ERROR_SUCCESS = new(0);
			public static readonly NativeMethods.Win32Error ERROR_TIMEOUT;
			public static readonly NativeMethods.Win32Error ERROR_TOO_MANY_OPEN_FILES;

			static Win32Error()
			{
				NativeMethods.Win32Error.ERROR_INVALID_FUNCTION = new NativeMethods.Win32Error(1);
				NativeMethods.Win32Error.ERROR_FILE_NOT_FOUND = new NativeMethods.Win32Error(2);
				NativeMethods.Win32Error.ERROR_PATH_NOT_FOUND = new NativeMethods.Win32Error(3);
				NativeMethods.Win32Error.ERROR_TOO_MANY_OPEN_FILES = new NativeMethods.Win32Error(4);
				NativeMethods.Win32Error.ERROR_ACCESS_DENIED = new NativeMethods.Win32Error(5);
				NativeMethods.Win32Error.ERROR_INVALID_HANDLE = new NativeMethods.Win32Error(6);
				NativeMethods.Win32Error.ERROR_OUTOFMEMORY = new NativeMethods.Win32Error(14);
				NativeMethods.Win32Error.ERROR_NO_MORE_FILES = new NativeMethods.Win32Error(18);
				NativeMethods.Win32Error.ERROR_SHARING_VIOLATION = new NativeMethods.Win32Error(32);
				NativeMethods.Win32Error.ERROR_INVALID_PARAMETER = new NativeMethods.Win32Error(87);
				NativeMethods.Win32Error.ERROR_INSUFFICIENT_BUFFER = new NativeMethods.Win32Error(122);
				NativeMethods.Win32Error.ERROR_NESTING_NOT_ALLOWED = new NativeMethods.Win32Error(215);
				NativeMethods.Win32Error.ERROR_KEY_DELETED = new NativeMethods.Win32Error(1018);
				NativeMethods.Win32Error.ERROR_NO_MATCH = new NativeMethods.Win32Error(1169);
				NativeMethods.Win32Error.ERROR_BAD_DEVICE = new NativeMethods.Win32Error(1200);
				NativeMethods.Win32Error.ERROR_CANCELLED = new NativeMethods.Win32Error(1223);
				NativeMethods.Win32Error.ERROR_INVALID_WINDOW_HANDLE = new NativeMethods.Win32Error(1400);
				NativeMethods.Win32Error.ERROR_TIMEOUT = new NativeMethods.Win32Error(1460);
				NativeMethods.Win32Error.ERROR_INVALID_DATATYPE = new NativeMethods.Win32Error(1804);
			}

			public Win32Error(int i) => _value = i;

			public override bool Equals(object obj)
			{
				try
				{
					return ((NativeMethods.Win32Error)obj)._value == _value;
				}
				catch
				{
					return false;
				}
			}

			public override int GetHashCode() => _value.GetHashCode();

			[SecurityCritical]
			public static NativeMethods.Win32Error GetLastError() => new(Marshal.GetLastWin32Error());

			public static bool operator ==(
			  NativeMethods.Win32Error errLeft,
			  NativeMethods.Win32Error errRight)
			{
				return errLeft._value == errRight._value;
			}

			public static explicit operator NativeMethods.HRESULT(
			  NativeMethods.Win32Error error)
			{
				return error._value <= 0 ? new NativeMethods.HRESULT((uint)error._value) : NativeMethods.HRESULT.Make(true, NativeMethods.Facility.Win32, error._value & ushort.MaxValue);
			}

			public static bool operator !=(
			  NativeMethods.Win32Error errLeft,
			  NativeMethods.Win32Error errRight)
			{
				return !(errLeft == errRight);
			}

			public NativeMethods.HRESULT ToHRESULT() => (NativeMethods.HRESULT)this;
		}

		public struct SIZE
		{
			public int width;
			public int height;

			public SIZE(int w, int h)
			{
				width = w;
				height = h;
			}

			public Size ToSize() => this;

			public static implicit operator Size(NativeMethods.SIZE s) => new(s.width, s.height);

			public static implicit operator NativeMethods.SIZE(Size s) => new(s.Width, s.Height);
		}

		[UnmanagedFunctionPointer(CallingConvention.Winapi, CharSet = CharSet.Unicode)]
		public delegate int DrawThemeTextCallback(
		  NativeMethods.SafeDCHandle hdc,
		  string text,
		  int textLen,
		  ref NativeMethods.RECT rc,
		  int flags,
		  IntPtr lParam);


		public enum DrawThemeTextSystemFonts
		{
			Caption = 801, // 0x00000321
			SmallCaption = 802, // 0x00000322
			Menu = 803, // 0x00000323
			Status = 804, // 0x00000324
			MessageBox = 805, // 0x00000325
			IconTitle = 806, // 0x00000326
		}

		public enum IntegerListProperty
		{
			TransitionDuration = 6000, // 0x00001770
		}


		public enum TextShadowType
		{
			None,
			Single,
			Continuous,
		}


		[Flags]
		public enum WindowThemeNonClientAttributes
		{
			NoDrawCaption = 1,
			NoDrawIcon = 2,
			NoSysMenu = 4,
			NoMirrorHelp = 8,
		}

		[Flags]
		private enum DrawThemeTextOptionsMasks
		{
			TextColor = 1,
			BorderColor = 2,
			ShadowColor = 4,
			ShadowType = 8,
			ShadowOffset = 16, // 0x00000010
			BorderSize = 32, // 0x00000020
			FontProp = 64, // 0x00000040
			ColorProp = 128, // 0x00000080
			StateId = 256, // 0x00000100
			CalcRect = 512, // 0x00000200
			ApplyOverlay = 1024, // 0x00000400
			GlowSize = 2048, // 0x00000800
			Callback = 4096, // 0x00001000
			Composited = 8192, // 0x00002000
		}

		private enum WindowThemeAttributeType
		{
			NonClient = 1,
		}

		public struct DrawThemeTextOptions
		{
			private readonly int dwSize;
			private NativeMethods.DrawThemeTextOptionsMasks dwMasks;
			private int crText;
			private int crBorder;
			private int crShadow;
			private NativeMethods.TextShadowType iTextShadowType;
			private Point ptShadowOffset;
			private int iBorderSize;
			private int iFontPropId;
			private int iColorPropId;
			private readonly int iStateId;
			[MarshalAs(UnmanagedType.Bool)]
			private bool fApplyOverlay;
			private int iGlowSize;
			[MarshalAs(UnmanagedType.FunctionPtr)]
			private NativeMethods.DrawThemeTextCallback pfnDrawTextCallback;
			private IntPtr lParam;

			public DrawThemeTextOptions(bool init)
			  : this()
			{
				dwSize = Marshal.SizeOf(typeof(NativeMethods.DrawThemeTextOptions));
			}

			public Color AlternateColor
			{
				get => ColorTranslator.FromWin32(iColorPropId);
				set
				{
					iColorPropId = ColorTranslator.ToWin32(value);
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.ColorProp;
				}
			}

			public NativeMethods.DrawThemeTextSystemFonts AlternateFont
			{
				get => (NativeMethods.DrawThemeTextSystemFonts)iFontPropId;
				set
				{
					iFontPropId = (int)value;
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.FontProp;
				}
			}

			public bool AntiAliasedAlpha
			{
				get => (dwMasks & NativeMethods.DrawThemeTextOptionsMasks.Composited) == NativeMethods.DrawThemeTextOptionsMasks.Composited;
				set => SetFlag(NativeMethods.DrawThemeTextOptionsMasks.Composited, value);
			}

			public bool ApplyOverlay
			{
				get => fApplyOverlay;
				set
				{
					fApplyOverlay = value;
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.ApplyOverlay;
				}
			}

			public Color BorderColor
			{
				get => ColorTranslator.FromWin32(crBorder);
				set
				{
					crBorder = ColorTranslator.ToWin32(value);
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.BorderColor;
				}
			}

			public int BorderSize
			{
				get => iBorderSize;
				set
				{
					iBorderSize = value;
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.BorderSize;
				}
			}

			public NativeMethods.DrawThemeTextCallback Callback
			{
				get => pfnDrawTextCallback;
				set
				{
					pfnDrawTextCallback = value;
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.Callback;
				}
			}

			public int GlowSize
			{
				get => iGlowSize;
				set
				{
					iGlowSize = value;
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.GlowSize;
				}
			}

			public IntPtr LParam
			{
				get => lParam;
				set => lParam = value;
			}

			public bool ReturnCalculatedRectangle
			{
				get => (dwMasks & NativeMethods.DrawThemeTextOptionsMasks.CalcRect) == NativeMethods.DrawThemeTextOptionsMasks.CalcRect;
				set => SetFlag(NativeMethods.DrawThemeTextOptionsMasks.CalcRect, value);
			}

			public Color ShadowColor
			{
				get => ColorTranslator.FromWin32(crShadow);
				set
				{
					crShadow = ColorTranslator.ToWin32(value);
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.ShadowColor;
				}
			}

			public Point ShadowOffset
			{
				get => new(ptShadowOffset.X, ptShadowOffset.Y);
				set
				{
					ptShadowOffset = value;
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.ShadowOffset;
				}
			}

			public NativeMethods.TextShadowType ShadowType
			{
				get => iTextShadowType;
				set
				{
					iTextShadowType = value;
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.ShadowType;
				}
			}

			public Color TextColor
			{
				get => ColorTranslator.FromWin32(crText);
				set
				{
					crText = ColorTranslator.ToWin32(value);
					dwMasks |= NativeMethods.DrawThemeTextOptionsMasks.TextColor;
				}
			}

			public static NativeMethods.DrawThemeTextOptions Default => new(true);

			private void SetFlag(NativeMethods.DrawThemeTextOptionsMasks f, bool value)
			{
				if (value)
				{
					dwMasks |= f;
				}
				else
				{
					dwMasks &= ~f;
				}
			}
		}

		private struct WTA_OPTIONS
		{
			public NativeMethods.WindowThemeNonClientAttributes Flags;
			public int Mask;
		}

		[StructLayout(LayoutKind.Sequential)]
		public class DrawThemeBackgroundOptions
		{
			private readonly int dwSize;
			private NativeMethods.DrawThemeBackgroundOptions.DrawThemeBackgroundFlags dwFlags;
			private NativeMethods.RECT rcClip;

			public DrawThemeBackgroundOptions(Rectangle? clipRect)
			{
				dwSize = Marshal.SizeOf((object)this);
				ClipRectangle = clipRect;
			}

			public Rectangle? ClipRectangle
			{
				get
				{
					Rectangle rcClip = this.rcClip;
					return !rcClip.IsEmpty ? new Rectangle?(rcClip) : new Rectangle?();
				}
				set
				{
					rcClip = value ?? new NativeMethods.RECT();
					SetFlag(NativeMethods.DrawThemeBackgroundOptions.DrawThemeBackgroundFlags.ClipRect, value.HasValue);
				}
			}


			public static implicit operator NativeMethods.DrawThemeBackgroundOptions(
			  Rectangle clipRectangle)
			{
				return new NativeMethods.DrawThemeBackgroundOptions(new Rectangle?(clipRectangle));
			}

			private void SetFlag(
			  NativeMethods.DrawThemeBackgroundOptions.DrawThemeBackgroundFlags f,
			  bool value)
			{
				if (value)
				{
					dwFlags |= f;
				}
				else
				{
					dwFlags &= ~f;
				}
			}

			[Flags]
			private enum DrawThemeBackgroundFlags
			{
				None = 0,
				ClipRect = 1,
				DrawSolid = 2,
				OmitBorder = 4,
				OmitContent = 8,
				ComputingRegion = 16, // 0x00000010
				HasMirroredDC = 32, // 0x00000020
				DoNotMirror = 64, // 0x00000040
			}
		}

		public class SafeThemeHandle : SafeHandle
		{
			public SafeThemeHandle(IntPtr hTheme, bool ownsHandle = true)
			  : base(IntPtr.Zero, ownsHandle)
			{
				SetHandle(hTheme);
			}

			public override bool IsInvalid => handle == IntPtr.Zero;

			public static implicit operator NativeMethods.SafeThemeHandle(
			  VisualStyleRenderer r)
			{
				return new NativeMethods.SafeThemeHandle(r.Handle, false);
			}

			protected override bool ReleaseHandle() => NativeMethods.CloseThemeData(handle) == 0;
		}
	}
}
