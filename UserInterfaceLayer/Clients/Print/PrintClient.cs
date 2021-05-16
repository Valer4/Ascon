using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Print;

namespace UserInterfaceLayer.Clients.Print
{
    public class PrintClient : IPrintClient
    {
        public byte[] GetMSWord(DetailRelationEntity selectedDetail, out string warningMessage) =>
            new ChannelsManager().GetChannel<IPrintService>().GetMSWord(selectedDetail, out warningMessage);
    }
}
