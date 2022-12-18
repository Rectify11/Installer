using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
	public class Installer
	{
		#region Variables
		private string newhardlink;
		#endregion
		#region Public Methods
		public async Task<bool> Install(frmWizard frm)
		{
			if (!File.Exists(Path.Combine(Variables.r11Folder, "7za.exe")))
			{
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "7za.exe"), Properties.Resources._7za);
			}

			File.WriteAllBytes(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files7z);

			if (!File.Exists(Path.Combine(Variables.r11Folder, "ResourceHacker.exe")))
			{
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"), Properties.Resources.ResourceHacker);
			}

			if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
			{
				Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup"));
			}

			if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
			{
				Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp"));
			}

			File.Copy(Assembly.GetExecutingAssembly().Location, Path.Combine(Variables.r11Folder, "Uninstall.exe"), true);

			if (!Directory.Exists(Path.Combine(Variables.r11Folder, "files")))
			{
				frm.InstallerProgress = "Extracting files...";
				Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
					" x -o" + Path.Combine(Variables.r11Folder, "files") +
					" " + Path.Combine(Variables.r11Folder, "files.7z"), AppWinStyle.Hide, true, -1);
			}
			if (InstallOptions.iconsList.Count > 0)
			{
				// Get all patches
				Patches patches = PatchesParser.GetAll();
				PatchesPatch[] ok = patches.Items;
				decimal progress = 0;
				List<string> fileList = new();
				List<string> x86List = new();
				foreach (PatchesPatch patch in ok)
				{
					foreach (string items in InstallOptions.iconsList)
					{
						if (patch.Mui.Contains(items))
						{
							decimal number = Math.Round((progress / InstallOptions.iconsList.Count) * 100m);
							frm.InstallerProgress = "Patching " + patch.Mui + " (" + number + "%)";
							fileList.Add(patch.HardlinkTarget);
							if (!string.IsNullOrWhiteSpace(patch.x86))
							{
								x86List.Add(patch.HardlinkTarget);
							}

							MatchAndApplyRule(patch);
							progress++;
						}
					}
				}
				var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true).CreateSubKey("Rectify11", true);
				if (reg != null)
				{
					reg.SetValue("PendingFiles", fileList.ToArray());
					if (x86List.Count != 0)
					{
						reg.SetValue("x86PendingFiles", x86List.ToArray());
					}

					reg.SetValue("Language", CultureInfo.CurrentUICulture.Name);
					reg.SetValue("Version", Application.ProductVersion);
				}
				reg.Close();
				if (!File.Exists(Path.Combine(Variables.r11Folder, "aRun.exe")))
				{
					File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun);
				}

				frm.InstallerProgress = "Replacing files";

				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2);

				if (Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp", "mmc")))
				{
					Directory.Delete(Path.Combine(Variables.r11Folder, "Tmp", "mmc"), true);
				}
				try
				{
					Directory.Move(Path.Combine(Variables.r11Files, "mmc"), Path.Combine(Variables.r11Folder, "Tmp", "mmc"));
				}
				catch { }
				try
				{
					await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "icons.reg"), AppWinStyle.Hide, true));
				}
				catch { }
				try
				{
					await Task.Run(() => File.Copy(Path.Combine(Variables.r11Files, "iconres.dll"), Path.Combine(Variables.sys32Folder, "iconres.dll"), true));
				}
				catch { }
				await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun.exe") + " /EXEFilename " + '"' + Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe") + '"' + " /RunAs 8 /Run", AppWinStyle.Hide, true));
				while (true)
				{
					if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
					{
						break;
					}
					else
					{
						Thread.Sleep(1000);
					}
				}
			}
			if (InstallOptions.InstallThemes)
			{
				frm.InstallerProgress = "Installing Themes";
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "themes.7z"), Properties.Resources.themes);
				if (!Directory.Exists(Path.Combine(Variables.r11Folder, "themes")))
				{
					Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
						" x -o" + Path.Combine(Variables.r11Folder, "themes") +
						" " + Path.Combine(Variables.r11Folder, "themes.7z"), AppWinStyle.Hide, true, -1);
				}
				try {

					DirectoryInfo cursors = new DirectoryInfo(Path.Combine(Variables.r11Folder, "themes", "cursors"));
					DirectoryInfo[] curdir = cursors.GetDirectories("*", SearchOption.TopDirectoryOnly);
					DirectoryInfo themedir = new DirectoryInfo(Path.Combine(Variables.r11Folder, "themes", "themes"));
					DirectoryInfo[] msstyleDirList = themedir.GetDirectories("*", SearchOption.TopDirectoryOnly);
					FileInfo[] themefiles = themedir.GetFiles("*.theme");

					try
					{
						Directory.Move(Path.Combine(Variables.r11Folder, "themes", "wallpapers"), Path.Combine(Variables.windir, "web", "wallpaper", "Rectified"));
					}
					catch { }
					try
					{
						File.Copy(Path.Combine(Variables.r11Folder, "themes", "ThemeTool.exe"), Path.Combine(Variables.windir, "ThemeTool.exe"), true);
					}
					catch { }
					try
					{
						File.Copy(Path.Combine(Variables.r11Folder, "themes", "Elevate.exe"), Path.Combine(Variables.windir, "Elevate.exe"), true);
					}
					catch { }
					try 
					{
						Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Folder, "themes", "Themes.reg"), AppWinStyle.Hide, true); 
					}
					catch { }

					foreach (DirectoryInfo dir in curdir)
						try
						{
							if (Directory.Exists(Path.Combine(Variables.windir, "cursors", dir.Name)))
							{
								Directory.Delete(Path.Combine(Variables.windir, "cursors", dir.Name), true);
							}
							Directory.Move(dir.FullName, Path.Combine(Variables.windir, "cursors", dir.Name));	
						}
						catch { }

					foreach (FileInfo file in themefiles)
						try
						{
							File.Copy(file.FullName, Path.Combine(Variables.windir, "Resources", "Themes", file.Name), true);
						}
						catch { }

					foreach (DirectoryInfo directory in msstyleDirList)
						try
						{
							Directory.Move(directory.FullName, Path.Combine(Variables.windir, "Resources", "Themes", directory.Name));
						}
						catch { }

				}
				catch { }

			}
			AddToControlPanel();
			// refresh icon cache
			try { await Task.Run(() => Interaction.Shell("taskkill.exe /f /im explorer.exe", AppWinStyle.Hide, true)); }
			catch { }
			try
			{
				DirectoryInfo di = new DirectoryInfo(Path.Combine(Environment.GetEnvironmentVariable("localappdata"), "microsoft", "windows", "explorer"));
				FileInfo[] files = di.GetFiles("*.db");

				foreach (FileInfo file in files)
					try
					{
						file.Attributes = FileAttributes.Normal;
						File.Delete(file.FullName);
					}
					catch { }
			}
			catch { }
			frm.InstallerProgress = "Cleaning up...";
			try
			{
				Directory.Delete(Variables.r11Files, true);
				File.Delete(Path.Combine(Variables.r11Folder, "files.7z"));
				try
				{
					Directory.Delete(Path.Combine(Variables.r11Folder, "themes"), true);
					File.Delete(Path.Combine(Variables.r11Folder, "themes.7z"));
				}
				catch { }
			}
			catch { }

			return true;
		}
		#endregion
		#region Private Methods
		private bool AddToControlPanel()
		{
			var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
			if (key != null)
			{
				var r11key = key.CreateSubKey("Rectify11", true);
				if (r11key != null)
				{
					r11key.SetValue("DisplayName", "Rectify11", RegistryValueKind.String);
					r11key.SetValue("DisplayVersion", Assembly.GetEntryAssembly().GetName().Version.ToString(), RegistryValueKind.String);
					r11key.SetValue("DisplayIcon", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
					r11key.SetValue("InstallLocation", Variables.r11Folder, RegistryValueKind.String);
					r11key.SetValue("UninstallString", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
					r11key.SetValue("ModifyPath", Path.Combine(Variables.r11Folder, "Uninstall.exe"), RegistryValueKind.String);
					r11key.SetValue("NoRepair", 1, RegistryValueKind.DWord);
					r11key.SetValue("VersionMajor", Assembly.GetEntryAssembly().GetName().Version.Major.ToString(), RegistryValueKind.String);
					r11key.SetValue("VersionMinor", Assembly.GetEntryAssembly().GetName().Version.Minor.ToString(), RegistryValueKind.String);
					r11key.SetValue("Publisher", "The Rectify11 Team", RegistryValueKind.String);
					r11key.SetValue("URLInfoAbout", "https://rectify.vercel.app/", RegistryValueKind.String);
					return true;
				}
				return false;
			}
			return false;
		}
		private enum PatchType
		{
			General = 0,
			Mui,
			Troubleshooter,
			Ignore,
			MinVersion,
			MaxVersion,
			x86

		}
		private static void Patch(string file, PatchesPatch patch, PatchType type)
		{
			if (File.Exists(file))
			{
				string name;
				string backupfolder;
				string tempfolder;
				if (type == PatchType.Troubleshooter)
				{
					name = patch.Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
					backupfolder = Path.Combine(Variables.r11Folder, "backup", "Diag");
					tempfolder = Path.Combine(Variables.r11Folder, "Tmp", "Diag");
				}
				else if (type == PatchType.x86)
				{
					string ext = Path.GetExtension(patch.Mui);
					name = Path.GetFileNameWithoutExtension(patch.Mui) + "86" + ext;
					backupfolder = Path.Combine(Variables.r11Folder, "backup");
					tempfolder = Path.Combine(Variables.r11Folder, "Tmp");
				}
				else
				{
					name = patch.Mui;
					backupfolder = Path.Combine(Variables.r11Folder, "backup");
					tempfolder = Path.Combine(Variables.r11Folder, "Tmp");
				}

				if (string.IsNullOrWhiteSpace(name))
				{
					return;
				}

				if (type == PatchType.Troubleshooter)
				{
					if (!Directory.Exists(backupfolder))
					{
						Directory.CreateDirectory(backupfolder);
					}
					if (!Directory.Exists(tempfolder))
					{
						Directory.CreateDirectory(tempfolder);
					}
				}
				if (!File.Exists(Path.Combine(backupfolder, name)))
				{
					//File.Copy(file, Path.Combine(backupfolder, name));
					File.Copy(file, Path.Combine(tempfolder, name), true);
				}

				string filename = name + ".res";
				string masks = patch.mask;
				string filepath;
				if (type == PatchType.Troubleshooter)
				{
					filepath = Path.Combine(Variables.r11Files, "Diag");
				}
				else
				{
					filepath = Variables.r11Files;
				}

				if (patch.mask.Contains("|"))
				{
					if (!String.IsNullOrWhiteSpace(patch.Ignore) && ((!String.IsNullOrWhiteSpace(patch.MinVersion) && Environment.OSVersion.Version.Build >= Int32.Parse(patch.MinVersion)) || (!String.IsNullOrWhiteSpace(patch.MaxVersion) && Environment.OSVersion.Version.Build <= Int32.Parse(patch.MaxVersion))))
					{
						masks = masks.Replace(patch.Ignore, "");
					}
					string[] str = masks.Split('|');
					for (int i = 0; i < str.Length; i++)
					{
						if (type == PatchType.x86)
						{
							filename = Path.GetFileNameWithoutExtension(name).Remove(Path.GetFileNameWithoutExtension(name).Length - 2, 2) + Path.GetExtension(name) + ".res";
						}
						if (type != PatchType.Mui)
						{
							Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
							" -open " + Path.Combine(tempfolder, name) +
							" -save " + Path.Combine(tempfolder, name) +
							" -action " + "delete" +
							" -mask " + str[i], AppWinStyle.Hide, true);
						}
						Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
							" -open " + Path.Combine(tempfolder, name) +
							" -save " + Path.Combine(tempfolder, name) +
							" -action " + "addskip" +
							" -resource " + Path.Combine(filepath, filename) +
							" -mask " + str[i], AppWinStyle.Hide, true);
					}
				}
				else
				{
					if (!String.IsNullOrWhiteSpace(patch.Ignore) && ((!String.IsNullOrWhiteSpace(patch.MinVersion) && Environment.OSVersion.Version.Build >= Int32.Parse(patch.MinVersion)) || (!String.IsNullOrWhiteSpace(patch.MaxVersion) && Environment.OSVersion.Version.Build <= Int32.Parse(patch.MaxVersion))))
					{
						masks = masks.Replace(patch.Ignore, "");
					}
					if (type == PatchType.x86)
					{
						filename = Path.GetFileNameWithoutExtension(name).Remove(Path.GetFileNameWithoutExtension(name).Length - 2, 2) + Path.GetExtension(name) + ".res";
					}
					if (type != PatchType.Mui)
					{
						Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
							" -open " + Path.Combine(tempfolder, name) +
							" -save " + Path.Combine(tempfolder, name) +
							" -action " + "delete" +
							" -mask " + masks, AppWinStyle.Hide, true);
					}
					Interaction.Shell(Path.Combine(Variables.r11Folder, "ResourceHacker.exe") +
							" -open " + Path.Combine(tempfolder, name) +
							" -save " + Path.Combine(tempfolder, name) +
							" -action " + "addskip" +
							" -resource " + Path.Combine(filepath, filename) +
							" -mask " + masks, AppWinStyle.Hide, true);
				}
			}
		}
		private void MatchAndApplyRule(PatchesPatch patch)
		{
			if (patch.HardlinkTarget.Contains("%sys32%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%sys32%", Variables.sys32Folder);
				Patch(newhardlink, patch, PatchType.General);
			}
			else if (patch.HardlinkTarget.Contains("%lang%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
				Patch(newhardlink, patch, PatchType.Mui);
			}
			else if (patch.HardlinkTarget.Contains("%en-US%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
				Patch(newhardlink, patch, PatchType.Mui);
			}
			else if (patch.HardlinkTarget.Contains("%windirLang%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%windirLang%", Path.Combine(Variables.windir, CultureInfo.CurrentUICulture.Name));
				Patch(newhardlink, patch, PatchType.Mui);
			}
			else if (patch.HardlinkTarget.Contains("%windirEn-US%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%windirEn-US%", Path.Combine(Variables.windir, "en-US"));
				Patch(newhardlink, patch, PatchType.Mui);
			}
			else if (patch.HardlinkTarget.Contains("mun"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%sysresdir%", Variables.sysresdir);
				Patch(newhardlink, patch, PatchType.General);
			}
			else if (patch.HardlinkTarget.Contains("%branding%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%branding%", Variables.brandingFolder);
				Patch(newhardlink, patch, PatchType.General);
			}
			else if (patch.HardlinkTarget.Contains("%prog%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%prog%", Variables.progfiles);
				Patch(newhardlink, patch, PatchType.General);
			}
			else if (patch.HardlinkTarget.Contains("%diag%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%diag%", Variables.diag);
				Patch(newhardlink, patch, PatchType.Troubleshooter);
			}
			else if (patch.HardlinkTarget.Contains("%windir%"))
			{
				newhardlink = patch.HardlinkTarget.Replace(@"%windir%", Variables.windir);
				Patch(newhardlink, patch, PatchType.General);
			}
			if (!string.IsNullOrWhiteSpace(patch.x86))
			{
				if (patch.HardlinkTarget.Contains("%sys32%"))
				{
					newhardlink = patch.HardlinkTarget.Replace(@"%sys32%", Variables.sysWOWFolder);
					Patch(newhardlink, patch, PatchType.x86);
				}
				else if (patch.HardlinkTarget.Contains("%prog%"))
				{
					newhardlink = patch.HardlinkTarget.Replace(@"%prog%", Variables.progfiles86);
					Patch(newhardlink, patch, PatchType.x86);
				}
			}
		}
		#endregion
	}
}
