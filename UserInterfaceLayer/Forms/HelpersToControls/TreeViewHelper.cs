using BusinessLogicLayer.Data.Entities.Interfaces;
using System.Linq;
using System.Windows.Forms;

namespace UserInterfaceLayer.Forms.HelpersToControls
{
    public class TreeViewHelper
    {
        #region Построение TreeView.
        public void BuildTreeView<TItem, TId, TParentId, TTypeId>(TreeView treeView, IQueryable<TItem> collection)
            where TItem : ITreeLogicalNode<TId, TParentId, TTypeId>
        {
            BuildRootNodesTreeView<TItem, TId, TParentId, TTypeId>(treeView, collection);

            foreach(TreeNode node in treeView.Nodes)
                BuildChildNodesTreeViewRecursive<TItem, TId, TParentId, TTypeId>(node, collection);
        }

        private void BuildRootNodesTreeView<TItem, TId, TParentId, TTypeId>(TreeView treeView, IQueryable<TItem> collection)
            where TItem : ITreeLogicalNode<TId, TParentId, TTypeId>
        {
            foreach(TItem detail in collection)
                if(detail.Root)
                    AddNode<TItem, TId, TParentId, TTypeId>(treeView.Nodes, detail);
        }

        private void BuildChildNodesTreeViewRecursive<TItem, TId, TParentId, TTypeId>(TreeNode treeNode, IQueryable<TItem> collection)
            where TItem : ITreeLogicalNode<TId, TParentId, TTypeId>
        {
            TId treeNodeId = (TId)treeNode.Tag;
            TTypeId typeId = collection.Where(x => treeNodeId.Equals(x.Id)).Single().TypeId;
            IQueryable<TItem> childsOfDetail = collection.Where(x => typeId.Equals(x.ParentId));

            TreeNode addedNode;
            foreach(TItem childOfDetail in childsOfDetail)
            {
                addedNode = AddNode<TItem, TId, TParentId, TTypeId>(treeNode.Nodes, childOfDetail);

                BuildChildNodesTreeViewRecursive<TItem, TId, TParentId, TTypeId>(addedNode, collection);
            }
        }

        private TreeNode AddNode<TItem, TId, TParentId, TTypeId>(TreeNodeCollection nodes, TItem detail)
            where TItem : ITreeLogicalNode<TId, TParentId, TTypeId>
        {
            TreeNode addedNode;
            string text = detail.Text;
            addedNode = nodes.Add(text);
            addedNode.Tag = detail.Id;
            return addedNode;
        }
        #endregion
    }
}
