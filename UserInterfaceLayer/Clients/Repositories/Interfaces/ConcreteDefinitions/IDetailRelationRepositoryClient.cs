using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions

{
    public interface IDetailRelationRepositoryClient : IAbstractRepositoryClient<DetailRelationEntity>
    {
        string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount);

        string Edit(DetailRelationEntity selectedDetail, string name, string amount);

        string Delete(DetailRelationEntity selectedDetail);
    }
}
