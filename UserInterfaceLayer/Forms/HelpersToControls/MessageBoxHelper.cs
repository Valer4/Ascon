using System.Windows.Forms;

namespace UserInterfaceLayer.Forms.HelpersToControls
{
    public class MessageBoxHelper
    {
        public bool ShowWarningMessage(string message) => ShowProblemMessage(message, "Предупреждение", MessageBoxIcon.Warning);
        public bool ShowErrorMessage(string message) => ShowProblemMessage(message, "Ошибка", MessageBoxIcon.Error);
        public bool ShowProblemMessage(string message, string caption, MessageBoxIcon icon)
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
