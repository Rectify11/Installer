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
            this.pnlContent = new Rectify11Installer.Controls.DarkAwareFlowLayoutPanel();
            this.chkExplorerPatcher = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.chkWallpaper = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.chkWinVer = new Rectify11Installer.Controls.DarkAwareCheckBox();
            this.radSafe = new Rectify11Installer.Controls.DarkAwareRadioButton();
            this.lblSafeDesc = new System.Windows.Forms.Label();
            this.radFull = new Rectify11Installer.Controls.DarkAwareRadioButton();
            this.lblFullDesc = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlContent.SuspendLayout();
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
            this.chkThemes.Size = new System.Drawing.Size(102, 19);
            this.chkThemes.TabIndex = 0;
            this.chkThemes.Text = "Install Themes";
            this.chkThemes.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Rectify11Installer.Properties.Resources.rectify11Installer;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(-3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(644, 150);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Controls.Add(this.chkExplorerPatcher);
            this.pnlContent.Controls.Add(this.chkThemes);
            this.pnlContent.Controls.Add(this.chkWallpaper);
            this.pnlContent.Controls.Add(this.chkWinVer);
            this.pnlContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlContent.Location = new System.Drawing.Point(99, 219);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(155, 181);
            this.pnlContent.TabIndex = 14;
            // 
            // chkExplorerPatcher
            // 
            this.chkExplorerPatcher.AutoSize = true;
            this.chkExplorerPatcher.Checked = true;
            this.chkExplorerPatcher.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExplorerPatcher.ForeColor = System.Drawing.Color.White;
            this.chkExplorerPatcher.Location = new System.Drawing.Point(3, 3);
            this.chkExplorerPatcher.Name = "chkExplorerPatcher";
            this.chkExplorerPatcher.Size = new System.Drawing.Size(142, 19);
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
            this.chkWinVer.Size = new System.Drawing.Size(146, 19);
            this.chkWinVer.TabIndex = 18;
            this.chkWinVer.Text = "Install Rectify11 winver";
            this.chkWinVer.UseVisualStyleBackColor = true;
            // 
            // radSafe
            // 
            this.radSafe.AutoSize = true;
            this.radSafe.Checked = true;
            this.radSafe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.radSafe.ForeColor = System.Drawing.Color.White;
            this.radSafe.Location = new System.Drawing.Point(269, 220);
            this.radSafe.Name = "radSafe";
            this.radSafe.Size = new System.Drawing.Size(235, 19);
            this.radSafe.TabIndex = 1;
            this.radSafe.TabStop = true;
            this.radSafe.Text = "Safe installation (recommended)";
            this.radSafe.UseVisualStyleBackColor = true;
            // 
            // lblSafeDesc
            // 
            this.lblSafeDesc.ForeColor = System.Drawing.Color.White;
            this.lblSafeDesc.Location = new System.Drawing.Point(269, 243);
            this.lblSafeDesc.Name = "lblSafeDesc";
            this.lblSafeDesc.Size = new System.Drawing.Size(289, 30);
            this.lblSafeDesc.TabIndex = 2;
            this.lblSafeDesc.Text = "This option patches files that won\'t create issues with software that checks file" +
    " integrity";
            // 
            // radFull
            // 
            this.radFull.AutoSize = true;
            this.radFull.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.radFull.ForeColor = System.Drawing.Color.White;
            this.radFull.Location = new System.Drawing.Point(269, 276);
            this.radFull.Name = "radFull";
            this.radFull.Size = new System.Drawing.Size(124, 19);
            this.radFull.TabIndex = 3;
            this.radFull.TabStop = true;
            this.radFull.Text = "Full installation";
            this.radFull.UseVisualStyleBackColor = true;
            // 
            // lblFullDesc
            // 
            this.lblFullDesc.ForeColor = System.Drawing.Color.White;
            this.lblFullDesc.Location = new System.Drawing.Point(269, 299);
            this.lblFullDesc.Name = "lblFullDesc";
            this.lblFullDesc.Size = new System.Drawing.Size(273, 95);
            this.lblFullDesc.TabIndex = 4;
            this.lblFullDesc.Text = "This option patches many system files, resulting in a more consistent experience." +
    " This may cause issues with:\r\n - Games with Anti-Cheat\r\n - Software that checks " +
    "file integrity";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(638, 23);
            this.label1.TabIndex = 16;
            this.label1.Text = "Choose what to install";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-3, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(640, 21);
            this.label2.TabIndex = 15;
            this.label2.Text = "You can choose what or will not be Rectified.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InstalllOptnsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblFullDesc);
            this.Controls.Add(this.radFull);
            this.Controls.Add(this.lblSafeDesc);
            this.Controls.Add(this.radSafe);
            this.Controls.Add(this.pictureBox1);
            this.Name = "InstalllOptnsPage";
            this.Size = new System.Drawing.Size(640, 436);
            this.WizardTopText = "Choose what to install";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkAwareCheckBox chkThemes;
        private PictureBox pictureBox1;
        private DarkAwareFlowLayoutPanel pnlContent;
        private DarkAwareRadioButton radSafe;
        private Label lblSafeDesc;
        private DarkAwareRadioButton radFull;
        private Label lblFullDesc;
        private DarkAwareCheckBox chkExplorerPatcher;
        private DarkAwareCheckBox chkWallpaper;
        private DarkAwareCheckBox chkWinVer;
        private Label label1;
        private Label label2;
    }
}
