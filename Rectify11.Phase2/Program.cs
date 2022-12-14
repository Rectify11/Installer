using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Rectify11.Phase2
{
	internal class Program
	{
		private static string[] pendingFiles = null;
		private static string[] x86Files = null;

		private static void Main()
		{
			var backupDir = Path.Combine(Variables.r11Folder, "Backup");
			var backupDiagDir = Path.Combine(Variables.r11Folder, "Backup", "Diag");
			var tempDiagDir = Path.Combine(Variables.r11Folder, "Tmp", "Diag");
			if (!Directory.Exists(backupDir))
			{
				Directory.CreateDirectory(backupDir);
			}

			if (!Directory.Exists(backupDiagDir))
			{
				Directory.CreateDirectory(backupDiagDir);
			}

			// temp solution
			if (!Directory.Exists(tempDiagDir))
			{
				Directory.CreateDirectory(tempDiagDir);
			}

			var r11Dir = Directory.GetFiles(Path.Combine(Variables.r11Folder, "Tmp"));
			var r11DiagDir = Directory.GetFiles(Path.Combine(Variables.r11Folder, "Tmp", "Diag"));
			var r11Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true).CreateSubKey("Rectify11", false);
			if (r11Reg != null)
			{
				pendingFiles = (string[])r11Reg.GetValue("PendingFiles");
				if (r11Reg.GetValue("x86PendingFiles") != null)
				{
					x86Files = (string[])r11Reg.GetValue("x86PendingFiles");
				}

			}
			r11Reg.Close();
			if (pendingFiles != null)
			{
				foreach (string file in r11Dir)
				{
					foreach (string regFile in pendingFiles)
					{
						if (regFile.Contains(Path.GetFileName(file)))
						{
							if (regFile.Contains("mun"))
							{
								string newval = regFile.Replace(@"%sysresdir%", Variables.sysresdir);
								MoveFile(newval, file);
							}
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
				foreach (string diagFile in r11DiagDir)
				{
					foreach (string regFile in pendingFiles)
					{
						if (regFile.Contains("%diag%"))
						{
							string name = regFile.Replace("%diag%\\", "").Replace("\\DiagPackage.dll", "");
							if (name.Contains(Path.GetFileNameWithoutExtension(diagFile).Replace("DiagPackage", "")))
							{
								string newval = regFile.Replace("%diag%", Variables.diag);
								MoveTrouble(newval, diagFile, name);
							}
						}
					}
				}
				foreach (string regFile in pendingFiles)
				{
					if (regFile.Contains("mmcbase.dll.mun") || regFile.Contains("mmcndmgr.dll.mun") || regFile.Contains("mmc.exe"))
					{
						if (!Directory.Exists(Path.Combine(backupDir, "msc")))
						{
							Directory.CreateDirectory(Path.Combine(backupDir, "msc"));
							Directory.CreateDirectory(Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name));
							Directory.CreateDirectory(Path.Combine(backupDir, "msc", "en-US"));
						}
						var langFolder = Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name);
						var usaFolder = Path.Combine(Variables.sys32Folder, "en-US");
						List<string> langMsc = new List<string>(Directory.GetFiles(langFolder, "*.msc", SearchOption.TopDirectoryOnly));
						List<string> usaMsc = new List<string>(Directory.GetFiles(usaFolder, "*.msc", SearchOption.TopDirectoryOnly));
						List<string> sysMsc = new List<string>(Directory.GetFiles(Variables.sys32Folder, "*.msc", SearchOption.TopDirectoryOnly));
						List<string> r11Msc = new List<string>(Directory.GetFiles(Path.Combine(Variables.r11Folder, "Tmp", "mmc"), "*.msc", SearchOption.TopDirectoryOnly));
						if (CultureInfo.CurrentUICulture.Name != "en-US")
						{
							for (int i = 0; i < langMsc.Count; i++)
							{
								for (int j = 0; j < usaMsc.Count; j++)
								{
									if (Path.GetFileName(langMsc[i]) == Path.GetFileName(usaMsc[j]))
									{
										usaMsc.RemoveAt(j);
									}
								}
							}
						}
						for (int j = 0; j < r11Msc.Count; j++)
						{
							if (CultureInfo.CurrentUICulture.Name != "en-US")
							{
								for (int i = 0; i < langMsc.Count; i++)
								{
									if (Path.GetFileName(langMsc[i]) == Path.GetFileName(r11Msc[j]))
									{
										Console.WriteLine(langMsc[i]);
										if (!File.Exists(Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name, Path.GetFileName(langMsc[i]))))
										{
											File.Move(langMsc[i], Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name, Path.GetFileName(langMsc[i])));
										}
										File.Copy(r11Msc[j], langMsc[i], true);
									}
								}
							}
							for (int i = 0; i < usaMsc.Count; i++)
							{
								if (Path.GetFileName(usaMsc[i]) == Path.GetFileName(r11Msc[j]))
								{
									Console.WriteLine(usaMsc[i]);
									if (!File.Exists(Path.Combine(backupDir, "msc", "en-US", Path.GetFileName(usaMsc[i]))))
									{
										File.Move(usaMsc[i], Path.Combine(backupDir, "msc", "en-US", Path.GetFileName(usaMsc[i])));
									}
									File.Copy(r11Msc[j], usaMsc[i], true);
								}
							}
							for (int i = 0; i < sysMsc.Count; i++)
							{
								if (Path.GetFileName(sysMsc[i]) == Path.GetFileName(r11Msc[j]))
								{
									Console.WriteLine(sysMsc[i]);
									if (!File.Exists(Path.Combine(backupDir, "msc", Path.GetFileName(sysMsc[i]))))
									{
										File.Move(sysMsc[i], Path.Combine(backupDir, "msc", Path.GetFileName(sysMsc[i])));
									}
									File.Copy(r11Msc[j], sysMsc[i], true);
								}
							}
						}
					}
				}
			}
			Directory.Delete(Path.Combine(Variables.r11Folder, "Tmp"), true);
		}

		private static void MoveFile(string newval, string file)
		{
			Console.WriteLine();
			Console.WriteLine(newval);
			Console.Write("Final path: ");
			string finalpath = Path.Combine(Variables.r11Folder, "Backup", Path.GetFileName(newval));
			Console.WriteLine(finalpath);
			if (!File.Exists(finalpath))
			{
				File.Move(newval, finalpath);
			}
			File.Copy(file, newval, true);

		}
		private static void MoveFilex86(string newval, string file)
		{
			Console.WriteLine();
			Console.WriteLine(newval);
			Console.Write("Final path: ");
			string finalpath = Path.Combine(Variables.r11Folder, "Backup", Path.GetFileNameWithoutExtension(newval) + "86" + Path.GetExtension(newval));
			Console.WriteLine(finalpath);
			if (!File.Exists(finalpath))
			{
				File.Move(newval, finalpath);
			}
			File.Copy(file, newval, true);
		}
		private static void MoveTrouble(string newval, string file, string name)
		{
			Console.WriteLine();
			Console.WriteLine(newval);
			Console.Write("Final path: ");
			string finalpath = Path.Combine(Variables.r11Folder, "Backup", "Diag", Path.GetFileNameWithoutExtension(newval) + name + Path.GetExtension(newval));
			Console.WriteLine(finalpath);
			if (!File.Exists(finalpath))
			{
				File.Move(newval, finalpath);
			}
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
