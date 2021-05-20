using BusinessLogicLayer.Services.Print;

namespace UserInterfaceLayer.Clients.Print
{
    internal class PrintClient : IPrintClient
    {
        public byte[] GetReportOnDetailInMSWord(long id) =>
            new ChannelsManager().GetChannel<IPrintService>().GetReportOnDetailInMSWord(id);
    }
}
