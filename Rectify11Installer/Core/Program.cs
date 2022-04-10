using Rectify11Installer.Core;

namespace Rectify11Installer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Build >= 22000) { }
            else
            {
                Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
                MessageBox.Show("Rectify11 installer is only supported on Windows 11 or greater.", "Compatibility Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists("rectify11.xml"))
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

            if (File.Exists("install.log"))
                File.Delete("install.log");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Run(new FrmWizard());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"A fatal error occured: {e.ToString()}\nPlease report this as a bug\n", "Runtime Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"A fatal error occured: {e.ToString()}\nPlease report this as a bug\n", "Runtime Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
    }
}