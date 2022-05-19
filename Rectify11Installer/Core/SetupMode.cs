using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer.Core
{
    public static class SetupMode
    {
        public static void Enter()
        {
            RegistryKey? sys = Registry.LocalMachine.OpenSubKey("SYSTEM", true);
            if (sys == null)
            {
                throw new Exception("HKEY_LOCAL_MACHINE/SYSTEM is null");
            }

            RegistryKey? setup = sys.OpenSubKey("Setup", true);
            if (setup == null)
            {
                throw new Exception("HKEY_LOCAL_MACHINE/SYSTEM/Setup is null");
            }

            //SetupType values
            //0=Do nothing, show login screen
            //1=Run CMDLine then REBOOT
            //2=Run CMDLine then show login screen

            setup.SetValue("SystemSetupInProgress", 1, RegistryValueKind.DWord);
            setup.SetValue("SetupType", 1, RegistryValueKind.DWord); //reboot
            setup.SetValue("SetupPhase", 1, RegistryValueKind.DWord); //?

            var path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            setup.SetValue("CmdLine", $"\"{path}\" /setup", RegistryValueKind.String);
            setup.Close();
        }

        public static void Exit()
        {
            RegistryKey? sys = Registry.LocalMachine.OpenSubKey("SYSTEM", true);
            if (sys == null)
            {
                throw new Exception("HKEY_LOCAL_MACHINE/SYSTEM is null");
            }

            RegistryKey? setup = sys.OpenSubKey("Setup", true);
            if (setup == null)
            {
                throw new Exception("HKEY_LOCAL_MACHINE/SYSTEM/Setup is null");
            }

            //SetupType values
            //0=Do nothing, show login screen
            //1=Run CMDLine then REBOOT
            //2=Run CMDLine then show login screen

            setup.SetValue("SystemSetupInProgress", 0, RegistryValueKind.DWord);
            setup.SetValue("SetupType", 0, RegistryValueKind.DWord); //reboot
            setup.SetValue("SetupPhase", 0, RegistryValueKind.DWord); //?

            setup.SetValue("CmdLine", "", RegistryValueKind.String);
            setup.Close();
        }
    }
}
