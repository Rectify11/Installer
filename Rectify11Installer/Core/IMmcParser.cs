using Rectify11.Phase2;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace MMC
{
	internal class IMmcParser
	{
		#region Private Methods
		private static MMC_ConsoleFile GetXml(string file)
		{
			XmlSerializer ser = new(typeof(MMC_ConsoleFile));
			MMC_ConsoleFile patches;
			using (var reader = new StringReader(file))
			{
				patches = (MMC_ConsoleFile)ser.Deserialize(reader);
			}
			return patches;
		}
		private static bool BackupCopyFiles()
		{
			var backupDir = Path.Combine(Variables.r11Folder, "Backup");
			var tempDir = Path.Combine(Variables.r11Folder, "Tmp");
			if (!Directory.Exists(Path.Combine(backupDir, "msc")))
			{
				Directory.CreateDirectory(Path.Combine(backupDir, "msc"));
				Directory.CreateDirectory(Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name));
				Directory.CreateDirectory(Path.Combine(backupDir, "msc", "en-US"));
			}
			if (!Directory.Exists(Path.Combine(tempDir, "msc")))
			{
				Directory.CreateDirectory(Path.Combine(tempDir, "msc"));
				Directory.CreateDirectory(Path.Combine(tempDir, "msc", CultureInfo.CurrentUICulture.Name));
				Directory.CreateDirectory(Path.Combine(tempDir, "msc", "en-US"));
			}
			var langFolder = Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name);
			var usaFolder = Path.Combine(Variables.sys32Folder, "en-US");
			List<string> langMsc = new(Directory.GetFiles(langFolder, "*.msc", SearchOption.TopDirectoryOnly));
			List<string> usaMsc = new(Directory.GetFiles(usaFolder, "*.msc", SearchOption.TopDirectoryOnly));
			List<string> sysMsc = new(Directory.GetFiles(Variables.sys32Folder, "*.msc", SearchOption.TopDirectoryOnly));
			List<string> r11Msc = new(Directory.GetFiles(Path.Combine(Variables.r11Files, "mmc"), "*.msc", SearchOption.TopDirectoryOnly));
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
							Debug.WriteLine(langMsc[i]);
							File.Copy(langMsc[i], Path.Combine(tempDir, "msc", CultureInfo.CurrentUICulture.Name, Path.GetFileName(langMsc[i])));
							if (!File.Exists(Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name, Path.GetFileName(langMsc[i]))))
							{
								File.Move(langMsc[i], Path.Combine(backupDir, "msc", CultureInfo.CurrentUICulture.Name, Path.GetFileName(langMsc[i])));
							}
						}
					}
				}
				for (int i = 0; i < usaMsc.Count; i++)
				{
					if (Path.GetFileName(usaMsc[i]) == Path.GetFileName(r11Msc[j]))
					{
						Debug.WriteLine(usaMsc[i]);
						File.Copy(usaMsc[i], Path.Combine(tempDir, "msc", "en-US", Path.GetFileName(usaMsc[i])));
						if (!File.Exists(Path.Combine(backupDir, "msc", "en-US", Path.GetFileName(usaMsc[i]))))
						{
							File.Move(usaMsc[i], Path.Combine(backupDir, "msc", "en-US", Path.GetFileName(usaMsc[i])));
						}
					}
				}
				for (int i = 0; i < sysMsc.Count; i++)
				{
					if (Path.GetFileName(sysMsc[i]) == Path.GetFileName(r11Msc[j]))
					{
						Debug.WriteLine(sysMsc[i]);
						File.Copy(sysMsc[i], Path.Combine(tempDir, "msc", Path.GetFileName(sysMsc[i])));
						if (!File.Exists(Path.Combine(backupDir, "msc", Path.GetFileName(sysMsc[i]))))
						{
							File.Move(sysMsc[i], Path.Combine(backupDir, "msc", Path.GetFileName(sysMsc[i])));
						}
					}
				}
			}
			return true;
		}
		#endregion
	}
}
