using BusinessLogicLayer.Entities.ConcreteDefinitions;
using BusinessLogicLayer.Managers.EntityManagers.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace BusinessLogicLayer.Services.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryService :
        AbstractRepositoryService<DetailRelationEntity, long, DetailRelationRepositoryManager>, IDetailRelationRepositoryService
    {
    }
}
