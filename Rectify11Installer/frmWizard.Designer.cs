namespace Rectify11Installer
{
    partial class frmWizard
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
            System.ComponentModel.ComponentResourceManager resources = new global::Rectify11Installer.Core.SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.headerText = new AeroWizard.ThemedLabel();
            this.navBackButton = new Rectify11Installer.Controls.NavigationButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.nextButton = new Rectify11Installer.Controls.WinUIButton();
            this.cancelButton = new Rectify11Installer.Controls.WinUIButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.sideImage = new System.Windows.Forms.PictureBox();
            this.rebootButton = new();
            this.navPane = new Rectify11Installer.Controls.TabControlWithoutHeader();
            this.wlcmPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.eulPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.expPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.installPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.themePage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.epPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.summaryPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.progressPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.rebootPage = new Rectify11Installer.Controls.DarkAwareTabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressLabel = new Controls.DarkAwareLabel();
            this.versionLabel = new Controls.DarkAwareLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sideImage)).BeginInit();
            this.navPane.SuspendLayout();
            this.SuspendLayout();
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
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.BackColor = System.Drawing.Color.Transparent;
            this.nextButton.ButtonText = resources.GetString("buttonNext");
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
            this.cancelButton.ButtonText = resources.GetString("buttonCancel");
            this.cancelButton.ForeColor = System.Drawing.Color.Black;
            this.cancelButton.Location = new System.Drawing.Point(534, 9);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(72, 27);
            this.cancelButton.TabIndex = 2;
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
            // sideImage
            // 
            this.sideImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sideImage.BackColor = System.Drawing.Color.Transparent;
            this.sideImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.sideImage.Location = new System.Drawing.Point(49, 72);
            this.sideImage.Name = "sideImage";
            this.sideImage.Size = new System.Drawing.Size(178, 194);
            this.sideImage.TabIndex = 0;
            this.sideImage.TabStop = false;
            // 
            // navPane
            // 
            this.navPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.navPane.Controls.Add(this.wlcmPage);
            this.navPane.Controls.Add(this.eulPage);
            this.navPane.Controls.Add(this.expPage);
            this.navPane.Controls.Add(this.installPage);
            this.navPane.Controls.Add(this.themePage);
            this.navPane.Controls.Add(this.epPage);
            this.navPane.Controls.Add(this.summaryPage);
            this.navPane.Controls.Add(this.progressPage);
            this.navPane.Controls.Add(this.rebootPage);
            this.navPane.ItemSize = new System.Drawing.Size(10, 20);
            this.navPane.Location = new System.Drawing.Point(280, 0);
            this.navPane.Multiline = true;
            this.navPane.Name = "navPane";
            this.navPane.SelectedIndex = 0;
            this.navPane.Size = new System.Drawing.Size(361, 342);
            this.navPane.TabIndex = 1;
            // 
            // wlcmPage
            // 
            this.wlcmPage.BackColor = System.Drawing.Color.White;
            this.wlcmPage.ForeColor = System.Drawing.Color.Black;
            this.wlcmPage.Location = new System.Drawing.Point(0, 44);
            this.wlcmPage.Name = "wlcmPage";
            this.wlcmPage.Size = new System.Drawing.Size(361, 284);
            this.wlcmPage.TabIndex = 8;
            this.wlcmPage.Text = "Welcome";
            // 
            // eulPage
            // 
            this.eulPage.BackColor = System.Drawing.Color.White;
            this.eulPage.ForeColor = System.Drawing.Color.Black;
            this.eulPage.Location = new System.Drawing.Point(0, 44);
            this.eulPage.Name = "eulPage";
            this.eulPage.Size = new System.Drawing.Size(365, 284);
            this.eulPage.TabIndex = 9;
            this.eulPage.Text = "Eula";
            // 
            // expPage
            // 
            this.expPage.BackColor = System.Drawing.Color.White;
            this.expPage.ForeColor = System.Drawing.Color.Black;
            this.expPage.Location = new System.Drawing.Point(0, 44);
            this.expPage.Name = "expPage";
            this.expPage.Size = new System.Drawing.Size(365, 284);
            this.expPage.TabIndex = 9;
            this.expPage.Text = "Experimental";
            // 
            // installPage
            // 
            this.installPage.BackColor = System.Drawing.Color.White;
            this.installPage.ForeColor = System.Drawing.Color.Black;
            this.installPage.Location = new System.Drawing.Point(4, 44);
            this.installPage.Name = "installPage";
            this.installPage.Size = new System.Drawing.Size(333, 284);
            this.installPage.TabIndex = 2;
            this.installPage.Text = "Install";
            // 
            // themePage
            // 
            this.themePage.BackColor = System.Drawing.Color.White;
            this.themePage.ForeColor = System.Drawing.Color.Black;
            this.themePage.Location = new System.Drawing.Point(4, 44);
            this.themePage.Name = "themePage";
            this.themePage.Size = new System.Drawing.Size(333, 284);
            this.themePage.TabIndex = 3;
            this.themePage.Text = "Theme";
            // 
            // epPage
            // 
            this.epPage.BackColor = System.Drawing.Color.White;
            this.epPage.ForeColor = System.Drawing.Color.Black;
            this.epPage.Location = new System.Drawing.Point(4, 44);
            this.epPage.Name = "epPage";
            this.epPage.Size = new System.Drawing.Size(333, 284);
            this.epPage.TabIndex = 4;
            this.epPage.Text = "Ep";
            // 
            // summaryPage
            // 
            this.summaryPage.BackColor = System.Drawing.Color.White;
            this.summaryPage.ForeColor = System.Drawing.Color.Black;
            this.summaryPage.Location = new System.Drawing.Point(4, 44);
            this.summaryPage.Name = "summaryPage";
            this.summaryPage.Size = new System.Drawing.Size(333, 284);
            this.summaryPage.TabIndex = 5;
            this.summaryPage.Text = "Summary";
            // 
            // progressPage
            // 
            this.progressPage.BackColor = System.Drawing.Color.White;
            this.progressPage.ForeColor = System.Drawing.Color.Black;
            this.progressPage.Location = new System.Drawing.Point(4, 44);
            this.progressPage.Name = "progressPage";
            this.progressPage.Size = new System.Drawing.Size(333, 284);
            this.progressPage.TabIndex = 6;
            this.progressPage.Text = "Progress";
            // 
            // rebootPage
            // 
            this.rebootPage.BackColor = System.Drawing.Color.White;
            this.rebootPage.ForeColor = System.Drawing.Color.Black;
            this.rebootPage.Location = new System.Drawing.Point(4, 44);
            this.rebootPage.Name = "rebootPage";
            this.rebootPage.Size = new System.Drawing.Size(333, 284);
            this.rebootPage.TabIndex = 7;
            this.rebootPage.Text = "Reboot";
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
            this.versionLabel.Text = resources.GetString("Version");
            //
            // rebootButton
            //
            rebootButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            rebootButton.BackColor = System.Drawing.Color.Transparent;
            rebootButton.ButtonText = resources.GetString("buttonReboot");
            rebootButton.ForeColor = System.Drawing.Color.Black;
            rebootButton.Location = new System.Drawing.Point(534, 390);
            rebootButton.Name = "rebootButton";
            rebootButton.Size = new System.Drawing.Size(78, 32);
            rebootButton.TabIndex = 1;
            rebootButton.Visible = false;
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
            // frmWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(644, 447);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.rebootButton);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("r11")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = resources.GetString("Title");
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
        private Controls.DarkAwareTabPage installPage;
        private Controls.DarkAwareTabPage themePage;
        private Controls.DarkAwareTabPage epPage;
        private Controls.DarkAwareTabPage summaryPage;
        private Controls.DarkAwareTabPage progressPage;
        private Controls.DarkAwareTabPage rebootPage;
        private Controls.DarkAwareTabPage wlcmPage;
        private Controls.DarkAwareTabPage eulPage;
        private Controls.DarkAwareTabPage expPage;
        private Controls.DarkAwareLabel versionLabel;
        private Controls.WinUIButton rebootButton;
        private Controls.DarkAwareLabel progressLabel;
        public Controls.WinUIButton nextButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Controls.WinUIButton cancelButton;
    }
}

