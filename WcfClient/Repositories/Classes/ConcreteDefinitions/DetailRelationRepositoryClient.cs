using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using WcfClient.Repositories.Interfaces.ConcreteDefinitions;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace WcfClient.Repositories.Classes.ConcreteDefinitions
{
	public class DetailRelationRepositoryClient :
		AbstractRepositoryClient<DetailRelationEntity, long, IDetailRelationRepositoryService>,
		IDetailRelationRepositoryClient
	{
		public DetailRelationRepositoryClient(ChannelsManager channelsManager)
			: base(channelsManager)
		{
		}

		public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount) =>
			_channelsManager.GetChannel<IDetailRelationRepositoryService>().Add(selectedDetail, isRoot, name, amount);

		public string Edit(DetailRelationEntity selectedDetail, string name, string amount) =>
			_channelsManager.GetChannel<IDetailRelationRepositoryService>().Edit(selectedDetail, name, amount);
	}
}
