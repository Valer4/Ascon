using BusinessLogicLayer.DataAccessInterfaces.Repositories;
using System;
using System.Linq;

namespace DataAccessLayer.DataAccessClasses.Repositories
{
    public abstract class AbstractRepository<TEntity, TId> : IAbstractRepository<TEntity, TId>
    {
        protected MainContext _db;

        public virtual void Save() => throw new NotImplementedException();

        #region Entity
        public virtual TEntity Get(TId id) => throw new NotImplementedException();
        public virtual void Add(TEntity entity) => throw new NotImplementedException();
        public virtual void Edit(TEntity entity) => throw new NotImplementedException();
        public virtual void Delete(TId id) => throw new NotImplementedException();
        #endregion

        #region Collection
        public virtual IQueryable<TEntity> GetAll() => throw new NotImplementedException();
        public virtual void AddCollection(IQueryable<TEntity> collection) => throw new NotImplementedException();
        public virtual void EditCollection(IQueryable<TEntity> collection) => throw new NotImplementedException();
        public virtual void DeleteCollection(IQueryable<TEntity> collection) => throw new NotImplementedException();
        #endregion
    }
}
