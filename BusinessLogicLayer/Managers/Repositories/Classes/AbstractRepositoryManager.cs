using BusinessLogicLayer.DataAccessInterfaces.Repositories;
using BusinessLogicLayer.Managers.Repositories.Interfaces;
using System;
using System.Linq;

namespace BusinessLogicLayer.Managers.Repositories.Classes
{
    public abstract class AbstractRepositoryManager<TEntity, TId, TInterfaceRepository> :
        IAbstractRepositoryManager<TEntity, TId>
            where TInterfaceRepository : IAbstractRepository<TEntity, TId>
    {
        public TInterfaceRepository _repository;

        public AbstractRepositoryManager(TInterfaceRepository repository) =>
            _repository = repository;

        public virtual void Save(string message = null)
        {
            try
            {
                _repository.Save();
            }
            catch(Exception ex)
            {
                throw new Exception(message, ex);
            }
        }

        #region Entity
        public virtual TEntity Get(TId id) => _repository.Get(id);
        public virtual void Add(TEntity entity) => _repository.Add(entity);
        public virtual void Edit(TEntity entity) => _repository.Edit(entity);
        public virtual void Delete(TId id) => _repository.Delete(id);
        #endregion

        #region Collection
        public virtual IQueryable<TEntity> GetAll() => _repository.GetAll();
        public virtual void AddCollection(IQueryable<TEntity> collection) => _repository.AddCollection(collection);
        public virtual void EditCollection(IQueryable<TEntity> collection) => _repository.EditCollection(collection);
        public virtual void DeleteCollection(IQueryable<TEntity> collection) => _repository.DeleteCollection(collection);
        #endregion
    }
}
