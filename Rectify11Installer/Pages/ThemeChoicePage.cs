
namespace Rectify11Installer.Pages
{
    public partial class ThemeChoicePage : WizardPage
    {
        public ThemeChoicePage()
        {
            InitializeComponent();
            lightPreview.Click += LightPreview_Click;
            darkPreview.Click += DarkPreview_Click;
            blackPreview.Click += BlackPreview_Click;
        }

        private void BlackPreview_Click(object sender, System.EventArgs e)
        {
            blackRadio.Checked = true;
        }

        private void DarkPreview_Click(object sender, System.EventArgs e)
        {
            darkRadio.Checked = true;
        }

        private void LightPreview_Click(object sender, System.EventArgs e)
        {
            lightRadio.Checked = true;
        }
    }
}
