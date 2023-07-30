using Rectify11Installer.Core;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class UninstallPage : WizardPage
	{
		#region Variables
		private readonly FrmWizard _frmWizard;
		private bool ExtrasSel = false;
		bool idleinit;
		#endregion
		#region Main
		public UninstallPage(FrmWizard Frm)
		{
			_frmWizard = Frm;
			InitializeComponent();
			Application.Idle += Application_Idle;
			NavigationHelper.OnNavigate += NavigationHelper_OnNavigate;
		}

		private void NavigationHelper_OnNavigate(object sender, EventArgs e)
		{
			if ((WizardPage)sender == RectifyPages.UninstallPage)
			{
				_frmWizard.nextButton.Enabled = Variables.IsItemsSelected;
			}
		}

		void Application_Idle(object sender, System.EventArgs e)
		{
			if (!idleinit)
			{
				//overwriteUpdatedFiles();
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
				if (!Directory.Exists(Path.Combine(Variables.Windir, "Resources", "Themes", "Rectified")))
				{
					treeView1.Nodes.Remove(themeNode);
				}
                if (!Directory.Exists(Path.Combine(Variables.Windir, "nilesoft")))
				{
					treeView1.Nodes.Remove(shell);
				}
                if (!Directory.Exists(Path.Combine(Variables.r11Folder, "extras", "AccentColorizer")))
				{
                    treeView1.Nodes.Remove(asdf);
                }
                if (!File.Exists(Path.Combine(Variables.Windir, "web", "wallpaper", "Rectified", "img41.jpg")))
				{
					treeView1.Nodes.Remove(wall);
				}
                if (!Directory.Exists(Path.Combine(Variables.progdata, "Microsoft", "User Account Pictures", "Default Pictures")))
				{
					treeView1.Nodes.Remove(av);
				}
				if (!File.Exists(Path.Combine(Variables.progfiles, "Windows Sidebar","sidebar.exe")))
				{
					treeView1.Nodes.Remove(gad);
				}
				if (extra.Nodes.Count == 0)
				{
					treeView1.Nodes.Remove(extra);
				}
                idleinit = true;
			}
		}

		#endregion
		#region Private Methods
		private static void UpdateListView(PatchesPatch[] patch, TreeNode basicNode, TreeNode advNode)
		{
			for (var i = 0; i < patch.Length; i++)
			{
				if (File.Exists(Path.Combine(Variables.r11Folder, "backup", patch[i].Mui)))
				{
					if (patch[i].HardlinkTarget.Contains("%sys32%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%sys32%", Variables.sys32Folder);
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].HardlinkTarget.Contains("%lang%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].HardlinkTarget.Contains("%en-US%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].HardlinkTarget.Contains("%windirEn-US%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%windirEn-US%", Path.Combine(Variables.Windir, "en-US"));
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].HardlinkTarget.Contains("%windirLang%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%windirLang%", Path.Combine(Variables.Windir, CultureInfo.CurrentUICulture.Name));
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].Mui.Contains("mun"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%sysresdir%", Variables.sysresdir);
						if (File.Exists(newpath))
							basicNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].HardlinkTarget.Contains("%windir%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%windir%", Variables.Windir);
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].HardlinkTarget.Contains("%branding%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%branding%", Variables.BrandingFolder);
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}
					else if (patch[i].HardlinkTarget.Contains("%prog%"))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%prog%", Variables.progfiles);
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
					}

				}
				if (patch[i].HardlinkTarget.Contains("%diag%"))
				{
					var name = patch[i].Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
					if (File.Exists(Path.Combine(Variables.r11Folder, "backup", "Diag", name)))
					{
						var newpath = patch[i].HardlinkTarget.Replace(@"%diag%", Variables.diag);
						if (File.Exists(newpath))
							advNode.Nodes.Add(patch[i].Mui);
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
							UninstallOptions.uninstIconsList.Add(x.Text);
							Variables.InstallIcons = true;
						}
						else
						{
							UninstallOptions.uninstIconsList.Remove(x.Text);
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
							UninstallOptions.uninstIconsList.Add(x.Text);
							Variables.InstallIcons = true;
						}
						else if ((x.Name != "basicNode") && (x.Name != "advancedNode"))
						{
							UninstallOptions.uninstIconsList.Remove(x.Text);
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
							UninstallOptions.uninstIconsList.Add(x.Text);
							Variables.InstallIcons = true;
						}
						else
						{
							UninstallOptions.uninstIconsList.Remove(x.Text);
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
							UninstallOptions.uninstExtrasList.Add(x.Name);
						}
						else
						{
                            UninstallOptions.uninstExtrasList.Remove(x.Name);
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
                            UninstallOptions.uninstExtrasList.Add(e.Node.Name);
                        }
                        else if (x.Name == "basicNode")
						{
							UninstallOptions.uninstIconsList.Add(e.Node.Text);
							Variables.InstallIcons = true;
						}
						else if (x.Name == "advancedNode")
						{
							UninstallOptions.uninstIconsList.Add(e.Node.Text);
							Variables.InstallIcons = true;
						}
					}
					else
					{
						if (x.Name == "extraNode")
						{
                            UninstallOptions.uninstExtrasList.Remove(e.Node.Name);
                        }
                        else if (x.Name == "basicNode")
						{
							UninstallOptions.uninstIconsList.Remove(e.Node.Text);
							Variables.InstallIcons = false;
						}
						else if (x.Name == "advancedNode")
						{
							UninstallOptions.uninstIconsList.Remove(e.Node.Text);
							Variables.InstallIcons = false;
						}
					}
				});
				if (e.Node.Name == "themeNode")
				{
					if (e.Node.Checked)
					{
						UninstallOptions.UninstallThemes = true;
					}
					else
					{
						UninstallOptions.UninstallThemes = false;
					}
				}
				if ((!_frmWizard.nextButton.Enabled) && ((UninstallOptions.uninstIconsList.Count > 0) || UninstallOptions.UninstallThemes || (UninstallOptions.uninstExtrasList.Count > 0)) )
				{
					_frmWizard.nextButton.Enabled = true;
					Variables.IsItemsSelected = true;

				}
				else if (UninstallOptions.uninstIconsList.Count == 0 || !UninstallOptions.UninstallThemes || UninstallOptions.uninstExtrasList.Count == 0)
				{
					_frmWizard.nextButton.Enabled = false;
					Variables.IsItemsSelected = false;
				}
			}
		}

		#endregion
	}
}
