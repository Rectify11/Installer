using Rectify11Installer.Controls;

namespace Rectify11Installer.Pages
{
    partial class RebootPage
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
            this.components = new System.ComponentModel.Container();
            this.progressBar1 = new Rectify11Installer.Controls.CustomProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.winuiButton1 = new Rectify11Installer.Controls.WinUIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.progressBar1.Error = true;
            this.progressBar1.Location = new System.Drawing.Point(12, 220);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(612, 23);
            this.progressBar1.TabIndex = 18;
            this.progressBar1.Value = 100;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // winuiButton1
            // 
            this.winuiButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.winuiButton1.BackColor = System.Drawing.Color.Transparent;
            this.winuiButton1.ButtonText = "Restart now";
            this.winuiButton1.ForeColor = System.Drawing.Color.White;
            this.winuiButton1.Location = new System.Drawing.Point(549, 355);
            this.winuiButton1.Name = "winuiButton1";
            this.winuiButton1.Size = new System.Drawing.Size(75, 23);
            this.winuiButton1.TabIndex = 19;
            this.winuiButton1.Click += new System.EventHandler(this.winuiButton1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-1, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(638, 35);
            this.label2.TabIndex = 21;
            this.label2.Text = "Your PC now needs to restart to begin applying the changes.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::Rectify11Installer.Properties.Resources.rectify11Installer;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(229, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 150);
            this.pictureBox2.TabIndex = 20;
            this.pictureBox2.TabStop = false;
            // 
            // RebootPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.winuiButton1);
            this.Controls.Add(this.progressBar1);
            this.Name = "RebootPage";
            this.WizardTopText = "Stage 1 completed";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private CustomProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private Controls.WinUIButton winuiButton1;
        private Label label2;
        private PictureBox pictureBox2;
    }
}
