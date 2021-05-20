using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper
{
    public class TreeViewNodeState
    {
        public string FullPath;
        public bool IsExpanded,
                    IsSelected;

        public TreeViewNodeState(string fullPath, bool isExpanded, bool isSelected)
        {
            FullPath = fullPath;
            IsExpanded = isExpanded;
            IsSelected = isSelected;
        }
    }

    public class TreeViewState
    {
        private IList<TreeViewNodeState> _nodeStates;

        private string GetFullPath(TreeNode node)
        {
            StringBuilder fullPath = new StringBuilder();
            GetFullPathRecursive(node, ref fullPath);
            return fullPath.ToString().Trim('_');
        }

        private void GetFullPathRecursive(TreeNode node, ref StringBuilder fullPath)
        {
            fullPath.Append($"_{node.Tag}_");
            if(null != node.Parent)
                GetFullPathRecursive(node.Parent, ref fullPath);
        }

        public void SaveState(TreeView treeView)
        {
            if(null == treeView)
                throw new ArgumentNullException("Дерево не создано.");

            _nodeStates = new List<TreeViewNodeState>();
            SaveStateRecursive(treeView.Nodes);
        }

        // В девэкспресс есть итератор, он быстрее рекурсий, если дерево не ленивое.
        public void SaveStateRecursive(TreeNodeCollection nodes)
        {
            foreach(TreeNode node in nodes)
            {
                _nodeStates.Add(
                    new TreeViewNodeState(
                        GetFullPath(node),
                        node.IsExpanded,
                        node.IsSelected));

                SaveStateRecursive(node.Nodes);
            }
        }

        public void RestoreState(TreeView treeView)
        {
            if(null == _nodeStates)
                throw new InvalidOperationException("Состояние дерева не сохранено.");

            treeView.SelectedNode = null;

            RestoreStateRecursive(treeView.Nodes);
        }

        public void RestoreStateRecursive(TreeNodeCollection nodes)
        {
            foreach(TreeNode node in nodes)
            {
                string fullPath = GetFullPath(node);
                TreeViewNodeState nodeState = _nodeStates.Where(x => fullPath == x.FullPath).SingleOrDefault();

                if(null != nodeState)
                {
                    if(nodeState.IsExpanded)
                        node.Expand();

                    if(nodeState.IsSelected)
                        node.TreeView.SelectedNode = node;
                }

                RestoreStateRecursive(node.Nodes);
            }
        }
    }
}
