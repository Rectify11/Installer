using Rectify11Installer.Core;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class InstallOptnsPage : WizardPage
    {
        private readonly frmWizard _frmWizard;
        public InstallOptnsPage(frmWizard Frm)
        {
            _frmWizard = Frm;
            InitializeComponent();
            Patches list = PatchesParser.GetAll();
            PatchesPatch[] ok = list.Items;
            var node = treeView1.Nodes[0];
            foreach (PatchesPatch patch in ok)
            {
                node.Nodes.Add(patch.Mui);
            }
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Name == "sysIconsNode")
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
                        else if (x.Name == "sysIconsNode")
                            InstallOptions.iconsList.Add(e.Node.Text);
                    }
                    else
                    {
                        if (x.Name == "extraNode")
                            InstallOptions.iconsList.Remove(e.Node.Name);
                        else if (x.Name == "sysIconsNode")
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
        
    }
}
