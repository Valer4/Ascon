using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryClient : AbstractRepositoryClient<DetailRelationEntity, long, IDetailRelationRepositoryService>, IDetailRelationRepositoryClient
    {
    }
}
