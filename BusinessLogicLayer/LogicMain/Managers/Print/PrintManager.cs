using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Managers.Common;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BusinessLogicLayer.LogicMain.Managers.Print
{
    public class PrintManager : IPrintManager
    {
        private IDetailRelationRepository _repository;

        public PrintManager(IDetailRelationRepository repository) =>
            _repository = repository;

        public byte[] GetReportOnDetailInMSWord(long id)
        {
            DetailRelationRepositoryHelper helper = new DetailRelationRepositoryHelper();
            IQueryable<DetailRelationEntity> allDetailRelations = _repository.GetAll();

            DetailRelationEntity selectedDetail = helper.Find(id, allDetailRelations);

            ICollection<DetailRelationEntity> descendants = helper.GetDescendants(
                selectedDetail.TypeId, allDetailRelations, new List<DetailRelationEntity>()).ToList();

            DetailRelationEntity detailRelation;
            int lastId = descendants.Count - 1;
            for(int i = lastId; i >= 0; -- i)
            {
                detailRelation = descendants.ElementAt(i);

                if(helper.GetChilds(detailRelation.TypeId, allDetailRelations).Any())
                    descendants.Remove(detailRelation);
            }

            descendants = descendants.OrderBy(x => x.Name).ToList();
            int countRows = descendants.Count;

            object missing = Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            _Application word = new Application();
            _Document doc = word.Documents.Add(ref missing, ref missing, ref missing, ref missing);
            Range range;

            range = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            Paragraph paragraph = doc.Content.Paragraphs.Add(ref missing);
            paragraph.Format.SpaceAfter = 12;
            paragraph.Range.Text = $"«{selectedDetail.Name}»";
            paragraph.Range.InsertParagraphAfter();

            range = doc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            Table table = doc.Tables.Add(range, countRows, 2, ref missing, ref missing);
            table.Range.ParagraphFormat.SpaceAfter = 6;
            for(int r = 0; r < countRows; ++ r)
            {
                table.Cell(r + 1, 1).Range.Text = descendants.ElementAt(r).Name;
                table.Cell(r + 1, 2).Range.Text = $"{descendants.ElementAt(r).Amount} шт";
            }
            table.Borders[WdBorderType.wdBorderVertical].LineStyle =
            table.Borders[WdBorderType.wdBorderHorizontal].LineStyle =
            table.Borders[WdBorderType.wdBorderRight].LineStyle =
            table.Borders[WdBorderType.wdBorderBottom].LineStyle = 
            table.Borders[WdBorderType.wdBorderLeft].LineStyle =
            table.Borders[WdBorderType.wdBorderTop].LineStyle = WdLineStyle.wdLineStyleSingle;

            string filePath = "C:\\service.doc";

            word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            SaveAs(doc, filePath);
            doc.Close(SaveChanges: true);
            word.Visible = false;
            word.Application.Quit(SaveChanges: false);
            Marshal.ReleaseComObject(word);

            return File.ReadAllBytes(filePath);
        }

        private void SaveAs(_Document doc, string filePath)
        {
            int applicationVersion = Convert.ToInt32(doc.Application.Version.Split(new char[] { '.' }, 2)[0]);
            
            if(applicationVersion < 14)
                doc.SaveAs(filePath);
            else
                doc.SaveAs2(filePath);
        }
    }
}
