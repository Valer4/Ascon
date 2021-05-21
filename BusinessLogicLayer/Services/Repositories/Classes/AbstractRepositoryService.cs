using BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces;
using BusinessLogicLayer.Security.RoleBasedAccessControl;
using BusinessLogicLayer.Services.Repositories.Interfaces;
using System.Linq;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.Repositories.Classes
{
    public abstract class AbstractRepositoryService<TEntity, TId, TInterfaceRepositoryPresenter, TInterfaceRepositoryManager> :
        IAbstractRepositoryService<TEntity, TId>
            where TInterfaceRepositoryPresenter : IAbstractRepositoryPresenter<TEntity>
            where TInterfaceRepositoryManager : IAbstractRepositoryManager<TEntity, TId>
    {
        TInterfaceRepositoryPresenter _repositoryPresenter;
        private TInterfaceRepositoryManager _repositoryManager;

        public AbstractRepositoryService(TInterfaceRepositoryPresenter repositoryPresenter, TInterfaceRepositoryManager repositoryManager)
        {
            _repositoryPresenter = repositoryPresenter;
            _repositoryManager = repositoryManager;
        }

        #region Entity.
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public TEntity Get(TId id) => _repositoryManager.Get(id);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void DeleteById(TId id) => _repositoryManager.Delete(id);
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public string Delete(TEntity entity) => _repositoryPresenter.Delete(entity);
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
