
namespace Rectify11ControlCenter
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deskImg = new System.Windows.Forms.PictureBox();
            this.r11Ver = new System.Windows.Forms.Label();
            this.themeApplied = new System.Windows.Forms.Label();
            this.pcname = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.OSname = new System.Windows.Forms.Label();
            this.previewImage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deskImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 632);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 19);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "chexBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = global::Rectify11ControlCenter.Properties.Resources.PreviewPane;
            this.panel1.Controls.Add(this.deskImg);
            this.panel1.Controls.Add(this.r11Ver);
            this.panel1.Controls.Add(this.themeApplied);
            this.panel1.Controls.Add(this.pcname);
            this.panel1.Controls.Add(this.username);
            this.panel1.Controls.Add(this.OSname);
            this.panel1.Controls.Add(this.previewImage);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(899, 213);
            this.panel1.TabIndex = 1;
            // 
            // deskImg
            // 
            this.deskImg.Location = new System.Drawing.Point(41, 43);
            this.deskImg.Name = "deskImg";
            this.deskImg.Size = new System.Drawing.Size(152, 110);
            this.deskImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.deskImg.TabIndex = 6;
            this.deskImg.TabStop = false;
            // 
            // r11Ver
            // 
            this.r11Ver.AutoSize = true;
            this.r11Ver.Location = new System.Drawing.Point(224, 138);
            this.r11Ver.Name = "r11Ver";
            this.r11Ver.Size = new System.Drawing.Size(100, 15);
            this.r11Ver.TabIndex = 5;
            this.r11Ver.Text = "Rectify11 Version:";
            // 
            // themeApplied
            // 
            this.themeApplied.AutoSize = true;
            this.themeApplied.Location = new System.Drawing.Point(224, 113);
            this.themeApplied.Name = "themeApplied";
            this.themeApplied.Size = new System.Drawing.Size(47, 15);
            this.themeApplied.TabIndex = 4;
            this.themeApplied.Text = "Theme:";
            // 
            // pcname
            // 
            this.pcname.AutoSize = true;
            this.pcname.Location = new System.Drawing.Point(224, 89);
            this.pcname.Name = "pcname";
            this.pcname.Size = new System.Drawing.Size(57, 15);
            this.pcname.TabIndex = 3;
            this.pcname.Text = "PC Name";
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Location = new System.Drawing.Point(224, 65);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(63, 15);
            this.username.TabIndex = 2;
            this.username.Text = "Username:";
            // 
            // OSname
            // 
            this.OSname.AutoSize = true;
            this.OSname.Location = new System.Drawing.Point(224, 42);
            this.OSname.Name = "OSname";
            this.OSname.Size = new System.Drawing.Size(67, 15);
            this.OSname.TabIndex = 1;
            this.OSname.Text = "OS Version:";
            // 
            // previewImage
            // 
            this.previewImage.Image = global::Rectify11ControlCenter.Properties.Resources.PreviewImg;
            this.previewImage.Location = new System.Drawing.Point(25, 26);
            this.previewImage.Name = "previewImage";
            this.previewImage.Size = new System.Drawing.Size(185, 163);
            this.previewImage.TabIndex = 0;
            this.previewImage.TabStop = false;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(900, 664);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(863, 660);
            this.Name = "frmSettings";
            this.Text = "Rectify11 Control Center";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deskImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label OSname;
        private System.Windows.Forms.PictureBox previewImage;
        private System.Windows.Forms.Label r11Ver;
        private System.Windows.Forms.Label themeApplied;
        private System.Windows.Forms.Label pcname;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.PictureBox deskImg;
    }
}

