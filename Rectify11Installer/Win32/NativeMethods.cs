using Microsoft.VisualBasic;
using Microsoft.Win32;
using Rectify11Installer.Core;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
#nullable enable

namespace Rectify11Installer.Win32
{
	public class NativeMethods
	{
		#region P/Invoke
		#region advapi32.dll
		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool LookupPrivilegeValue(string? lpSystemName, string lpName, out LUID lpLuid);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, [MarshalAs(UnmanagedType.Bool)] bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, UInt32 Zero, IntPtr Null1, IntPtr Null2);

		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);
		#endregion
		#region kernel32.dll
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool IsWow64Process2(
			IntPtr process,
			out ushort processMachine,
			out ushort nativeMachine
		);
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr hObject);
		#endregion
		#region user32.dll
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool ExitWindowsEx(ExitWindows uFlags, ShutdownReason dwReason);

		[DllImport("user32.dll")]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool revert);

		[DllImport("user32.dll")]
		public static extern int EnableMenuItem(IntPtr hMenu, int IDEnableItem, int enable);
		#endregion
		#region gdi32.dll
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr CreateCompatibleDC(IntPtr hDC);
		[DllImport("gdi32.dll")]
		internal static extern unsafe IntPtr CreateDIBSection(IntPtr hdc, BITMAPINFO pbmi, uint iUsage, out int* ppvBits, IntPtr hSection, uint dwOffset);

		[DllImport("gdi32.dll", EntryPoint = "SelectObject")]
		internal static extern IntPtr SelectObject([In] IntPtr hdc, [In] IntPtr hgdiobj);

		[DllImport("gdi32.dll")]
		internal static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
		#endregion
		[DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
		internal static extern int DrawThemeTextEx(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, string pszText, int iCharCount, uint flags, ref RECT rect, ref DTTOPTS poptions);

		[DllImport("dwmapi.dll")]
		public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

		[DllImport("SrClient.dll")]
		public static extern bool SRSetRestorePoint(ref RESTOREPOINTINFO SRPInfo, ref STATEMGRSTATUS SRPStatus);
		#endregion
		#region Flags
		public const int SC_CLOSE = 0xF060;
		public const int MF_BYCOMMAND = 0;
		public const int MF_ENABLED = 0;
		public const int MF_GRAYED = 1;
		public const int WP_CAPTION = 1;
		public const int CS_ACTIVE = 1;
		private const UInt32 TOKEN_QUERY = 0x0008;
		private const UInt32 TOKEN_ADJUST_PRIVILEGES = 0x0020;
		private const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
		private const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
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
		[Flags]
		private enum ExitWindows : uint
		{
			LogOff = 0x00,
			ShutDown = 0x01,
			Reboot = 0x02,
			PowerOff = 0x08,
			RestartApps = 0x40,
			Force = 0x04,
			ForceIfHung = 0x10,
		}

		[Flags]
		private enum ShutdownReason : uint
		{
			MajorOther = 0x00000000,
			MinorOther = 0x00000000,
			FlagPlanned = 0x80000000
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct LUID
		{
			public uint LowPart;
			public int HighPart;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct LUID_AND_ATTRIBUTES
		{
			public LUID Luid;
			public UInt32 Attributes;
		}
		private struct TOKEN_PRIVILEGES
		{
			public UInt32 PrivilegeCount;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public LUID_AND_ATTRIBUTES[] Privileges;
		}

		[Flags]
		public enum MoveFileFlags
		{
			MOVEFILE_REPLACE_EXISTING = 0x00000001,
			MOVEFILE_COPY_ALLOWED = 0x00000002,
			MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
			MOVEFILE_WRITE_THROUGH = 0x00000008,
			MOVEFILE_CREATE_HARDLINK = 0x00000010,
			MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RESTOREPOINTINFO
		{
			public int dwEventType;
			public int dwRestorePtType;
			public Int64 llSequenceNumber;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
			public string szDescription;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct STATEMGRSTATUS
		{
			public uint nStatus;
			public Int64 llSequenceNumber;
		}
		#endregion
		#region Public Methods
		public static bool SetCloseButton(FrmWizard frm, bool enable)
		{
			var hMenu = GetSystemMenu(frm.Handle, false);
			if (hMenu != IntPtr.Zero)
			{
				EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | (enable ? MF_ENABLED : MF_GRAYED));
				return true;
			}
			return false;
		}
		public static void Reboot()
		{
			var tokenHandle = IntPtr.Zero;
			try
			{
				// get process token
				if (!OpenProcessToken(Process.GetCurrentProcess().Handle,
					TOKEN_QUERY | TOKEN_ADJUST_PRIVILEGES,
					out tokenHandle))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to open process token handle");
				}

				// lookup the shutdown privilege
				TOKEN_PRIVILEGES tokenPrivs = new()
				{
					PrivilegeCount = 1,
					Privileges = new LUID_AND_ATTRIBUTES[1]
				};
				tokenPrivs.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;

				if (!LookupPrivilegeValue(null,
					SE_SHUTDOWN_NAME,
					out tokenPrivs.Privileges[0].Luid))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to open lookup shutdown privilege");
				}

				// add the shutdown privilege to the process token
				if (!AdjustTokenPrivileges(tokenHandle,
					false,
					ref tokenPrivs,
					0,
					IntPtr.Zero,
					IntPtr.Zero))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to adjust process token privileges");
				}

				// reboot
				if (!ExitWindowsEx(ExitWindows.Reboot,
					ShutdownReason.MajorOther | ShutdownReason.MinorOther | ShutdownReason.FlagPlanned))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to reboot system");
				}
			}
			finally
			{
				// close the process token
				if (tokenHandle != IntPtr.Zero)
				{
					CloseHandle(tokenHandle);
				}
			}
		}
		public static bool IsArm64()
		{
			var handle = Process.GetCurrentProcess().Handle;
			try
			{
				IsWow64Process2(handle, out var processMachine, out var nativeMachine);
				if (nativeMachine == 0xaa64) return true;
				else return false;
			}
			catch { return false; }
		}
		public static int GetUbr()
		{
			using var key = Registry.LocalMachine.OpenSubKey(@"software\microsoft\Windows NT\CurrentVersion");
			return Convert.ToInt32(key?.GetValue("UBR"));
		}
		public static void CreateSystemRestorePoint()
		{
			RegistryKey SystemRestoreKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion\\SystemRestore", true);
			SystemRestoreKey.SetValue("SystemRestorePointCreationFrequency", 0, RegistryValueKind.DWord);
			// ugh
			Interaction.Shell(Path.Combine(Variables.sys32Folder, "cmd.exe") + " /c powershell.exe -Command Checkpoint-Computer -Description \"Rectify11\" -RestorePointType \"APPLICATION_INSTALL\"", AppWinStyle.Hide, true);
			SystemRestoreKey.DeleteValue("SystemRestorePointCreationFrequency");
			SystemRestoreKey.Dispose();
		}
		#endregion
	}
}

