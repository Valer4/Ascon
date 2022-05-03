using System.Linq;
using WcfClient.Repositories.Interfaces;
using WcfService;
using WcfService.Services.Repositories.Interfaces;

namespace WcfClient.Repositories.Classes
{
	internal abstract class AbstractRepositoryClient<TEntity, TId, TInterfaceEntityService> :
        IAbstractRepositoryClient<TEntity, TId>
            where TInterfaceEntityService : IAbstractRepositoryService<TEntity, TId>
    {
        protected readonly WcfClientConfigurator _wcfClientConfigurator;

        public AbstractRepositoryClient(WcfClientConfigurator wcfClientConfigurator)
        {
            _wcfClientConfigurator = wcfClientConfigurator;
        }

#region Entity.

        public TEntity Get(TId id) => new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceEntityService>().Get(id);
        public void Delete(TId id) => new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceEntityService>().DeleteById(id);

        public string Delete(TEntity entity) =>
            new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceEntityService>().Delete(entity);

#endregion

#region Collection.

        public IQueryable<TEntity> GetAll() => new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceEntityService>().GetAll();

        public void AddCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceEntityService>().AddCollection(collection);
        public void EditCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceEntityService>().EditCollection(collection);
        public void DeleteCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceEntityService>().DeleteCollection(collection);

#endregion
    }
}
