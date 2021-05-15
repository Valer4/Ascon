using BusinessLogicLayer.Entities.Classes.ConcreteDefinitions;

namespace BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions
{
    public interface IChildDetailRelationRepository : IAbstractRepository<ChildDetailRelationEntity, long>
    {
        void Delete(long id);
    }
}
