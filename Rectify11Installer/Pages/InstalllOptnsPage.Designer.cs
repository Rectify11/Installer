using Rectify11Installer.Controls;

namespace Rectify11Installer.Pages
{
    partial class InstalllOptnsPage
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
            this.chkThemes = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblChoose = new System.Windows.Forms.Label();
            this.pnlContent = new Rectify11Installer.Controls.DarkAwareFlowLayoutPanel();
            this.chkExplorerPatcher = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.chkWallpaper = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.chkWinVer = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.lblInstallMode = new System.Windows.Forms.Label();
            this.pnlInstallOptions = new Rectify11Installer.Controls.DarkAwareFlowLayoutPanel();
            this.radSafe = new Rectify11Installer.Controls.DarkAwareRadioButton();
            this.lblSafeDesc = new System.Windows.Forms.Label();
            this.radFull = new Rectify11Installer.Controls.DarkAwareRadioButton();
            this.lblFullDesc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlInstallOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkThemes
            // 
            this.chkThemes.AutoSize = true;
            this.chkThemes.Checked = true;
            this.chkThemes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThemes.ForeColor = System.Drawing.Color.White;
            this.chkThemes.Location = new System.Drawing.Point(3, 28);
            this.chkThemes.Name = "chkThemes";
            this.chkThemes.Size = new System.Drawing.Size(101, 19);
            this.chkThemes.TabIndex = 0;
            this.chkThemes.Text = "Install Themes";
            this.chkThemes.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Rectify11Installer.Properties.Resources.rectify11Installer;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(3, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // lblChoose
            // 
            this.lblChoose.AutoSize = true;
            this.lblChoose.ForeColor = System.Drawing.Color.White;
            this.lblChoose.Location = new System.Drawing.Point(187, 8);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(243, 15);
            this.lblChoose.TabIndex = 13;
            this.lblChoose.Text = "You can choose what or will not be Rectified.";
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.chkExplorerPatcher);
            this.pnlContent.Controls.Add(this.chkThemes);
            this.pnlContent.Controls.Add(this.chkWallpaper);
            this.pnlContent.Controls.Add(this.chkWinVer);
            this.pnlContent.Controls.Add(this.lblInstallMode);
            this.pnlContent.Controls.Add(this.pnlInstallOptions);
            this.pnlContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlContent.Location = new System.Drawing.Point(191, 31);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(405, 277);
            this.pnlContent.TabIndex = 14;
            this.pnlContent.WrapContents = false;
            // 
            // chkExplorerPatcher
            // 
            this.chkExplorerPatcher.AutoSize = true;
            this.chkExplorerPatcher.Checked = true;
            this.chkExplorerPatcher.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExplorerPatcher.ForeColor = System.Drawing.Color.White;
            this.chkExplorerPatcher.Location = new System.Drawing.Point(3, 3);
            this.chkExplorerPatcher.Name = "chkExplorerPatcher";
            this.chkExplorerPatcher.Size = new System.Drawing.Size(143, 19);
            this.chkExplorerPatcher.TabIndex = 16;
            this.chkExplorerPatcher.Text = "Install ExplorerPatcher";
            this.chkExplorerPatcher.UseVisualStyleBackColor = true;
            // 
            // chkWallpaper
            // 
            this.chkWallpaper.AutoSize = true;
            this.chkWallpaper.Checked = true;
            this.chkWallpaper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWallpaper.ForeColor = System.Drawing.Color.White;
            this.chkWallpaper.Location = new System.Drawing.Point(3, 53);
            this.chkWallpaper.Name = "chkWallpaper";
            this.chkWallpaper.Size = new System.Drawing.Size(116, 19);
            this.chkWallpaper.TabIndex = 17;
            this.chkWallpaper.Text = "Install wallpapers";
            this.chkWallpaper.UseVisualStyleBackColor = true;
            // 
            // chkWinVer
            // 
            this.chkWinVer.AutoSize = true;
            this.chkWinVer.Checked = true;
            this.chkWinVer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWinVer.ForeColor = System.Drawing.Color.White;
            this.chkWinVer.Location = new System.Drawing.Point(3, 78);
            this.chkWinVer.Name = "chkWinVer";
            this.chkWinVer.Size = new System.Drawing.Size(242, 19);
            this.chkWinVer.TabIndex = 18;
            this.chkWinVer.Text = "Install Rectify11 \"About Windows\" applet";
            this.chkWinVer.UseVisualStyleBackColor = true;
            // 
            // lblInstallMode
            // 
            this.lblInstallMode.AutoSize = true;
            this.lblInstallMode.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInstallMode.ForeColor = System.Drawing.Color.White;
            this.lblInstallMode.Location = new System.Drawing.Point(3, 100);
            this.lblInstallMode.Name = "lblInstallMode";
            this.lblInstallMode.Size = new System.Drawing.Size(103, 13);
            this.lblInstallMode.TabIndex = 0;
            this.lblInstallMode.Text = "Installation mode: ";
            // 
            // pnlInstallOptions
            // 
            this.pnlInstallOptions.AutoScroll = true;
            this.pnlInstallOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInstallOptions.Controls.Add(this.radSafe);
            this.pnlInstallOptions.Controls.Add(this.lblSafeDesc);
            this.pnlInstallOptions.Controls.Add(this.radFull);
            this.pnlInstallOptions.Controls.Add(this.lblFullDesc);
            this.pnlInstallOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlInstallOptions.Location = new System.Drawing.Point(3, 116);
            this.pnlInstallOptions.Name = "pnlInstallOptions";
            this.pnlInstallOptions.Size = new System.Drawing.Size(397, 128);
            this.pnlInstallOptions.TabIndex = 15;
            this.pnlInstallOptions.WrapContents = false;
            // 
            // radSafe
            // 
            this.radSafe.AutoSize = true;
            this.radSafe.Checked = true;
            this.radSafe.ForeColor = System.Drawing.Color.White;
            this.radSafe.Location = new System.Drawing.Point(3, 3);
            this.radSafe.Name = "radSafe";
            this.radSafe.Size = new System.Drawing.Size(197, 19);
            this.radSafe.TabIndex = 1;
            this.radSafe.TabStop = true;
            this.radSafe.Text = "Safe installation (recommended)";
            this.radSafe.UseVisualStyleBackColor = true;
            // 
            // lblSafeDesc
            // 
            this.lblSafeDesc.AutoSize = true;
            this.lblSafeDesc.ForeColor = System.Drawing.Color.White;
            this.lblSafeDesc.Location = new System.Drawing.Point(3, 25);
            this.lblSafeDesc.Name = "lblSafeDesc";
            this.lblSafeDesc.Size = new System.Drawing.Size(361, 30);
            this.lblSafeDesc.TabIndex = 2;
            this.lblSafeDesc.Text = "This option patches files that won\'t create issues with software that checks file" +
    " integrity";
            // 
            // radFull
            // 
            this.radFull.AutoSize = true;
            this.radFull.ForeColor = System.Drawing.Color.White;
            this.radFull.Location = new System.Drawing.Point(3, 58);
            this.radFull.Name = "radFull";
            this.radFull.Size = new System.Drawing.Size(105, 19);
            this.radFull.TabIndex = 3;
            this.radFull.TabStop = true;
            this.radFull.Text = "Full installation";
            this.radFull.UseVisualStyleBackColor = true;
            // 
            // lblFullDesc
            // 
            this.lblFullDesc.ForeColor = System.Drawing.Color.White;
            this.lblFullDesc.Location = new System.Drawing.Point(3, 80);
            this.lblFullDesc.Name = "lblFullDesc";
            this.lblFullDesc.Size = new System.Drawing.Size(273, 95);
            this.lblFullDesc.TabIndex = 4;
            this.lblFullDesc.Text = "This option patches many system files, resulting in a more consistent experience." +
    " This may cause issues with:\r\n - Games with Anti-Cheat\r\n - Software that checks " +
    "file integrity";
            // 
            // InstalllOptnsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblChoose);
            this.Controls.Add(this.pictureBox1);
            this.Name = "InstalllOptnsPage";
            this.WizardTopText = "Choose what to install";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlInstallOptions.ResumeLayout(false);
            this.pnlInstallOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkAwareCheckBox chkThemes;
        private PictureBox pictureBox1;
        private Label lblChoose;
        private DarkAwareFlowLayoutPanel pnlContent;
        private DarkAwareRadioButton radSafe;
        private Label lblSafeDesc;
        private DarkAwareRadioButton radFull;
        private Label lblFullDesc;
        private DarkAwareFlowLayoutPanel pnlInstallOptions;
        private Label lblInstallMode;
        private DarkAwareCheckBox chkExplorerPatcher;
        private DarkAwareCheckBox chkWallpaper;
        private DarkAwareCheckBox chkWinVer;
    }
}
