using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Core
{
    public static class InstallStatus
    {
        public static bool IsRectify11Installed
        {
            get
            {
                RegistryKey? key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Rectify11");
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


    }
}
