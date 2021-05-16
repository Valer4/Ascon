using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryClient :
        AbstractRepositoryClient<DetailRelationEntity, IDetailRelationRepositoryService>,
            IDetailRelationRepositoryClient
    {
        public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount) =>
            new ChannelsManager().GetChannel<IDetailRelationRepositoryService>().Add(selectedDetail, isRoot, name, amount);

        public string Edit(DetailRelationEntity selectedDetail, string name, string amount) =>
            new ChannelsManager().GetChannel<IDetailRelationRepositoryService>().Edit(selectedDetail, name, amount);

        public string Delete(DetailRelationEntity selectedDetail) =>
            new ChannelsManager().GetChannel<IDetailRelationRepositoryService>().Delete(selectedDetail);
    }
}
