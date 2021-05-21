using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions
{
    internal interface IDetailRelationRepositoryClient : IAbstractRepositoryClient<DetailRelationEntity, long>
    {
        string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount);

        string Edit(DetailRelationEntity selectedDetail, string name, string amount);
    }
}
