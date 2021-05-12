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
        TInterfaceRepositoryManager _interfaceRepositoryManager;

        public AbstractRepositoryService(TInterfaceRepositoryManager interfaceRepositoryManager) =>
            _interfaceRepositoryManager = interfaceRepositoryManager;

        #region Entity.
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public TEntity Get(TId id) => _interfaceRepositoryManager.Get(id);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void Add(TEntity entity) => _interfaceRepositoryManager.Add(entity);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void Edit(TEntity entity) => _interfaceRepositoryManager.Edit(entity);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void Delete(TEntity entity) => _interfaceRepositoryManager.Delete(entity);
        #endregion

        #region Collection.
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public IQueryable<TEntity> GetAll() => _interfaceRepositoryManager.GetAll();

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void AddCollection(IQueryable<TEntity> collection) => _interfaceRepositoryManager.AddCollection(collection);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void EditCollection(IQueryable<TEntity> collection) => _interfaceRepositoryManager.EditCollection(collection);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void DeleteCollection(IQueryable<TEntity> collection) => _interfaceRepositoryManager.DeleteCollection(collection);
        #endregion
    }
}
