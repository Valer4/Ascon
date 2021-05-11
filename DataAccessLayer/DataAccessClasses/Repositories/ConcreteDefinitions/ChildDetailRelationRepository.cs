using BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Entities.ConcreteDefinitions;
using System;
using System.Linq;

namespace DataAccessLayer.DataAccessClasses.Repositories.ConcreteDefinitions
{
    public class ChildDetailRelationRepository :
        AbstractRepository<ChildDetailRelationEntity, long>, IChildDetailRelationRepository
    {
        public ChildDetailRelationRepository(MainContext context) => _db = context;

        #region Entity.
        public override void Add(ChildDetailRelationEntity item) => _db.ChildDetailRelations.Add(item);

        public void Delete(long id)
        {
            ChildDetailRelationEntity item = GetAll().Where(x => id == x.Id).SingleOrDefault();
            if(null != item)
                Delete(item);
        }
        public override void Delete(ChildDetailRelationEntity item) => _db.ChildDetailRelations.Remove(item);
        #endregion

        #region Collection.
        public override IQueryable<ChildDetailRelationEntity> GetAll() => _db.ChildDetailRelations;
        #endregion
    }
}
