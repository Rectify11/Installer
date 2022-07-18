using Rectify11Installer.Core;
using System.Diagnostics;

namespace Rectify11Installer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool setupMode = false;
            if (args.Length > 0)
            {
                var a = args[0];
                if (a == "/setup")
                {
                    setupMode = true;
                }
            }
            if (!setupMode)
            {
                if (Environment.OSVersion.Version.Build >= 21242) { }
                else
                {
                    Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
                    var h = MessageBox.Show("Rectify11 installer is only tested on Windows 11. If you want to continue, press Yes. The Rectify11 team is not responsible for damaging your Windows installation.", "Compatibility Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (h == DialogResult.Yes)
                    {

                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (!File.Exists(Application.StartupPath + "/rectify11.xml"))
            {
                Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAreaEnabled;
                MessageBox.Show("Failure while loading: Rectify11.xml. The error (0x2) occured. The file cannot be found", "Initialization Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Patches.GetAll();
            }
            catch (Exception ex)
            {
                Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NonClientAreaEnabled;
                MessageBox.Show("Failure while loading: Rectify11.xml. The error details can be found below.\n" + ex.ToString(), "Initialization Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _ = DarkMode.fnAllowDarkModeForApp(DarkMode.PreferredAppMode.AllowDark);

            if (File.Exists(Application.StartupPath + "/install.log"))
                File.Delete(Application.StartupPath + "/install.log");


            // This is done to prevent Visual Studio to "combine" the EXE when publishing the
            // Rectify11 Installer project via visual studio. I have no idea how to prevent this,
            // So I just replace the MZ signature with RE, and then we write the fixed file back.
            try
            {
                if (!File.Exists(Application.StartupPath + "/files/winver.exe"))
                {
                    byte[] winver = File.ReadAllBytes(Application.StartupPath + "/files/Winver.ex");
                    winver[0] = 0x4d;
                    winver[1] = 0x5a;
                    File.WriteAllBytes(Application.StartupPath + "/files/winver.exe", winver);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Warning: Unpacking Winver.exe failed\n" + ex.ToString(), "Initialization Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            try
            {
                Theme.LoadTheme();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failure while loading theming. The error details can be found below.\n" + ex.ToString(), "Initialization Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!setupMode)
                    return;
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            if (setupMode)
            {
                Application.Run(new SetupModeForm());
            }
            else
            {
                Application.Run(new FrmWizard());
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"A fatal error occured: {(Exception)e.ExceptionObject}\nPlease report this as a bug\n", "Runtime Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"A fatal error occured: {e.Exception}\nPlease report this as a bug\n", "Runtime Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    }
}