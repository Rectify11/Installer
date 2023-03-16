namespace Rectify11Installer.Pages
{
	partial class ProgressPage
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
			this.progressText = new Rectify11Installer.Controls.DarkAwareLabel();
			this.progressInfo = new Rectify11Installer.Controls.DarkAwareLabel();
			//this.progressBar1 = new Rectify11Installer.Controls.CustomProgressBar();
			this.timer1 = new System.Windows.Forms.Timer();
			this.SuspendLayout();
			// 
			// progressText
			// 
			this.progressText.BackColor = System.Drawing.Color.Transparent;
			this.progressText.Font = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progressText.ForeColor = System.Drawing.Color.Black;
			this.progressText.Location = new System.Drawing.Point(0, 5);
			this.progressText.Name = "progressText";
			this.progressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.progressText.Size = new System.Drawing.Size(361, 40);
			this.progressText.TabIndex = 1;
			this.progressText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// timer1
			// 
			this.timer1.Interval = 10000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// progressInfo
			// 
			this.progressInfo.BackColor = System.Drawing.Color.Transparent;
			this.progressInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.progressInfo.ForeColor = System.Drawing.Color.Black;
			this.progressInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.progressInfo.Location = new System.Drawing.Point(0, 40);
			this.progressInfo.Name = "progressInfo";
			this.progressInfo.Size = new System.Drawing.Size(290, 200);
			this.progressInfo.TabIndex = 2;
			// 
			// ProgressPage
			// 
			//this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.progressText);
			this.Controls.Add(this.progressInfo);
			this.Name = "ProgressPage";
			this.SideImage = global::Rectify11Installer.Properties.Resources.install;
			this.HeaderVisible = false;
			this.FooterVisible = false;
			this.UpdateFrame = false;
			this.Page = Rectify11Installer.Core.TabPages.progressPage;
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.DarkAwareLabel progressText;
		private Controls.DarkAwareLabel progressInfo;
		private System.Windows.Forms.Timer timer1;
	}
}
