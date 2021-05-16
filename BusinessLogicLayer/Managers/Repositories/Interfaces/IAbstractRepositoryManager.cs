﻿using System.Linq;

namespace BusinessLogicLayer.Managers.Repositories.Interfaces
{
    public interface IAbstractRepositoryManager<TEntity, TId>
    {
        void Save(string message = null);

        #region Entity
        TEntity Get(TId id);
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        #endregion

        #region Collection
        IQueryable<TEntity> GetAll();
        void AddCollection(IQueryable<TEntity> collection);
        void EditCollection(IQueryable<TEntity> collection);
        void DeleteCollection(IQueryable<TEntity> collection);
        #endregion
    }
}
