namespace Rectify11Installer.Pages
{
	partial class InstallOptnsPage
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
			System.ComponentModel.ComponentResourceManager resources = new global::Rectify11Installer.Core.SingleAssemblyComponentResourceManager(typeof(Strings.Rectify11));
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Basic");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Advanced");
			treeNode1 = new System.Windows.Forms.TreeNode(resources.GetString("optionIcons"), new System.Windows.Forms.TreeNode[]{
			treeNode8,
			treeNode9});
			treeNode7 = new System.Windows.Forms.TreeNode(resources.GetString("optionThemes"));
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Winver");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ExplorerPatcher");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Shell");
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Accent Colorizer");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Rectify11 Wallpapers");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode(resources.GetString("optionExtra"), new System.Windows.Forms.TreeNode[] {
			treeNode2,
			treeNode3,
			treeNode4,
			treeNode10,
			treeNode5});
			this.groupBox1 = new Rectify11Installer.Controls.GroupBox();
			this.label1 = new Rectify11Installer.Controls.DarkAwareLabel();
			this.treeView1 = new Rectify11Installer.Controls.DarkAwareTreeView();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.groupBox1.Location = new System.Drawing.Point(0, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(345, 294);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.Text = "groupBox1";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(0, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(361, 40);
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.TabIndex = 1;
			this.label1.Text = resources.GetString("installChoiceDescription");
			// 
			// treeView1
			// 
			this.treeView1.BackColor = System.Drawing.Color.White;
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.CheckBoxes = true;
			this.treeView1.ForeColor = System.Drawing.Color.Black;
			this.treeView1.Location = new System.Drawing.Point(1, 58);
			this.treeView1.Name = "treeView1";
			treeNode1.Name = "sysIconsNode";
			treeNode1.Text = resources.GetString("optionIcons");
			treeNode2.Name = "winverNode";
			treeNode2.Text = "Winver";
			treeNode3.Name = "epNode";
			treeNode3.Text = "ExplorerPatcher";
			treeNode4.Name = "shellNode";
			treeNode4.Text = "Shell";
			treeNode5.Name = "wallpapersNode";
			treeNode5.Text = resources.GetString("optionWallpaper");
			treeNode6.Name = "extraNode";
			treeNode6.Text = resources.GetString("optionExtra");
			treeNode7.Name = "themeNode";
			treeNode7.Text = resources.GetString("optionThemes");
			treeNode8.Name = "basicNode";
			treeNode8.Text = "Basic";
			treeNode9.Name = "advancedNode";
			treeNode9.Text = "Advanced";
			treeNode10.Name = "asdfNode";
			treeNode10.Text = "Accent Colorizer";
			this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			treeNode1,
			treeNode7,
			treeNode6});
			this.treeView1.Size = new System.Drawing.Size(342, 270);
			this.treeView1.TabIndex = 2;
			this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
			// 
			// InstallOptnsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Name = "InstallOptnsPage";
			this.SideImage = global::Rectify11Installer.Properties.Resources.installoptns;
			this.WizardHeader = resources.GetString("installChoiceTitle");
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.GroupBox groupBox1;
		private Controls.DarkAwareLabel label1;
		private Controls.DarkAwareTreeView treeView1;
		public System.Windows.Forms.TreeNode treeNode7;
        public System.Windows.Forms.TreeNode treeNode1;
	}
}
