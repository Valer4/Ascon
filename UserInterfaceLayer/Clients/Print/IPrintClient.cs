using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Print
{
	internal interface IPrintClient
	{
		byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string message);
	}
}
