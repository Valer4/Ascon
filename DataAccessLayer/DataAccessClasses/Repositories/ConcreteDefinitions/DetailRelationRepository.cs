using BusinessLogicLayer;
using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DataAccessClasses.Repositories.ConcreteDefinitions
{
    public class DetailRelationRepository :
        AbstractRepository<DetailRelationEntity, long>, IDetailRelationRepository
    {
        private IDetailTypeRepository _detailTypeRepository;
        public IDetailTypeRepository _DetailTypeRepository
        {
            get
            {
                if (_detailTypeRepository == null)
                    _detailTypeRepository = new DetailTypeRepository(_db);
                return _detailTypeRepository;
            }
        }

        private IChildDetailRelationRepository _childDetailRelationRepository;
        public IChildDetailRelationRepository _ChildDetailRelationRepository
        {
            get
            {
                if (_childDetailRelationRepository == null)
                    _childDetailRelationRepository = new ChildDetailRelationRepository(_db);
                return _childDetailRelationRepository;
            }
        }

        public DetailRelationRepository(string nameOrConnectionString) => _db = new MainContext(nameOrConnectionString);

        public override void Save() => _db.SaveChanges();

        public override IQueryable<DetailRelationEntity> GetAll()
        {
            var listA = _DetailTypeRepository.GetAll();
            var listB = _ChildDetailRelationRepository.GetAll();

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
