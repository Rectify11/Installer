using Rectify11Installer.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace Rectify11Installer.Pages
{
    public partial class InstallOptnsPage : WizardPage
    {
        public InstallOptnsPage()
        {
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
        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (String.Compare(e.Node.Name, "epNode", true) == 0)
                {
                    InstallOptions.InstallEP = e.Node.Checked;
                }
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }
        
    }
}
