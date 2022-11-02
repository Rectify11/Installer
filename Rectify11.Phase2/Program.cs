using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace Rectify11.Phase2
{
	class Program
	{
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName,
		  MoveFileFlags dwFlags);

		[Flags]
		enum MoveFileFlags
		{
			MOVEFILE_REPLACE_EXISTING = 0x00000001,
			MOVEFILE_COPY_ALLOWED = 0x00000002,
			MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
			MOVEFILE_WRITE_THROUGH = 0x00000008,
			MOVEFILE_CREATE_HARDLINK = 0x00000010,
			MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
		}
		static void Main(string[] args)
		{
			string[] pendingFiles = null;
			string[] x86Files = null;
			var r11dir = Directory.GetFiles(Path.Combine(Variables.r11Folder, "Tmp"));
			var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true).CreateSubKey("Rectify11", false);
			if (reg != null)
			{
				pendingFiles = (string[])reg.GetValue("PendingFiles");
				if (reg.GetValue("x86PendingFiles") != null)
				{
					x86Files = (string[])reg.GetValue("x86PendingFiles");
				}
			}
			reg.Close();
			foreach (string file in r11dir)
			{
				foreach (string regFile in pendingFiles)
				{
					if (regFile.Contains(Path.GetFileName(file)))
					{
						// TrustedInstaller part
						if (regFile.Contains("mun"))
						{
							string newval = regFile.Replace(@"%sysresdir%", Variables.sysresdir);
							MoveFile(newval, file);
						}
						// will improve later
						else if (regFile.Contains("%sys32%"))
						{
							string newval = regFile.Replace(@"%sys32%", Variables.sys32Folder);
							MoveFile(newval, file);
						}
						else if (regFile.Contains("%lang%"))
						{
							string newval = regFile.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
							MoveFile(newval, file);
						}
						else if (regFile.Contains("%en-US%"))
						{
							string newval = regFile.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
							MoveFile(newval, file);
						}
						else if (regFile.Contains("%branding%"))
						{
							string newval = regFile.Replace(@"%branding%", Variables.brandingFolder);
							MoveFile(newval, file);
						}
						else if (regFile.Contains("%prog%"))
						{
							string newval = regFile.Replace(@"%prog%", Variables.progfiles);
							MoveFile(newval, file);
						}
						else if (regFile.Contains("%windir%"))
						{
							string newval = regFile.Replace(@"%windir%", Variables.windir);
							MoveFile(newval, file);
						}
						// troubleshooter later
						// x86 later
					}
				}
				if (x86Files != null)
				{
					foreach (string x86file in x86Files)
					{
						if (x86file.Contains(Path.GetFileName(file)))
						{
							if (x86file.Contains("%sys32%"))
							{
								string newval = x86file.Replace(@"%sys32%", Variables.sysWOWFolder);
								MoveFilex86(newval, file);
							}
							else if (x86file.Contains("%prog%"))
							{
								string newval = x86file.Replace(@"%prog%", Variables.progfiles86);
								MoveFilex86(newval, file);
							}
						}
					}
				}
			}
		}

		private static void MoveFile(string newval, string file)
		{
			Console.WriteLine();
			Console.WriteLine(newval);
			Console.Write("Final path: ");
			Console.WriteLine(Path.Combine(Variables.r11Folder, "Backup", Path.GetFileName(newval)));
			File.Move(newval, Path.Combine(Variables.r11Folder, "Backup", Path.GetFileName(newval)));
			File.Copy(file, newval, true);
		}
		private static void MoveFilex86(string newval, string file)
		{
			Console.WriteLine();
			Console.WriteLine(newval);
			Console.Write("Final path: ");
			Console.WriteLine(Path.Combine(Variables.r11Folder, "Backup", Path.GetFileNameWithoutExtension(newval) + "86" + Path.GetExtension(newval)));
			File.Move(newval, Path.Combine(Variables.r11Folder, "Backup", Path.GetFileNameWithoutExtension(newval) + "86" + Path.GetExtension(newval)));
			File.Copy(file, newval, true);
		}
	}
	public class Variables
	{
		public static string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
		public static string r11Folder = System.IO.Path.Combine(windir, "Rectify11");
		public static string r11Files = System.IO.Path.Combine(r11Folder, "files");
		public static string sys32Folder = Environment.SystemDirectory;
		public static string sysWOWFolder = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
		public static string sysresdir = System.IO.Path.Combine(windir, "SystemResources");
		public static string brandingFolder = System.IO.Path.Combine(windir, "Branding");
		public static string progfiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
		public static string progfiles86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
		public static string diag = System.IO.Path.Combine(windir, "diagnostics", "system");
	}
}
