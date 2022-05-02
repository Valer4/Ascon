using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Print;
using System.Security.Permissions;
using WcfService.Security.RoleBasedAccessControl;

namespace WcfService.Services.Print
{
    public class PrintService : IPrintService
    {
        private IPrintPresenter _printPresenter;

        public PrintService(IPrintPresenter printPresenter) => _printPresenter = printPresenter;

        [PrincipalPermission(SecurityAction.Demand, Role = AppRoles.Admin)]
        public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage) =>
            _printPresenter.GetReportOnDetailInMSWord(selectedDetail, out warningMessage);
    }
}
