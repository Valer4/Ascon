using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace UserInterfaceLayer.Forms.IViews
{
    public interface IPrintView
    {
        event GetReportEventHandler<DetailRelationEntity> GetReportOnDetailInMSWord;
    }

    public delegate byte[] GetReportEventHandler<T>(T entity, out string warningMessage);
}
