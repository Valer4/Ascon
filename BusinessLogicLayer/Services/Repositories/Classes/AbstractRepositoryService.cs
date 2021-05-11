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
        #region Entity.
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public TEntity Get(TId id) => Configurator._Container.Resolve<TInterfaceRepositoryManager>().Get(id);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void Add(TEntity entity) => Configurator._Container.Resolve<TInterfaceRepositoryManager>().Add(entity);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void Edit(TEntity entity) => Configurator._Container.Resolve<TInterfaceRepositoryManager>().Edit(entity);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void Delete(TEntity entity) => Configurator._Container.Resolve<TInterfaceRepositoryManager>().Delete(entity);
        #endregion

        #region Collection.
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public IQueryable<TEntity> GetAll() => Configurator._Container.Resolve<TInterfaceRepositoryManager>().GetAll();

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void AddCollection(IQueryable<TEntity> collection) =>
            Configurator._Container.Resolve<TInterfaceRepositoryManager>().AddCollection(collection);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void EditCollection(IQueryable<TEntity> collection) =>
            Configurator._Container.Resolve<TInterfaceRepositoryManager>().EditCollection(collection);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public void DeleteCollection(IQueryable<TEntity> collection) =>
            Configurator._Container.Resolve<TInterfaceRepositoryManager>().DeleteCollection(collection);
        #endregion
    }
}
