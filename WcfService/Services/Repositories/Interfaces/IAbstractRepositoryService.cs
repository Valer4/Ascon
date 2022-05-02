using System.Linq;
using System.ServiceModel;

namespace WcfService.Services.Repositories.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAbstractRepositoryService<TEntity, TId>
    {
        #region Entity.
        [OperationContract]
        TEntity Get(TId id);

        [OperationContract]
        void DeleteById(TId id);
        [OperationContract]
        string Delete(TEntity entity);
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
