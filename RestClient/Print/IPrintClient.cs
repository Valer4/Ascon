using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace RestClient.Print
{
    public interface IPrintClient
    {
        byte[] GetReportOnDetailInMSWord(
            string accessToken,
            DetailRelationEntity selectedDetail,
            out string message
        );
    }
}
