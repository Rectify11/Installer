using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Rectify11Installer.Pages;

namespace Rectify11Installer
{
    public partial class FrmWizard : Form
    {
        private static readonly WelcomePage WelcomePage = new();
        private static readonly EulaPage EulaPage = new();
        private static readonly InstalllOptnsPage InstallOptions = new();
        private static readonly ConfirmOperationPage ConfirmOpPage = new();
        private static readonly ProgressPage ProgressPage = new();
        private static FinishPage? FinishPage;

        private WizardPage CurrentPage;

        private bool HideCloseButton = false;
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                if (HideCloseButton)
                    myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        //Visual studio does not understand that we assign "CurrentPage" in Navigate()
#pragma warning disable CS8618
        public FrmWizard()
#pragma warning restore CS8618
        {
            InitializeComponent();

            WelcomePage.InstallButton.Click += InstallButton_Click;
            WelcomePage.UninstallButton.Click += UninstallButton_Click;

            Navigate(WelcomePage);

            SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;

            navigationButton1.Location = new Point(
                panel1.Width / 2 - navigationButton1.Size.Width / 2,
                panel1.Height / 2 - navigationButton1.Size.Height / 2);
        }
        #region Welcome Page

        private bool CheckIfUpdatesPending()
        {
            WUApiLib.ISystemInformation systemInfo = new WUApiLib.SystemInformation();

            if (systemInfo.RebootRequired)
            {
                var pg = new TaskDialogPage()
                {
                    Icon = TaskDialogIcon.ShieldErrorRedBar,

                    Text = "You cannot install Rectify11 as Windows Updates are pending.",
                    Heading = "Compatibility Error",
                    Caption = "Rectify11 Setup",
                };
                TaskDialog.ShowDialog(this, pg);

                return false;
            }
            else
            {
                return true;
            }
        }
        private void InstallButton_Click(object? sender, EventArgs e)
        {
            if (CheckIfUpdatesPending())
            {
                Navigate(EulaPage);
            }

        }

        private void UninstallButton_Click(object? sender, EventArgs e)
        {
            if (CheckIfUpdatesPending())
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
        }
        #endregion
        #region Navigation
        private void Navigate(WizardPage page)
        {
            CurrentPage = page;

            pnlMain.Controls.Clear();
            page.Dock= DockStyle.Fill;
            pnlMain.Controls.Add(page);

            lblTopText.Text = page.WizardTopText;

            if (page == EulaPage)
            {
                BtnBack.Visible = true;
                BtnNext.Visible = true;

                BtnBack.Enabled = true;
                BtnNext.Visible = true;

                BtnBack.ButtonText = "Disagree";
                BtnNext.ButtonText = "Agree";

                panel1.Visible = true;
            }
            else if (page == ConfirmOpPage)
            {
                BtnBack.Visible = true;
                BtnNext.Visible = true;

                BtnBack.Enabled = true;
                BtnNext.Enabled = true;

                BtnBack.ButtonText = "Back";
                BtnNext.ButtonText = "Install";
                panel1.Visible = true;
            }
            else if (page == FinishPage)
            {
                BtnNext.Visible = true;
                BtnNext.ButtonText = "Finish";
            }
            else if (page == InstallOptions)
            {
                BtnBack.Visible = true;
                BtnNext.Visible = true;

                BtnBack.Enabled = true;
                BtnNext.Enabled = true;

                BtnBack.ButtonText = "Back";
                BtnNext.ButtonText = "Next";
                panel1.Visible = true;
            }
            else
            {
                BtnBack.Visible = false;
                BtnNext.Visible = false;

                BtnBack.Enabled = false;
                BtnNext.Visible = false;

                BtnBack.ButtonText = "Back";
                BtnNext.ButtonText = "Next";
                panel1.Visible = false;
            }

            FixColors();
        }
        internal void Complete(RectifyInstallerWizardCompleteInstallerEnum type, string errorDescription)
        {
            FinishPage = new FinishPage();
            if (type == RectifyInstallerWizardCompleteInstallerEnum.Success)
            {
                FinishPage.MainText.Text = "Your computer was successfully rectified.\nPlease reboot for the changes to take affect.";
            }
            else
            {
                FinishPage.MainText.Text = "Installing Rectify11 failed.\nThe error is: " + errorDescription;
            }
            Navigate(FinishPage);

            HideCloseButton = false;
            ControlBox = true;
            pnlBottom.Visible = true;
        }
        #endregion
        private void Form1_Shown(object sender, EventArgs e)
        {
            var buildNumber = Environment.OSVersion.Version.Build;

            // _ = SetWindowTheme(this.Handle, "DarkMode_Explorer", null);
            try
            {
                _ = DarkMode.AllowDarkModeForWindow(this.Handle, true);
            }
            catch
            {

            }

            try
            {
                SetTitlebarColor();
            }
            catch { }

            try
            {
                //mica


                bool extend = Theme.IsUsingDarkMode;

                if (buildNumber >= 22523)
                {
                    int micaValue = 0x02;
                    _ = DwmSetWindowAttribute(this.Handle, WindowCompositionAttribute.DWMWA_SYSTEMBACKDROP_TYPE, ref micaValue, Marshal.SizeOf(typeof(int)));
                }

                else
                {
                    int trueValue = 0x01;
                    _ = DwmSetWindowAttribute(this.Handle, WindowCompositionAttribute.DWMWA_MICA_EFFECT, ref trueValue, Marshal.SizeOf(typeof(int)));
                }

                MARGINS m = new();
                if (extend)
                {
                    m.cyTopHeight = this.Height - pnlBottom.Height;
                    m.cyBottomHeight = pnlBottom.Height;

                    BackColor = Color.Black;
                }
                else
                {
                    BackColor = Color.White;
                    pnlTop.BackColor = Color.White;

                    m.cyTopHeight = pnlTop.Height;
                    panel1.BackColor = Color.Black;
                }
                _ = DwmExtendFrameIntoClientArea(this.Handle, ref m);
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

            FixColors();
        }

        private void FixColors()
        {
            if (Theme.IsUsingDarkMode)
            {
                this.ForeColor = Color.White;
                foreach (var item in GetAllControls(this))
                {
                    if (item is not RichTextBox)
                    {
                        item.ForeColor = Color.White;
                    }
                }
            }
            else
            {
                this.ForeColor = Color.Gray;
                foreach (var item in GetAllControls(this))
                {
                    item.ForeColor = Color.Black;
                }
            }
        }
        private IEnumerable<Control> GetAllControls(Control container)
        {
            List<Control> controlList = new();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllControls(c));
                controlList.Add(c);
            }
            return controlList;
        }

        private void SetTitlebarColor()
        {
            bool darkTheme = Theme.IsUsingDarkMode;
            var buildNumber = Environment.OSVersion.Version.Build;

            //Enable dark title bar
            if (buildNumber < 18362)
            {
                SetPropW(this.Handle, "UseImmersiveDarkModeColors", new IntPtr(darkTheme ? 1 : 0));
            }
            else
            {
                WindowCompositionAttributeData d = new()
                {
                    Attribute = WindowCompositionAttribute.WCA_USEDARKMODECOLORS
                };
                int size = Marshal.SizeOf(typeof(bool));
                IntPtr ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(darkTheme, ptr, false);

                d.Data = ptr;
                d.SizeOfData = size;
                _ = SetWindowCompositionAttribute(this.Handle, ref d);
            }
            if (Marshal.GetLastWin32Error() != 0) { throw new Win32Exception(); }
        }


        #region Win32

        [DllImport("uxtheme.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        internal static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string? pszSubIdList);
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (CurrentPage == EulaPage)
            {
                //We have disagreed with the license
                Navigate(WelcomePage);
            }
            else if (CurrentPage == ConfirmOpPage)
            {
                //The user clicked on "Back" when on the confirm install/uninstall page
                Navigate(InstallOptions);
            }
            else if (CurrentPage == InstallOptions)
            {
                //The user clicked on "Back" when on the confirm install/uninstall page
                Navigate(EulaPage);
            }

        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage == EulaPage)
            {
                //Show install options page
                Navigate(InstallOptions);


            }
            else if (CurrentPage == InstallOptions)
            {
                //We are about to install it
                Navigate(ConfirmOpPage);

                //Change text
                ConfirmOpPage.TextLable.Text = "You are about to do the following operation:\nInstall Rectify11 on top of this Windows 11 Installation\n\nIt is recommended to save your work before installing.";
            }
            else if (CurrentPage == ConfirmOpPage)
            {
                //Install/Uninstall Rectify11
                Navigate(ProgressPage);

                IRectifyInstaller installer = new RectifyInstaller();
                installer.SetParentWizard(new RectifyInstallerWizard(this, ProgressPage));
                HideCloseButton = true;
                ControlBox = false;
                pnlBottom.Visible = false;
                var thread = new Thread(delegate ()
                {
                    installer.Install();
                });
                thread.Start();
            }
            else if (CurrentPage == FinishPage)
            {
                //We are done
                Application.Exit();
            }
        }
        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            switch (e.Category)
            {
                case UserPreferenceCategory.General:
                    Form1_Shown(this, new EventArgs());
                    lblTopText.Invalidate();
                    break;
            }
        }

        private void NavigationButton1_Click(object sender, EventArgs e)
        {
            BtnBack_Click(sender, e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (HideCloseButton)
                e.Cancel = true;
            base.OnClosing(e);
        }
    }
}