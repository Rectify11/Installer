using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
    public partial class SetupModeForm : Form, IRectifyInstallerWizard
    {
        public SetupModeForm()
        {
            InitializeComponent();
            BackColor = Color.Fuchsia;
            TransparencyKey = Color.Fuchsia;
        }
        private void UpdateLabelPosition()
        {

            label1.Location = new Point(
    this.ClientSize.Width / 2 - label1.Size.Width / 2,
    this.ClientSize.Height - label1.Size.Height - 20);
            label1.Anchor = AnchorStyles.None;
        }
        private void SetupModeForm_Shown(object sender, EventArgs e)
        {
            SetupMode.Exit();
            UpdateLabelPosition();

            //var handle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "FirstUXWndClass", null);
            //if (handle == IntPtr.Zero)
            //{
            //    MessageBox.Show("Setup Mode UI window not found");
            //}
            //else
            //{
            //    MessageBox.Show("Setup Mode UI window found");

            //    var dcHandle = GetDC(handle);


            //}


            try
            {
                string iniPath = @"C:\Windows\Rectify11\work.ini";

                if (!File.Exists(iniPath))
                {
                    MessageBox.Show("Fatal error: Attempted to boot into setup mode, but the installer does not know if you need to install or uninstall.\nPress OK to reboot back into your operating system.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IniFile ini = new IniFile(iniPath);

                var mode = ini.Read("Mode");

                if (mode == "Install")
                {
                    var options = new InstallerOptions()
                    {
                        DoSafeInstall = ini.Read("DoSafeInstall") == bool.TrueString,
                        ShouldInstallExplorerPatcher = ini.Read("InstallEP") == bool.TrueString,
                        ShouldInstallThemes = ini.Read("InstallThemes") == bool.TrueString,
                        ShouldInstallWallpaper = ini.Read("InstallWP") == bool.TrueString,
                        ShouldInstallWinver = ini.Read("InstallVer") == bool.TrueString,
                    };


                    IRectifyInstaller installer = new RectifyInstaller();
                    installer.SetParentWizard(this);

                    var thread = new Thread(delegate ()
                    {
                        installer.Install(options);
                    });
                    thread.Start();
                }
            }
            catch(Exception ee)
            {
                TopMost = false;
                var u = new FailUI();
                u.InfoLabel.Text = ee.ToString();
                u.Show();
                Cursor.Show();
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr hWndChildAfter, string className, string? windowTitle);
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        private void button1_Click(object sender, EventArgs e)
        {

        }
        int progress = 0;
        public void SetProgress(int val)
        {
            progress = val;
        }

        public void SetProgressText(string text)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                label1.Text = text + " (" + progress + "%)";
                UpdateLabelPosition();
            });
        }

        public void CompleteInstaller(RectifyInstallerWizardCompleteInstallerEnum type, bool IsInstalling, string ErrorDescription = "")
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                SetupMode.Exit();
                if (type == RectifyInstallerWizardCompleteInstallerEnum.Fail)
                {
                    TopMost = false;
                    cancelClose = false; 
                    Cursor.Show();
                    var u = new FailUI();
                    u.InfoLabel.Text = ErrorDescription;
                    u.Show();
                    Hide();
                }
                else if (type == RectifyInstallerWizardCompleteInstallerEnum.Success)
                {
                    label1.Text = "Installation success. Rebooting...";
                    TopMost = false;
                    cancelClose = false;
                    Application.Exit();
                }
            });
        }
        bool cancelClose = true;
        private void SetupModeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = cancelClose;
        }
    }
}
