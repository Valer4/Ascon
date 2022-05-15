using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Interfaces.Repositories.ConcreteDefinitions;
using System.Security.Permissions;
using WcfService.Security.RoleBasedAccessControl;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace WcfService.Services.Repositories.Classes.ConcreteDefinitions
{
	public class DetailRelationRepositoryService :
		AbstractRepositoryService<DetailRelationEntity, long, IDetailRelationRepositoryPresenter>,
		IDetailRelationRepositoryService
	{
		public DetailRelationRepositoryService(IDetailRelationRepositoryPresenter detailRelationRepositoryPresenter) : base(detailRelationRepositoryPresenter) {}

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount) => _repositoryPresenter.Add(selectedDetail, isRoot, name, amount);

		[PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
		public string Edit(DetailRelationEntity selectedDetail, string name, string amount) => _repositoryPresenter.Edit(selectedDetail, name, amount);
	}
}
