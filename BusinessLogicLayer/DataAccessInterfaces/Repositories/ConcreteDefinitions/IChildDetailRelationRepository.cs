using BusinessLogicLayer.Entities.ConcreteDefinitions;

namespace BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions
{
    public interface IChildDetailRelationRepository : IAbstractRepository<ChildDetailRelationEntity, long>
    {
        void Delete(long id);
    }
}
