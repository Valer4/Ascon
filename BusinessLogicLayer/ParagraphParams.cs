using System;

namespace BusinessLogicLayer
{
	internal class ParagraphParams : ICloneable
	{
		/// <summary>
		/// Исходный текст.
		/// </summary>
		internal string Text { get; }

		/// <summary>
		/// Размер минимального шрифта.
		/// </summary>
		internal float MinFontSize { get; }

		/// <summary>
		/// Обрезать текст, в конце добавляется многоточие.
		/// </summary>
		internal bool CropText { get; }

		/// <summary>
		/// Разбивать слова на части при переносе на следующую строку.
		/// </summary>
		internal bool BreakWords { get; }

		/// <summary>
		/// Заполнять оставшуюся ширину пробелами.
		/// </summary>
		internal bool PadRemainingLineWidthWithSpaces { get; }

		/// <summary>
		///  Убирать пробелы в начале строки.
		/// </summary>
		internal bool RemoveSpacesAtBeginningOfLine { get; }

		/// <summary>
		/// Ширина добавляемых строк в пикселях. Когда в шаблоне не таблица и допустимо добавлять строки.
		/// </summary>
		internal int PixelsAddLines { get; }

		internal ParagraphParams(
			string text,
			float minFontSize = 8.0F,
			bool cropText = true,
			bool breakWords = false,
			bool padRemainingLineWidthWithSpaces = true,
			bool removeSpacesAtBeginningOfLine = true,
			int pixelsAddLines = 0
		)
		{
			Text = text;
			MinFontSize = minFontSize;
			CropText = cropText;
			BreakWords = breakWords;
			PadRemainingLineWidthWithSpaces = padRemainingLineWidthWithSpaces;
			RemoveSpacesAtBeginningOfLine = removeSpacesAtBeginningOfLine;
			PixelsAddLines = pixelsAddLines;
		}

		internal ParagraphParams(
			ParagraphParams paragraphParams,
			bool cropText
		)
		{
			Text = paragraphParams.Text;
			MinFontSize = paragraphParams.MinFontSize;
			CropText = cropText;
			BreakWords = paragraphParams.BreakWords;
			PadRemainingLineWidthWithSpaces = paragraphParams.PadRemainingLineWidthWithSpaces;
			RemoveSpacesAtBeginningOfLine = paragraphParams.RemoveSpacesAtBeginningOfLine;
			PixelsAddLines = paragraphParams.PixelsAddLines;
		}

		internal ParagraphParams(
			ParagraphParams paragraphParams,
			int pixelsAddLines
		)
		{
			Text = paragraphParams.Text;
			MinFontSize = paragraphParams.MinFontSize;
			CropText = paragraphParams.CropText;
			BreakWords = paragraphParams.BreakWords;
			PadRemainingLineWidthWithSpaces = paragraphParams.PadRemainingLineWidthWithSpaces;
			RemoveSpacesAtBeginningOfLine = paragraphParams.RemoveSpacesAtBeginningOfLine;
			PixelsAddLines = pixelsAddLines;
		}

		public object Clone()
		{
			return new ParagraphParams(
				Text,
				MinFontSize,
				CropText,
				BreakWords,
				PadRemainingLineWidthWithSpaces,
				RemoveSpacesAtBeginningOfLine,
				PixelsAddLines
			);
		}
	}
}
