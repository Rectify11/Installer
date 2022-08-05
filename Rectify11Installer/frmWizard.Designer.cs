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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWizard));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.headerText = new System.Windows.Forms.Label();
            this.navBackButton = new Rectify11Installer.Controls.NavigationButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.nextButton = new Rectify11Installer.Controls.WinUIButton();
            this.cancelButton = new Rectify11Installer.Controls.WinUIButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.sideImage = new System.Windows.Forms.PictureBox();
            this.navPane = new Rectify11Installer.Controls.TabControlWithoutHeader();
            this.wlcmPage = new Controls.DarkAwareTabPage();
            this.eulPage = new Controls.DarkAwareTabPage();
            this.installPage = new Controls.DarkAwareTabPage();
            this.themePage = new Controls.DarkAwareTabPage();
            this.epPage = new Controls.DarkAwareTabPage();
            this.summaryPage = new Controls.DarkAwareTabPage();
            this.progressPage = new Controls.DarkAwareTabPage();
            this.rebootPage = new Controls.DarkAwareTabPage();
            this.versionLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sideImage)).BeginInit();
            this.navPane.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.headerText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.navBackButton, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(624, 45);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // headerText
            // 
            this.headerText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.headerText.BackColor = System.Drawing.Color.Transparent;
            this.headerText.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel2.Controls.Add(this.nextButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cancelButton, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 389);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(624, 47);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.BackColor = System.Drawing.Color.Transparent;
            this.nextButton.ButtonText = global::Rectify11Installer.Strings.Rectify11.buttonNext;
            this.nextButton.ForeColor = System.Drawing.Color.White;
            this.nextButton.Location = new System.Drawing.Point(456, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(72, 29);
            this.nextButton.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.ButtonText = global::Rectify11Installer.Strings.Rectify11.buttonCancel;
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(534, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(72, 29);
            this.cancelButton.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.55128F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.44872F));
            this.tableLayoutPanel3.Controls.Add(this.sideImage, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.navPane, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 48);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 338F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(624, 338);
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
            this.navPane.Controls.Add(this.installPage);
            this.navPane.Controls.Add(this.themePage);
            this.navPane.Controls.Add(this.epPage);
            this.navPane.Controls.Add(this.summaryPage);
            this.navPane.Controls.Add(this.progressPage);
            this.navPane.Controls.Add(this.rebootPage);
            this.navPane.ItemSize = new System.Drawing.Size(10, 20);
            this.navPane.Location = new System.Drawing.Point(280, 3);
            this.navPane.Multiline = true;
            this.navPane.Name = "navPane";
            this.navPane.SelectedIndex = 0;
            this.navPane.Size = new System.Drawing.Size(341, 332);
            this.navPane.TabIndex = 1;
            // 
            // wlcmPage
            // 
            this.wlcmPage.Location = new System.Drawing.Point(4, 44);
            this.wlcmPage.Name = "wlcmPage";
            this.wlcmPage.Size = new System.Drawing.Size(333, 284);
            this.wlcmPage.TabIndex = 8;
            this.wlcmPage.Text = "Welcome";
            this.wlcmPage.UseVisualStyleBackColor = true;
            // 
            // eulPage
            // 
            this.eulPage.Location = new System.Drawing.Point(4, 44);
            this.eulPage.Name = "eulPage";
            this.eulPage.Size = new System.Drawing.Size(333, 284);
            this.eulPage.TabIndex = 9;
            this.eulPage.Text = "Eula";
            this.eulPage.UseVisualStyleBackColor = true;
            // 
            // installPage
            // 
            this.installPage.Location = new System.Drawing.Point(4, 44);
            this.installPage.Name = "installPage";
            this.installPage.Size = new System.Drawing.Size(333, 284);
            this.installPage.TabIndex = 2;
            this.installPage.Text = "Install";
            this.installPage.UseVisualStyleBackColor = true;
            // 
            // themePage
            // 
            this.themePage.Location = new System.Drawing.Point(4, 44);
            this.themePage.Name = "themePage";
            this.themePage.Size = new System.Drawing.Size(333, 284);
            this.themePage.TabIndex = 3;
            this.themePage.Text = "Theme";
            this.themePage.UseVisualStyleBackColor = true;
            // 
            // epPage
            // 
            this.epPage.Location = new System.Drawing.Point(4, 44);
            this.epPage.Name = "epPage";
            this.epPage.Size = new System.Drawing.Size(333, 284);
            this.epPage.TabIndex = 4;
            this.epPage.Text = "Ep";
            this.epPage.UseVisualStyleBackColor = true;
            // 
            // summaryPage
            // 
            this.summaryPage.Location = new System.Drawing.Point(4, 44);
            this.summaryPage.Name = "summaryPage";
            this.summaryPage.Size = new System.Drawing.Size(333, 284);
            this.summaryPage.TabIndex = 5;
            this.summaryPage.Text = "Summary";
            this.summaryPage.UseVisualStyleBackColor = true;
            // 
            // progressPage
            // 
            this.progressPage.Location = new System.Drawing.Point(4, 44);
            this.progressPage.Name = "progressPage";
            this.progressPage.Size = new System.Drawing.Size(333, 284);
            this.progressPage.TabIndex = 6;
            this.progressPage.Text = "Progress";
            this.progressPage.UseVisualStyleBackColor = true;
            // 
            // rebootPage
            // 
            this.rebootPage.Location = new System.Drawing.Point(4, 44);
            this.rebootPage.Name = "rebootPage";
            this.rebootPage.Size = new System.Drawing.Size(333, 284);
            this.rebootPage.TabIndex = 7;
            this.rebootPage.Text = "Reboot";
            this.rebootPage.UseVisualStyleBackColor = true;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(6, 416);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(51, 15);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = Strings.Rectify11.Version;
            // 
            // frmWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(624, 437);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rectify11 Installer";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sideImage)).EndInit();
            this.navPane.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label headerText;
        private Controls.NavigationButton navBackButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Controls.WinUIButton nextButton;
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
        private Controls.WinUIButton cancelButton;
        private System.Windows.Forms.Label versionLabel;
    }
}

