using Rectify11Installer.Core;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class InstallOptnsPage : WizardPage
    {
        #region Variables
        private readonly frmWizard _frmWizard;
        bool idleinit;
        #endregion
        #region Main
        public InstallOptnsPage(frmWizard Frm)
        {
            _frmWizard = Frm;
            InitializeComponent();
            Application.Idle += Application_Idle;
        }
        void Application_Idle(object sender, System.EventArgs e)
        {
            if (!idleinit)
            {
                overwriteUpdatedFiles();
                Patches list = PatchesParser.GetAll();
                PatchesPatch[] ok = list.Items;
                var basicNode = treeView1.Nodes[0].Nodes[0];
                var advNode = treeView1.Nodes[0].Nodes[1];
                UpdateListView(ok, basicNode, advNode);
                if (basicNode.Nodes.Count == 0)
                    treeView1.Nodes.Remove(basicNode);
                if (advNode.Nodes.Count == 0)
                    treeView1.Nodes.Remove(advNode);
                if (treeNode1.Nodes.Count == 0)
                    treeView1.Nodes.Remove(treeNode1);
                idleinit = true;
            }
        }
        #endregion
        #region Private Methods
        private static void UpdateListView(PatchesPatch[] ok, TreeNode basicNode, TreeNode advNode)
        {
            foreach (PatchesPatch patch in ok)
            {
                if (!File.Exists(Path.Combine(Variables.r11Folder, "backup", patch.Mui)))
                {
                    if (patch.HardlinkTarget.Contains("%sys32%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%sys32%", Variables.sys32Folder);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui);
                    }
                    else if (patch.HardlinkTarget.Contains("%lang%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui.Replace(".mui", ""));
                    }
                    else if (patch.HardlinkTarget.Contains("%en-US%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui.Replace(".mui", ""));
                    }
                    else if (patch.HardlinkTarget.Contains("%windirEn-US%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%windirEn-US%", Path.Combine(Variables.windir, "en-US"));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui.Replace(".mui", ""));
                    }
                    else if (patch.HardlinkTarget.Contains("%windirLang%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%windirLang%", Path.Combine(Variables.windir, CultureInfo.CurrentUICulture.Name));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui.Replace(".mui", ""));
                    }
                    else if (patch.Mui.Contains("mun"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%sysresdir%", Variables.sysresdir);
                        if (File.Exists(newpath))
                            basicNode.Nodes.Add(patch.Mui);
                    }
                    else if (patch.HardlinkTarget.Contains("%windir%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%windir%", Variables.windir);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui);
                    }
                    else if (patch.HardlinkTarget.Contains("%branding%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%branding%", Variables.brandingFolder);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui);
                    }
                    else if (patch.HardlinkTarget.Contains("%prog%"))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%prog%", Variables.progfiles);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui);
                    }

                }
                if (patch.HardlinkTarget.Contains("%diag%"))
                {
                    string name = patch.Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
                    if (!File.Exists(Path.Combine(Variables.r11Folder, "backup", "Diag", name)))
                    {
                        string newpath = patch.HardlinkTarget.Replace(@"%diag%", Variables.diag);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch.Mui);
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
                            InstallOptions.iconsList.Add(x.Text);
                        else
                            InstallOptions.iconsList.Remove(x.Text);
                    });
                }
                if (e.Node.Name == "sysIconsNode")
                {
                    e.Node.Descendants().ToList().ForEach(x =>
                    {
                        x.Checked = e.Node.Checked;
                        if (e.Node.Checked && (x.Name != "basicNode") && (x.Name != "advancedNode"))
                            InstallOptions.iconsList.Add(x.Text);
                        else if ((x.Name != "basicNode") && (x.Name != "advancedNode"))
                            InstallOptions.iconsList.Remove(x.Text);
                    });
                }
                if (e.Node.Name == "advancedNode")
                {
                    e.Node.Descendants().ToList().ForEach(x =>
                    {
                        x.Checked = e.Node.Checked;
                        if (e.Node.Checked)
                            InstallOptions.iconsList.Add(x.Text);
                        else
                            InstallOptions.iconsList.Remove(x.Text);
                    });
                }
                if (e.Node.Name == "extraNode")
                {
                    e.Node.Descendants().ToList().ForEach(x =>
                    {
                        x.Checked = e.Node.Checked;
                        if (e.Node.Checked)
                            InstallOptions.iconsList.Add(x.Name);
                        else
                            InstallOptions.iconsList.Remove(x.Name);
                    });
                }
                e.Node.Ancestors().ToList().ForEach(x =>
                {
                    x.Checked = x.Descendants().ToList().Any(y => y.Checked);
                    if (e.Node.Checked)
                    {
                        if (x.Name == "extraNode")
                            InstallOptions.iconsList.Add(e.Node.Name);
                        else if (x.Name == "basicNode")
                            InstallOptions.iconsList.Add(e.Node.Text);
                        else if (x.Name == "advancedNode")
                            InstallOptions.iconsList.Add(e.Node.Text);
                    }
                    else
                    {
                        if (x.Name == "extraNode")
                            InstallOptions.iconsList.Remove(e.Node.Name);
                        else if (x.Name == "basicNode")
                            InstallOptions.iconsList.Remove(e.Node.Text);
                        else if (x.Name == "advancedNode")
                            InstallOptions.iconsList.Remove(e.Node.Text);
                    }
                });
                if (e.Node.Name == "themeNode")
                {
                    if (e.Node.Checked)
                        InstallOptions.iconsList.Add(e.Node.Name);
                    else
                        InstallOptions.iconsList.Remove(e.Node.Name);
                }
                if ((!_frmWizard.nextButton.Enabled) && (InstallOptions.iconsList.Count > 0))
                {
                    _frmWizard.nextButton.Enabled = true;
                    frmWizard.IsItemsSelected = true;

                }
                else if (InstallOptions.iconsList.Count == 0)
                {
                    _frmWizard.nextButton.Enabled = false;
                    frmWizard.IsItemsSelected = false;
                }
            }
        }
        private void overwriteUpdatedFiles()
        {
            if (Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
            {
                File.WriteAllText(Path.Combine(Variables.r11Folder, "newfiles.txt"), Properties.Resources.newfiles);
                if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup", "oldfiles")))
                {
                    Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup", "oldfiles"));
                }
                string[] newFiles = File.ReadAllLines(Path.Combine(Variables.r11Folder, "newfiles.txt"));
                foreach (string file in newFiles)
                {
                    if (File.Exists(Path.Combine(Variables.r11Folder, "Backup", "oldfiles", file)))
                    {
                        File.Delete(Path.Combine(Variables.r11Folder, "Backup", "oldfiles", file));
                    }
                    if (File.Exists(Path.Combine(Variables.r11Folder, "Backup", file)))
                    {
                        File.Move(Path.Combine(Variables.r11Folder, "Backup", file), Path.Combine(Variables.r11Folder, "Backup", "oldfiles", file));
                    }
                }
            }
        }
        #endregion
    }
}
