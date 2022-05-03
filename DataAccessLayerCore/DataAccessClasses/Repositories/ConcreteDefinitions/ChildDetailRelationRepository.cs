using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace DataAccessLayerCore.DataAccessClasses.Repositories.ConcreteDefinitions
{
    internal class ChildDetailRelationRepository :
        AbstractRepository<ChildDetailRelationEntity, long>,
        IChildDetailRelationRepository
    {
        public ChildDetailRelationRepository(MainContext? context) => _db = context;

#region Entity

        public override void Add(ChildDetailRelationEntity entity) => _db?.ChildDetailRelations?.Add(entity);

        public override void Delete(long id)
        {
            ChildDetailRelationEntity? entity = GetAll()?.Where(x => id == x.Id).SingleOrDefault();
            if (null != entity)
                Delete(entity);
        }
        private void Delete(ChildDetailRelationEntity entity) => _db?.ChildDetailRelations?.Remove(entity);

#endregion

#region Collection

        public override IQueryable<ChildDetailRelationEntity>? GetAll() => _db?.ChildDetailRelations;

#endregion
    }
}
