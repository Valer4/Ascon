using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Logic.Presenters.Interfaces.Repositories;
using BusinessLogicLayer.Managers.Repositories.Interfaces.ConcreteDefinitions;
using BusinessLogicLayer.Security.RoleBasedAccessControl;
using BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryService :
        AbstractRepositoryService<DetailRelationEntity, long, IDetailRelationRepositoryManager>,
        IDetailRelationRepositoryService
    {
        IDetailRelationRepositoryPresenter _detailRelationRepositoryPresenter;

        public DetailRelationRepositoryService(
            IDetailRelationRepositoryPresenter detailRelationRepositoryPresenter,
            IDetailRelationRepositoryManager detailRelationRepositoryManager) :
                base(detailRelationRepositoryManager) =>
                    _detailRelationRepositoryPresenter = detailRelationRepositoryPresenter;

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount) =>
            _detailRelationRepositoryPresenter.Add(selectedDetail, isRoot, name, amount);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public string Edit(DetailRelationEntity selectedDetail, string name, string amount) =>
            _detailRelationRepositoryPresenter.Edit(selectedDetail, name, amount);

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public string Delete(DetailRelationEntity selectedDetail) =>
            _detailRelationRepositoryPresenter.Delete(selectedDetail);
    }
}
