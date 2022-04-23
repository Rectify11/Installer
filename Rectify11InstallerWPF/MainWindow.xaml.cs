using Rectify11InstallerWPF.Core;
using Rectify11InstallerWPF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rectify11InstallerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            lblVersion.Text = "Version: v2.9.0 (PRIVATE) WPF branch";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var buildNumber = Environment.OSVersion.Version.Build;

            if (Theme.IsUsingDarkMode)
                WPFUI.Appearance.Background.ApplyDarkMode(this);

            WPFUI.Appearance.Watcher.Watch(
         this,                           // Window class
         WPFUI.Appearance.BackgroundType.Mica, // Background type
         true                            // Whether to change accents automatically
       );

            //  var x = WPFUI.Appearance.Background.Apply(this, WPFUI.Appearance.BackgroundType.Mica);
            IntPtr handle = new WindowInteropHelper(Application.Current.MainWindow).Handle;

            try
            {
                //mica


                bool extend = Theme.IsUsingDarkMode;

                if (buildNumber >= 22523)
                {
                    int micaValue = 0x02;
                    _ = DwmSetWindowAttribute(handle, WindowCompositionAttribute.DWMWA_SYSTEMBACKDROP_TYPE, ref micaValue, Marshal.SizeOf(typeof(int)));
                }

                else
                {
                    int trueValue = 0x01;
                    _ = DwmSetWindowAttribute(handle, WindowCompositionAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
                }

                UpdateWindowFrame();
            }
            catch
            {

            }

            //mainFrame.Items.Add();
            mainFrame.Navigate(new Welcome());
        }

        private void UpdateWindowFrame()
        {
            var m = new MARGINS() { cyTopHeight = (int)ActualHeight };
            DwmExtendFrameIntoClientArea(new WindowInteropHelper(Application.Current.MainWindow).Handle, ref m);
        }



        #region Win32

        [DllImport("uxtheme.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);
        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, WindowCompositionAttribute dwAttribute, ref int pvAttribute, int cbAttribute);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool SetPropW(IntPtr hwnd, string prop, IntPtr value);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern long DwmSetWindowAttribute(IntPtr hwnd,
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
