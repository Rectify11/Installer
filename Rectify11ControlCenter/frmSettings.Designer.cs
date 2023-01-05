
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
            this.themesSec = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.miscSec = new System.Windows.Forms.GroupBox();
            this.themePrev = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deskImg = new System.Windows.Forms.PictureBox();
            this.r11Ver = new System.Windows.Forms.Label();
            this.themeApplied = new System.Windows.Forms.Label();
            this.pcname = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.OSname = new System.Windows.Forms.Label();
            this.previewImage = new System.Windows.Forms.PictureBox();
            this.themesSec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.themePrev)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deskImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(710, 570);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 19);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "chexBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // themesSec
            // 
            this.themesSec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.themesSec.Controls.Add(this.button1);
            this.themesSec.Controls.Add(this.label2);
            this.themesSec.Controls.Add(this.themePrev);
            this.themesSec.Controls.Add(this.label1);
            this.themesSec.Controls.Add(this.checkBox2);
            this.themesSec.Controls.Add(this.comboBox1);
            this.themesSec.Location = new System.Drawing.Point(28, 245);
            this.themesSec.Name = "themesSec";
            this.themesSec.Size = new System.Drawing.Size(359, 200);
            this.themesSec.TabIndex = 2;
            this.themesSec.TabStop = false;
            this.themesSec.Text = "groupBox1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 139);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 24);
            this.button1.TabIndex = 5;
            this.button1.Text = "butt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Preview:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select theme:";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(98, 168);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(75, 19);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "chexBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(98, 139);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(151, 23);
            this.comboBox1.TabIndex = 0;
            // 
            // miscSec
            // 
            this.miscSec.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.miscSec.Location = new System.Drawing.Point(411, 245);
            this.miscSec.Name = "miscSec";
            this.miscSec.Size = new System.Drawing.Size(357, 200);
            this.miscSec.TabIndex = 3;
            this.miscSec.TabStop = false;
            this.miscSec.Text = "groupBox2";
            // 
            // themePrev
            // 
            this.themePrev.Image = global::Rectify11ControlCenter.Properties.Resources.placeholder;
            this.themePrev.Location = new System.Drawing.Point(98, 25);
            this.themePrev.Name = "themePrev";
            this.themePrev.Size = new System.Drawing.Size(151, 95);
            this.themePrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.themePrev.TabIndex = 3;
            this.themePrev.TabStop = false;
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
            this.panel1.Size = new System.Drawing.Size(796, 206);
            this.panel1.TabIndex = 1;
            // 
            // deskImg
            // 
            this.deskImg.Location = new System.Drawing.Point(37, 36);
            this.deskImg.Name = "deskImg";
            this.deskImg.Size = new System.Drawing.Size(158, 111);
            this.deskImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.deskImg.TabIndex = 6;
            this.deskImg.TabStop = false;
            // 
            // r11Ver
            // 
            this.r11Ver.AutoSize = true;
            this.r11Ver.Location = new System.Drawing.Point(224, 141);
            this.r11Ver.Name = "r11Ver";
            this.r11Ver.Size = new System.Drawing.Size(100, 15);
            this.r11Ver.TabIndex = 5;
            this.r11Ver.Text = "Rectify11 Version:";
            // 
            // themeApplied
            // 
            this.themeApplied.AutoSize = true;
            this.themeApplied.Location = new System.Drawing.Point(224, 116);
            this.themeApplied.Name = "themeApplied";
            this.themeApplied.Size = new System.Drawing.Size(47, 15);
            this.themeApplied.TabIndex = 4;
            this.themeApplied.Text = "Theme:";
            // 
            // pcname
            // 
            this.pcname.AutoSize = true;
            this.pcname.Location = new System.Drawing.Point(224, 92);
            this.pcname.Name = "pcname";
            this.pcname.Size = new System.Drawing.Size(57, 15);
            this.pcname.TabIndex = 3;
            this.pcname.Text = "PC Name";
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Location = new System.Drawing.Point(224, 68);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(63, 15);
            this.username.TabIndex = 2;
            this.username.Text = "Username:";
            // 
            // OSname
            // 
            this.OSname.AutoSize = true;
            this.OSname.Location = new System.Drawing.Point(224, 45);
            this.OSname.Name = "OSname";
            this.OSname.Size = new System.Drawing.Size(67, 15);
            this.OSname.TabIndex = 1;
            this.OSname.Text = "OS Version:";
            // 
            // previewImage
            // 
            this.previewImage.Image = global::Rectify11ControlCenter.Properties.Resources.previewIcon;
            this.previewImage.Location = new System.Drawing.Point(23, 11);
            this.previewImage.Name = "previewImage";
            this.previewImage.Size = new System.Drawing.Size(185, 185);
            this.previewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewImage.TabIndex = 0;
            this.previewImage.TabStop = false;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(797, 601);
            this.Controls.Add(this.miscSec);
            this.Controls.Add(this.themesSec);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(813, 640);
            this.Name = "frmSettings";
            this.Text = "Rectify11 Control Center";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.themesSec.ResumeLayout(false);
            this.themesSec.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.themePrev)).EndInit();
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
        private System.Windows.Forms.GroupBox themesSec;
        private System.Windows.Forms.GroupBox miscSec;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox themePrev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox deskImg;
    }
}

