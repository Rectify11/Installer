using System;
using System.Runtime.InteropServices;
using System.Drawing;

namespace vbAccelerator.Components.Shell
{
	/// <summary>
	/// Enables extraction of icons for any file type from
	/// the Shell.
	/// </summary>
	public class FileIcon
	{

		#region UnmanagedCode
		private const int MAX_PATH = 260;
		
		[StructLayout(LayoutKind.Sequential)]
		private struct SHFILEINFO
		{
			public IntPtr hIcon;
			public int iIcon;
			public int dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=MAX_PATH)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
			public string szTypeName;
		}

		[DllImport("shell32")]
		private static extern int SHGetFileInfo (
			string pszPath, 
			int dwFileAttributes,
			ref SHFILEINFO psfi, 
			uint cbFileInfo, 
			uint uFlags);

		[DllImport("user32.dll")]
		private static extern int DestroyIcon(IntPtr hIcon);

		private const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100; 
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;
		private const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;
		private const int FORMAT_MESSAGE_FROM_STRING = 0x400;
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
		private const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 0xFF;
		[DllImport("kernel32")]
		private extern static int FormatMessage (
			int dwFlags, 
			IntPtr lpSource, 
			int dwMessageId, 
			int dwLanguageId, 
			string lpBuffer,
			uint nSize, 
			int argumentsLong);

		[DllImport("kernel32")]
		private extern static int GetLastError();
		#endregion
		
		#region Member Variables
		private string fileName;
		private string displayName;
		private string typeName;
		private SHGetFileInfoConstants flags;
		private Icon fileIcon;
		#endregion

		#region Enumerations
		[Flags]		
			public enum SHGetFileInfoConstants : int
		{
			SHGFI_ICON = 0x100,                // get icon 
			SHGFI_DISPLAYNAME = 0x200,         // get display name 
			SHGFI_TYPENAME = 0x400,            // get type name 
			SHGFI_ATTRIBUTES = 0x800,          // get attributes 
			SHGFI_ICONLOCATION = 0x1000,       // get icon location 
			SHGFI_EXETYPE = 0x2000,            // return exe type 
			SHGFI_SYSICONINDEX = 0x4000,       // get system icon index 
			SHGFI_LINKOVERLAY = 0x8000,        // put a link overlay on icon 
			SHGFI_SELECTED = 0x10000,          // show icon in selected state 
			SHGFI_ATTR_SPECIFIED = 0x20000,    // get only specified attributes 
			SHGFI_LARGEICON = 0x0,             // get large icon 
			SHGFI_SMALLICON = 0x1,             // get small icon 
			SHGFI_OPENICON = 0x2,              // get open icon 
			SHGFI_SHELLICONSIZE = 0x4,         // get shell size icon 
			//SHGFI_PIDL = 0x8,                  // pszPath is a pidl 
			SHGFI_USEFILEATTRIBUTES = 0x10,     // use passed dwFileAttribute 
			SHGFI_ADDOVERLAYS = 0x000000020,     // apply the appropriate overlays
			SHGFI_OVERLAYINDEX = 0x000000040     // Get the index of the overlay
		}
		#endregion

		#region Implementation
		/// <summary>
		/// Gets/sets the flags used to extract the icon
		/// </summary>
		public FileIcon.SHGetFileInfoConstants Flags
		{
			get
			{
				return flags;
			}
			set
			{
				flags = value;
			}
		}

		/// <summary>
		/// Gets/sets the filename to get the icon for
		/// </summary>
		public string FileName
		{
			get
			{
				return fileName;
			}
			set
			{
				fileName = value;
			}
		}

		/// <summary>
		/// Gets the icon for the chosen file
		/// </summary>
		public Icon ShellIcon
		{
			get
			{
				return fileIcon;
			}
		}

		/// <summary>
		/// Gets the display name for the selected file
		/// if the SHGFI_DISPLAYNAME flag was set.
		/// </summary>
		public string DisplayName
		{
			get
			{
				return displayName;
			}
		}

		/// <summary>
		/// Gets the type name for the selected file
		/// if the SHGFI_TYPENAME flag was set.
		/// </summary>
		public string TypeName
		{
			get
			{
				return typeName;
			}
		}

		/// <summary>
		///  Gets the information for the specified 
		///  file name and flags.
		/// </summary>
		public void GetInfo()
		{
			fileIcon = null;
			typeName = "";
			displayName = "";

			SHFILEINFO shfi = new SHFILEINFO();
			uint shfiSize = (uint)Marshal.SizeOf(shfi.GetType());

			int ret = SHGetFileInfo(
				fileName, 0, ref shfi, shfiSize, (uint)(flags));
			if (ret != 0)
			{
				if (shfi.hIcon != IntPtr.Zero)
				{
					fileIcon = System.Drawing.Icon.FromHandle(shfi.hIcon);
					// Now owned by the GDI+ object
					//DestroyIcon(shfi.hIcon);
				}
				typeName = shfi.szTypeName;
				displayName = shfi.szDisplayName;
			}
			else
			{
			
				int err = GetLastError();
				Console.WriteLine("Error {0}", err);
				string txtS = new string('\0', 256);
				int len = FormatMessage(
					FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
					IntPtr.Zero, err, 0, txtS, 256, 0);
				Console.WriteLine("Len {0} text {1}", len, txtS);

				// throw exception

			}
		}

		/// <summary>
		/// Constructs a new, default instance of the FileIcon
		/// class.  Specify the filename and call GetInfo()
		/// to retrieve an icon.
		/// </summary>
		public FileIcon()
		{
			flags = SHGetFileInfoConstants.SHGFI_ICON | 
				SHGetFileInfoConstants.SHGFI_DISPLAYNAME |
				SHGetFileInfoConstants.SHGFI_TYPENAME |
				SHGetFileInfoConstants.SHGFI_ATTRIBUTES |
				SHGetFileInfoConstants.SHGFI_EXETYPE;
		}
		/// <summary>
		/// Constructs a new instance of the FileIcon class
		/// and retrieves the icon, display name and type name
		/// for the specified file.		
		/// </summary>
		/// <param name="fileName">The filename to get the icon, 
		/// display name and type name for</param>
		public FileIcon(string fileName) : this()
		{
			this.fileName = fileName;
			GetInfo();
		}
		/// <summary>
		/// Constructs a new instance of the FileIcon class
		/// and retrieves the information specified in the 
		/// flags.
		/// </summary>
		/// <param name="fileName">The filename to get information
		/// for</param>
		/// <param name="flags">The flags to use when extracting the
		/// icon and other shell information.</param>
		public FileIcon(string fileName, FileIcon.SHGetFileInfoConstants flags)
		{
			this.fileName = fileName;
			this.flags = flags;
			GetInfo();
		}

		#endregion	
	}
}