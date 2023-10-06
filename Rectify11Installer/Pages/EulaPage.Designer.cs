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
			this.label1 = new Controls.DarkAwareLabel();
			this.richTextBox1 = new Controls.DarkAwareRichTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(-3, 5);
			this.label1.Name = "label1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.Size = new System.Drawing.Size(361, 40);
			this.label1.TabIndex = 0;
			this.label1.Text = Rectify11Installer.Strings.Rectify11.eulaTitle;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox1.Location = new System.Drawing.Point(4, 48);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(326, 290);
			this.richTextBox1.TabIndex = 1; 
			this.richTextBox1.Rtf = @"{\rtf1\ansi\pard{\*\pn\pnlvlbody\pnf0\pnindent0\pnstart1\pndec{\pntxta.}}\fi-360\li360\f0\fs20{\pntext\f0 1.\tab}This software is provided 'as-is', without any express or implied warranty. In NO event will the author be held liable for any damages arising from the use of this software.\par{\pntext\f0 2.\tab}Rectify11 is free to use by anyone, but you cannot sell it. You cannot bundle this product as a part of another product without written permission from the author.\par{\pntext\f0 3.\tab}You cannot claim that you made the software.\par{\pntext\f0 4.\tab}This notice may not be removed or altered from any distribution.\par\pard\par{\pntext\f0\space}Copyright \'a9 Microsoft Corporation and the Rectify11 Team.\par{\pntext\f0\space}We are NOT affiliated with Microsoft Corporation in ANY way. This is a community made project.\par}";
			// 
			// EulaPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.label1);
			this.Name = "EulaPage";
			this.WizardHeader = Rectify11Installer.Strings.Rectify11.eulaPageHeader;
            this.SideImage = global::Rectify11Installer.Properties.Resources.eula;
			this.HeaderVisible = true;
			this.FooterVisible = true;
			this.UpdateFrame = true;
			this.IsWelcomePage = false;
			this.NextButtonEnabled = true;
			this.NextButtonText = Rectify11Installer.Strings.Rectify11.buttonAgree;
            this.Page = Rectify11Installer.Core.TabPages.eulPage;
			this.ResumeLayout(false);
		}

		#endregion

		private Controls.DarkAwareLabel label1;
		private Controls.DarkAwareRichTextBox richTextBox1;
	}
}
