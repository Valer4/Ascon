using BusinessLogicLayer;
using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayerCore.DataAccessClasses.Repositories.ConcreteDefinitions
{
	public class DetailRelationRepository : AbstractRepository<DetailRelationEntity, long>, IDetailRelationRepository
	{
		private IDetailTypeRepository? _detailTypeRepository;
		public IDetailTypeRepository DetailTypeRepository
		{
			get
			{
				if (null == _detailTypeRepository)
					_detailTypeRepository = new DetailTypeRepository(_db);
				return _detailTypeRepository;
			}
		}

		private IChildDetailRelationRepository? _childDetailRelationRepository;
		public IChildDetailRelationRepository ChildDetailRelationRepository
		{
			get
			{
				if (null == _childDetailRelationRepository)
					_childDetailRelationRepository = new ChildDetailRelationRepository(_db);
				return _childDetailRelationRepository;
			}
		}

		public DetailRelationRepository(DbContextOptions<MainContext> options) => _db = new MainContext(options);

		public override IQueryable<DetailRelationEntity> GetAll()
		{
			IQueryable<DetailTypeEntity> listA = DetailTypeRepository.GetAll();
			IQueryable<ChildDetailRelationEntity> listB = ChildDetailRelationRepository.GetAll();

			IEnumerable<DetailRelationEntity> query =
				from itemA in listA.Outer()
				join itemB in listB.Outer()
					on itemA.Id equals itemB.TypeId
				select new DetailRelationEntity()
				{
					Id = itemA.IsRoot ? itemA.Id * -1 - 1 : itemB.Id,
					IsRoot = itemA.IsRoot,
					RelationId = itemB?.Id,
					ParentId = itemB?.ParentId,
					TypeId = itemA.Id,
					Name = itemA.Name,
					Amount = itemB?.Amount
				};

			return query.AsQueryable();
		}
	}
}
