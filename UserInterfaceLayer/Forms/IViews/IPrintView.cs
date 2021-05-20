using BusinessLogicLayer;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace UserInterfaceLayer.Forms.IViews
{
    internal interface IPrintView
    {
        event ReturnOutDelegate<byte[], DetailRelationEntity, string> GetReportOnDetailInMSWord;
    }
}
