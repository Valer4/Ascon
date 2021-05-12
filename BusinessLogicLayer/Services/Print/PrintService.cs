using BusinessLogicLayer.Managers.Print;
using BusinessLogicLayer.Security.RoleBasedAccessControl;
using System.Security.Permissions;

namespace BusinessLogicLayer.Services.Print
{
    public class PrintService : IPrintService
    {
        IPrintManager _printManager;

        public PrintService(IPrintManager printManager) => _printManager = printManager;

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public byte[] GetMSWord(long id) => _printManager.GetMSWord(id);
    }
}
