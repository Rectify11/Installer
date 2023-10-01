using Rectify11Installer.Core;
namespace Rectify11Installer.Pages
{
	partial class CMenuPage
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
			this.darkAwareLabel1 = new Rectify11Installer.Controls.DarkAwareLabel();
			this.PrevImg = new System.Windows.Forms.PictureBox();
			this.Rad1 = new Rectify11Installer.Controls.DarkAwareRadioButton();
			this.Rad2 = new Rectify11Installer.Controls.DarkAwareRadioButton();
			this.Rad3 = new Rectify11Installer.Controls.DarkAwareRadioButton();
			this.Rad4 = new Rectify11Installer.Controls.DarkAwareRadioButton();
			((System.ComponentModel.ISupportInitialize)(this.PrevImg)).BeginInit();
			this.SuspendLayout();
			// 
			// darkAwareLabel1
			// 
			this.darkAwareLabel1.BackColor = System.Drawing.Color.Transparent;
			this.darkAwareLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.darkAwareLabel1.ForeColor = System.Drawing.Color.Black;
			this.darkAwareLabel1.Location = new System.Drawing.Point(0, 5);
			this.darkAwareLabel1.Name = "darkAwareLabel1";
			this.darkAwareLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.darkAwareLabel1.Size = new System.Drawing.Size(361, 40);
			this.darkAwareLabel1.TabIndex = 2;
			this.darkAwareLabel1.Text = Rectify11Installer.Strings.Rectify11.epTitle;


			this.PrevImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.PrevImg.Location = new System.Drawing.Point(170, 53);
			this.PrevImg.Name = "PrevImg";
			this.PrevImg.Size = new System.Drawing.Size(160, 200);
			this.PrevImg.TabIndex = 0;
			this.PrevImg.TabStop = false;
			this.PrevImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			// 
			// Rad1
			// 
			this.Rad1.ForeColor = System.Drawing.Color.Black;
			this.Rad1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad1.Location = new System.Drawing.Point(0, 70);
			this.Rad1.Name = "Rad1";
			this.Rad1.Size = new System.Drawing.Size(160, 35);
			this.Rad1.TabIndex = 1;
			this.Rad1.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad1.TabStop = true;
			this.Rad1.Text = Rectify11Installer.Strings.Rectify11.cmenuFluent1;
            this.Rad1.UseVisualStyleBackColor = true;
			// 
			// Rad2
			// 
			this.Rad2.ForeColor = System.Drawing.Color.Black;
			this.Rad2.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad2.Location = new System.Drawing.Point(0, 110);
			this.Rad2.Name = "Rad2";
			this.Rad2.Size = new System.Drawing.Size(160, 35);
			this.Rad2.TabIndex = 2;
			this.Rad2.TabStop = true;
			this.Rad2.Text = Rectify11Installer.Strings.Rectify11.cmenuFluent2;

            this.Rad2.UseVisualStyleBackColor = true;
			// 
			// Rad3
			// 
			this.Rad3.ForeColor = System.Drawing.Color.Black;
			this.Rad3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad3.Location = new System.Drawing.Point(0, 150);
			this.Rad3.Name = "Rad3";
			this.Rad3.Size = new System.Drawing.Size(160, 35);
			this.Rad3.TabIndex = 3;
			this.Rad3.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad3.TabStop = true;
			this.Rad3.Text = Rectify11Installer.Strings.Rectify11.cmenuClassic;

            this.Rad3.UseVisualStyleBackColor = true;
			// 
			// Rad4
			// 
			this.Rad4.ForeColor = System.Drawing.Color.Black;
			this.Rad4.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.Rad4.Location = new System.Drawing.Point(0, 180);
			this.Rad4.Name = "Rad4";
			this.Rad4.Size = new System.Drawing.Size(160, 35);
			this.Rad4.TabIndex = 4;
			this.Rad4.TabStop = true;
			this.Rad4.Text = Rectify11Installer.Strings.Rectify11.cmenuClassicA;
            this.Rad4.UseVisualStyleBackColor = true;
            // 
            // CMenuPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.PrevImg);
			this.Controls.Add(this.Rad4);
			this.Controls.Add(this.Rad3);
			this.Controls.Add(this.Rad2);
			this.Controls.Add(this.Rad1);
			this.Controls.Add(this.darkAwareLabel1);
			this.Name = "EPPage";
			this.SideImage = global::Rectify11Installer.Properties.Resources.menus;
			if (Theme.IsUsingDarkMode) this.SideImage = global::Rectify11Installer.Properties.Resources.menusD;
			this.HeaderVisible = true;
			this.FooterVisible = true;
			this.UpdateFrame = true;
			this.NextButtonEnabled = true;
			this.Page = Rectify11Installer.Core.TabPages.cmenupage;
			this.NextButtonText = Rectify11Installer.Strings.Rectify11.buttonNext;
			this.WizardHeader = Rectify11Installer.Strings.Rectify11.CMenuPageHeader;
            ((System.ComponentModel.ISupportInitialize)(this.PrevImg)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.DarkAwareLabel darkAwareLabel1;
		private System.Windows.Forms.PictureBox PrevImg;
		private Controls.DarkAwareRadioButton Rad4;
		private Controls.DarkAwareRadioButton Rad3;
		private Controls.DarkAwareRadioButton Rad1;
		private Controls.DarkAwareRadioButton Rad2;
	}
}
