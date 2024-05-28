namespace Rectify11Installer.Pages
{
	partial class DefenderPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DefenderPage));
            this.progressText = new Rectify11Installer.Controls.DarkAwareLabel();
            this.SuspendLayout();
            // 
            // progressText
            // 
            this.progressText.BackColor = System.Drawing.Color.White;
            this.progressText.Font = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressText.ForeColor = System.Drawing.Color.Black;
            this.progressText.Location = new System.Drawing.Point(0, 5);
            this.progressText.Name = "progressText";
            this.progressText.Size = new System.Drawing.Size(344, 221);
            this.progressText.TabIndex = 1;
            this.progressText.Text = resources.GetString("progressText.Text");
            // 
            // DefenderPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.progressText);
            this.FooterVisible = true;
            this.HeaderVisible = true;
            this.Name = "DefenderPage";
            this.NextButtonEnabled = true;
            this.NextButtonText = "Next";
            this.SideImage = global::Rectify11Installer.Properties.Resources.install;
            this.UpdateFrame = true;
            this.WizardHeader = "Disable antivirus";
            this.ResumeLayout(false);

		}

		#endregion

		private Controls.DarkAwareLabel progressText;
	}
}
