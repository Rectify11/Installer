namespace Rectify11Installer.Pages
{
    partial class ThemeChoicePage
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
            this.lightPreview = new System.Windows.Forms.PictureBox();
            this.darkPreview = new System.Windows.Forms.PictureBox();
            this.blackPreview = new System.Windows.Forms.PictureBox();
            this.themeTitle = new Rectify11Installer.Controls.DarkAwareLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lightPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.darkPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // lightPreview
            // 
            this.lightPreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.lightPreview;
            this.lightPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.lightPreview.Location = new System.Drawing.Point(7, 44);
            this.lightPreview.Name = "lightPreview";
            this.lightPreview.Size = new System.Drawing.Size(129, 78);
            this.lightPreview.TabIndex = 0;
            this.lightPreview.TabStop = false;
            // 
            // darkPreview
            // 
            this.darkPreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.darkPreview;
            this.darkPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.darkPreview.Location = new System.Drawing.Point(167, 44);
            this.darkPreview.Name = "darkPreview";
            this.darkPreview.Size = new System.Drawing.Size(129, 78);
            this.darkPreview.TabIndex = 1;
            this.darkPreview.TabStop = false;
            // 
            // blackPreview
            // 
            this.blackPreview.BackgroundImage = global::Rectify11Installer.Properties.Resources.blackPreview;
            this.blackPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.blackPreview.Location = new System.Drawing.Point(7, 153);
            this.blackPreview.Name = "blackPreview";
            this.blackPreview.Size = new System.Drawing.Size(129, 78);
            this.blackPreview.TabIndex = 2;
            this.blackPreview.TabStop = false;
            // 
            // themeTitle
            // 
            this.themeTitle.AutoSize = true;
            this.themeTitle.BackColor = System.Drawing.Color.White;
            this.themeTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.themeTitle.ForeColor = System.Drawing.Color.Black;
            this.themeTitle.Location = new System.Drawing.Point(3, 0);
            this.themeTitle.Name = "themeTitle";
            this.themeTitle.Size = new System.Drawing.Size(190, 20);
            this.themeTitle.TabIndex = 3;
            this.themeTitle.Text = Strings.Rectify11.themeChoiceTitle;
            // 
            // ThemeChoicePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.themeTitle);
            this.Controls.Add(this.blackPreview);
            this.Controls.Add(this.darkPreview);
            this.Controls.Add(this.lightPreview);
            this.Name = "ThemeChoicePage";
            this.SideImage = global::Rectify11Installer.Properties.Resources.themepage;
            this.WizardHeader = "Personalize your experience";
            ((System.ComponentModel.ISupportInitialize)(this.lightPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.darkPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blackPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox lightPreview;
        private System.Windows.Forms.PictureBox darkPreview;
        private System.Windows.Forms.PictureBox blackPreview;
        private Controls.DarkAwareLabel themeTitle;
    }
}
