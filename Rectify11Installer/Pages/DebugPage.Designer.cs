
namespace Rectify11Installer.Pages
{
	partial class DebugPage
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
			this.checkBox1 = new Rectify11Installer.Controls.DarkAwareCheckBox();
			this.checkBox2 = new Rectify11Installer.Controls.DarkAwareCheckBox();
			this.darkAwareLabel1 = new Rectify11Installer.Controls.DarkAwareLabel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.darkAwareLabel2 = new Rectify11Installer.Controls.DarkAwareLabel();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.winUIButton1 = new Rectify11Installer.Controls.WinUIButton();
			this.SuspendLayout();
			// 
			// checkBox1
			// 
			this.checkBox1.ForeColor = System.Drawing.Color.Black;
			this.checkBox1.Location = new System.Drawing.Point(4, 10);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(288, 19);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Disable update check";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.ForeColor = System.Drawing.Color.Black;
			this.checkBox2.Location = new System.Drawing.Point(4, 30);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(288, 19);
			this.checkBox2.TabIndex = 0;
			this.checkBox2.Text = "Disable phase2";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// darkAwareLabel1
			// 
			this.darkAwareLabel1.AutoSize = true;
			this.darkAwareLabel1.BackColor = System.Drawing.Color.White;
			this.darkAwareLabel1.ForeColor = System.Drawing.Color.Black;
			this.darkAwareLabel1.Location = new System.Drawing.Point(4, 66);
			this.darkAwareLabel1.Name = "darkAwareLabel1";
			this.darkAwareLabel1.Size = new System.Drawing.Size(96, 15);
			this.darkAwareLabel1.TabIndex = 1;
			this.darkAwareLabel1.Text = "Execute function";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(4, 84);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(105, 23);
			this.textBox1.TabIndex = 2;
			// 
			// darkAwareLabel2
			// 
			this.darkAwareLabel2.AutoSize = true;
			this.darkAwareLabel2.BackColor = System.Drawing.Color.White;
			this.darkAwareLabel2.ForeColor = System.Drawing.Color.Black;
			this.darkAwareLabel2.Location = new System.Drawing.Point(113, 88);
			this.darkAwareLabel2.Name = "darkAwareLabel2";
			this.darkAwareLabel2.Size = new System.Drawing.Size(17, 15);
			this.darkAwareLabel2.TabIndex = 3;
			this.darkAwareLabel2.Text = "in";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(134, 84);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(142, 23);
			this.textBox2.TabIndex = 4;
			// 
			// winUIButton1
			// 
			this.winUIButton1.BackColor = System.Drawing.Color.Transparent;
			this.winUIButton1.ButtonText = "Execute";
			this.winUIButton1.ForeColor = System.Drawing.Color.Black;
			this.winUIButton1.Location = new System.Drawing.Point(280, 85);
			this.winUIButton1.Name = "winUIButton1";
			this.winUIButton1.Size = new System.Drawing.Size(64, 23);
			this.winUIButton1.TabIndex = 5;
			this.winUIButton1.Click += WinUIButton1_Click;
            // 
            // DebugPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.winUIButton1);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.darkAwareLabel2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.darkAwareLabel1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.checkBox2);
			this.FooterVisible = true;
			this.HeaderVisible = true;
			this.Name = "DebugPage";
			this.NextButtonEnabled = false;
			this.Page = Rectify11Installer.Core.TabPages.debPage;
			this.SideImage = global::Rectify11Installer.Properties.Resources.incomplete;
			this.UpdateFrame = true;
			this.IsWelcomePage = false;
			this.WizardHeader = "Debug";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion
		private Controls.DarkAwareCheckBox checkBox1;
		private Controls.DarkAwareCheckBox checkBox2;
		private Controls.DarkAwareLabel darkAwareLabel1;
		private System.Windows.Forms.TextBox textBox1;
		private Controls.DarkAwareLabel darkAwareLabel2;
		private System.Windows.Forms.TextBox textBox2;
		private Controls.WinUIButton winUIButton1;
	}
}
