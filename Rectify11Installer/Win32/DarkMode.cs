using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rectify11Installer
{
    public class DarkMode
    {
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        internal static extern int fnAllowDarkModeForApp(PreferredAppMode allow);
        [DllImport("uxtheme.dll", EntryPoint = "#133")]
        internal static extern int AllowDarkModeForWindow(IntPtr handle, bool allow);

        internal enum PreferredAppMode
        {
            Default,
            AllowDark,
            ForceDark,
            ForceLight,
            Max
        }
    }
}
