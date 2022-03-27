using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win32UIDemo
{
    public class DarkMode
    {
        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        public static extern int fnAllowDarkModeForApp(PreferredAppMode allow);
        [DllImport("uxtheme.dll", EntryPoint = "#133")]
        public static extern int AllowDarkModeForWindow(IntPtr handle, bool allow);

        public enum PreferredAppMode
        {
            Default,
            AllowDark,
            ForceDark,
            ForceLight,
            Max
        }
    }
}
