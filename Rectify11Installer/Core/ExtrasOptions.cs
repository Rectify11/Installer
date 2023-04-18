using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
	class ExtrasOptions
	{
		// add extra options here ok
		private static List<Tuple<string, string>> options = new()
		{
			new("shellNode", "Enhanced context menus"),
			new("gadgetsNode", "Rectified Gadgets"),
			// new("winverNode", "WinverUWP"),
			// new("rectpadNode", "RectifyPad"),
			// new("epNode", "ExplorerPatcher"), 
			new("asdfNode", "Enable Accent Color Colorizing"),
			new("wallpapersNode", Rectify11Installer.Strings.Rectify11.optionWallpaper),
			new("useravNode", "Remastered user avatars"),
			new("soundNode","Remastered Logon, Logoff, and Shutdown sounds")
		};
		public static List<TreeNode> GetExtras()
		{
			List<TreeNode> lis = new();
			for (int i = 0; i < options.Count; i++)
			{
				lis.Add(new TreeNode { Name = options[i].Item1, Text = options[i].Item2 });
			}
			return lis;
		}
		public static bool UpdateIRectify11()
		{
			InstallOptions.InstallThemes = InstallOptions.iconsList.Contains("themeNode");
			InstallOptions.InstallEP = InstallOptions.iconsList.Contains("epNode");
			InstallOptions.InstallWinver = InstallOptions.iconsList.Contains("winverNode");
			InstallOptions.InstallShell = InstallOptions.iconsList.Contains("shellNode");
			InstallOptions.InstallGadgets = InstallOptions.iconsList.Contains("gadgetsNode");
			InstallOptions.InstallWallpaper = InstallOptions.iconsList.Contains("wallpapersNode");
			InstallOptions.InstallASDF = InstallOptions.iconsList.Contains("asdfNode");
			InstallOptions.userAvatars = InstallOptions.iconsList.Contains("useravNode");
			InstallOptions.InstallSounds = InstallOptions.iconsList.Contains("soundNode");
			return true;
		}
		public static bool FinalizeIRectify11()
		{
			InstallOptions.iconsList.Remove("themeNode");
			InstallOptions.iconsList.Remove("epNode");
			InstallOptions.iconsList.Remove("gadgetsNode");
			InstallOptions.iconsList.Remove("winverNode");
			InstallOptions.iconsList.Remove("shellNode");
			InstallOptions.iconsList.Remove("wallpapersNode");
			InstallOptions.iconsList.Remove("asdfNode");
			InstallOptions.iconsList.Remove("useravNode");
			InstallOptions.iconsList.Remove("soundNode");
			return true;
		}
	}
}
