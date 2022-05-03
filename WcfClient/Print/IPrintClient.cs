using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace WcfClient.Print
{
	public interface IPrintClient
    {
        byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string message);
    }
}
