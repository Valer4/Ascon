using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions
{
    public interface IDetailRelationRepository : IAbstractRepository<DetailRelationEntity, long>
    {
        IDetailTypeRepository _DetailTypeRepository { get; }
        IChildDetailRelationRepository _ChildDetailRelationRepository { get; }
    }
}
