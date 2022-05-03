using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace RestClient.Print
{
	internal interface IPrintClient
    {
        byte[] GetReportOnDetailInMSWord(
            string accessToken,
            DetailRelationEntity selectedDetail,
            out string message
        );
    }
}
