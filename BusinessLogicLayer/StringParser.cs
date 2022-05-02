using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Font = System.Drawing.Font;

namespace BusinessLogicLayer
{
	internal class StringParser
	{
		internal string GetSplit(
			out bool needFitText,
			ParagraphParams paragraphParams,
			int[] pixelsInLines,
			ref IList<StringBuilder> linesParagraph,
			ref Font fontBookmark)
		{
			string result;

			int[] pixelsInLines2 = pixelsInLines.ToArray();
			int countLines = pixelsInLines.Length,
				p,
				l;

			var paragraphParamsCropTextOff = new ParagraphParams(paragraphParams,cropText: false);
			var results = new List<string>();
			var linesList = new List<IList<StringBuilder>>();
			var fonts = new List<Font>();

			for (p = 0; p <= 50; p += 5)
			{
				for (l = 0; l < countLines; ++l)
					pixelsInLines2[l] = (int)(pixelsInLines[l] / 100.0D * (100.0D + p) + 0.5D);

				result = GetSplit(paragraphParamsCropTextOff, pixelsInLines2, ref linesParagraph, ref fontBookmark, isTable: true, pickUpFontSize: false);

				results.Add(result);
				linesList.Add(linesParagraph);
				fonts.Add(fontBookmark);

				if (countLines >= linesParagraph.Count)
					break;
			}

			int trueLinesList = 0;
			int minLines = linesList[0].Count;
			int variants = linesList.Count;
			for (int v = 1; v < variants; ++v)
				if (minLines > linesList[v].Count)
				{
					minLines = linesList[v].Count;
					trueLinesList = v;
				}
			result = results[trueLinesList];
			linesParagraph = linesList[trueLinesList];
			fontBookmark = fonts[trueLinesList];

			needFitText = trueLinesList > 0;

			if (paragraphParams.CropText && countLines < linesParagraph.Count)
			{
				int trueLastLineID = countLines - 1;
				var lastLine = new StringBuilder(linesParagraph[trueLastLineID].ToString());
				CropText(ref lastLine, fontBookmark, pixelsInLines2[trueLastLineID], paragraphParams.PadRemainingLineWidthWithSpaces);
				linesParagraph[trueLastLineID].Clear();
				linesParagraph[trueLastLineID].Append(lastLine);

				if (pixelsInLines.Length == 1) result = linesParagraph[0].ToString();
			}

			return result;
		}

		/// <summary>
		/// Разбивает строку на несколько строк указанной в пикселях ширины.
		/// </summary>
		/// <param name="paragraphParams">Параметры формирования абзаца.</param>
		/// <param name="pixelsInLines">Ширина каждой строки в пикселях.</param>
		/// <param name="linesParagraph">Результат - несколько строк.</param>
		/// <param name="fontBookmark">Шрифт метки.</param>
		/// <param name="isTable">Таблицы или нет.</param>
		/// <param name="pickUpFontSize">Подбирать или нет размер шрифта.</param>
		/// <returns></returns>
		internal string GetSplit(
			ParagraphParams paragraphParams,
			int[] pixelsInLines,
			ref IList<StringBuilder> linesParagraph,
			ref Font fontBookmark,
				bool isTable = false,
				bool pickUpFontSize = true)
		{
			if (null == paragraphParams) throw new ArgumentException();
			if (0 == pixelsInLines.Length) throw new ArgumentException("Не указана ширина строк для форматируемого текста!");
			if (null == fontBookmark) throw new ArgumentException();
			if (paragraphParams.MinFontSize > fontBookmark.Size) throw new ArgumentException();

			if (paragraphParams.PixelsAddLines <= 0)
				paragraphParams = new ParagraphParams(paragraphParams, pixelsInLines[pixelsInLines.Length - 1]);

			var linesList = new List<List<StringBuilder>>();
			int countDecreaseFont = -1;

			int paragraphCount = paragraphParams.Text.Count(),
				paragraphLastID = paragraphCount - 1;

			const float stepFontSize = 0.5F;

			int countLines;
			var pixels = new List<int>();
			pixels.AddRange(pixelsInLines);

			int startID,
				endID,
				endIDInLine;

			Font font;
			var tempSB = new StringBuilder();

			float fontSize = fontBookmark.Size;
			bool stop = false;
			bool addLines;
			int l;
			for (; fontSize > paragraphParams.MinFontSize && stop == false; fontSize -= stepFontSize)
			{
				addLines = false;
				startID = 0;
				font = new Font(fontBookmark.Name, fontSize, fontBookmark.Style);
				countLines = pixelsInLines.Length;

				pixels.Clear();
				pixels.AddRange(pixelsInLines);

				countDecreaseFont++;
				linesList.Add(new List<StringBuilder>());
				for (l = 0; l < countLines; ++l)
					linesList[countDecreaseFont].Add(new StringBuilder());

				for (l = 0; l < countLines; ++l)
				{
					endID = startID + GetLastID(pixels[l], font, paragraphParams.Text.Substring(startID));

					if (paragraphParams.PadRemainingLineWidthWithSpaces)
					{
						linesList[countDecreaseFont][l].Append(paragraphParams.Text.Substring(startID, endID - startID + 1));
						endIDInLine = linesList[countDecreaseFont][l].Length - 1;

						if ( ! paragraphParams.BreakWords)
							if (endID < paragraphLastID && paragraphParams.Text[endID + 1] != ' ')
								for (; endID >= startID; --endID, --endIDInLine)
									if (linesList[countDecreaseFont][l][endIDInLine] != ' ')
										linesList[countDecreaseFont][l][endIDInLine] = ' ';
									else
										break;

						if (endID <= startID || string.IsNullOrWhiteSpace(paragraphParams.Text.Substring(startID, endID - startID + 1)))
						{
							endID = startID + GetLastID(pixels[l], font, paragraphParams.Text.Substring(startID));
							linesList[countDecreaseFont][l].Clear();
							linesList[countDecreaseFont][l].Append(paragraphParams.Text.Substring(startID, endID - startID + 1));
						}

						tempSB = new StringBuilder(linesList[countDecreaseFont][l].ToString());
						PickUpSpaces(ref tempSB, font, pixels[l]);
						linesList[countDecreaseFont][l].Clear();
						linesList[countDecreaseFont][l].Append(tempSB);
					}
					else
					{
						if ( ! paragraphParams.BreakWords)
							if (endID < paragraphLastID && paragraphParams.Text[endID + 1] != ' ')
								for (; endID >= startID; --endID)
									if (paragraphParams.Text[endID] == ' ')
										break;

						if (endID <= startID || string.IsNullOrWhiteSpace(paragraphParams.Text.Substring(startID, endID - startID + 1)))
							endID = startID + GetLastID(pixels[l], font, paragraphParams.Text.Substring(startID));

						linesList[countDecreaseFont][l].Append(paragraphParams.Text.Substring(startID, endID - startID + 1));
					}

					if (l == countLines - 1)
						if (endID < paragraphLastID)
						{
							if (paragraphParams.CropText)
							{
								tempSB = new StringBuilder(linesList[countDecreaseFont][l].ToString());
								CropText(ref tempSB, font, pixels[l], paragraphParams.PadRemainingLineWidthWithSpaces);
								linesList[countDecreaseFont][l].Clear();
								linesList[countDecreaseFont][l].Append(tempSB);
							}
							else
							{
								if ( ! isTable)
									linesList[countDecreaseFont][l].Append("\n");
								addLines = true;
								countLines++;
								pixels.Add(paragraphParams.PixelsAddLines);
								linesList[countDecreaseFont].Add(new StringBuilder());
							}

							if ( ! pickUpFontSize)
								stop = true;
						}

					endID++;

					if (paragraphParams.RemoveSpacesAtBeginningOfLine)
						for (; endID <= paragraphLastID; ++endID)
							if (paragraphParams.Text[endID] != ' ')
								break;

					if (endID <= paragraphLastID)
						startID = endID;
					else
					{
						if ( ! addLines)
							stop = true;
						break;
					}
				}
			}

			int trueLinesList;

			if (paragraphParams.CropText)
			{
				trueLinesList = countDecreaseFont;
				fontBookmark = new Font(fontBookmark.Name, fontSize + stepFontSize, fontBookmark.Style);
			}
			else
			{
				trueLinesList = 0;
				int minLines = linesList[0].Count;

				for (int v = 1; v <= countDecreaseFont; ++v)
					if (minLines > linesList[v].Count)
					{
						minLines = linesList[v].Count;
						trueLinesList = v;
					}

				fontBookmark = new Font(fontBookmark.Name, fontBookmark.Size - trueLinesList * stepFontSize, fontBookmark.Style);

				if ( ! isTable)
				{
					int lastID = pixelsInLines.Length - 1,
						diffLinesCount = pixels.Count - pixelsInLines.Length;

					for (l = 0; l < diffLinesCount; ++l)
						linesList[trueLinesList][lastID].Append(linesList[trueLinesList][lastID + l + 1]);

					if (diffLinesCount > 0)
						linesList[trueLinesList].RemoveRange(lastID + 1, diffLinesCount);
				}
			}

			linesParagraph = new List<StringBuilder>(linesList[trueLinesList]);

			if (pixelsInLines.Length == 1) return linesList[trueLinesList][0].ToString();
			return string.Empty;
		}

		private int GetLastID(int pixelsInLine, Font font, string text)
		{
			int step = 32;
			const int ITERATIONS = 555;
			int lenText = text.Length;
			int lenCurrent = lenText;
			bool more = false;
			bool decreaseStep = false;
			for (int i = 0; i < ITERATIONS; ++i)
			{
				if (lenCurrent > lenText
				|| lenCurrent >= 0
				&& TextRenderer.MeasureText(
					new StringBuilder(text.Substring(0, lenCurrent)).ToString(), font).Width > pixelsInLine)
				{
					if (i > 0 && ! more)
					{
						if (step == 1) return lenCurrent - 2;
						decreaseStep = true;
					}
					more = true;
				}
				else
				{
					if (i > 0 && more)
					{
						if (step == 1) return lenCurrent - 1;
						decreaseStep = true;
					}
					more = false;
				}
				if (decreaseStep && step > 1)
					step /= 2;
				lenCurrent += (more ? -step : step);
			}
			return 0;
		}

		private void PickUpSpaces(ref StringBuilder text, Font font, int pixelsInLine)
		{
			if (TextRenderer.MeasureText(text.ToString(), font).Width <= pixelsInLine)
				while (true)
				{
					text.Append(' ');

					if (TextRenderer.MeasureText(text.ToString(), font).Width > pixelsInLine)
					{
						text.Remove(text.Length - 1, 1);
						break;
					}
				}
			else
				while (text[text.Length - 1] == ' ')
				{
					text.Remove(text.Length - 1, 1);

					if (TextRenderer.MeasureText(text.ToString(), font).Width <= pixelsInLine)
						break;
				}
		}

		private void LastLineCut(ref StringBuilder text, Font font, int pixelsInLine, int endIDInLine)
		{
			for (; endIDInLine >= 0; --endIDInLine)
				if (TextRenderer.MeasureText(text.ToString(), font).Width > pixelsInLine)
					text.Remove(endIDInLine, 1);
				else
					break;
		}

		private StringBuilder CropText(ref StringBuilder lastLine, Font font, int pixelsInLine, bool addSpaces)
		{
			const int countPoints = 3;
			int len = lastLine.ToString().TrimEnd().Length;
			lastLine.Insert(len, ".", countPoints);
			LastLineCut(ref lastLine, font, pixelsInLine, len - 1);
			if (addSpaces)
				PickUpSpaces(ref lastLine, font, pixelsInLine);
			return lastLine;
		}
	}
}
