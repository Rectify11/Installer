using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Win32UIDemo.Pages;

namespace Win32UIDemo
{
    public partial class Form1 : Form
    {
        private static WelcomePage WelcomePage = new WelcomePage();
        private static EulaPage EulaPage = new EulaPage();
        public Form1()
        {
            InitializeComponent();

            WelcomePage.InstallButton.Click += InstallButton_Click;
            WelcomePage.UninstallButton.Click += UninstallButton_Click;

            EulaPage.DenyButton.Click += DenyButton_Click;
            EulaPage.AcceptButton.Click += AcceptButton_Click;
            Navigate(WelcomePage);
        }

        #region EULA Page
        private void AcceptButton_Click(object? sender, EventArgs e)
        {
            var pg = new TaskDialogPage()
            {
                Icon = TaskDialogIcon.ShieldErrorRedBar,

                Text = "You have attempted to navigate to a missing page. This operation has been stopped as it could cause a buffer overrun attack to your computer. Press OK to go back to the application's configured welcome page. If this does not solve the issue, please contact the author of this application.",
                Heading = "Security Failure",
                Caption = "Windows Forms",
                Footnote = new TaskDialogFootnote()
                {
                    Text = "Build #11"
                },
                Expander = new TaskDialogExpander()
                {
                    Text = "Faulting Module Name: Win32UIDemo.exe"
                }
            };
            TaskDialog.ShowDialog(this, pg);
        }
        private void DenyButton_Click(object? sender, EventArgs e)
        {
            Navigate(WelcomePage);
        }
        #endregion
        #region Welcome Page
        private void InstallButton_Click(object? sender, EventArgs e)
        {
            Navigate(EulaPage);
        }
        private void UninstallButton_Click(object? sender, EventArgs e)
        {
            var pg = new TaskDialogPage()
            {
                Icon = TaskDialogIcon.ShieldErrorRedBar,

                Text = "Uninstall button has not yet been implemented in this Rectify11 Build",
                Heading = "Security Failure",
                Caption = "Rectify11 rounded edition",
                Footnote = new TaskDialogFootnote()
                {
                    Text = "Build #10"
                },
                Expander = new TaskDialogExpander()
                {
                    Text = "Faulting Module Name: Win32UIDemo.exe"
                }
            };
            TaskDialog.ShowDialog(this, pg);
        }
        #endregion
        #region Navigation
        private void Navigate(WizardPage page)
        {
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(page);

            lblTopText.Text = page.WizardTopText;
        }
        #endregion

        private void Form1_Shown(object sender, EventArgs e)
        {
            RegistryKey? registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            var buildNumber = uint.Parse(registryKey.GetValue("CurrentBuild").ToString());

            SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
            DarkMode.AllowDarkModeForWindow(this.Handle, true);

            //Enable dark title bar
            if (buildNumber < 18362)
            {
                SetPropW(this.Handle, "UseImmersiveDarkModeColors", new IntPtr(1));
            }
            else
            {
                WindowCompositionAttributeData d = new WindowCompositionAttributeData();
                d.Attribute = WindowCompositionAttribute.WCA_USEDARKMODECOLORS;
                int size = Marshal.SizeOf(typeof(bool));
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(true, ptr, false);

                d.Data = ptr;
                d.SizeOfData = size;
                SetWindowCompositionAttribute(this.Handle, ref d);
            }
            if (Marshal.GetLastWin32Error() != 0) { throw new Win32Exception(); }

            try
            {
                //mica

                if (buildNumber >= 22523)
                {
                    int micaValue = 0x02;
                    DwmSetWindowAttribute(this.Handle, WindowCompositionAttribute.DWMWA_SYSTEMBACKDROP_TYPE, ref micaValue, Marshal.SizeOf(typeof(int)));
                }

                else
                {
                    int trueValue = 0x01;
                    DwmSetWindowAttribute(this.Handle, WindowCompositionAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
                }

                MARGINS m = new MARGINS();
                m.cyTopHeight = this.Height - pnlBottom.Height;
                m.cyBottomHeight = pnlBottom.Height;
                DwmExtendFrameIntoClientArea(this.Handle, ref m);
            }
            catch
            {

            }

            try
            {
                var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
                var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
                DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            }
            catch
            {

            }
        }


        #region Win32

        [DllImport("uxtheme.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, WindowCompositionAttribute dwAttribute, ref int pvAttribute, int cbAttribute);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetPropW(IntPtr hwnd, string prop, IntPtr value);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }
        public enum WindowCompositionAttribute
        {
            // ...
            WCA_ACCENT_POLICY = 19,
            WCA_USEDARKMODECOLORS = 26,
            DWMWA_MICA_EFFECT = 1029,
            DWMWA_SYSTEMBACKDROP_TYPE = 38
            // ...
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;      // width of left border that retains its size
            public int cxRightWidth;     // width of right border that retains its size
            public int cyTopHeight;      // height of top border that retains its size
            public int cyBottomHeight;   // height of bottom border that retains its size
        };

        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        [DllImport("DwmApi.dll")]
        static extern int DwmExtendFrameIntoClientArea(
         IntPtr hwnd,
         ref MARGINS pMarInset);
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}