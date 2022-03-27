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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            DarkMode.fnAllowDarkModeForApp(DarkMode.PreferredAppMode.AllowDark);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}