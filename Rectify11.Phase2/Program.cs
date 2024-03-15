using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using static Rectify11.Phase2.Helper;

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

                Directory.CreateDirectory(backupDir);
                Directory.CreateDirectory(backupDiagDir);
                Directory.CreateDirectory(tempDiagDir);

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

                // copy necessary files
                SafeFileCopy("iconres.dll");
                ImportReg(Path.Combine(Variables.r11Files, "icons.reg"));
                SafeFileCopy("duires.dll");
                SafeFileCopy("ImmersiveFontHandler.dll");
                SafeFileCopy("twinuifonts.dll");
	            SafeFileCopyWOW64("iconres.dll");
                SafeFileCopyWOW64("duires.dll");
                SafeFileCopyWOW64("ImmersiveFontHandler.dll");
                SafeFileCopyWOW64("twinuifonts.dll");
                InstallFonts();

                r11Reg?.Close();
                if (pendingFiles == null) return;

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

                // help
                for (int k = 0; k < pendingFiles.Length; k++)
                {
                    if (pendingFiles[k].Contains("mmc.exe"))
                    {
                        Directory.CreateDirectory(Path.Combine(backupDir, "msc"));
                        if (CultureInfo.CurrentUICulture.Name != "en-US")
                        {
                            Directory.CreateDirectory(Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name));
                        }
                        Directory.CreateDirectory(Path.Combine(backupDir, "msc", "en-US"));
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
                                    else SafeFileDeletion(usaMsc[i]);

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
                                    else SafeFileDeletion(sysMsc[i]);

                                    File.Copy(r11Msc[j], sysMsc[i], true);
                                }
                            }
                        }
                        try
                        {
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
                                            else SafeFileDeletion(langMsc[i]);

                                            File.Copy(r11LangMsc[j], langMsc[i], true);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error patching language related msc files");
                        }
                    }
                }
                Directory.Delete(Path.Combine(Variables.r11Folder, "Tmp"), true);
                Console.WriteLine("");
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
            }
            else if (args[0] == "/uninstall")
            {
                var patches = PatchesParser.GetAll();
                var backup = Path.Combine(Variables.r11Folder, "Backup");
                var backupFiles = Directory.GetFiles(backup, "*", SearchOption.TopDirectoryOnly);

                string[] backupDiagDir = new string[] { };
                if (Directory.Exists(Path.Combine(backup, "Diag")))
                {
                    backupDiagDir = Directory.GetFiles(Path.Combine(backup, "Diag"), "*", SearchOption.TopDirectoryOnly);
                }
                var r11Reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", true).OpenSubKey("Rectify11", false);
                if (r11Reg != null)
                    uninstallFiles = (string[])r11Reg.GetValue("UninstallFiles");

                if (uninstallFiles == null) return;
                string lastfile = "";
                for (int k = 0; k < uninstallFiles.Length; k++)
                {
                    for (int j = 0; j < patches.Items.Length; j++)
                    {
                        for (int i = 0; i < backupFiles.Length; i++)
                        {
                            if (backupFiles[i].Contains(uninstallFiles[k])
                                && patches.Items[j].Mui.Contains(uninstallFiles[k]))
                            {
                                if (lastfile != uninstallFiles[k])
                                {
                                    string backupPath = backupFiles[i];
                                    string finalPath = FixString(patches.Items[j].HardlinkTarget, false);
                                    Console.WriteLine("Backup: " + backupPath);
                                    Console.WriteLine("Final: " + finalPath);
                                    SafeFileMove(backupPath, finalPath);
                                    lastfile = uninstallFiles[k];
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(patches.Items[j].x86))
                            {
                                if (Path.GetFileName(backupFiles[i]).Contains(Path.GetFileNameWithoutExtension(patches.Items[j].Mui) + "86" + Path.GetExtension(patches.Items[j].Mui))
                                    && uninstallFiles[k].Contains(patches.Items[j].Mui))
                                {
                                    Console.WriteLine("\n==x86==");
                                    string backupPath = backupFiles[i];
                                    string finalPath = FixString(patches.Items[j].HardlinkTarget, true);
                                    Console.WriteLine("Backup: " + backupPath);
                                    Console.WriteLine("Final: " + finalPath);
                                    SafeFileMove(backupPath, finalPath);
                                }
                            }
                        }
                        for (int i = 0; i < backupDiagDir.Length; i++)
                        {
                            if (Path.GetFileNameWithoutExtension(backupDiagDir[i]).Replace("DiagPackage", "Troubleshooter: ").Contains(uninstallFiles[k])
                                && string.Equals(uninstallFiles[k], patches.Items[j].Mui))
                            {
                                string finalPath = FixString(patches.Items[j].HardlinkTarget, false);
                                Console.WriteLine("Backup: " + backupDiagDir[i]);
                                Console.WriteLine("Final: " + finalPath + "\n");
                                SafeFileMove(backupDiagDir[i], finalPath);
                            }
                        }
                    }
                }
                for (int k = 0; k < uninstallFiles.Length; k++)
                {
					if (uninstallFiles[k].Contains("mmc.exe"))
                    {
						foreach (var process in Process.GetProcessesByName("mmc"))
                        {
                            process.Kill();
                        }
                        var sys32Msc = new string[] { };
                        if (Directory.Exists(Path.Combine(backup, "msc")))
                        {
                            sys32Msc = Directory.GetFiles(Path.Combine(backup, "msc"), "*.msc", SearchOption.TopDirectoryOnly);
                        }
                        for (int i = 0; i < sys32Msc.Length; i++)
                        {
                            Console.WriteLine("Backup: " + sys32Msc[i]);
                            Console.WriteLine("Final: " + Path.Combine(Variables.sys32Folder, Path.GetFileName(sys32Msc[i])));
                            File.Copy(sys32Msc[i], Path.Combine(Variables.sys32Folder, Path.GetFileName(sys32Msc[i])), true);
                            File.Delete(sys32Msc[i]);
                        }
                        var mscLang = new string[] { };
                        if (Directory.Exists(Path.Combine(backup, "msc")))
                        {
                            mscLang = Directory.GetDirectories(Path.Combine(backup, "msc"));
                        }
                        for (int i = 0; i < mscLang.Length; i++)
                        {
                            var files = Directory.GetFiles(mscLang[i], "*.msc", SearchOption.TopDirectoryOnly);
                            for (int j = 0; j < files.Length; j++)
                            {
                                Console.WriteLine("Backup: " + files[j]);
                                Console.WriteLine("Final: " + Path.Combine(Variables.sys32Folder, new DirectoryInfo(mscLang[i]).Name, Path.GetFileName(files[j])));
                                File.Copy(files[j], Path.Combine(Variables.sys32Folder, new DirectoryInfo(mscLang[i]).Name, Path.GetFileName(files[j])), true);
                                File.Delete(files[j]);
                            }
                        }
                    }
                }
                if (Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
                {
                    var dirs = Directory.GetDirectories(Path.Combine(Variables.r11Folder, "Backup"));
                    for (int i = 0; i < dirs.Length; i++)
                    {
                        var dirsi = Directory.GetDirectories(dirs[i]);
                        // msc
                        if (dirsi.Length > 0)
                        {
                            for (int j = 0; j < dirsi.Length; j++)
                            {
                                if (Directory.GetFiles(dirsi[j]).Length == 0)
                                {
                                    Directory.Delete(dirsi[j], true);
                                }
                            }
                            if (Directory.GetDirectories(dirs[i]).Length == 0 && Directory.GetFiles(dirs[i]).Length == 0)
                            {
                                Directory.Delete(dirs[i], true);
                            }
                        }
                        else
                        {
                            if (Directory.GetFiles(dirs[i]).Length == 0 && Directory.GetDirectories(dirs[i]).Length == 0)
                            {
                                Directory.Delete(dirs[i], true);
                            }
                        }
                    }
                    if (Directory.GetFiles(Path.Combine(Variables.r11Folder, "Backup")).Length == 0 && Directory.GetFiles(Path.Combine(Variables.r11Folder, "Backup")).Length == 0)
                    {
                        Directory.Delete(Path.Combine(Variables.r11Folder, "Backup"), true);
                    }
                }

                SafeFileDeletion(Path.Combine(Variables.sys32Folder,"iconres.dll"));
                SafeFileDeletion(Path.Combine(Variables.sys32Folder, "duires.dll"));
                SafeFileDeletion(Path.Combine(Variables.sys32Folder, "ImmersiveFontHandler.dll"));
                SafeFileDeletion(Path.Combine(Variables.sys32Folder, "twinuifonts.dll"));
				SafeFileDeletion(Path.Combine(Variables.sysWOWFolder,"iconres.dll"));
                SafeFileDeletion(Path.Combine(Variables.sysWOWFolder, "duires.dll"));
                SafeFileDeletion(Path.Combine(Variables.sysWOWFolder, "ImmersiveFontHandler.dll"));
                SafeFileDeletion(Path.Combine(Variables.sysWOWFolder, "twinuifonts.dll"));

                Console.WriteLine("");
                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
            }
            Environment.Exit(0);
        }

        private static void InstallFonts()
        {
            try
            {
                // aaaaaaaaaaaa 
                var MarlettDest = Path.Combine(Variables.windir, "Fonts", "marlett.ttf");
                var MarlettBackupDest = Path.Combine(Variables.windir, "Fonts", "marlett.ttf.backup");
                var marlett = Path.Combine(Variables.r11Files, "marlett.ttf");

                if (!File.Exists(MarlettBackupDest))
                {
                    File.Move(MarlettDest, MarlettBackupDest);
                }
                SafeFileCopy(marlett, MarlettDest);

                var BackIconsDest = Path.Combine(Variables.windir, "Fonts", "BackIcons.ttf");
                var backicons = Path.Combine(Variables.r11Files, "BackIcons.ttf");
                SafeFileCopy(backicons, BackIconsDest);
                ImportReg(Path.Combine(Variables.r11Files, "backicons.reg"));

                if (Environment.OSVersion.Version.Build <= 21996)
                {
                    var SegoeIconsDest = Path.Combine(Variables.windir, "Fonts", "SegoeIcons.ttf");
                    var segoeicons = Path.Combine(Variables.r11Files, "SegoeIcons.ttf");
                    SafeFileCopy(segoeicons, SegoeIconsDest);
                    ImportReg(Path.Combine(Variables.r11Files, "segoeicons.reg"));

                    var SegoeUIVarDest = Path.Combine(Variables.windir, "Fonts", "SegUIVar.ttf");
                    var segoeuivar = Path.Combine(Variables.r11Files, "SegUIVar.ttf");
                    SafeFileCopy(segoeuivar, SegoeUIVarDest);
                    ImportReg(Path.Combine(Variables.r11Files, "segoeuivar.reg"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
        }

        #region P/Invoke
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);
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
        #endregion
    }
}
