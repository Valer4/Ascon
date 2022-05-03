using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using RestPrint = RestClient.Print;
using WcfPrint = WcfClient.Print;

namespace UserInterfaceLayer.Clients.Print
{
	internal class PrintClient : IPrintClient
    {
        private readonly WcfPrint.IPrintClient _wcfPrint;
        private readonly RestPrint.IPrintClient _restPrint;

        internal PrintClient(
            WcfPrint.IPrintClient wcfPrint,
            RestPrint.IPrintClient restPrint
        )
        {
            _wcfPrint = wcfPrint;
            _restPrint = restPrint;
        }

        public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage)
        {
            if (ChannelType.Wcf == Program.ChannelType)
                return _wcfPrint.GetReportOnDetailInMSWord(selectedDetail, out warningMessage);

            return _restPrint.GetReportOnDetailInMSWord(Program.AccessToken, selectedDetail, out warningMessage);
        }
    }
}
