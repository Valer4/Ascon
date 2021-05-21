using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Print;

namespace UserInterfaceLayer.Clients.Print
{
    internal class PrintClient : IPrintClient
    {
        public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage) =>
            new ChannelsManager().GetChannel<IPrintService>().GetReportOnDetailInMSWord(selectedDetail, out warningMessage);
    }
}
