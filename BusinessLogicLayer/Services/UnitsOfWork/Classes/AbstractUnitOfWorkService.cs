using BusinessLogicLayer.Security.RoleBasedAccessControl;
using BusinessLogicLayer.Managers.UnitsOfWorkManagers.Interfaces;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.UnitsOfWork.Classes
{
    public abstract class AbstractUnitOfWorkService<TModel, TInterfaceUnitOfWorkManager>
        where TInterfaceUnitOfWorkManager : IAbstractUnitOfWorkManager<TModel>
    {
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public TModel Get() => Configurator._Container.Resolve<TInterfaceUnitOfWorkManager>().Get();
    }
}
