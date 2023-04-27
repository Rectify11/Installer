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
		private static string[] pendingFiles;
		private static string[] uninstallFiles;
		private static string[] x86Files;

		private static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Environment.Exit(1);
			}

			if (args[0] == "/install")
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
				var r11Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true)?.CreateSubKey("Rectify11", false);
				if (r11Reg != null)
				{
					pendingFiles = (string[])r11Reg.GetValue("PendingFiles");
					if (r11Reg.GetValue("x86PendingFiles") != null)
					{
						x86Files = (string[])r11Reg.GetValue("x86PendingFiles");
					}

				}
				MoveIconres();
				r11Reg?.Close();
				if (pendingFiles != null)
				{
					for (int i = 0; i < r11Dir.Length; i++)
					{
						for (int j = 0; j < pendingFiles.Length; j++)
						{
							if (pendingFiles[j].Contains(Path.GetFileName(r11Dir[i])))
							{
								MoveFile(FixString(pendingFiles[j], false), r11Dir[i], MoveType.General, null);
							}
						}
						if (x86Files != null)
						{
							for (int j = 0; j < x86Files.Length; j++)
							{
								if (x86Files[j].Contains(Path.GetFileName(r11Dir[i])))
								{
									MoveFile(FixString(x86Files[j], true), r11Dir[i], MoveType.x86, null);
								}
							}
						}
					}
					for (int i = 0; i < r11DiagDir.Length; i++)
					{
						for (int j = 0; j < pendingFiles.Length; j++)
						{
							if (pendingFiles[j].Contains("%diag%"))
							{
								string name = pendingFiles[j].Replace("%diag%\\", "").Replace("\\DiagPackage.dll", "");
								if (name.Contains(Path.GetFileNameWithoutExtension(r11DiagDir[i]).Replace("DiagPackage", "")))
								{
									MoveFile(FixString(pendingFiles[j], false), r11DiagDir[i], MoveType.Trouble, name);
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
			else if (args[0] == "/test")
			{
				Console.WriteLine(Path.GetTempPath());
			}
			else if (args[0] == "/uninstall")
			{
				var patches = PatchesParser.GetAll();
				var backup = Path.Combine(Variables.r11Folder, "Backup");
				var backupFiles = Directory.GetFiles(backup, "*", SearchOption.TopDirectoryOnly);

				// later
				// var backupDirs = Directory.GetDirectories(backup);

				var r11Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true).OpenSubKey("Rectify11", false);
				if (r11Reg != null)
					uninstallFiles = (string[])r11Reg.GetValue("UninstallFiles");

				if (uninstallFiles == null) return;

				for (int i = 0; i < backupFiles.Length; i++)
				{
					for (int j = 0; j < patches.Items.Length; j++)
					{
						for (int k = 0; k < uninstallFiles.Length; k++)
						{
							if (uninstallFiles[k].Contains(Path.GetFileName(backupFiles[i]))
								&& string.Equals(uninstallFiles[k], patches.Items[j].Mui))
							{
								string backupPath = backupFiles[i];
								string finalPath = FixString(patches.Items[j].HardlinkTarget, false);
								Console.WriteLine("Backup: " + backupPath);
								Console.WriteLine("Final: " + finalPath);
								File.Move(finalPath, Path.Combine(Path.GetTempPath(), Path.GetFileName(finalPath)));
								File.Move(backupPath, finalPath);
							}
							if (!string.IsNullOrWhiteSpace(patches.Items[j].x86))
							{
								if (Path.GetFileName(backupFiles[i]).Contains(Path.GetFileNameWithoutExtension(patches.Items[j].Mui) + "86" + Path.GetExtension(patches.Items[j].Mui)))
								{
									Console.WriteLine("\n==x86==");
									string backupPath = backupFiles[i];
									string finalPath = FixString(patches.Items[j].HardlinkTarget, true);
									Console.WriteLine("Backup: " + backupPath);
									Console.WriteLine("Final: " + finalPath);
									File.Move(finalPath, Path.Combine(Path.GetTempPath(), Path.GetFileName(finalPath)));
									File.Move(backupPath, finalPath);
								}
							}
						}
					}
				}
				Console.WriteLine("");
				Console.Write("Press any key to continue...");
				Console.ReadKey(true);
			}
			else if (args[0] == "/cleanup")
			{
				// E
			}
			Environment.Exit(0);
		}
		private static string FixString(string path, bool x86)
		{
			if (path.Contains("mun"))
			{
				return path.Replace(@"%sysresdir%", Variables.sysresdir);
			}
			else if (path.Contains("%sys32%"))
			{
				if (x86)
				{
					return path.Replace(@"%sys32%", Variables.sysWOWFolder);
				}
				else
				{
					return path.Replace(@"%sys32%", Variables.sys32Folder);
				}
			}
			else if (path.Contains("%lang%"))
			{
				return path.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
			}
			else if (path.Contains("%en-US%"))
			{
				return path.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
			}
			else if (path.Contains("%windirLang%"))
			{
				return path.Replace(@"%windirLang%", Path.Combine(Variables.windir, CultureInfo.CurrentUICulture.Name));
			}
			else if (path.Contains("%windirEn-US%"))
			{
				return path.Replace(@"%windirEn-US%", Path.Combine(Variables.windir, "en-US"));
			}
			else if (path.Contains("%branding%"))
			{
				return path.Replace(@"%branding%", Variables.brandingFolder);
			}
			else if (path.Contains("%prog%"))
			{
				if (x86)
				{
					return path.Replace(@"%prog%", Variables.progfiles86);
				}
				else
				{
					return path.Replace(@"%prog%", Variables.progfiles);
				}
			}
			else if (path.Contains("%windir%"))
			{
				return path.Replace(@"%windir%", Variables.windir);
			}
			else if (path.Contains("%diag%"))
			{
				return path.Replace("%diag%", Variables.diag);
			}
			return path;
		}
		private enum MoveType
		{
			General = 0,
			x86,
			Trouble
		}
		private static void MoveFile(string newval, string file, MoveType type, string name)
		{
			Console.WriteLine();
			Console.WriteLine(newval);
			Console.Write("Final path: ");
			string finalpath = string.Empty;
			if (type == MoveType.General)
			{
				finalpath = Path.Combine(Variables.r11Folder, "Backup", Path.GetFileName(newval));
			}
			else if (type == MoveType.x86)
			{
				finalpath = Path.Combine(Variables.r11Folder, "Backup", Path.GetFileNameWithoutExtension(newval) + "86" + Path.GetExtension(newval));
			}
			else if (type == MoveType.Trouble)
			{
				finalpath = Path.Combine(Variables.r11Folder, "Backup", "Diag", Path.GetFileNameWithoutExtension(newval) + name + Path.GetExtension(newval));
			}
			Console.WriteLine(finalpath);
			if (string.IsNullOrWhiteSpace(finalpath)) return;
			if (!File.Exists(finalpath))
			{
				File.Move(newval, finalpath);
			}
			File.Copy(file, newval, true);

		}
		private static void MoveIconres()
		{
			var iconresDest = Path.Combine(Variables.sys32Folder, "iconres.dll");
			var iconres = Path.Combine(Variables.r11Files, "iconres.dll");
			try
			{
				File.Copy(iconres, iconresDest, true);
				Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "icons.reg"), AppWinStyle.Hide, true);
			}
			catch
			{
				// ignored
			}
		}
	}
	public class Variables
	{
		public static string windir = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
		public static string r11Folder = Path.Combine(windir, "Rectify11");
		public static string r11Files = Path.Combine(r11Folder, "files");
		public static string sys32Folder = Environment.SystemDirectory;
		public static string sysWOWFolder = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
		public static string sysresdir = Path.Combine(windir, "SystemResources");
		public static string brandingFolder = Path.Combine(windir, "Branding");
		public static string progfiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
		public static string progfiles86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
		public static string diag = Path.Combine(windir, "diagnostics", "system");
	}
}
