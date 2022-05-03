using System.Linq;

namespace UserInterfaceLayer.Clients.Repositories.Interfaces
{
    internal interface IAbstractRepositoryClient<TEntity, TId>
    {
#region Entity.

        TEntity Get(TId id);
        void Delete(TId id);

        string Delete(TEntity entity);

#endregion

#region Collection.

        IQueryable<TEntity> GetAll();

        void AddCollection(IQueryable<TEntity> collection);
        void EditCollection(IQueryable<TEntity> collection);
        void DeleteCollection(IQueryable<TEntity> collection);

#endregion
    }
}
