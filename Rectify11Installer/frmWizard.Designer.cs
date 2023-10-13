namespace Rectify11Installer
{
	sealed partial class FrmWizard
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.headerText = new AeroWizard.ThemedLabel();
			this.navBackButton = new Rectify11Installer.Controls.NavigationButton();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.nextButton = new Rectify11Installer.Controls.WinUIButton();
			this.cancelButton = new Rectify11Installer.Controls.WinUIButton();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.sideImage = new System.Windows.Forms.PictureBox();
			this.navPane = new Rectify11Installer.Controls.TabControlWithoutHeader();
			Core.TabPages.wlcmPage = new Rectify11Installer.Controls.DarkAwareTabPage();
			Core.TabPages.eulPage = new Rectify11Installer.Controls.DarkAwareTabPage();
			Core.TabPages.debPage = new Rectify11Installer.Controls.DarkAwareTabPage();
			Core.TabPages.installPage = new Rectify11Installer.Controls.DarkAwareTabPage();
			Core.TabPages.themePage = new Rectify11Installer.Controls.DarkAwareTabPage();
			Core.TabPages.cmenupage = new Rectify11Installer.Controls.DarkAwareTabPage();
			Core.TabPages.summaryPage = new Rectify11Installer.Controls.DarkAwareTabPage();
			Core.TabPages.progressPage = new Rectify11Installer.Controls.DarkAwareTabPage();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.progressLabel = new Controls.DarkAwareLabel();
			this.versionLabel = new Controls.DarkAwareLabel();
			this.timer = new System.Windows.Forms.Timer();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sideImage)).BeginInit();
			this.navPane.SuspendLayout();
			this.SuspendLayout();
			// 
			// headerText
			// 
			this.headerText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.headerText.BackColor = System.Drawing.Color.Transparent;
			this.headerText.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.headerText.ForeColor = System.Drawing.Color.Black;
			this.headerText.Location = new System.Drawing.Point(48, 0);
			this.headerText.Name = "headerText";
			this.headerText.Size = new System.Drawing.Size(578, 45);
			this.headerText.TabIndex = 1;
			this.headerText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// navBackButton
			// 
			this.navBackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.navBackButton.BackColor = System.Drawing.Color.Transparent;
			this.navBackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.navBackButton.Location = new System.Drawing.Point(3, 3);
			this.navBackButton.Name = "navBackButton";
			this.navBackButton.NavigationButtonType = Rectify11Installer.Controls.NavigationButtonType.Backward;
			this.navBackButton.Size = new System.Drawing.Size(39, 39);
			this.navBackButton.TabIndex = 2;
			this.navBackButton.Text = "navigationButton2";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.headerText, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.navBackButton, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 45);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// nextButton
			// 
			this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.nextButton.BackColor = System.Drawing.Color.Transparent;
			this.nextButton.ButtonText = Rectify11Installer.Strings.Rectify11.buttonNext;
            this.nextButton.ForeColor = System.Drawing.Color.Black;
			this.nextButton.Location = new System.Drawing.Point(456, 9);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(72, 27);
			this.nextButton.TabIndex = 1;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.BackColor = System.Drawing.Color.Transparent;
			this.cancelButton.ButtonText = Rectify11Installer.Strings.Rectify11.buttonCancel;
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
			this.cancelButton.Location = new System.Drawing.Point(534, 9);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(72, 27);
			this.cancelButton.TabIndex = 2;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
			this.tableLayoutPanel2.Controls.Add(this.nextButton, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.cancelButton, 2, 1);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 402);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 3F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(644, 45);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// sideImage
			// 
			this.sideImage.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.sideImage.BackColor = System.Drawing.Color.Transparent;
			this.sideImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.sideImage.Location = new System.Drawing.Point(49, 72);
			this.sideImage.Name = "sideImage";
			this.sideImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.sideImage.Size = new System.Drawing.Size(178, 194);
			this.sideImage.TabIndex = 0;
			this.sideImage.TabStop = false;
			// 
			// navPane
			// 
			this.navPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.navPane.Controls.Add(Core.TabPages.wlcmPage);
			this.navPane.Controls.Add(Core.TabPages.eulPage);
			this.navPane.Controls.Add(Core.TabPages.debPage);
			this.navPane.Controls.Add(Core.TabPages.installPage);
			this.navPane.Controls.Add(Core.TabPages.themePage);
			this.navPane.Controls.Add(Core.TabPages.cmenupage);
			this.navPane.Controls.Add(Core.TabPages.summaryPage);
			this.navPane.Controls.Add(Core.TabPages.progressPage);
			this.navPane.ItemSize = new System.Drawing.Size(10, 20);
			this.navPane.Location = new System.Drawing.Point(280, 0);
			this.navPane.Multiline = true;
			this.navPane.Name = "navPane";
			this.navPane.SelectedIndex = 0;
			this.navPane.Size = new System.Drawing.Size(361, 342);
			this.navPane.KeyDown += tabControl1_KeyDown;
            this.navPane.TabIndex = 1;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56F));
			this.tableLayoutPanel3.Controls.Add(this.sideImage, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.navPane, 1, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 48);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(644, 348);
			this.tableLayoutPanel3.TabIndex = 3;
			// 
			// wlcmPage
			// 
			Core.TabPages.wlcmPage.BackColor = System.Drawing.Color.White;
			Core.TabPages.wlcmPage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.wlcmPage.Location = new System.Drawing.Point(0, 44);
			Core.TabPages.wlcmPage.Name = "wlcmPage";
			Core.TabPages.wlcmPage.Size = new System.Drawing.Size(361, 284);
			Core.TabPages.wlcmPage.TabIndex = 8;
			Core.TabPages.wlcmPage.Text = "Welcome";
			// 
			// eulPage
			// 
			Core.TabPages.eulPage.BackColor = System.Drawing.Color.White;
			Core.TabPages.eulPage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.eulPage.Location = new System.Drawing.Point(0, 44);
			Core.TabPages.eulPage.Name = "eulPage";
			Core.TabPages.eulPage.Size = new System.Drawing.Size(365, 284);
			Core.TabPages.eulPage.TabIndex = 9;
			Core.TabPages.eulPage.Text = "Eula";
			// 
			// debPage
			// 
			Core.TabPages.debPage.BackColor = System.Drawing.Color.White;
			Core.TabPages.debPage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.debPage.Location = new System.Drawing.Point(0, 44);
			Core.TabPages.debPage.Name = "debPage";
			Core.TabPages.debPage.Size = new System.Drawing.Size(365, 284);
			Core.TabPages.debPage.TabIndex = 9;
			Core.TabPages.debPage.Text = "Debug";
			// 
			// installPage
			// 
			Core.TabPages.installPage.BackColor = System.Drawing.Color.White;
			Core.TabPages.installPage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.installPage.Location = new System.Drawing.Point(4, 44);
			Core.TabPages.installPage.Name = "installPage";
			Core.TabPages.installPage.Size = new System.Drawing.Size(333, 284);
			Core.TabPages.installPage.TabIndex = 2;
			Core.TabPages.installPage.Text = "Install";
			// 
			// themePage
			// 
			Core.TabPages.themePage.BackColor = System.Drawing.Color.White;
			Core.TabPages.themePage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.themePage.Location = new System.Drawing.Point(4, 44);
			Core.TabPages.themePage.Name = "themePage";
			Core.TabPages.themePage.Size = new System.Drawing.Size(333, 284);
			Core.TabPages.themePage.TabIndex = 3;
			Core.TabPages.themePage.Text = "Theme";
			// 
			// cmenupage
			// 
			Core.TabPages.cmenupage.BackColor = System.Drawing.Color.White;
			Core.TabPages.cmenupage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.cmenupage.Location = new System.Drawing.Point(4, 44);
			Core.TabPages.cmenupage.Name = "cmenupage";
			Core.TabPages.cmenupage.Size = new System.Drawing.Size(333, 284);
			Core.TabPages.cmenupage.TabIndex = 4;
			Core.TabPages.cmenupage.Text = "CMenu";
			// 
			// summaryPage
			// 
			Core.TabPages.summaryPage.BackColor = System.Drawing.Color.White;
			Core.TabPages.summaryPage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.summaryPage.Location = new System.Drawing.Point(4, 44);
			Core.TabPages.summaryPage.Name = "summaryPage";
			Core.TabPages.summaryPage.Size = new System.Drawing.Size(333, 284);
			Core.TabPages.summaryPage.TabIndex = 5;
			Core.TabPages.summaryPage.Text = "Summary";
			// 
			// progressPage
			// 
			Core.TabPages.progressPage.BackColor = System.Drawing.Color.White;
			Core.TabPages.progressPage.ForeColor = System.Drawing.Color.Black;
			Core.TabPages.progressPage.Location = new System.Drawing.Point(4, 44);
			Core.TabPages.progressPage.Name = "progressPage";
			Core.TabPages.progressPage.Size = new System.Drawing.Size(333, 284);
			Core.TabPages.progressPage.TabIndex = 6;
			Core.TabPages.progressPage.Text = "Progress";
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.BackColor = System.Drawing.Color.Transparent;
			this.versionLabel.ForeColor = System.Drawing.Color.Black;
			this.versionLabel.Location = new System.Drawing.Point(6, 426);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(52, 15);
			this.versionLabel.TabIndex = 3;
			this.versionLabel.Text = Rectify11Installer.Strings.Rectify11.Version;
            this.versionLabel.Click += VersionLabel_Click;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Image = global::Rectify11Installer.Properties.Resources.installprogress;
			this.pictureBox1.Location = new System.Drawing.Point(15, 390);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(40, 40);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			//
			// progressLabel
			//
			this.progressLabel.AutoSize = true;
			this.progressLabel.BackColor = System.Drawing.Color.Transparent;
			this.progressLabel.ForeColor = System.Drawing.Color.Black;
			this.progressLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.progressLabel.Location = new System.Drawing.Point(50, 400);
			this.progressLabel.Name = "progressLabel";
			this.progressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.progressLabel.Size = new System.Drawing.Size(300, 40);
			this.progressLabel.TabIndex = 3;
			this.progressLabel.Visible = false;
			//
			// timer
			//
			this.timer.Interval = 40;
			this.timer.Tick += Timer1_Tick;
			// 
			// frmWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(644, 447);
			this.Controls.Add(this.progressLabel);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = Rectify11Installer.Strings.Rectify11.r11;

            this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmWizard";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = Rectify11Installer.Strings.Rectify11.Title;
            this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sideImage)).EndInit();
			this.navPane.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private AeroWizard.ThemedLabel headerText;
		private Controls.NavigationButton navBackButton;
		public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.PictureBox sideImage;
		private Controls.TabControlWithoutHeader navPane;
		public Controls.DarkAwareLabel versionLabel;
		public Controls.DarkAwareLabel progressLabel;
		public Controls.WinUIButton nextButton;
		public System.Windows.Forms.PictureBox pictureBox1;
		private Controls.WinUIButton cancelButton;
		public System.Windows.Forms.Timer timer;
	}
}

