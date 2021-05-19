using System.Linq;

namespace UserInterfaceLayer.Clients.Repositories.Interfaces
{
    public interface IAbstractRepositoryClient<TEntity, TId>
    {
        #region Entity.
        TEntity Get(TId id);
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TId id);
        #endregion

        #region Collection.
        IQueryable<TEntity> GetAll();
        void AddCollection(IQueryable<TEntity> collection);
        void EditCollection(IQueryable<TEntity> collection);
        void DeleteCollection(IQueryable<TEntity> collection);
        #endregion
    }
}
