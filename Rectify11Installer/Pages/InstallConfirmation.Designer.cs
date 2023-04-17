namespace Rectify11Installer.Pages
{
	partial class InstallConfirmation
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
			this.summaryTitle = new Rectify11Installer.Controls.DarkAwareLabel();
			this.summaryItems = new Rectify11Installer.Controls.DarkAwareLabel();
			this.summaryFooter = new Rectify11Installer.Controls.DarkAwareLabel();
			this.SuspendLayout();
			// 
			// summaryTitle
			// 
			this.summaryTitle.BackColor = System.Drawing.Color.Transparent;
			this.summaryTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.summaryTitle.ForeColor = System.Drawing.Color.Black;
			this.summaryTitle.Location = new System.Drawing.Point(-1, 5);
			this.summaryTitle.Name = "summaryTitle";
			this.summaryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.summaryTitle.Size = new System.Drawing.Size(288, 40);
			this.summaryTitle.TabIndex = 0;
			this.summaryTitle.Text = resources.GetString("summaryTitle");
			// 
			// summaryItems
			// 
			this.summaryItems.BackColor = System.Drawing.Color.Transparent;
			this.summaryItems.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.summaryItems.ForeColor = System.Drawing.Color.Black;
			this.summaryItems.Location = new System.Drawing.Point(0, 40);
			this.summaryItems.Name = "summaryItems";
			this.summaryItems.Size = new System.Drawing.Size(297, 195);
			this.summaryItems.TabIndex = 1;
			this.summaryItems.Text = "";
			// 
			// summaryFooter
			// 
			this.summaryFooter.BackColor = System.Drawing.Color.Transparent;
			this.summaryFooter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.summaryFooter.ForeColor = System.Drawing.Color.Black;
			this.summaryFooter.Location = new System.Drawing.Point(0, 220);
			this.summaryFooter.Name = "summaryFooter";
			this.summaryFooter.Size = new System.Drawing.Size(297, 54);
			this.summaryFooter.TabIndex = 2;
			this.summaryFooter.Text = resources.GetString("summaryFooter");
			// 
			// InstallConfirmation
			// 
			this.Controls.Add(this.summaryFooter);
			this.Controls.Add(this.summaryItems);
			this.Controls.Add(this.summaryTitle);
			this.Name = "InstallConfirmation";
			this.WizardHeader = resources.GetString("summaryHeader");
			this.SideImage = global::Rectify11Installer.Properties.Resources.installConfirm;
			this.HeaderVisible = true;
			this.FooterVisible = true;
			this.NextButtonEnabled = true;
			this.UpdateFrame = true;
			this.Page = Rectify11Installer.Core.TabPages.summaryPage;
			this.NextButtonText = resources.GetString("buttonInstall");
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion

		private Controls.DarkAwareLabel summaryTitle;
		private Controls.DarkAwareLabel summaryItems;
		private Controls.DarkAwareLabel summaryFooter;
	}
}
