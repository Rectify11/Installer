using KPreisser.UI;
using System.Text;

namespace Rectify11Installer.Core
{
    public class Helper
    {
        public static bool CheckIfUpdatesPending()
        {
            WUApiLib.ISystemInformation systemInfo = new WUApiLib.SystemInformation();

            if (systemInfo.RebootRequired)
            {
                TaskDialog.Show(text: "You cannot install Rectify11 as Windows Updates are pending.", 
                    instruction: "Compatibility Error", 
                    title: "Rectify11 Setup", 
                    buttons: TaskDialogButtons.OK, 
                    icon: TaskDialogStandardIcon.SecurityErrorRedBar);

                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool UpdateIRectify11()
        {
            if (InstallOptions.iconsList.Contains("themeNode"))
                InstallOptions.InstallThemes = true;
            else
                InstallOptions.InstallThemes = false;
            if (InstallOptions.iconsList.Contains("epNode"))
                InstallOptions.InstallEP = true;
            else
                InstallOptions.InstallEP = false;
            if (InstallOptions.iconsList.Contains("winverNode"))
                InstallOptions.InstallWinver = true;
            else
                InstallOptions.InstallWinver = false;
            if (InstallOptions.iconsList.Contains("shellNode"))
                InstallOptions.InstallShell = true;
            else
                InstallOptions.InstallShell = false;
            if (InstallOptions.iconsList.Contains("wallpapersNode"))
                InstallOptions.InstallWallpaper = true;
            else
                InstallOptions.InstallWallpaper = false;
            return true;
        }
        public static bool FinalizeIRectify11()
        {
            if (InstallOptions.iconsList.Contains("themeNode"))
                InstallOptions.iconsList.Remove("themeNode");
            if (InstallOptions.iconsList.Contains("epNode"))
                InstallOptions.iconsList.Remove("epNode");
            if (InstallOptions.iconsList.Contains("winverNode"))
                InstallOptions.iconsList.Remove("winverNode");
            if (InstallOptions.iconsList.Contains("shellNode"))
                InstallOptions.iconsList.Remove("shellNode");
            if (InstallOptions.iconsList.Contains("wallpapersNode"))
                InstallOptions.iconsList.Remove("wallpapersNode");
            return true;
        }
        public static StringBuilder FinalText()
        {
            StringBuilder ok = new StringBuilder();
            ok.AppendLine();
            ok.AppendLine();
            if (InstallOptions.InstallThemes)
                ok.AppendLine(Strings.Rectify11.installThemes);
            if (InstallOptions.InstallEP)
                ok.AppendLine(Strings.Rectify11.installEP);
            if (InstallOptions.InstallWinver)
                ok.AppendLine(Strings.Rectify11.installWinver);
            if (InstallOptions.InstallShell)
                ok.AppendLine(Strings.Rectify11.installShell);
            if (InstallOptions.InstallWallpaper)
                ok.AppendLine(Strings.Rectify11.installWallpapers);
            return ok;
        }
    }
}
