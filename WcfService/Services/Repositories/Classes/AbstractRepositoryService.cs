using BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces;
using System.Linq;
using System.Security.Permissions;
using WcfService.Security.RoleBasedAccessControl;
using WcfService.Services.Repositories.Interfaces;

namespace WcfService.Services.Repositories.Classes
{
	public abstract class AbstractRepositoryService<TEntity, TId, TInterfaceRepositoryPresenter> :
		IAbstractRepositoryService<TEntity, TId>
			where TInterfaceRepositoryPresenter : IAbstractRepositoryPresenter<TEntity, TId>
	{
		protected TInterfaceRepositoryPresenter _repositoryPresenter;

		public AbstractRepositoryService(TInterfaceRepositoryPresenter repositoryPresenter) => _repositoryPresenter = repositoryPresenter;

#region Entity.

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public TEntity Get(TId id) => _repositoryPresenter.AbstractRepositoryManager.Get(id);

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public void DeleteById(TId id) => _repositoryPresenter.AbstractRepositoryManager.Delete(id);
		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public string Delete(TEntity entity) => _repositoryPresenter.Delete(entity);

#endregion

#region Collection.

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public IQueryable<TEntity> GetAll() => _repositoryPresenter.AbstractRepositoryManager.GetAll();

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public void AddCollection(IQueryable<TEntity> collection) => _repositoryPresenter.AbstractRepositoryManager.AddCollection(collection);

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public void EditCollection(IQueryable<TEntity> collection) => _repositoryPresenter.AbstractRepositoryManager.EditCollection(collection);

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public void DeleteCollection(IQueryable<TEntity> collection) => _repositoryPresenter.AbstractRepositoryManager.DeleteCollection(collection);

#endregion
	}
}
