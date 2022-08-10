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
                string package = Path.GetFileName(patch.HardlinkTarget);
                node.Nodes.Add(package);
            }
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                e.Node.Descendants().ToList().ForEach(x =>
                {
                    x.Checked = e.Node.Checked;
                    if (e.Node.Checked)
                        InstallOptions.iconsList.Add(x.Text);
                    else
                        InstallOptions.iconsList.Remove(x.Text);
                });
                e.Node.Ancestors().ToList().ForEach(x =>
                {
                    x.Checked = x.Descendants().ToList().Any(y => y.Checked);
                    if (e.Node.Checked)
                        InstallOptions.iconsList.Add(e.Node.Text);
                    else
                        InstallOptions.iconsList.Remove(e.Node.Text);
                });
                if ((!_frmWizard.nextButton.Enabled) && (InstallOptions.iconsList.Count > 0))
                {
                    _frmWizard.nextButton.Enabled = true;
                    _frmWizard.IsItemsSelected = true;

                }
                else if (InstallOptions.iconsList.Count == 0)
                {
                    _frmWizard.nextButton.Enabled = false;
                    _frmWizard.IsItemsSelected = false;
                }
            }
        }
        
    }
}
