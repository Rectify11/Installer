using Microsoft.VisualBasic;
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
			MoveIconres();
			r11Reg.Close();
			if (pendingFiles != null)
			{
				for (int i = 0; i < r11Dir.Length; i++)
				{
					for (int j = 0; j < pendingFiles.Length; j++)
					{
						if (pendingFiles[j].Contains(Path.GetFileName(r11Dir[i])))
						{
							if (pendingFiles[j].Contains("mun"))
							{
								string newval = pendingFiles[j].Replace(@"%sysresdir%", Variables.sysresdir);
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%sys32%"))
							{
								string newval = pendingFiles[j].Replace(@"%sys32%", Variables.sys32Folder);
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%lang%"))
							{
								string newval = pendingFiles[j].Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%en-US%"))
							{
								string newval = pendingFiles[j].Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%windirLang%"))
							{
								string newval = pendingFiles[j].Replace(@"%windirLang%", Path.Combine(Variables.windir, CultureInfo.CurrentUICulture.Name));
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%windirEn-US%"))
							{
								string newval = pendingFiles[j].Replace(@"%windirEn-US%", Path.Combine(Variables.windir, "en-US"));
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%branding%"))
							{
								string newval = pendingFiles[j].Replace(@"%branding%", Variables.brandingFolder);
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%prog%"))
							{
								string newval = pendingFiles[j].Replace(@"%prog%", Variables.progfiles);
								MoveFile(newval, r11Dir[i]);
							}
							else if (pendingFiles[j].Contains("%windir%"))
							{
								string newval = pendingFiles[j].Replace(@"%windir%", Variables.windir);
								MoveFile(newval, r11Dir[i]);
							}
						}
					}
					if (x86Files != null)
					{
						for (int j = 0; j < x86Files.Length; j++)
						{
							if (x86Files[j].Contains(Path.GetFileName(r11Dir[i])))
							{
								if (x86Files[j].Contains("%sys32%"))
								{
									string newval = x86Files[j].Replace(@"%sys32%", Variables.sysWOWFolder);
									MoveFilex86(newval, r11Dir[i]);
								}
								else if (x86Files[j].Contains("%prog%"))
								{
									string newval = x86Files[j].Replace(@"%prog%", Variables.progfiles86);
									MoveFilex86(newval, r11Dir[i]);
								}
							}
						}
					}
				}
				for (int i  = 0; i < r11DiagDir.Length; i++)
				{
					for (int j = 0; j < pendingFiles.Length; j++)
					{
						if (pendingFiles[j].Contains("%diag%"))
						{
							string name = pendingFiles[j].Replace("%diag%\\", "").Replace("\\DiagPackage.dll", "");
							if (name.Contains(Path.GetFileNameWithoutExtension(r11DiagDir[i]).Replace("DiagPackage", "")))
							{
								string newval = pendingFiles[j].Replace("%diag%", Variables.diag);
								MoveTrouble(newval, r11DiagDir[i], name);
							}
						}
					}
				}
				for (int k = 0; k < pendingFiles.Length; k++)
				{
					if (pendingFiles[k].Contains("mmcbase.dll.mun") 
						|| pendingFiles[k].Contains("mmcndmgr.dll.mun") 
						|| pendingFiles[k].Contains("mmc.exe"))
					{
						if (!Directory.Exists(Path.Combine(backupDir, "msc")))
						{
							Directory.CreateDirectory(Path.Combine(backupDir, "msc"));
							if (CultureInfo.CurrentUICulture.Name != "en-US")
							{
								Directory.CreateDirectory(Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name));
							}
							Directory.CreateDirectory(Path.Combine(backupDir, "msc", "en-US"));
						}
						var langFolder = Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name);
						var usaFolder = Path.Combine(Variables.sys32Folder, "en-US");
						List<string> langMsc = new List<string>(Directory.GetFiles(langFolder, "*.msc", SearchOption.TopDirectoryOnly));
						List<string> usaMsc = new List<string>(Directory.GetFiles(usaFolder, "*.msc", SearchOption.TopDirectoryOnly));
						List<string> sysMsc = new List<string>(Directory.GetFiles(Variables.sys32Folder, "*.msc", SearchOption.TopDirectoryOnly));
						List<string> r11Msc = new List<string>(Directory.GetFiles(Path.Combine(Variables.r11Folder, "Tmp", "msc"), "*.msc", SearchOption.TopDirectoryOnly));
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
						if (CultureInfo.CurrentUICulture.Name != "en-US")
						{
							List<string> r11LangMsc = new List<string>(Directory.GetFiles(Path.Combine(Variables.r11Folder, "Tmp", "msc", CultureInfo.CurrentUICulture.Name), "*.msc", SearchOption.TopDirectoryOnly));
							for (int j = 0; j < r11LangMsc.Count; j++)
							{
								for (int i = 0; i < langMsc.Count; i++)
								{
									if (Path.GetFileName(langMsc[i]) == Path.GetFileName(r11LangMsc[j]))
									{
										Console.WriteLine(langMsc[i]);
										if (!File.Exists(Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name, Path.GetFileName(langMsc[i]))))
										{
											File.Move(langMsc[i], Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name, Path.GetFileName(langMsc[i])));
										}
										File.Copy(r11LangMsc[j], langMsc[i], true);
									}
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
		private static void MoveIconres()
		{
			string iconresDest = Path.Combine(Variables.sys32Folder, "iconres.dll");
			string iconres = Path.Combine(Variables.r11Files, "iconres.dll");
			try
			{
				File.Copy(iconres, iconresDest, true);
				Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "icons.reg"), AppWinStyle.Hide, true);
			}
			catch { }
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
