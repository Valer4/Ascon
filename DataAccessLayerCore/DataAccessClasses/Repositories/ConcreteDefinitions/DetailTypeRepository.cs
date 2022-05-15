using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace DataAccessLayerCore.DataAccessClasses.Repositories.ConcreteDefinitions
{
	internal class DetailTypeRepository : AbstractRepository<DetailTypeEntity, long>, IDetailTypeRepository
	{
		public DetailTypeRepository(MainContext? context) => _db = context;

#region Entity

		public override void Add(DetailTypeEntity entity) => _db?.DetailTypes?.Add(entity);

		public override void Delete(long id)
		{
			DetailTypeEntity? entity = GetAll()?.Where(x => id == x.Id).SingleOrDefault();
			if (null != entity)
				Delete(entity);
		}
		private void Delete(DetailTypeEntity entity) => _db?.DetailTypes?.Remove(entity);

#endregion

#region Collection

		public override IQueryable<DetailTypeEntity>? GetAll() => _db?.DetailTypes;

#endregion
	}
}
