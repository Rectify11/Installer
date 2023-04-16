using Rectify11Installer.Core;
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

			if (System.Environment.OSVersion.Version.Build >= 22530)
			{

				randomCheckbox2.Enabled = true;

			}
			InstallOptions.SkipMFE = false;
			InstallOptions.TabbedNotMica = false;

			this.themeSel.SelectedIndexChanged += themeSel_SelectedIndexChanged;
			this.randomCheckbox1.CheckedChanged += RandomCheckbox1_CheckedChanged;
			this.randomCheckbox2.CheckedChanged += RandomCheckbox2_CheckedChanged;
		}

		private void RandomCheckbox2_CheckedChanged(object sender, System.EventArgs e)
		{
			InstallOptions.TabbedNotMica = false;
			if (this.randomCheckbox2.Checked)
			{

				InstallOptions.TabbedNotMica = true;
			}
		}

		private void RandomCheckbox1_CheckedChanged(object sender, System.EventArgs e)
		{
			InstallOptions.SkipMFE = false;
			if (this.randomCheckbox1.Checked)
			{

				InstallOptions.SkipMFE = true;
			}
		}

		void themeSel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			var comboBox = (ComboBox)sender;
			var val = comboBox.SelectedIndex;
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
