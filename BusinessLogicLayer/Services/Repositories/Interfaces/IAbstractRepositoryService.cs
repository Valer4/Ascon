﻿using System.Linq;
using System.ServiceModel;

namespace BusinessLogicLayer.Services.Repositories.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAbstractRepositoryService<TEntity, TId>
    {
        #region Entity.
        [OperationContract]
        TEntity Get(TId id);

        [OperationContract]
        void Add(TEntity entity);

        [OperationContract]
        void Edit(TEntity entity);

        [OperationContract]
        void Delete(TId id);
        #endregion

        #region Collection.
        [OperationContract]
        IQueryable<TEntity> GetAll();

        [OperationContract]
        void AddCollection(IQueryable<TEntity> collection);

        [OperationContract]
        void EditCollection(IQueryable<TEntity> collection);

        [OperationContract]
        void DeleteCollection(IQueryable<TEntity> collection);
        #endregion
    }
}
