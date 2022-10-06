using Microsoft.Win32;

namespace Rectify11Installer.Core
{
    public class InstallStatus
    {
        public static bool IsRectify11Installed
        {
            get
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11");
                if (key == null)
                    return false;

                var b = key.GetValue("IsInstalled");
                if (b == null)
                {
                    return false;
                }


                var value = (int)b;
                return value == 1;
            }
            set
            {
                RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11");
                key.SetValue("IsInstalled", value ? 1 : 0);
            }
        }
        public static string InstalledVersion
        {
            get
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11");
                if (key == null)
                    return null;

                var b = key.GetValue("Version");
                if (b == null)
                {
                    return null;
                }


                var value = (string)b;
                return value;
            }
            set
            {
                RegistryKey key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Rectify11");
                key.SetValue("Version", value);
            }
        }
    }
}
