using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Managers.Repositories.Interfaces.ConcreteDefinitions;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace BusinessLogicLayer.Services.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryService :
        AbstractRepositoryService<DetailRelationEntity, long, IDetailRelationRepositoryManager>,
        IDetailRelationRepositoryService
    {
        public DetailRelationRepositoryService(IDetailRelationRepositoryManager _detailRelationRepositoryManager) :
            base(_detailRelationRepositoryManager) {}
    }
}
