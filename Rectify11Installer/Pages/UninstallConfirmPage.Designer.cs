namespace Rectify11Installer.Pages
{
    partial class UninstallConfirmPage
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
            this.pnlContent = new Rectify11Installer.Controls.DarkAwareFlowLayoutPanel();
            this.chkExplorerPatcher = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.chkRemoveThemes = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.chkRestoreWallpaper = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.chkRestoreWinVer = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.lblChoose = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.chkExplorerPatcher);
            this.pnlContent.Controls.Add(this.chkRemoveThemes);
            this.pnlContent.Controls.Add(this.chkRestoreWallpaper);
            this.pnlContent.Controls.Add(this.chkRestoreWinVer);
            this.pnlContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlContent.Location = new System.Drawing.Point(190, 38);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(405, 277);
            this.pnlContent.TabIndex = 17;
            this.pnlContent.WrapContents = false;
            // 
            // chkExplorerPatcher
            // 
            this.chkExplorerPatcher.AutoSize = true;
            this.chkExplorerPatcher.ForeColor = System.Drawing.Color.White;
            this.chkExplorerPatcher.Location = new System.Drawing.Point(3, 3);
            this.chkExplorerPatcher.Name = "chkExplorerPatcher";
            this.chkExplorerPatcher.Size = new System.Drawing.Size(158, 19);
            this.chkExplorerPatcher.TabIndex = 16;
            this.chkExplorerPatcher.Text = "Uninstall ExplorerPatcher";
            this.chkExplorerPatcher.UseVisualStyleBackColor = true;
            // 
            // chkRemoveThemes
            // 
            this.chkRemoveThemes.AutoSize = true;
            this.chkRemoveThemes.Checked = true;
            this.chkRemoveThemes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemoveThemes.ForeColor = System.Drawing.Color.White;
            this.chkRemoveThemes.Location = new System.Drawing.Point(3, 28);
            this.chkRemoveThemes.Name = "chkRemoveThemes";
            this.chkRemoveThemes.Size = new System.Drawing.Size(196, 19);
            this.chkRemoveThemes.TabIndex = 0;
            this.chkRemoveThemes.Text = "Remove Themetool and Themes";
            this.chkRemoveThemes.UseVisualStyleBackColor = true;
            // 
            // chkRestoreWallpaper
            // 
            this.chkRestoreWallpaper.AutoSize = true;
            this.chkRestoreWallpaper.Checked = true;
            this.chkRestoreWallpaper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestoreWallpaper.ForeColor = System.Drawing.Color.White;
            this.chkRestoreWallpaper.Location = new System.Drawing.Point(3, 53);
            this.chkRestoreWallpaper.Name = "chkRestoreWallpaper";
            this.chkRestoreWallpaper.Size = new System.Drawing.Size(164, 19);
            this.chkRestoreWallpaper.TabIndex = 17;
            this.chkRestoreWallpaper.Text = "Restore default wallpapers";
            this.chkRestoreWallpaper.UseVisualStyleBackColor = true;
            // 
            // chkRestoreWinVer
            // 
            this.chkRestoreWinVer.AutoSize = true;
            this.chkRestoreWinVer.Checked = true;
            this.chkRestoreWinVer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRestoreWinVer.ForeColor = System.Drawing.Color.White;
            this.chkRestoreWinVer.Location = new System.Drawing.Point(3, 78);
            this.chkRestoreWinVer.Name = "chkRestoreWinVer";
            this.chkRestoreWinVer.Size = new System.Drawing.Size(239, 19);
            this.chkRestoreWinVer.TabIndex = 18;
            this.chkRestoreWinVer.Text = "Restore default \"About Windows\" applet";
            this.chkRestoreWinVer.UseVisualStyleBackColor = true;
            // 
            // lblChoose
            // 
            this.lblChoose.AutoSize = true;
            this.lblChoose.ForeColor = System.Drawing.Color.White;
            this.lblChoose.Location = new System.Drawing.Point(186, 15);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(133, 15);
            this.lblChoose.TabIndex = 16;
            this.lblChoose.Text = "Choose what to remove";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Rectify11Installer.Properties.Resources.rectify11Installer;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(2, 89);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // UninstallConfirmPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblChoose);
            this.Controls.Add(this.pictureBox1);
            this.Name = "UninstallConfirmPage";
            this.WizardTopText = "Choose what to uninstall";
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.DarkAwareFlowLayoutPanel pnlContent;
        private Controls.DarkAwareCheckBox chkExplorerPatcher;
        private Controls.DarkAwareCheckBox chkRemoveThemes;
        private Controls.DarkAwareCheckBox chkRestoreWallpaper;
        private Controls.DarkAwareCheckBox chkRestoreWinVer;
        private Label lblChoose;
        private PictureBox pictureBox1;
    }
}
