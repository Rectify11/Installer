using Rectify11Installer.Core;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class UninstallPage : WizardPage
    {
        #region Variables
        private readonly FrmWizard _frmWizard;
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
                if (!File.Exists(Path.Combine(Variables.progfiles, "Windows Sidebar", "sidebar.exe")))
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
                    if (!patch[i].HardlinkTarget.Contains("%diag%"))
                    {
                        var newpath = Helper.FixString(patch[i].HardlinkTarget, !string.IsNullOrWhiteSpace(patch[i].x86));
                        if (File.Exists(newpath))
                        {
                            if (newpath.Contains(".mun")) basicNode.Nodes.Add(patch[i].Mui);
                            else advNode.Nodes.Add(patch[i].Mui);
                        }
                    }
                }
                if (patch[i].HardlinkTarget.Contains("%diag%"))
                {
                    var name = patch[i].Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
                    if (File.Exists(Path.Combine(Variables.r11Folder, "backup", "Diag", name)))
                    {
                        var newpath = Helper.FixString(patch[i].HardlinkTarget, false);
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
                    if (e.Node.Checked)
                    {
                        UninstallOptions.uninstDummylist.Add(e.Node.Text);
                    }
                    else
                    {
                        UninstallOptions.uninstDummylist.Remove(e.Node.Text);
                    }
                    e.Node.Descendants().ToList().ForEach(x =>
                    {
                        x.Checked = e.Node.Checked;
                        if (e.Node.Checked)
                        {
                            UninstallOptions.uninstIconsList.Add(x.Text);
                            UninstallOptions.uninstDummylist.Add(x.Text);
                            Variables.InstallIcons = true;
                        }
                        else
                        {
                            UninstallOptions.uninstIconsList.Remove(x.Text);
                            UninstallOptions.uninstDummylist.Remove(x.Text);
                            Variables.InstallIcons = false;
                        }
                    });
                }
                if (e.Node.Name == "sysIconsNode")
                {
                    if (e.Node.Checked)
                    {
                        UninstallOptions.uninstDummylist.Add(e.Node.Text);
                    }
                    else
                    {
                        UninstallOptions.uninstDummylist.Remove(e.Node.Text);
                    }
                    e.Node.Descendants().ToList().ForEach(x =>
                    {
                        x.Checked = e.Node.Checked;
                        if (e.Node.Checked && (x.Name != "basicNode") && (x.Name != "advancedNode"))
                        {
                            UninstallOptions.uninstIconsList.Add(x.Text);
                            UninstallOptions.uninstDummylist.Add(x.Text);
                            Variables.InstallIcons = true;
                        }
                        else if ((x.Name != "basicNode") && (x.Name != "advancedNode"))
                        {
                            UninstallOptions.uninstIconsList.Remove(x.Text);
                            UninstallOptions.uninstDummylist.Remove(x.Text);
                            Variables.InstallIcons = false;
                        }
                        else if (e.Node.Checked && ((x.Name == "basicNode") || (x.Name == "advancedNode")))
                        {
                            UninstallOptions.uninstDummylist.Add(x.Text);
                        }
                        else if (((x.Name == "basicNode") || (x.Name == "advancedNode")))
                        {
                            UninstallOptions.uninstDummylist.Remove(x.Text);
                        }

                    });
                }
                if (e.Node.Name == "advancedNode")
                {
                    if (e.Node.Checked)
                    {
                        UninstallOptions.uninstDummylist.Add(e.Node.Text);
                    }
                    else
                    {
                        UninstallOptions.uninstDummylist.Remove(e.Node.Text);
                    }
                    e.Node.Descendants().ToList().ForEach(x =>
                    {
                        x.Checked = e.Node.Checked;
                        if (e.Node.Checked)
                        {
                            UninstallOptions.uninstIconsList.Add(x.Text);
                            UninstallOptions.uninstDummylist.Add(x.Text);
                            Variables.InstallIcons = true;
                        }
                        else
                        {
                            UninstallOptions.uninstIconsList.Remove(x.Text);
                            UninstallOptions.uninstDummylist.Remove(x.Text);
                            Variables.InstallIcons = false;
                        }
                    });
                }
                if (e.Node.Name == "extraNode")
                {
                    if (e.Node.Checked)
                    {
                        UninstallOptions.uninstDummylist.Add(e.Node.Text);
                    }
                    else
                    {
                        UninstallOptions.uninstDummylist.Remove(e.Node.Text);
                    }
                    e.Node.Descendants().ToList().ForEach(x =>
                    {
                        x.Checked = e.Node.Checked;
                        if (e.Node.Checked)
                        {
                            UninstallOptions.uninstExtrasList.Add(x.Name);
                            UninstallOptions.uninstDummylist.Add(x.Name);
                        }
                        else
                        {
                            UninstallOptions.uninstExtrasList.Remove(x.Name);
                            UninstallOptions.uninstDummylist.Remove(x.Name);
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
                            UninstallOptions.uninstDummylist.Add(e.Node.Name);
                        }
                        else if (x.Name == "basicNode")
                        {
                            UninstallOptions.uninstIconsList.Add(e.Node.Text);
                            UninstallOptions.uninstDummylist.Add(e.Node.Text);
                            Variables.InstallIcons = true;
                        }
                        else if (x.Name == "advancedNode")
                        {
                            UninstallOptions.uninstIconsList.Add(e.Node.Text);
                            UninstallOptions.uninstDummylist.Add(e.Node.Text);
                            Variables.InstallIcons = true;
                        }
                        else if (x.Name == "sysIconsNode")
                        {
                            UninstallOptions.uninstDummylist.Add(x.Text);
                        }
                    }
                    else
                    {
                        if (x.Name == "extraNode")
                        {
                            UninstallOptions.uninstExtrasList.Remove(e.Node.Name);
                            UninstallOptions.uninstDummylist.Remove(e.Node.Name);
                        }
                        else if (x.Name == "basicNode")
                        {
                            UninstallOptions.uninstIconsList.Remove(e.Node.Text);
                            UninstallOptions.uninstDummylist.Remove(e.Node.Text);
                            Variables.InstallIcons = false;
                        }
                        else if (x.Name == "advancedNode")
                        {
                            UninstallOptions.uninstIconsList.Remove(e.Node.Text);
                            UninstallOptions.uninstDummylist.Remove(e.Node.Text);
                            Variables.InstallIcons = false;
                        }
                        else if (x.Name == "sysIconsNode")
                        {
                            UninstallOptions.uninstDummylist.Remove(x.Text);
                        }
                    }
                });
                if (e.Node.Name == "themeNode")
                {
                    if (e.Node.Checked)
                    {
                        UninstallOptions.UninstallThemes = true;
                        UninstallOptions.uninstDummylist.Add(e.Node.Text);
                    }
                    else
                    {
                        UninstallOptions.UninstallThemes = false;
                        UninstallOptions.uninstDummylist.Remove(e.Node.Text);
                    }
                }
                if (!_frmWizard.nextButton.Enabled && UninstallOptions.uninstDummylist.Count > 0)
                {
                    _frmWizard.nextButton.Enabled = true;
                    Variables.IsItemsSelected = true;

                }
                else if (UninstallOptions.uninstDummylist.Count == 0)
                {
                    _frmWizard.nextButton.Enabled = false;
                    Variables.IsItemsSelected = false;
                }
                if (UninstallOptions.uninstDummylist.Count == treeView1.GetNodeCount(true))
                {
                    Variables.CompleteUninstall = true;
                }
                else
                {
                    Variables.CompleteUninstall = false;
                }
            }
        }

        #endregion
    }
}
