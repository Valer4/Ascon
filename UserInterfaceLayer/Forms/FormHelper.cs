using System.Windows.Forms;

namespace UserInterfaceLayer.Forms
{
	internal class FormHelper : Form
	{
		protected void ButtonSuspend(Button button)
		{
			Cursor.Current = Cursors.WaitCursor;
			button.Enabled = false;
		}

		protected void ButtonResume(Button button)
		{
			Cursor.Current = Cursors.Default;
			button.Enabled = true;
		}
	}
}
