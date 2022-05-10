using System.Linq;
using WcfClient.Repositories.Interfaces;
using WcfService;
using WcfService.Services.Repositories.Interfaces;

namespace WcfClient.Repositories.Classes
{
    public abstract class AbstractRepositoryClient<TEntity, TId, TInterfaceEntityService> :
        IAbstractRepositoryClient<TEntity, TId>
            where TInterfaceEntityService : IAbstractRepositoryService<TEntity, TId>
    {
        private protected readonly ChannelsManager _channelsManager;

        public AbstractRepositoryClient(ChannelsManager channelsManager)
        {
            _channelsManager = channelsManager;
        }

#region Entity.

        public TEntity Get(TId id) => _channelsManager.GetChannel<TInterfaceEntityService>().Get(id);
        public void Delete(TId id) => _channelsManager.GetChannel<TInterfaceEntityService>().DeleteById(id);

        public string Delete(TEntity entity) => _channelsManager.GetChannel<TInterfaceEntityService>().Delete(entity);

#endregion

#region Collection.

        public IQueryable<TEntity> GetAll() => _channelsManager.GetChannel<TInterfaceEntityService>().GetAll();

        public void AddCollection(IQueryable<TEntity> collection) => _channelsManager.GetChannel<TInterfaceEntityService>().AddCollection(collection);
        public void EditCollection(IQueryable<TEntity> collection) => _channelsManager.GetChannel<TInterfaceEntityService>().EditCollection(collection);
        public void DeleteCollection(IQueryable<TEntity> collection) => _channelsManager.GetChannel<TInterfaceEntityService>().DeleteCollection(collection);

#endregion
    }
}
