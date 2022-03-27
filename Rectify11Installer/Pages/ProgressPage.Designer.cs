namespace Rectify11Installer.Pages
{
    partial class ProgressPage
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
            this.customProgressBar1 = new Rectify11Installer.Controls.CustomProgressBar();
            this.SuspendLayout();
            // 
            // customProgressBar1
            // 
            this.customProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.customProgressBar1.Location = new System.Drawing.Point(0, 307);
            this.customProgressBar1.Name = "customProgressBar1";
            this.customProgressBar1.Size = new System.Drawing.Size(596, 23);
            this.customProgressBar1.TabIndex = 14;
            this.customProgressBar1.Value = 50;
            // 
            // ProgressPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customProgressBar1);
            this.Name = "ProgressPage";
            this.WizardTopText = "Please wait";
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.CustomProgressBar customProgressBar1;
    }
}
