using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using WcfService.Services.Print;

namespace WcfClient.Print
{
	public class PrintClient : IPrintClient
	{
		private readonly ChannelsManager _channelsManager;

		public PrintClient(ChannelsManager channelsManager)
		{
			_channelsManager = channelsManager;
		}

		public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage) =>
			_channelsManager.GetChannel<IPrintService>().GetReportOnDetailInMSWord(selectedDetail, out warningMessage);
	}
}
