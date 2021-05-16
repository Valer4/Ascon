using BusinessLogicLayer.Services.Repositories.Interfaces;
using System.Linq;
using UserInterfaceLayer.Clients.Repositories.Interfaces;

namespace UserInterfaceLayer.Clients.Repositories.Classes
{
    public class AbstractRepositoryClient<TEntity, TInterfaceRepositoryService> : IAbstractRepositoryClient<TEntity>
        where TInterfaceRepositoryService : IAbstractRepositoryService<TEntity>
    {
        public IQueryable<TEntity> GetAll() =>
            new ChannelsManager().GetChannel<TInterfaceRepositoryService>().GetAll();
    }
}
