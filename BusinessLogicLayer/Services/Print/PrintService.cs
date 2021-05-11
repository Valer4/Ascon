using BusinessLogicLayer.Managers.Print;
using BusinessLogicLayer.Security.RoleBasedAccessControl;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.Print
{
    public class PrintService : IPrintService
    {
        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public byte[] GetMSWord(long id) => Configurator._Container.Resolve<IPrintManager>().GetMSWord(id);
    }
}
