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
			System.ComponentModel.ComponentResourceManager resources = new global::Rectify11Installer.Core.SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
			this.darkAwareLabel1 = new Rectify11Installer.Controls.DarkAwareLabel();
			this.PrevImg = new System.Windows.Forms.PictureBox();
			this.CompactRad = new Rectify11Installer.Controls.DarkAwareRadioButton();
			this.FullRad = new Rectify11Installer.Controls.DarkAwareRadioButton();
			this.ClassicRad = new Rectify11Installer.Controls.DarkAwareRadioButton();
			this.ClassicwTransparentRad = new Rectify11Installer.Controls.DarkAwareRadioButton();
			this.DefaultMenu = new Rectify11Installer.Controls.DarkAwareRadioButton();
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
			this.darkAwareLabel1.Text = resources.GetString("epTitle");


			this.PrevImg.BackgroundImage = global::Rectify11Installer.Properties.Resources._11start;
			this.PrevImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.PrevImg.Location = new System.Drawing.Point(190, 53);
			this.PrevImg.Name = "PrevImg";
			this.PrevImg.Size = new System.Drawing.Size(140, 200);
			this.PrevImg.TabIndex = 0;
			this.PrevImg.TabStop = false;
		//	this.w11StartImg.Click += new System.EventHandler(this.w11StartImg_Click);
			// 
			// CompactRad
			// 
			this.CompactRad.ForeColor = System.Drawing.Color.Black;
			this.CompactRad.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.CompactRad.Location = new System.Drawing.Point(0, 70);
			this.CompactRad.Name = "CompactRad";
			this.CompactRad.Size = new System.Drawing.Size(190, 35);
			this.CompactRad.TabIndex = 3;
			this.CompactRad.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.CompactRad.TabStop = true;
			this.CompactRad.Text = "Enhanced Fluent Menus (More Options)";
			this.CompactRad.UseVisualStyleBackColor = true;
			// 
			// FullRad
			// 
			this.FullRad.ForeColor = System.Drawing.Color.Black;
			this.FullRad.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.FullRad.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.FullRad.Location = new System.Drawing.Point(0, 110);
			this.FullRad.Name = "FullRad";
			this.FullRad.Size = new System.Drawing.Size(190, 35);
			this.FullRad.TabIndex = 4;
			this.FullRad.TabStop = true;
			this.FullRad.Text = "Enhanced Fluent Menus (All Items in root of the menu)";
			this.FullRad.UseVisualStyleBackColor = true;
			// 
			// ClassicRad
			// 
			this.ClassicRad.ForeColor = System.Drawing.Color.Black;
			this.ClassicRad.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.ClassicRad.Location = new System.Drawing.Point(0, 150);
			this.ClassicRad.Name = "ClassicRad";
			this.ClassicRad.Size = new System.Drawing.Size(190, 35);
			this.ClassicRad.TabIndex = 3;
			this.ClassicRad.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.ClassicRad.TabStop = true;
			this.ClassicRad.Text = "Classic Context Menus";
			this.ClassicRad.UseVisualStyleBackColor = true;
			// 
			// ClassicwTransparentRad
			// 
			this.ClassicwTransparentRad.ForeColor = System.Drawing.Color.Black;
			this.ClassicwTransparentRad.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.ClassicwTransparentRad.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.ClassicwTransparentRad.Location = new System.Drawing.Point(0, 180);
			this.ClassicwTransparentRad.Name = "ClassicwTransparentRad";
			this.ClassicwTransparentRad.Size = new System.Drawing.Size(190, 35);
			this.ClassicwTransparentRad.TabIndex = 4;
			this.ClassicwTransparentRad.TabStop = true;
			this.ClassicwTransparentRad.Text = "Classic Menus with transparency";
			this.ClassicwTransparentRad.UseVisualStyleBackColor = true;
			// 
			// DefaultMenu
			// 
			this.DefaultMenu.ForeColor = System.Drawing.Color.Black;
			this.DefaultMenu.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DefaultMenu.Location = new System.Drawing.Point(0, 220);
			this.DefaultMenu.Name = "DefaultMenu";
			this.DefaultMenu.Size = new System.Drawing.Size(190, 35);
			this.DefaultMenu.TabIndex = 3;
			this.DefaultMenu.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.DefaultMenu.TabStop = true;
			this.DefaultMenu.Text = "Windows 11 Default";
			this.DefaultMenu.UseVisualStyleBackColor = true;
			// 
			// CMenuPage
			// 
			this.Controls.Add(this.DefaultMenu);
			this.Controls.Add(this.ClassicwTransparentRad);
			this.Controls.Add(this.ClassicRad);
			this.Controls.Add(this.FullRad);
			this.Controls.Add(this.CompactRad);
			this.Controls.Add(this.PrevImg);
			this.Controls.Add(this.darkAwareLabel1);
			this.Name = "EPPage";
			this.SideImage = global::Rectify11Installer.Properties.Resources.menus;
			this.HeaderVisible = true;
			this.FooterVisible = true;
			this.UpdateFrame = true;
			this.NextButtonEnabled = true;
			this.Page = Rectify11Installer.Core.TabPages.cmenupage;
			this.NextButtonText = resources.GetString("buttonNext");
			this.WizardHeader = resources.GetString("CMenuPageHeader");
			((System.ComponentModel.ISupportInitialize)(this.PrevImg)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.DarkAwareLabel darkAwareLabel1;
		private System.Windows.Forms.PictureBox PrevImg;
		private Controls.DarkAwareRadioButton DefaultMenu;
		private Controls.DarkAwareRadioButton ClassicwTransparentRad;
		private Controls.DarkAwareRadioButton ClassicRad;
		private Controls.DarkAwareRadioButton CompactRad;
		private Controls.DarkAwareRadioButton FullRad;
	}
}
