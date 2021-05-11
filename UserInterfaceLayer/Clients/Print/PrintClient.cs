using BusinessLogicLayer.Services.Print;

namespace UserInterfaceLayer.Clients.Print
{
    public class PrintClient : IPrintClient
    {
        public byte[] GetMSWord(long id) => new ChannelsManager().GetChannel<IPrintService>().GetMSWord(id);
    }
}
