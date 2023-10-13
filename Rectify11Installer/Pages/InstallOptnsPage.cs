using Rectify11Installer.Core;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
	public partial class InstallOptnsPage : WizardPage
	{
		#region Variables
		private readonly FrmWizard _frmWizard;
		private bool ExtrasSel = false;
		bool idleinit;
		#endregion
		#region Main
		public InstallOptnsPage(FrmWizard Frm)
		{
			_frmWizard = Frm;
			InitializeComponent();
			Application.Idle += Application_Idle;
			treeView1.AfterSelect += TreeView1_AfterSelect;
			NavigationHelper.OnNavigate += NavigationHelper_OnNavigate;
		}

		private void NavigationHelper_OnNavigate(object sender, EventArgs e)
		{
			if ((WizardPage)sender == RectifyPages.InstallOptnsPage)
			{
				_frmWizard.nextButton.Enabled = Variables.IsItemsSelected;
			}
		}

		void Application_Idle(object sender, EventArgs e)
		{
			if (!idleinit)
			{
				var list = PatchesParser.GetAll();
				var ok = list.Items;
				var basicNode = treeView1.Nodes[0].Nodes[0];
				var advNode = treeView1.Nodes[0].Nodes[1];
				var themeNode = treeView1.Nodes[1];
				var extra = treeView1.Nodes[2];
				var shell = treeView1.Nodes[2].Nodes[0];
				var gad = treeView1.Nodes[2].Nodes[1];
				var asdf = treeView1.Nodes[2].Nodes[2];
				var wall = treeView1.Nodes[2].Nodes[3];
				var av = treeView1.Nodes[2].Nodes[4];
				UpdateListView(ok, basicNode, advNode);
				if (basicNode.Nodes.Count == 0)
					treeView1.Nodes.Remove(basicNode);
				if (advNode.Nodes.Count == 0)
					treeView1.Nodes.Remove(advNode);
				if (treeNode1.Nodes.Count == 0)
					treeView1.Nodes.Remove(treeNode1);

				// ugh
				bool skip = false;
				if (Helper.CheckIfUpdate()) skip = true;

				// make a list with installed stuff
				// and check installed stuff
				if (!skip)
				{
					if (Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified")))
					{
						themeNode.Checked = true;
						InstallOptions.origList.Add("themeNode");
					}

					if (Directory.Exists(Path.Combine(Variables.Windir, "nilesoft")))
					{
						shell.Checked = true;
						InstallOptions.origList.Add("shellNode");
					}

					if (Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
					{
						asdf.Checked = true;
						InstallOptions.origList.Add("asdfNode");
					}

					if (File.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified", "img41.jpg")))
					{
						wall.Checked = true;
						InstallOptions.origList.Add("wallpapersNode");
					}

					if (Directory.Exists(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures")))
					{
						av.Checked = true;
						InstallOptions.origList.Add("useravNode");
					}

					if (File.Exists(Path.Combine(Variables.progfiles, "Windows Sidebar", "sidebar.exe")))
					{
						gad.Checked = true;
						InstallOptions.origList.Add("gadgetsNode");
					}
					if (shell.Checked || asdf.Checked || wall.Checked || av.Checked || gad.Checked)
					{
						shell.Ancestors()[0].Checked = true;
					}
				}
				idleinit = true;
			}
		}

		#endregion
		#region Private Methods
		private static void UpdateListView(PatchesPatch[] patch, TreeNode basicNode, TreeNode advNode)
		{
			string path = Path.Combine(Variables.r11Folder, "backup");
			if (Helper.CheckIfUpdate()) path = Variables.r11Folder;
			for (var i = 0; i < patch.Length; i++)
			{
				if (!patch[i].HardlinkTarget.Contains("%diag%"))
				{
					var newpath = Helper.FixString(patch[i].HardlinkTarget, !string.IsNullOrWhiteSpace(patch[i].x86));
					if (File.Exists(newpath))
					{
						if (newpath.Contains(".mun"))
						{
							var tree = basicNode.Nodes.Add(patch[i].Mui);
							if (File.Exists(Path.Combine(path, patch[i].Mui)))
							{
								tree.Checked = true;
								tree.Ancestors()[0].Checked = true;
								tree.Ancestors()[0].Ancestors()[0].Checked = true;
								InstallOptions.origList.Add(patch[i].Mui);
							}
						}
						else
						{
							var tree = advNode.Nodes.Add(patch[i].Mui);
							if (File.Exists(Path.Combine(path, patch[i].Mui)))
							{
								tree.Checked = true;
								tree.Ancestors()[0].Checked = true;
								InstallOptions.origList.Add(patch[i].Mui);
							}
						}
					}

				}
				if (patch[i].HardlinkTarget.Contains("%diag%"))
				{
					var name = patch[i].Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
					var newpath = Helper.FixString(patch[i].HardlinkTarget, false);
					if (File.Exists(newpath))
					{
						var tree = advNode.Nodes.Add(patch[i].Mui);
						if (File.Exists(Path.Combine(path, "Diag", name)))
						{
							tree.Checked = true;
							tree.Ancestors()[0].Checked = true;
							tree.Ancestors()[0].Ancestors()[0].Checked = true;
							InstallOptions.origList.Add(patch[i].Mui);
						}
					}
				}
			}
		}
		private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (e.Action != TreeViewAction.Unknown)
			{
				if (e.Node.Name == "basicNode")
				{
					e.Node.Descendants().ToList().ForEach(x =>
					{
						x.Checked = e.Node.Checked;
						if (e.Node.Checked)
						{
							if (!InstallOptions.origList.Contains(x.Text))
							{
								InstallOptions.iconsList.Add(x.Text);
							}
							UninstallOptions.uninstIconsList.Remove(x.Text);
							Variables.InstallIcons = true;
						}
						else
						{
							InstallOptions.iconsList.Remove(x.Text);
							if (InstallOptions.origList.Contains(x.Text))
							{
								UninstallOptions.uninstIconsList.Add(x.Text);
							}
							Variables.InstallIcons = false;
						}
					});
				}
				if (e.Node.Name == "sysIconsNode")
				{
					e.Node.Descendants().ToList().ForEach(x =>
					{
						x.Checked = e.Node.Checked;
						if (e.Node.Checked && (x.Name != "basicNode") && (x.Name != "advancedNode"))
						{
							if (!InstallOptions.origList.Contains(x.Text))
							{
								InstallOptions.iconsList.Add(x.Text);
							}
							UninstallOptions.uninstIconsList.Remove(x.Text);
							Variables.InstallIcons = true;
						}
						else if ((x.Name != "basicNode") && (x.Name != "advancedNode"))
						{
							InstallOptions.iconsList.Remove(x.Text);
							if (InstallOptions.origList.Contains(x.Text))
							{
								UninstallOptions.uninstIconsList.Add(x.Text);
							}
							Variables.InstallIcons = false;
						}
					});
				}
				if (e.Node.Name == "advancedNode")
				{
					e.Node.Descendants().ToList().ForEach(x =>
					{
						x.Checked = e.Node.Checked;
						if (e.Node.Checked)
						{
							if (!InstallOptions.origList.Contains(x.Text))
							{
								InstallOptions.iconsList.Add(x.Text);
							}
							UninstallOptions.uninstIconsList.Remove(x.Text);
							Variables.InstallIcons = true;
						}
						else
						{
							InstallOptions.iconsList.Remove(x.Text);
							if (InstallOptions.origList.Contains(x.Text))
							{
								UninstallOptions.uninstIconsList.Add(x.Text);
							}
							Variables.InstallIcons = false;
						}
					});
				}
				if (e.Node.Name == "extraNode")
				{
					e.Node.Descendants().ToList().ForEach(x =>
					{
						x.Checked = e.Node.Checked;
						if (e.Node.Checked)
						{
							if (!InstallOptions.origList.Contains(x.Name))
							{
								InstallOptions.iconsList.Add(x.Name);
							}
							UninstallOptions.uninstExtrasList.Remove(x.Name);
						}
						else
						{
							InstallOptions.iconsList.Remove(x.Name);
							if (InstallOptions.origList.Contains(x.Name))
							{
								UninstallOptions.uninstExtrasList.Add(x.Name);
							}
						}
					});
				}
				e.Node.Ancestors().ToList().ForEach(x =>
				{
					x.Checked = x.Descendants().ToList().Any(y => y.Checked);
					if (e.Node.Checked)
					{
						if (x.Name == "extraNode")
						{
							if (!InstallOptions.origList.Contains(e.Node.Name))
							{
								InstallOptions.iconsList.Add(e.Node.Name);
							}
							UninstallOptions.uninstExtrasList.Remove(e.Node.Name);
						}
						else if (x.Name == "basicNode")
						{
							if (!InstallOptions.origList.Contains(e.Node.Text))
							{
								InstallOptions.iconsList.Add(e.Node.Text);
							}
							UninstallOptions.uninstIconsList.Remove(e.Node.Text);
							Variables.InstallIcons = true;
						}
						else if (x.Name == "advancedNode")
						{
							if (!InstallOptions.origList.Contains(e.Node.Text))
							{
								InstallOptions.iconsList.Add(e.Node.Text);
							}
							UninstallOptions.uninstIconsList.Remove(e.Node.Text);
							Variables.InstallIcons = true;
						}
					}
					else
					{
						if (x.Name == "extraNode")
						{
							InstallOptions.iconsList.Remove(e.Node.Name);
							if (InstallOptions.origList.Contains(e.Node.Name))
							{
								UninstallOptions.uninstExtrasList.Add(e.Node.Name);
							}
						}
						else if (x.Name == "basicNode")
						{
							InstallOptions.iconsList.Remove(e.Node.Text);
							if (InstallOptions.origList.Contains(e.Node.Text))
							{
								UninstallOptions.uninstIconsList.Add(e.Node.Text);
							}
							Variables.InstallIcons = false;
						}
						else if (x.Name == "advancedNode")
						{
							InstallOptions.iconsList.Remove(e.Node.Text);
							if (InstallOptions.origList.Contains(e.Node.Text))
							{
								UninstallOptions.uninstIconsList.Add(e.Node.Text);
							}
							Variables.InstallIcons = false;
						}
					}
				});
				if (e.Node.Name == "themeNode")
				{
					if (e.Node.Checked)
					{
						if (!InstallOptions.origList.Contains(e.Node.Name))
						{
							InstallOptions.iconsList.Add(e.Node.Name);
						}
						UninstallOptions.UninstallThemes = false;
					}
					else
					{
						InstallOptions.iconsList.Remove(e.Node.Name);
						if (InstallOptions.origList.Contains(e.Node.Name))
						{
							UninstallOptions.UninstallThemes = true;
						}
					}
				}
				bool enable = InstallOptions.iconsList.Count > 0
					|| UninstallOptions.uninstIconsList.Count > 0
					|| UninstallOptions.uninstExtrasList.Count > 0
					|| UninstallOptions.UninstallThemes;

				if (InstallOptions.iconsList.Count > 0) Variables.RunInstaller = true;
				if (UninstallOptions.uninstIconsList.Count > 0
					||UninstallOptions.uninstExtrasList.Count > 0
					|| UninstallOptions.UninstallThemes) Variables.RunUninstaller = true;

				_frmWizard.nextButton.Enabled = enable;
				Variables.IsItemsSelected = enable;

				var list = ExtrasOptions.GetExtras();
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Name == e.Node.Name)
					{
						ExtrasSel = true;
						// Update the Image property of the PictureBox based on the selected TreeNode
						Helper.UpdateSideImageOptns(e.Node.Name, _frmWizard);
						return;
					}
					else if (ExtrasSel)
					{
						_frmWizard.UpdateSideImage = Properties.Resources.installoptns;
						ExtrasSel = false;
					}
				}
			}
		}

		private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Action != TreeViewAction.Unknown)
			{
				var list = ExtrasOptions.GetExtras();
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].Name == e.Node.Name)
					{
						ExtrasSel = true;
						// Update the Image property of the PictureBox based on the selected TreeNode
						Helper.UpdateSideImageOptns(e.Node.Name, _frmWizard);
						return;
					}
					else if (ExtrasSel)
					{
						_frmWizard.UpdateSideImage = Properties.Resources.installoptns;
						ExtrasSel = false;
					}
				}
			}
		}
		#endregion
	}
}
