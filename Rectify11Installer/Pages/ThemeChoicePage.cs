using System.Windows.Forms;

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
                themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.blackPreview;
                themeSel.SelectedIndex = 2;
            }
            else
            {
                themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.lightPreview;
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
                    break;
                case 1:
                    themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.darkPreview;
                    break;
                case 2:
                    themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.blackPreview;
                    break;
                default:
                    themePreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.lightPreview;
                    break;
            }
        }
    }
}
