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
			this.progressText = new Rectify11Installer.Controls.DarkAwareLabel();
			this.progressInfo = new Rectify11Installer.Controls.DarkAwareLabel();
			this.r1 = new Controls.DarkAwareRadioButton();
			this.r2 = new Controls.DarkAwareRadioButton();
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
			// r1
			//
			this.r1.BackColor = System.Drawing.Color.Transparent;
			this.r1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.r1.ForeColor = System.Drawing.Color.Black;
			this.r1.Location = new System.Drawing.Point(2, 120);
			this.r1.Name = "r1";
			this.r1.Size = new System.Drawing.Size(200, 20);
			this.r1.Text = Rectify11Installer.Strings.Rectify11.restartNow;
			this.r1.Visible = false;
			this.r1.Checked = true;

			//
			// r2
			//
			this.r2.BackColor = System.Drawing.Color.Transparent;
			this.r2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.r2.ForeColor = System.Drawing.Color.Black;
			this.r2.Location = new System.Drawing.Point(2, 145);
			this.r2.Name = "r2";
			this.r2.Size = new System.Drawing.Size(200, 20);
			this.r2.Text = Rectify11Installer.Strings.Rectify11.restartLater;
			this.r2.Visible = false;

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
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            //this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.r1);
			this.Controls.Add(this.r2);
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
		private Controls.DarkAwareRadioButton r1;
		private Controls.DarkAwareRadioButton r2;
		private System.Windows.Forms.Timer timer1;
	}
}
