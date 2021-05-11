using BusinessLogicLayer.Managers.Repositories.Interfaces;
using System;
using System.Linq;

namespace BusinessLogicLayer.Managers.Repositories.Classes
{
    public abstract class AbstractRepositoryManager<TEntity, TId> : IAbstractRepositoryManager<TEntity, TId>
    {
        #region Entity.
        public virtual TEntity Get(TId id) => throw new NotImplementedException();
        public virtual void Add(TEntity entity) => throw new NotImplementedException();
        public virtual void Edit(TEntity entity) => throw new NotImplementedException();
        public virtual void Delete(TEntity entity) => throw new NotImplementedException();
        #endregion

        #region Collection.
        public virtual IQueryable<TEntity> GetAll() => throw new NotImplementedException();
        public virtual void AddCollection(IQueryable<TEntity> collection) => throw new NotImplementedException();
        public virtual void EditCollection(IQueryable<TEntity> collection) => throw new NotImplementedException();
        public virtual void DeleteCollection(IQueryable<TEntity> collection) => throw new NotImplementedException();
        #endregion
    }
}
