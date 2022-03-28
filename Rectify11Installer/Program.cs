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
            _ = DarkMode.fnAllowDarkModeForApp(DarkMode.PreferredAppMode.AllowDark);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.Run(new FrmWizard());
        }
    }
}