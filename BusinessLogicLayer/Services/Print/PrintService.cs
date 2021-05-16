using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Logic.Print;
using BusinessLogicLayer.Managers.Print;
using BusinessLogicLayer.Security.RoleBasedAccessControl;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.Print
{
    public class PrintService : IPrintService
    {
        IPrintPresenter _printPresenter;

        public PrintService(IPrintPresenter printPresenter) => _printPresenter = printPresenter;

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public byte[] GetMSWord(DetailRelationEntity selectedDetail, out string warningMessage) =>
            _printPresenter.GetMSWord(selectedDetail, out warningMessage);
    }
}
