using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;

namespace BusinessLogicLayer
{
	public class MSDocHelper
	{
		internal class ParagraphSettings
		{
			internal string BookmarkName;
			internal int CountLines;

			internal ParagraphSettings(string bookmarkName, int countLines)
			{
				BookmarkName = bookmarkName;
				CountLines = countLines;
			}
		}

		internal void SetParagraph(
			_Application word,
			_Document doc,
			string bookmarkName,
				ParagraphParams paragraphParams,
					int countLines = 1,
					int[] pixelsInLines = null,
					int[] startLens = null,
					bool fitText = true,
						bool isTable = true,
						bool pickUpFontSize = false)
		{
			bool hereIsOneLine = countLines == 1;

			int l;
			IList<StringBuilder> linesParagraph = new List<StringBuilder>();

			System.Drawing.Font fontBookmark = GetFontBookmark(word, doc, hereIsOneLine ? bookmarkName : $"{ bookmarkName }1");

			IList<string> bookmarksNames = new List<string>();
			for (l = 0; l < countLines; ++l)
				bookmarksNames.Add(hereIsOneLine ? bookmarkName : $"{ bookmarkName }{ l + 1 }");

			if (null == pixelsInLines)
			{
				pixelsInLines = new int[countLines];

				for (l = 0; l < countLines; ++l)
					pixelsInLines[l] = GetPixelsInLineCell(
											word, doc, bookmarksNames[l], fontBookmark, null != startLens && startLens.Length > l ? startLens[l] : 0);
			}

			StringParser stringParser = new StringParser();
			bool needFitText = false;

			if (fitText)
			{
				if (pickUpFontSize) throw new ArgumentException();

				stringParser.GetSplit(out needFitText, paragraphParams, pixelsInLines, ref linesParagraph, ref fontBookmark);
			}
			else
			{
				stringParser.GetSplit(paragraphParams, pixelsInLines, ref linesParagraph, ref fontBookmark,
					isTable, pickUpFontSize);
			}

			if (isTable)
			{
				int diffLines = linesParagraph.Count - countLines;
				if (diffLines > 0)
				{
					doc.Bookmarks[bookmarksNames[0]].Select();

					int linesParagraphCount = linesParagraph.Count;
					for (l = countLines; l < linesParagraphCount; ++l)
					{
						word.Selection.Tables[1].Rows.Add();
						word.Selection.Tables[1].Rows.Last.Select();
						word.Selection.MoveLeft();
						word.Selection.Bookmarks.Add($"{ bookmarkName }{ l + 1 }");
					}

					countLines = linesParagraph.Count;
				}
			}

			float fontSize = fontBookmark?.Size ?? 0.0F;

			SetBookmarkExcel2003Compatibility(
				word, doc, hereIsOneLine ? bookmarkName : $"{ bookmarkName }1", linesParagraph[0].ToString(), needFitText, fontSize);

			for (l = 1; l < countLines; ++l)
				SetBookmarkExcel2003Compatibility(
					word, doc, $"{ bookmarkName }{ l + 1 }", linesParagraph[l].ToString(), needFitText, fontSize);
		}

		private int GetPixelsInLineCell(
			_Application word,
			_Document doc,
			string bookmarkName,
			System.Drawing.Font fontBookmark,
			int startLen = 0)
		{
			bool isTable = BookmarkInTable(word, doc, bookmarkName);

			int step = 16;

			const int ITERATIONS = 555;
			const string SYM = "5";
			bool more = false;
			bool decreaseStep = false;
			StringBuilder textSB = new StringBuilder().Insert(0, SYM, startLen > 0 ? startLen : 77);
			Range rangeStart,
					rangeEnd;
			int start,
				end;

			Selection selection = null;

			string bookmarkTextBackup = string.Empty;
			bool AllowAutoFitBackup = false;

			for (int i = 0; i < ITERATIONS; ++i)
			{
				doc.Bookmarks[bookmarkName].Select();
				selection = word.Selection;

				if (i == 0)
				{
					bookmarkTextBackup = selection.Text;

					if (isTable)
					{
						AllowAutoFitBackup = word.Selection.Tables[1].AllowAutoFit;
						word.Selection.Tables[1].AllowAutoFit = false;
					}
				}

				selection.Text = textSB.ToString();

				rangeStart = selection.Range;
				rangeEnd = selection.Range;

				rangeEnd.MoveEnd(WdUnits.wdCharacter, -1);
				rangeEnd.Collapse(WdCollapseDirection.wdCollapseEnd);

				start = rangeStart.Information[WdInformation.wdFirstCharacterLineNumber];
				end = rangeEnd.Information[WdInformation.wdFirstCharacterLineNumber];

				if (end - start + 1 > 1)
				{
					if (i > 0 && ! more)
					{
						if (step == 1)
						{
							textSB.Remove(textSB.Length - 1, 1);
							break;
						}
						decreaseStep = true;
					}
					more = true;

					if (decreaseStep && step > 1)
						step /= 2;
					textSB.Remove(Math.Max(textSB.Length - step, 0), step);
				}
				else
				{
					if (i > 0 && more)
					{
						if (step == 1)
							break;
						decreaseStep = true;
					}
					more = false;

					if (decreaseStep && step > 1)
						step /= 2;
					textSB.Insert(textSB.Length, SYM, step);
				}

				selection.Bookmarks.Add(bookmarkName);
			}

			selection.Text = bookmarkTextBackup;
			selection.Bookmarks.Add(bookmarkName);

			int offset = 0;

			if (isTable)
				word.Selection.Tables[1].AllowAutoFit = AllowAutoFitBackup;
			else
			{
				// Похоже проблема из-за '\r'.

				string prefix = selection.FormattedText.Sentences.First.Text.TrimEnd(new char[] { '\r' });
				int index = prefix.IndexOf(bookmarkTextBackup);
				if (index != -1)
					prefix = prefix.Remove(index, bookmarkTextBackup.Length);

				offset = TextRenderer.MeasureText(prefix, fontBookmark).Width;

				if ( ! string.IsNullOrEmpty(prefix))
					offset += 7;
			}

			string text = textSB.ToString();
			
			if (0 == offset
			&& ! string.IsNullOrEmpty(text))
				offset = 1;
			
			return TextRenderer.MeasureText(text, fontBookmark).Width - offset;
		}

		private bool BookmarkInTable(_Application word, _Document doc, string bookmarkName)
		{
			if ( ! doc.Bookmarks.Exists(bookmarkName)) throw new InvalidOperationException();

			doc.Bookmarks[bookmarkName].Select();

			if (Convert.ToInt32(word.Selection.Tables.Count) > 0)
				return true;

			return false;
		}

		internal System.Drawing.Font GetFontBookmark(_Application word, _Document doc, string bookmarkName)
		{
			if ( ! doc.Bookmarks.Exists(bookmarkName)) throw new InvalidOperationException();

			doc.Bookmarks[bookmarkName].Select();
			Selection selection = word.Selection;

			string name = selection.Font.Name;
			float size = selection.Font.Size;
			System.Drawing.FontStyle bold = (selection.Font.Bold == 1) ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular;
			System.Drawing.FontStyle italic = (selection.Font.Italic == 1) ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular;
			System.Drawing.FontStyle underline = (selection.Font.Underline == WdUnderline.wdUnderlineSingle) ? System.Drawing.FontStyle.Underline : System.Drawing.FontStyle.Regular;

			return new System.Drawing.Font(name, size, bold | italic | underline);
		}

		internal void SetFontBookmark(_Application word, _Document doc, string bookmarkName, System.Drawing.Font fontBookmark)
		{
			if ( ! doc.Bookmarks.Exists(bookmarkName)) throw new InvalidOperationException();

			doc.Bookmarks[bookmarkName].Select();
			Selection selection = word.Selection;

			selection.Font.Name = fontBookmark.Name;
			selection.Font.Size = fontBookmark.Size;
			selection.Font.Bold = (int)(fontBookmark.Style & System.Drawing.FontStyle.Bold) > 0 ? 1 : 0;
			selection.Font.Italic = (int)(fontBookmark.Style & System.Drawing.FontStyle.Italic) > 0 ? 1 : 0;
			selection.Font.Underline = (int)(fontBookmark.Style & System.Drawing.FontStyle.Underline) > 0 ? WdUnderline.wdUnderlineSingle : WdUnderline.wdUnderlineNone;
		}

		internal void SetBookmarkExcel2003Compatibility(
			_Application word,
			_Document doc,
			string bookmarkName,
			string text,
			bool needFitText = true,
			float fontSize = 0.0F) =>
				SetBookmark(word, doc, bookmarkName, Excel2003Compatibility(ref text), needFitText, fontSize);

		private string Excel2003Compatibility(ref string text)
		{
			if (string.IsNullOrEmpty(text))
				text = CommonStrings.SpaceString;

			return text;
		}

		/// <summary>
		/// Выводит текст на указанную метку.
		/// </summary>
		internal void SetBookmark(
			_Application word,
			_Document doc,
			string bookmarkName,
			string text,
				bool needFitText = true,
				float fontSize = 0.0f,
					bool debug = false,
					int countLines = 1)
		{
			if ( ! debug)
			{
				if (doc.Bookmarks.Exists(bookmarkName))
				{
					doc.Bookmarks[bookmarkName].Select();
					Selection selection = word.Selection;

					if ( ! string.IsNullOrEmpty(text))
					{
						if (needFitText)
						{
							if ( ! BookmarkInTable(word, doc, bookmarkName))
								throw new ArgumentException();

							selection.Range.Cells[1].FitText = true;
						}
					
						if (fontSize >= 1.0F) selection.Font.Size = fontSize;

						selection.TypeText(text);
					}
					else
						selection.Delete();
				}
			}
			else
			{
				bool hereIsOneLine = countLines == 1;

				System.Drawing.Font fontBookmark = GetFontBookmark(word, doc, hereIsOneLine ? bookmarkName : $"{ bookmarkName }1");

				string bookmarkName2 = bookmarkName;
				int pixelsInLine;

				for (int l = 0; l < countLines; ++l)
				{
					if ( ! hereIsOneLine)
						bookmarkName2 = $"{ bookmarkName }{l + 1}";

					pixelsInLine = GetPixelsInLineCell(word, doc, bookmarkName2, fontBookmark);

					word.Selection.TypeText($"[{ bookmarkName2 }][{ pixelsInLine }]");
				}
			}
		}

		internal string GetFilePath()
		{
			string dir = string.Format(@"{0}/Пример печати/Temp/{1:yyyy\/MM\/dd}",
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace(@"\", "/"),
				DateTime.Now);

			if ( ! Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			return $"{ dir }/word{Guid.NewGuid()}.doc";
		}

		/// <summary>
		/// Закрывает Word.
		/// </summary>
		public void ReleaseMSWord(_Application word)
		{
			word.DisplayAlerts = WdAlertLevel.wdAlertsNone;

			Documents documents = word.Documents;
			foreach (_Document doc in documents)
				CloseDocument(word, doc);

			word.Visible = false;
			word.Application.Quit(SaveChanges: false);
			Marshal.ReleaseComObject(word);
		}

		private void CloseDocument(_Application word, _Document doc, string filePath = null)
		{
			WdAlertLevel backup = word.DisplayAlerts;
			word.DisplayAlerts = WdAlertLevel.wdAlertsNone;

			if ( ! string.IsNullOrEmpty(filePath))
				SaveAs(doc, filePath);

			doc.Close(SaveChanges: false);

			word.DisplayAlerts = backup;
		}

		private void SaveAs(_Document doc, string filePath, WdSaveFormat format = WdSaveFormat.wdFormatDocumentDefault)
		{
			int applicationVersion = Convert.ToInt32(doc.Application.Version.Split(new char[] { '.' }, 2)[0]);
			
			if (applicationVersion < 14)
				doc.SaveAs(filePath, format);
			else
				doc.SaveAs2(filePath, format);
		}

		internal void PrintEmptyTemplate(string typeReport)
		{
			_Application word = new Application();
			_Document doc = GetDoc(typeReport, ref word);

			Bookmarks bookmarks = doc.Bookmarks;

			foreach (Bookmark bookmark in bookmarks)
			{
				bookmark.Select();
				word.Selection.TypeText(CommonStrings.SpaceString);
			}
		}

		/// <summary>
		/// Создает документ, используя шаблон по заданному пути.
		/// </summary>
		public Document GetDoc(string typeReport, ref _Application word, string templatesDirectory = "document_templates", bool useOnlyTemplatesDirectory = false)
		{
			string filePath = null;

			try
			{
				filePath = $@"{(
					!useOnlyTemplatesDirectory ?
						// AppDomain.CurrentDomain.BaseDirectory : // Папка с .exe.
						(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath) + @"\") :
						string.Empty)
							}{templatesDirectory}\{typeReport}";

				return word.Documents.Add(filePath);
			}
			catch (Exception ex)
			{
				throw new Exception($@"Не удалось загрузить шаблон документа { filePath }{ '\n' }{ ex.Message }");
			}
		}

		public void ShowMSWord(string filePath)
		{
			try
			{
				_Application word = new Application();
				word.Documents.Add(filePath);
				word.Visible = true;
			}
			catch (Exception ex)
			{
				throw new Exception(
					$@"Не удалось загрузить шаблон для экспорта { filePath }{'\n'}{ ex.Message }");
			}
		}

		internal string GetBookmarkText(_Application word, _Document doc, string bookmarkName)
		{
			if ( ! doc.Bookmarks.Exists(bookmarkName)) throw new InvalidOperationException();

			doc.Bookmarks[bookmarkName].Select();

			return word.Selection.Text;
		}

		internal void CopyPaste(_Document docCopy, _Document docPaste, bool inEnd = true)
		{
			docCopy.ActiveWindow.Selection.WholeStory();
			docCopy.ActiveWindow.Selection.Copy();

			if (inEnd)
				MovePointerToEnd(docPaste);

			docPaste.ActiveWindow.Selection.PasteAndFormat(WdRecoveryType.wdFormatOriginalFormatting);
		}

		internal void MovePointerToEnd(_Document doc)
		{
			doc.Range(
				doc.Content.End - 1,
				doc.Content.End - 1).Select();
		}

		public int GetPagesCount(_Document doc) => doc.ComputeStatistics(WdStatistic.wdStatisticPages, Type.Missing);
	}
}
