namespace Rectify11Installer.Pages
{
    partial class ConfirmOperationPage
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
            this.lblOperation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblOperation
            // 
            this.lblOperation.AutoSize = true;
            this.lblOperation.Font = new System.Drawing.Font("Segoe UI Variable Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOperation.ForeColor = System.Drawing.Color.White;
            this.lblOperation.Location = new System.Drawing.Point(8, 16);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(322, 21);
            this.lblOperation.TabIndex = 13;
            this.lblOperation.Text = "You are about to do the following operation: ";
            // 
            // ConfirmOperationPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblOperation);
            this.Name = "ConfirmOperationPage";
            this.WizardTopText = "Summary";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblOperation;
    }
}
