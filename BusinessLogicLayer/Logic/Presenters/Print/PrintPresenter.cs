using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Managers.Print;

namespace BusinessLogicLayer.Logic.Presenters.Print
{
    public class PrintPresenter : IPrintPresenter
    {
        private const string _detailNotSelected = "Деталь не выбрана.";

        IPrintManager _printManager;

        public PrintPresenter(IPrintManager printManager) =>
            _printManager = printManager;

        public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage)
        {
            warningMessage = string.Empty;

            if(null == selectedDetail)
            {
                warningMessage = _detailNotSelected;
                return new byte[] {};
            }

            return _printManager.GetReportOnDetailInMSWord(selectedDetail.Id);
        }
    }
}
