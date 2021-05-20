using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Forms.IViews;

namespace UserInterfaceLayer.Forms.Presenters
{
    internal class PrintPresenter
    {
        private const string _detailNotSelected = "Деталь не выбрана.";

        private readonly IPrintView _view;
        private readonly IPrintClient _printClient;

        internal PrintPresenter(IPrintView view, IPrintClient printClient)
        {
            _view = view;
            _printClient = printClient;

            EventBinding();
        }

        private void EventBinding() => _view.GetReportOnDetailInMSWord += OnGetReportOnDetailInMSWord;

        internal byte[] OnGetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage)
        {
            warningMessage = string.Empty;

            if(null == selectedDetail)
            {
                warningMessage = _detailNotSelected;
                return new byte[] {};
            }

            return _printClient.GetReportOnDetailInMSWord(selectedDetail.Id);
        }
    }
}
