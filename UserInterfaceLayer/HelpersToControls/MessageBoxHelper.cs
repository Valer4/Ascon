using System.Windows.Forms;

namespace UserInterfaceLayer.HelpersToControls
{
    internal class MessageBoxHelper
    {
        internal bool ShowWarningMessage(string message) => ShowProblemMessage(message, "Предупреждение", MessageBoxIcon.Warning);
        internal bool ShowErrorMessage(string message) => ShowProblemMessage(message, "Ошибка", MessageBoxIcon.Error);

        internal bool ShowProblemMessage(string message, string caption, MessageBoxIcon icon)
        {
            if( ! string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
                return true;
            }
            return false;
        }
    }
}
