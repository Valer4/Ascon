namespace UserInterfaceLayer.Forms.HelpersToControls.TreeViewHelper.TreeViewStoresState
{
    public class TreeViewNodeState
    {
        public string FullPath;
        public bool Expanded,
                    Selected;

        public TreeViewNodeState(string fullPath, bool expanded, bool selected)
        {
            FullPath = fullPath;
            Expanded = expanded;
            Selected = selected;
        }
    }
}
