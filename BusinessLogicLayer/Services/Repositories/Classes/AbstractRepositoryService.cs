using BusinessLogicLayer.Managers.Repositories.Interfaces;
using BusinessLogicLayer.Security.RoleBasedAccessControl;
using BusinessLogicLayer.Services.Repositories.Interfaces;
using System.Linq;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.Repositories.Classes
{
    public abstract class AbstractRepositoryService<TEntity, TId, TInterfaceRepositoryManager> :
        IAbstractRepositoryService<TEntity, TId>
            where TInterfaceRepositoryManager : IAbstractRepositoryManager<TEntity, TId>
    {
        private TInterfaceRepositoryManager _repositoryManager;

        public AbstractRepositoryService(TInterfaceRepositoryManager repositoryManager) =>
            _repositoryManager = repositoryManager;

        #region Entity.
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public TEntity Get(TId id) => _repositoryManager.Get(id);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void DeleteById(TId id) => _repositoryManager.Delete(id);
        #endregion

        #region Collection.
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public IQueryable<TEntity> GetAll() => _repositoryManager.GetAll();

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void AddCollection(IQueryable<TEntity> collection) => _repositoryManager.AddCollection(collection);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void EditCollection(IQueryable<TEntity> collection) => _repositoryManager.EditCollection(collection);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void DeleteCollection(IQueryable<TEntity> collection) => _repositoryManager.DeleteCollection(collection);
        #endregion
    }
}
