using Rectify11Installer.Controls;

namespace Rectify11Installer.Pages
{
    partial class WelcomePage
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new Rectify11Installer.Controls.DarkAwareFlowLayoutPanel();
            this.cmbInstallNOW = new Rectify11Installer.Controls.FakeCommandLink();
            this.cmbUninstall = new Rectify11Installer.Controls.FakeCommandLink();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Rectify11Installer.Properties.Resources.rectify11Installer;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(234, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.cmbInstallNOW);
            this.flowLayoutPanel1.Controls.Add(this.cmbUninstall);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(151, 204);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(316, 126);
            this.flowLayoutPanel1.TabIndex = 13;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // cmbInstallNOW
            // 
            this.cmbInstallNOW.BackColor = System.Drawing.Color.Transparent;
            this.cmbInstallNOW.Location = new System.Drawing.Point(3, 3);
            this.cmbInstallNOW.Name = "cmbInstallNOW";
            this.cmbInstallNOW.Note = "Rectify your current Windows 11 installation";
            this.cmbInstallNOW.Size = new System.Drawing.Size(295, 52);
            this.cmbInstallNOW.TabIndex = 7;
            this.cmbInstallNOW.Text = "Update or Install Now";
            // 
            // cmbUninstall
            // 
            this.cmbUninstall.BackColor = System.Drawing.Color.Transparent;
            this.cmbUninstall.Enabled = false;
            this.cmbUninstall.Location = new System.Drawing.Point(3, 61);
            this.cmbUninstall.Name = "cmbUninstall";
            this.cmbUninstall.Note = "Restores the original Windows 11 look.";
            this.cmbUninstall.Size = new System.Drawing.Size(295, 52);
            this.cmbUninstall.TabIndex = 8;
            this.cmbUninstall.Text = "Uninstall Rectify11";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI Variable Small Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(592, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Please select what you would like to do";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(592, 23);
            this.label1.TabIndex = 14;
            this.label1.Text = "Rectify11 Installer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "WelcomePage";
            this.WizardShowTitle = false;
            this.WizardTopText = "Welcome to Rectify11 Installer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox pictureBox1;
        private DarkAwareFlowLayoutPanel flowLayoutPanel1;
        private FakeCommandLink cmbInstallNOW;
        private FakeCommandLink cmbUninstall;
        private Label label2;
        private Label label1;
    }
}
