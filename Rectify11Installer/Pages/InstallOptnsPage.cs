using Microsoft.Win32;
using Rectify11Installer.Core;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private static void UpdateListView(PatchesPatch[] patch, TreeNode basicNode, TreeNode advNode)
        {
            for (int i = 0; i < patch.Length; i++)
            {
                if (!File.Exists(Path.Combine(Variables.r11Folder, "backup", patch[i].Mui)))
                {
                    if (patch[i].HardlinkTarget.Contains("%sys32%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%sys32%", Variables.sys32Folder);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui);
                    }
                    else if (patch[i].HardlinkTarget.Contains("%lang%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%lang%", Path.Combine(Variables.sys32Folder, CultureInfo.CurrentUICulture.Name));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui.Replace(".mui", ""));
                    }
                    else if (patch[i].HardlinkTarget.Contains("%en-US%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%en-US%", Path.Combine(Variables.sys32Folder, "en-US"));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui.Replace(".mui", ""));
                    }
                    else if (patch[i].HardlinkTarget.Contains("%windirEn-US%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%windirEn-US%", Path.Combine(Variables.windir, "en-US"));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui.Replace(".mui", ""));
                    }
                    else if (patch[i].HardlinkTarget.Contains("%windirLang%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%windirLang%", Path.Combine(Variables.windir, CultureInfo.CurrentUICulture.Name));
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui.Replace(".mui", ""));
                    }
                    else if (patch[i].Mui.Contains("mun"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%sysresdir%", Variables.sysresdir);
                        if (File.Exists(newpath))
                            basicNode.Nodes.Add(patch[i].Mui);
                    }
                    else if (patch[i].HardlinkTarget.Contains("%windir%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%windir%", Variables.windir);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui);
                    }
                    else if (patch[i].HardlinkTarget.Contains("%branding%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%branding%", Variables.brandingFolder);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui);
                    }
                    else if (patch[i].HardlinkTarget.Contains("%prog%"))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%prog%", Variables.progfiles);
                        if (File.Exists(newpath))
                            advNode.Nodes.Add(patch[i].Mui);
                    }

                }
                if (patch[i].HardlinkTarget.Contains("%diag%"))
                {
                    string name = patch[i].Mui.Replace("Troubleshooter: ", "DiagPackage") + ".dll";
                    if (!File.Exists(Path.Combine(Variables.r11Folder, "backup", "Diag", name)))
                    {
                        string newpath = patch[i].HardlinkTarget.Replace(@"%diag%", Variables.diag);
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
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Rectify11", true);
            if (key != null)
            {
                object build = key.GetValue("Build");
                if (build == null || (build != null && Int32.Parse(build.ToString()) < Assembly.GetEntryAssembly().GetName().Version.Build))
                {
                    if (Directory.Exists(Path.Combine(Variables.r11Folder, "Backup")))
                    {
                        File.WriteAllText(Path.Combine(Variables.r11Folder, "newfiles.txt"), Properties.Resources.newfiles);
                        if (!Directory.Exists(Path.Combine(Variables.r11Folder, "Backup", "oldfiles")))
                        {
                            Directory.CreateDirectory(Path.Combine(Variables.r11Folder, "Backup", "oldfiles"));
                        }
                        string[] newFiles = File.ReadAllLines(Path.Combine(Variables.r11Folder, "newfiles.txt"));
                        for (int i = 0; i < newFiles.Length; i++)
                        {
                            if (File.Exists(Path.Combine(Variables.r11Folder, "Backup", "oldfiles", newFiles[i])))
                            {
                                File.Delete(Path.Combine(Variables.r11Folder, "Backup", "oldfiles", newFiles[i]));
                            }
                            if (File.Exists(Path.Combine(Variables.r11Folder, "Backup", newFiles[i])))
                            {
                                File.Move(Path.Combine(Variables.r11Folder, "Backup", newFiles[i]), Path.Combine(Variables.r11Folder, "Backup", "oldfiles", newFiles[i]));
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
