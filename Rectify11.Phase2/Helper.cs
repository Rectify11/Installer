using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using static Rectify11.Phase2.Program;

namespace Rectify11.Phase2
{
    internal class Helper
    {
        #region Variables
        public enum MoveType
        {
            General = 0,
            x86,
            Trouble
        }
        #endregion
        public static bool SafeFileDeletion(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch
                    {
                        string name = Path.GetRandomFileName();
                        string tmpPath = Path.Combine(Path.GetTempPath(), name);
                        File.Move(path, tmpPath);
                        MoveFileEx(tmpPath, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    return true;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SafeFileCopy(string file)
        {
            // whatever, its only for a few cases
            try
            {
                if (!SafeFileDeletion(Path.Combine(Variables.sys32Folder, file))) return false;
                File.Copy(Path.Combine(Variables.r11Files, file), Path.Combine(Variables.sys32Folder, file), true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SafeFileCopy(string src, string dest)
        {
            try
            {
                if (!SafeFileDeletion(dest)) return false;
                File.Copy(src, dest, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
		public static bool SafeFileCopyWOW64(string file)
        {
            // whatever, its only for a few cases
            try
            {
                if (!SafeFileDeletion(Path.Combine(Variables.sysWOWFolder, file))) return false;
                File.Copy(Path.Combine(Variables.r11Files, file), Path.Combine(Variables.sysWOWFolder, file), true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SafeFileCopyWOW64(string src, string dest)
        {
            try
            {
                if (!SafeFileDeletion(dest)) return false;
                File.Copy(src, dest, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
		public static bool SafeFileMove(string src, string dest)
		{
			try
			{
				if (!SafeFileDeletion(dest)) return false;
				File.Move(src, dest);
				return true;
			}
			catch
			{
				return false;
			}
		}
		public static void ImportReg(string path)
        {
            try
            {
                Interaction.Shell(Path.Combine(Variables.sys32Folder, "reg.exe") + " import " + path, AppWinStyle.Hide, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Path.GetFileName(path) + " failed.", ex);
            }
        }
        public static string FixString(string path, bool x86)
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
        public static void MoveFile(string newval, string file, MoveType type, string name)
        {
            try
            {
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
                if (string.IsNullOrWhiteSpace(finalpath)) return;
                if (!File.Exists(finalpath))
                {
                    Console.WriteLine(finalpath);
                    File.Move(newval, finalpath);
                }
                else if (File.Exists(finalpath))
                {
                    bool wu = false;
                    int? value = (int?)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11")?.GetValue("WindowsUpdate");
                    if (value == 1) wu = true;
                    if (!wu)
                    {
                        finalpath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                        Console.WriteLine(finalpath);
                        MoveFileEx(finalpath, null, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);
                    }
                    else
                    {
                        Console.WriteLine("WU: " + finalpath);
                        SafeFileDeletion(finalpath);
                    }
                    File.Move(newval, finalpath);
                }
                File.Copy(file, newval, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Path.GetFileName(newval) + " failed." + ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
