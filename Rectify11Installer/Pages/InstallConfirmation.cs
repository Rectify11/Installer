namespace Rectify11Installer.Pages
{
    public partial class InstallConfirmation : WizardPage
    {
        public string Summary
        {
            get { return summaryItems.Text; }
            set { summaryItems.Text = value; }
        }
        public InstallConfirmation()
        {
            InitializeComponent();
        }
    }
}
