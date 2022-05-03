using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using WcfClient.Repositories.Interfaces.ConcreteDefinitions;
using WcfService;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace WcfClient.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryClient :
        AbstractRepositoryClient<DetailRelationEntity, long, IDetailRelationRepositoryService>,
        IDetailRelationRepositoryClient
    {
        public DetailRelationRepositoryClient(WcfClientConfigurator wcfClientConfigurator)
            : base(wcfClientConfigurator)
        {
		}

        public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount) =>
            new ChannelsManager(_wcfClientConfigurator).GetChannel<IDetailRelationRepositoryService>().Add(selectedDetail, isRoot, name, amount);

        public string Edit(DetailRelationEntity selectedDetail, string name, string amount) =>
            new ChannelsManager(_wcfClientConfigurator).GetChannel<IDetailRelationRepositoryService>().Edit(selectedDetail, name, amount);
    }
}
