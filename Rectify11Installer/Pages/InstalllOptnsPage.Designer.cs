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
            this.chkExploderPatcher = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkExploderPatcher
            // 
            this.chkExploderPatcher.AutoSize = true;
            this.chkExploderPatcher.Checked = true;
            this.chkExploderPatcher.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExploderPatcher.ForeColor = System.Drawing.Color.White;
            this.chkExploderPatcher.Location = new System.Drawing.Point(8, 11);
            this.chkExploderPatcher.Name = "chkExploderPatcher";
            this.chkExploderPatcher.Size = new System.Drawing.Size(143, 19);
            this.chkExploderPatcher.TabIndex = 0;
            this.chkExploderPatcher.Text = "Install ExplorerPatcher";
            this.chkExploderPatcher.UseVisualStyleBackColor = true;
            // 
            // InstalllOptnsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkExploderPatcher);
            this.Name = "InstalllOptnsPage";
            this.WizardTopText = "Installation Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox chkExploderPatcher;
    }
}
