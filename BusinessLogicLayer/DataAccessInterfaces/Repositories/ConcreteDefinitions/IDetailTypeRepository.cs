using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions
{
    public interface IDetailTypeRepository : IAbstractRepository<DetailTypeEntity, long>
    {
        void Delete(long id);
    }
}
