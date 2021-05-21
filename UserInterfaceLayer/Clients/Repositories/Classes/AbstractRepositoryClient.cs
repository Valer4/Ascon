﻿using BusinessLogicLayer.Services.Repositories.Interfaces;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using System.Linq;
using UserInterfaceLayer.Clients.Repositories.Interfaces;

namespace UserInterfaceLayer.Clients.Repositories.Classes
{
    internal abstract class AbstractRepositoryClient<TEntity, TId, TInterfaceEntityService> :
        IAbstractRepositoryClient<TEntity, TId>
            where TInterfaceEntityService : IAbstractRepositoryService<TEntity, TId>
    {
        #region Entity.
        public TEntity Get(TId id) => new ChannelsManager().GetChannel<TInterfaceEntityService>().Get(id);

        public void Delete(TId id) => new ChannelsManager().GetChannel<TInterfaceEntityService>().DeleteById(id);
        public string Delete(TEntity entity) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().Delete(entity);
        #endregion

        #region Collection.
        public IQueryable<TEntity> GetAll() => new ChannelsManager().GetChannel<TInterfaceEntityService>().GetAll();
        public void AddCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().AddCollection(collection);
        public void EditCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().EditCollection(collection);
        public void DeleteCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().DeleteCollection(collection);
        #endregion
    }
}
