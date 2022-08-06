using KPreisser.UI;

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
    }
}
