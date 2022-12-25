using Microsoft.VisualBasic;
using Microsoft.Win32;
using MMC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using vbAccelerator.Components.Shell;

namespace Rectify11Installer.Core
{
	public class Installer
	{
		#region Variables
		private string newhardlink;
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
		#endregion
		#region Public Methods
		public async Task<bool> Install(frmWizard frm)
		{
			await Task.Run(() => WriteFiles(false, false));
			await Task.Run(() => CreateDirs());

			// backup
			File.Copy(Assembly.GetExecutingAssembly().Location, Path.Combine(Variables.r11Folder, "Uninstall.exe"), true);

			// always extract files, delete if folder exists
			frm.InstallerProgress = "Extracting files...";
			if (Directory.Exists(Path.Combine(Variables.r11Folder, "files")))
			{
				Directory.Delete(Path.Combine(Variables.r11Folder, "files"));
			}
			await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
					" x -o" + Path.Combine(Variables.r11Folder, "files") +
					" " + Path.Combine(Variables.r11Folder, "files.7z"), AppWinStyle.Hide, true));

			frm.InstallerProgress = "Installing runtimes";
			await Task.Run(() => InstallRuntimes());

			// Icons
			if (InstallOptions.iconsList.Count > 0)
			{
				// Get all patches
				Patches patches = PatchesParser.GetAll();
				PatchesPatch[] patch = patches.Items;
				decimal progress = 0;
				List<string> fileList = new();
				List<string> x86List = new();
				for (int i = 0; i < patch.Length; i++)
				{
					for (int j = 0; j < InstallOptions.iconsList.Count; j++)
					{
						if (patch[i].Mui.Contains(InstallOptions.iconsList[j]))
						{
							decimal number = Math.Round((progress / InstallOptions.iconsList.Count) * 100m);
							frm.InstallerProgress = "Patching " + patch[i].Mui + " (" + number + "%)";
							fileList.Add(patch[i].HardlinkTarget);
							if (!string.IsNullOrWhiteSpace(patch[i].x86))
							{
								x86List.Add(patch[i].HardlinkTarget);
							}

							await Task.Run(() => MatchAndApplyRule(patch[i]));
							progress++;
						}
					}
				}
				await Task.Run(() => WritePendingFiles(fileList, x86List));

				await Task.Run(() => WriteFiles(true, false));

				frm.InstallerProgress = "Replacing files";

				// runs only if SSText3D.scr is selected
				if (InstallOptions.iconsList.Contains("SSText3D.scr"))
				{
					await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "screensaver.reg"), AppWinStyle.Hide, true));
				}

				// runs only if any one of mmcbase.dll.mun, mmc.exe.mui and mmcndmgr.dll.mun is selected
				if (InstallOptions.iconsList.Contains("mmcbase.dll.mun")
					|| InstallOptions.iconsList.Contains("mmc.exe.mui")
					|| InstallOptions.iconsList.Contains("mmcndmgr.dll.mun"))
				{
					await Task.Run(() => IMmcHelper.PatchAll());
				}
				if (InstallOptions.iconsList.Contains("odbcad32.exe"))
				{
					await Task.Run(() => FixOdbc());
				}
				// phase 2
				await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "aRun.exe") + " /EXEFilename " + '"' + Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe") + '"' + " /RunAs 8 /Run", AppWinStyle.NormalFocus, true));

				// reg files for various file extensions
				await Task.Run(() => Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Files, "icons.reg"), AppWinStyle.Hide, true));

				await Task.Run(() => WaitForPhase2());
			}

			// theme
			if (InstallOptions.InstallThemes)
			{
				frm.InstallerProgress = "Installing Themes";
				await Task.Run(() => WriteFiles(false, true));

				if (Directory.Exists(Path.Combine(Variables.r11Folder, "themes")))
				{
					Directory.Delete(Path.Combine(Variables.r11Folder, "themes"));
				}
				await Task.Run(() => Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
						" x -o" + Path.Combine(Variables.r11Folder, "themes") +
						" " + Path.Combine(Variables.r11Folder, "themes.7z"), AppWinStyle.Hide, true));

				await Task.Run(() => InstallThemes());
			}
			await Task.Run(() => AddToControlPanel());
			InstallStatus.IsRectify11Installed = true;
			RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", true);
			if (key != null)
			{
				key.SetValue("ResetIconCache", Path.Combine(Variables.sys32Folder, "ie4uinit.exe") + " -show", RegistryValueKind.String);
			}
			key.Close();
			// cleanup
			frm.InstallerProgress = "Cleaning up...";
			await Task.Run(() => Cleanup());
			return true;
		}
		#endregion
		#region Private Methods

		/// <summary>
		/// fixes 32-bit odbc shortcut icon
		/// </summary>
		public void FixOdbc()
		{
			string filepath = string.Empty;
			string filename = string.Empty;
			string[] files = Directory.GetFiles(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools");
			for (int i = 0; i< files.Length; i++)
			{
				if (Path.GetFileName(files[i]).Contains("ODBC"))
				{
					if (Path.GetFileName(files[i]).Contains("32"))
					{
						filename = Path.GetFileName(files[i]);
						File.Delete(files[i]);
					}
				}
			}
			using ShellLink shortcut = new();
			shortcut.Target = Path.Combine(Variables.sysWOWFolder, "odbcad32.exe");
			shortcut.WorkingDirectory = @"%windir%\system32";
			shortcut.IconPath = Path.Combine(Variables.sys32Folder, "odbcint.dll");
			shortcut.IconIndex = 0;
			shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
			shortcut.Save(Path.Combine(@"C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Administrative Tools", filename));
		}

		/// <summary>
		/// installs themes
		/// </summary>
		private void InstallThemes()
		{
			DirectoryInfo cursors = new(Path.Combine(Variables.r11Folder, "themes", "cursors"));
			DirectoryInfo[] curdir = cursors.GetDirectories("*", SearchOption.TopDirectoryOnly);
			DirectoryInfo themedir = new(Path.Combine(Variables.r11Folder, "themes", "themes"));
			DirectoryInfo[] msstyleDirList = themedir.GetDirectories("*", SearchOption.TopDirectoryOnly);
			FileInfo[] themefiles = themedir.GetFiles("*.theme");

			Directory.Move(Path.Combine(Variables.r11Folder, "themes", "wallpapers"), Path.Combine(Variables.windir, "web", "wallpaper", "Rectified"));
			File.Copy(Path.Combine(Variables.r11Folder, "themes", "ThemeTool.exe"), Path.Combine(Variables.windir, "ThemeTool.exe"), true);
			Interaction.Shell(Path.Combine(Variables.windir, "SecureUXHelper.exe") + " install", AppWinStyle.Hide, true);
			Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + Path.Combine(Variables.r11Folder, "themes", "Themes.reg"), AppWinStyle.Hide, true);

			for (int i = 0; i < curdir.Length; i++)
			{
				if (Directory.Exists(Path.Combine(Variables.windir, "cursors", curdir[i].Name)))
				{
					Directory.Delete(Path.Combine(Variables.windir, "cursors", curdir[i].Name), true);
				}
				Directory.Move(curdir[i].FullName, Path.Combine(Variables.windir, "cursors", curdir[i].Name));
			}
			for (int i = 0; i < themefiles.Length; i++)
			{
				File.Copy(themefiles[i].FullName, Path.Combine(Variables.windir, "Resources", "Themes", themefiles[i].Name), true);
			}
			for (int i = 0; i < msstyleDirList.Length; i++)
			{
				if(Directory.Exists(Path.Combine(Variables.windir, "Resources", "Themes", msstyleDirList[i].Name)))
				{
					Directory.Delete(Path.Combine(Variables.windir, "Resources", "Themes", msstyleDirList[i].Name));
				}
				Directory.Move(msstyleDirList[i].FullName, Path.Combine(Variables.windir, "Resources", "Themes", msstyleDirList[i].Name));
			}
			RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", true);
			if (key != null)
			{
				if (InstallOptions.ThemeLight)
				{
					Process.Start(Path.Combine(Variables.windir, "Resources", "Themes", "lightrectified.theme"));
					key.SetValue("ApplyTheme", Path.Combine(Variables.windir, "SecureUXHelper.exe") + " apply " + '"' + "Rectify11 light theme" + '"', RegistryValueKind.String);
				}
				else if (InstallOptions.ThemeDark)
				{
					Process.Start(Path.Combine(Variables.windir, "Resources", "Themes", "darkrectified.theme"));
					key.SetValue("ApplyTheme", Path.Combine(Variables.windir, "SecureUXHelper.exe") + " apply " + '"' + "Rectify11 dark theme" + '"', RegistryValueKind.String);
				}
				else
				{
					Process.Start(Path.Combine(Variables.windir, "Resources", "Themes", "black.theme"));
					key.SetValue("ApplyTheme", Path.Combine(Variables.windir, "SecureUXHelper.exe") + " apply " + '"' + "Rectify11 Dark Mica theme (Fixed Ribbon)" + '"', RegistryValueKind.String);
				}
			}
			key.Close();
		}

		/// <summary>
		/// waits for phase2 to finish
		/// </summary>
		/// <returns>true if phase2 finished</returns>
		private bool WaitForPhase2()
		{
			// waits for the temp folder to be deleted (used for knowing when phase2 will be finished)
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
			return true;
		}

		/// <summary>
		/// writes all the needed files
		/// </summary>
		/// <param name="icons">indicates whether icons only files are written</param>
		/// <param name="themes">indicates whether themes only files are written</param>
		private void WriteFiles(bool icons, bool themes)
		{
			if (icons)
			{
				if (!File.Exists(Path.Combine(Variables.r11Folder, "aRun.exe")))
				{
					File.WriteAllBytes(Path.Combine(Variables.r11Folder, "aRun.exe"), Properties.Resources.AdvancedRun);
				}
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "Rectify11.Phase2.exe"), Properties.Resources.Rectify11Phase2);
			}
			if (themes)
			{
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "themes.7z"), Properties.Resources.themes);
				if (Win32.NativeMethods.IsArm64())
				{
					File.WriteAllBytes(Path.Combine(Variables.windir, "SecureUXHelper.exe"), Properties.Resources.SecureUxHelper_arm64);
				}
				else
				{
					File.WriteAllBytes(Path.Combine(Variables.windir, "SecureUXHelper.exe"), Properties.Resources.SecureUxHelper_x64);
				}
			}
			if (!themes && !icons)
			{
				if (!File.Exists(Path.Combine(Variables.r11Folder, "7za.exe")))
				{
					File.WriteAllBytes(Path.Combine(Variables.r11Folder, "7za.exe"), Properties.Resources._7za);
				}
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "files.7z"), Properties.Resources.files7z);
				File.WriteAllBytes(Path.Combine(Variables.r11Folder, "extras.7z"), Properties.Resources.extras);

				if (!File.Exists(Path.Combine(Variables.r11Folder, "ResourceHacker.exe")))
				{
					File.WriteAllBytes(Path.Combine(Variables.r11Folder, "ResourceHacker.exe"), Properties.Resources.ResourceHacker);
				}
			}
		}

		/// <summary>
		/// creates backup and temp folder
		/// </summary>
		private void CreateDirs()
		{
			if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
			{
				Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup"));
			}

			if (Directory.Exists(Path.Combine(Variables.r11Folder, "Tmp")))
			{
				Directory.Delete(Path.Combine(Variables.r11Folder, "Tmp"), true);
			}
			Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Tmp"));
		}

		/// <summary>
		/// installs runtimes
		/// </summary>
		private void InstallRuntimes()
		{
			Interaction.Shell(Path.Combine(Variables.r11Folder, "7za.exe") +
		      " e -o" + Variables.r11Folder + " " + Path.Combine(Variables.r11Folder, "extras.7z") +
		      " vcredist.exe", AppWinStyle.Hide, true);
			Interaction.Shell(Path.Combine(Variables.r11Folder, "vcredist.exe") + " /install /quiet /norestart", AppWinStyle.NormalFocus, true);
		}

		/// <summary>
		/// sets required registry values for phase 2
		/// </summary>
		/// <param name="fileList">normal files list</param>
		/// <param name="x86List">32-bit files list</param>
		private void WritePendingFiles(List<string> fileList, List<string> x86List)
		{
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
		}

		/// <summary>
		/// Adds installer entry to control panel uninstall apps list
		/// </summary>
		/// <returns>true if writing to registry was successful, otherwise false</returns>
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
					r11key.SetValue("Build", Assembly.GetEntryAssembly().GetName().Version.Build.ToString(), RegistryValueKind.String);
					r11key.SetValue("Publisher", "The Rectify11 Team", RegistryValueKind.String);
					r11key.SetValue("URLInfoAbout", "https://rectify.vercel.app/", RegistryValueKind.String);
					key.Close();
					return true;
				}
				key.Close();
				return false;
			}
			key.Close();
			return false;
		}

		/// <summary>
		/// Patches a specific file
		/// </summary>
		/// <param name="file">The file to be patched</param>
		/// <param name="patch">Xml element containing all the info</param>
		/// <param name="type">The type of the file to be patched.</param>
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
					if (!string.IsNullOrWhiteSpace(patch.Ignore) && ((!string.IsNullOrWhiteSpace(patch.MinVersion) && Environment.OSVersion.Version.Build <= Int32.Parse(patch.MinVersion)) || (!string.IsNullOrWhiteSpace(patch.MaxVersion) && Environment.OSVersion.Version.Build >= Int32.Parse(patch.MaxVersion))))
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
					if (!string.IsNullOrWhiteSpace(patch.Ignore) && ((!string.IsNullOrWhiteSpace(patch.MinVersion) && Environment.OSVersion.Version.Build <= Int32.Parse(patch.MinVersion)) || (!string.IsNullOrWhiteSpace(patch.MaxVersion) && Environment.OSVersion.Version.Build >= Int32.Parse(patch.MaxVersion))))
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

		/// <summary>
		/// Replaces the path and patches the file accordingly.
		/// </summary>
		/// <param name="patch">Xml element containing all the info</param>
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

		/// <summary>
		/// cleans up files
		/// </summary>
		private void Cleanup()
		{
			if (Directory.Exists(Variables.r11Files))
			{
				Directory.Delete(Variables.r11Files, true);
			}
			if (File.Exists(Path.Combine(Variables.r11Folder, "files.7z")))
			{
				File.Delete(Path.Combine(Variables.r11Folder, "files.7z"));
			}
			if (File.Exists(Path.Combine(Variables.r11Folder, "extras.7z")))
			{
				File.Delete(Path.Combine(Variables.r11Folder, "extras.7z"));
			}
			if (File.Exists(Path.Combine(Variables.r11Folder, "vcredist.exe")))
			{
				File.Delete(Path.Combine(Variables.r11Folder, "vcredist.exe"));
			}
			if (File.Exists(Path.Combine(Variables.r11Folder, "newfiles.txt")))
			{
				File.Delete(Path.Combine(Variables.r11Folder, "newfiles.txt"));
			}
			if (Directory.Exists(Path.Combine(Variables.r11Folder, "themes")))
			{
				Directory.Delete(Path.Combine(Variables.r11Folder, "themes"), true);
			}
			if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras")))
			{
				Directory.Delete(Path.Combine(Variables.r11Folder, "extras"), true);
			}
			if (File.Exists(Path.Combine(Variables.r11Folder, "themes.7z")))
			{
				File.Delete(Path.Combine(Variables.r11Folder, "themes.7z"));
			}
		}
	}
	#endregion
}
