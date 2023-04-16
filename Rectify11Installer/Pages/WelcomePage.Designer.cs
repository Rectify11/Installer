namespace Rectify11Installer.Pages
{
	partial class WelcomePage
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new global::Rectify11Installer.Core.SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
			this.wlcmPageTitle = new Controls.DarkAwareLabel();
			this.label2 = new Controls.DarkAwareLabel();
			this.cmbUninstall = new Rectify11Installer.Controls.FakeCommandLink();
			this.cmbInstall = new Rectify11Installer.Controls.FakeCommandLink();
			this.SuspendLayout();
			// 
			// wlcmPageTitle
			// 
			this.wlcmPageTitle.AutoSize = true;
			this.wlcmPageTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.wlcmPageTitle.Location = new System.Drawing.Point(3, 0);
			this.wlcmPageTitle.Name = "wlcmPageTitle";
			this.wlcmPageTitle.Size = new System.Drawing.Size(103, 30);
			this.wlcmPageTitle.TabIndex = 0;
			this.wlcmPageTitle.Text = resources.GetString("welcome");
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(5, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(313, 92);
			this.label2.TabIndex = 2;
			this.label2.Text = resources.GetString("welcomeDescription");
			// 
			// cmbUninstall
			// 
			this.cmbUninstall.AutoSize = true;
			this.cmbUninstall.BackColor = System.Drawing.Color.Transparent;
			this.cmbUninstall.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbUninstall.Location = new System.Drawing.Point(0, 176);
			this.cmbUninstall.Name = "cmbUninstall";
			this.cmbUninstall.Note = resources.GetString("uninstallNote");
			this.cmbUninstall.Size = new System.Drawing.Size(305, 45);
			this.cmbUninstall.TabIndex = 4;
			this.cmbUninstall.Text = resources.GetString("uninstallTitle");
			// 
			// cmbInstall
			// 
			this.cmbInstall.AutoSize = true;
			this.cmbInstall.BackColor = System.Drawing.Color.Transparent;
			this.cmbInstall.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbInstall.Location = new System.Drawing.Point(0, 125);
			this.cmbInstall.Name = "cmbInstall";
			this.cmbInstall.Note = resources.GetString("installNote");
			this.cmbInstall.Size = new System.Drawing.Size(305, 45);
			this.cmbInstall.TabIndex = 3;
			this.cmbInstall.Text = resources.GetString("installTitle");
			// 
			// WelcomePage
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.cmbUninstall);
			this.Controls.Add(this.cmbInstall);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.wlcmPageTitle);
			this.Name = "WelcomePage";
			this.SideImage = global::Rectify11Installer.Properties.Resources.install;
			this.HeaderVisible = false;
			this.FooterVisible = false;
			this.UpdateFrame = false;
			this.IsWelcomePage = true;
			this.Page = Rectify11Installer.Core.TabPages.wlcmPage;
			this.Size = new System.Drawing.Size(333, 322);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.DarkAwareLabel wlcmPageTitle;
		private Controls.DarkAwareLabel label2;
		private Controls.FakeCommandLink cmbInstall;
		private Controls.FakeCommandLink cmbUninstall;
	}
}
