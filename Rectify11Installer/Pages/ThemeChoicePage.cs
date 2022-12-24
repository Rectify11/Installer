using System.Windows.Forms;
using Rectify11Installer.Core;
namespace Rectify11Installer.Pages
{
    public partial class ThemeChoicePage : WizardPage
    {
        public ThemeChoicePage()
        {
            InitializeComponent();
            System.ComponentModel.ComponentResourceManager resources = new global::Rectify11Installer.Core.SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
            if (Theme.IsUsingDarkMode)
            {
                themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.darkPreview;
                InstallOptions.ThemeLight = false;
                InstallOptions.ThemeDark = true;
                InstallOptions.ThemeBlack = false;
                themeSel.SelectedIndex = 1;
            }
            else
            {
                themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.lightPreview;
                InstallOptions.ThemeLight = true;
                InstallOptions.ThemeDark = false;
                InstallOptions.ThemeBlack = false;
                themeSel.SelectedIndex = 0;
            }

            this.themeSel.SelectedIndexChanged += themeSel_SelectedIndexChanged;
        }

        void themeSel_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            int val = comboBox.SelectedIndex;
            switch (val)
            {
                case 0:
                    themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.lightPreview;
                    InstallOptions.ThemeLight = true;
                    InstallOptions.ThemeDark = false;
                    InstallOptions.ThemeBlack = false;
                    break;
                case 1:
                    themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.darkPreview;
                    InstallOptions.ThemeLight = false;
                    InstallOptions.ThemeDark = true;
                    InstallOptions.ThemeBlack = false;
                    break;
                case 2:
                    themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.blackPreview;
                    InstallOptions.ThemeLight = false;
                    InstallOptions.ThemeDark = false;
                    InstallOptions.ThemeBlack = true;
                    break;
                default:
                    themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.lightPreview;
                    InstallOptions.ThemeLight = true;
                    InstallOptions.ThemeDark = false;
                    InstallOptions.ThemeBlack = false;
                    break;
            }
        }
    }
}
