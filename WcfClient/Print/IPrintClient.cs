using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace WcfClient.Print
{
	internal interface IPrintClient
    {
        byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string message);
    }
}
