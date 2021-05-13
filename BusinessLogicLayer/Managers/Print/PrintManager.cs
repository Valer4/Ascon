using BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Entities.ConcreteDefinitions;
using BusinessLogicLayer.Managers.Common;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BusinessLogicLayer.Managers.Print
{
    public class PrintManager : IPrintManager
    {
        public IDetailRelationRepository _repository;

        public PrintManager(IDetailRelationRepository repository) =>
            _repository = repository;

        public byte[] GetMSWord(long id)
        {
            DetailRelationRepositoryHelper helper = new DetailRelationRepositoryHelper();
            IQueryable<DetailRelationEntity> allDetailRelations = _repository.GetAll();

            DetailRelationEntity selectedDetailRelation = helper.Find(id, allDetailRelations);

            ICollection<DetailRelationEntity> descendants = helper.GetDescendants(
                selectedDetailRelation.TypeId, allDetailRelations, new List<DetailRelationEntity>()).ToList();

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
            paragraph.Range.Text = $"«{selectedDetailRelation.Name}»";
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

            string fileName = "C:\\service.doc";

            word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            doc.SaveAs(fileName);
            word.Application.Documents.Close();
            word.Application.Quit(false);
            Marshal.ReleaseComObject((Application)word);

            return File.ReadAllBytes(fileName);
        }
    }
}
