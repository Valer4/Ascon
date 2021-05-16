using BusinessLogicLayer.Managers.Repositories.Interfaces;
using BusinessLogicLayer.Security.RoleBasedAccessControl;
using BusinessLogicLayer.Services.Repositories.Interfaces;
using System.Linq;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.Repositories.Classes
{
    public class AbstractRepositoryService<TEntity, TId, TInterfaceRepositoryManager> :
        IAbstractRepositoryService<TEntity>
            where TInterfaceRepositoryManager : IAbstractRepositoryManager<TEntity, TId>
    {
        TInterfaceRepositoryManager _repositoryManager;

        public AbstractRepositoryService(TInterfaceRepositoryManager repositoryManager) =>
            _repositoryManager = repositoryManager;

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public IQueryable<TEntity> GetAll() => _repositoryManager.GetAll();
    }
}
