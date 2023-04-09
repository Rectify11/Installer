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
            new("shellNode", "Nilesoft Shell"),
            new("gadgetsNode", "Rectified Gadgets"),
            // new("winverNode", "WinverUWP"),
			// new("rectpadNode", "RectifyPad"),
			// new("epNode", "ExplorerPatcher"), 
			new("asdfNode", "Enable Accent Color Colorizing"),
			new("wallpapersNode", Rectify11Installer.Strings.Rectify11.optionWallpaper)
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
	}
}
