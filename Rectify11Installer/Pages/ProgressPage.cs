using System;

namespace Rectify11Installer.Pages
{
    public partial class ProgressPage : WizardPage
    {
        public ProgressPage()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        public void Start()
        {
            timer1.Start();
        }
    }
}
