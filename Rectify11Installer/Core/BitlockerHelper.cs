using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace Rectify11Installer.Core
{
    class BitlockerHelper
    {
        public bool GetBitlocker()
        {
            IShellProperty prop = ShellObject.FromParsingName("C:").Properties.GetProperty("System.Volume.BitLockerProtection");
            int? bitLockerProtectionStatus = (prop as ShellProperty<int?>).Value;

            if (bitLockerProtectionStatus.HasValue && (bitLockerProtectionStatus == 1 || bitLockerProtectionStatus == 3 || bitLockerProtectionStatus == 5))
                return true;
            else
                return false;
        }
    }
}
