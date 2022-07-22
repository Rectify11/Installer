using System.Diagnostics;

namespace Rectify11Installer.Pages
{
    public partial class RebootPage : WizardPage
    {
        public RebootPage()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value -= 1;
            if (progressBar1.Value <= 0)
            {
                timer1.Stop();
                winuiButton1_Click(this, new EventArgs());
            }
        }
        public void Start()
        {
            timer1.Start();
        }
        private void winuiButton1_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "-r -t 10");
        }
    }
}
