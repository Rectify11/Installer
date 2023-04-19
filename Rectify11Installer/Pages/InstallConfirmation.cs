using Rectify11Installer.Core;

namespace Rectify11Installer.Pages
{
	public partial class InstallConfirmation : WizardPage
	{
		FrmWizard _frm;
		public string Summary
		{
			get { return summaryItems.Text; }
			set { summaryItems.Text = value; }
		}
		private readonly System.ComponentModel.ComponentResourceManager _resources = new SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
		public InstallConfirmation(FrmWizard frm)
		{
			InitializeComponent();
			_frm = frm;
			NavigationHelper.OnNavigate += NavigationHelper_OnNavigate; ;
		}
		// fix annoying bug where extras options doesnt get printed properly if you go back 
		// to the options page after selecting extras
		private void NavigationHelper_OnNavigate(object sender, System.EventArgs e)
		{
			WizardPage page = (WizardPage)sender;
			if (page == RectifyPages.InstallConfirmation)
			{
				RectifyPages.InstallConfirmation.Summary = _resources.GetString("summaryItems");
				RectifyPages.InstallConfirmation.Summary += Helper.FinalText().ToString();
				_frm._timerFrames = 72;
				_frm._timerFramesTmp = 0;
				_frm.timer.Start();
			}
		}
	}
}
