using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using WcfService;
using WcfService.Services.Print;

namespace WcfClient.Print
{
    public class PrintClient : IPrintClient
    {
        private readonly WcfClientConfigurator _wcfClientConfigurator;

        public PrintClient(WcfClientConfigurator wcfClientConfigurator)
        {
            _wcfClientConfigurator = wcfClientConfigurator;
        }

        public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage) =>
            new ChannelsManager(_wcfClientConfigurator).GetChannel<IPrintService>().GetReportOnDetailInMSWord(selectedDetail, out warningMessage);
    }
}
