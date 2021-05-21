using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Linq;

namespace DataAccessLayer.DataAccessClasses.Repositories.ConcreteDefinitions
{
    internal class DetailTypeRepository : AbstractRepository<DetailTypeEntity, long>, IDetailTypeRepository
    {
        public DetailTypeRepository(MainContext context) => _db = context;

        #region Entity
        public override void Add(DetailTypeEntity item) => _db.DetailTypes.Add(item);

        public override void Delete(long id)
        {
            DetailTypeEntity item = GetAll().Where(x => id == x.Id).SingleOrDefault();
            if(null != item)
                Delete(item);
        }
        private void Delete(DetailTypeEntity item) => _db.DetailTypes.Remove(item);
        #endregion

        #region Collection
        public override IQueryable<DetailTypeEntity> GetAll() => _db.DetailTypes;
        #endregion
    }
}
