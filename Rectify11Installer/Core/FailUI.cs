namespace Rectify11Installer.Core
{
    public partial class FailUI : Form
    {
        public FailUI()
        {
            InitializeComponent();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            TopMost = false;
            SetupMode.RebootSystem();
        }
    }
}
