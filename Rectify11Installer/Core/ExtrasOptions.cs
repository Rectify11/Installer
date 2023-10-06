using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
    class ExtrasOptions
    {
        #region Options
        // extra options
        private static List<Tuple<string, string>> options = new()
        {
            new("shellNode", Strings.Rectify11.installShell),
            new("gadgetsNode", Strings.Rectify11.installGadgets),
            new("asdfNode", Strings.Rectify11.installASDF),
            new("wallpapersNode", Strings.Rectify11.installWallpapers),
            new("useravNode", Strings.Rectify11.installUserAV)
        };
        #endregion
        #region Public Methods
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
        #endregion
    }
}
