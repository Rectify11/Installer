namespace Rectify11Installer.Pages
{
    public partial class FinishPage : WizardPage
    {
        public Label MainText
        {
            get
            {
                return lblText;
            }
            set
            {
                lblText = value;
            }
        }
        public bool CopyButtonVisible
        {
            get
            {
                return btnCopy.Visible;
            }
            set
            {
                btnCopy.Visible = value;
            }
        }
        public FinishPage()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblText.Text);
        }
    }
}
