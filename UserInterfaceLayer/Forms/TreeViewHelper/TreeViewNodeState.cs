namespace Client.Forms.TreeViewHelper
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
