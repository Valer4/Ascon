﻿using BusinessLogicLayer.Security.RoleBasedAccessControl;
using BusinessLogicLayer.Managers.UnitsOfWorkManagers.Interfaces;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.UnitsOfWork.Classes
{
    public abstract class AbstractUnitOfWorkService<TDataModel, TInterfaceUnitOfWorkManager>
        where TInterfaceUnitOfWorkManager : IAbstractUnitOfWorkManager<TDataModel>
    {
        TInterfaceUnitOfWorkManager _unitOfWorkManager;

        public AbstractUnitOfWorkService(TInterfaceUnitOfWorkManager repositoryManager) =>
            _unitOfWorkManager = repositoryManager;

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public TDataModel Get() => _unitOfWorkManager.Get();
    }
}
