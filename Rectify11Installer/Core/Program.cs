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
            Application.Run(new FrmWizard());
        }
    }
}