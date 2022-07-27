using Rectify11Installer.Controls;

namespace Rectify11Installer.Pages
{
    partial class EulaPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxEx1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Rectify11Installer.Properties.Resources.eula;
            this.pictureBox1.Location = new System.Drawing.Point(54, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(299, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 41);
            this.label1.TabIndex = 20;
            this.label1.Text = "Before you can install Rectify11, you must agree to this license agreement:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.BackColor = System.Drawing.Color.Black;
            this.richTextBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxEx1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBoxEx1.ForeColor = System.Drawing.Color.White;
            this.richTextBoxEx1.Location = new System.Drawing.Point(300, 56);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.ReadOnly = true;
            this.richTextBoxEx1.Size = new System.Drawing.Size(313, 315);
            this.richTextBoxEx1.TabIndex = 21;
            this.richTextBoxEx1.Text = "";
            this.richTextBoxEx1.TextChanged += new System.EventHandler(this.richTextBoxEx1_TextChanged);
            // 
            // EulaPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.richTextBoxEx1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "EulaPage";
            this.Size = new System.Drawing.Size(640, 436);
            this.WizardTopText = "License Agreement";
            this.Load += new System.EventHandler(this.EulaPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private PictureBox pictureBox1;
        private Label label1;
        private RichTextBox richTextBoxEx1;
    }
}
