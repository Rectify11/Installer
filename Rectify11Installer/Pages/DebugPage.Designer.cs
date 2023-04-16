
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
			this.SuspendLayout();
			checkBox1 = new Controls.DarkAwareCheckBox();
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
			this.checkBox1.Click += CheckBox1_Click;
			//
			// DebugPage
			//
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.checkBox1);
			this.Name = "DebugPage";
			this.WizardHeader = "Debug";
			this.HeaderVisible = true;
			this.FooterVisible = true;
			this.Page = Rectify11Installer.Core.TabPages.debPage;
			this.UpdateFrame = true;
			this.IsWelcomePage = false;
			this.NextButtonEnabled = false;
			this.SideImage = global::Rectify11Installer.Properties.Resources.incomplete;
			this.ResumeLayout(false);
		}

		#endregion
		private Controls.DarkAwareCheckBox checkBox1;
	}
}
