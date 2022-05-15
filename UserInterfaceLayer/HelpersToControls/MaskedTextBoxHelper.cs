using System.Windows.Forms;

namespace UserInterfaceLayer.HelpersToControls
{
	internal class MaskedTextBoxHelper
	{
		internal void MoveCaretBeforeSpaces(MaskedTextBox maskedTextBox) =>
			maskedTextBox.Select(GetPositionOfFirstSpace(maskedTextBox), 0);

		private int GetPositionOfFirstSpace(MaskedTextBox maskedTextBox)
		{
			string text = maskedTextBox.Text;
			int i = 0,
				len = text.Length;
			for (; i < len; ++i)
				if (text[i] == ' ')
					return i;
			return i;
		}
	}
}
