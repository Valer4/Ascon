namespace UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper.TreeViewStoresState
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
}
