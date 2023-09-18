using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Rectify11Installer.Core
{
	public static class TreeViewExtension
	{
		public static List<TreeNode> Descendants(this TreeNode node)
		{
			var nodes = node.Nodes.Cast<TreeNode>().ToList();
			return nodes.SelectMany(x => Descendants(x)).Concat(nodes).ToList();
		}
		public static List<TreeNode> Ancestors(this TreeNode node)
		{
			return AncestorsInternal(node).ToList();
		}
		private static IEnumerable<TreeNode> AncestorsInternal(TreeNode node)
		{
			while (node.Parent != null)
			{
				node = node.Parent;
				yield return node;
			}
		}
	}
}

