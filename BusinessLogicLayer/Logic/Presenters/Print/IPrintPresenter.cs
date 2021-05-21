using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace BusinessLogicLayer.Logic.Presenters.Print
{
    public interface IPrintPresenter
    {
        byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage);
    }
}
