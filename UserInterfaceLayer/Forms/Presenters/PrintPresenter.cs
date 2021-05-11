using UserInterfaceLayer.Clients.Print;
using UserInterfaceLayer.Forms.IViews;

namespace UserInterfaceLayer.Forms.Presenters
{
    public class PrintPresenter
    {
        private readonly IPrintView _view;
        private readonly IPrintClient _printClient;

        public PrintPresenter(IPrintView view, IPrintClient printClient)
        {
            _view = view;
            _printClient = printClient;

            EventBinding();
        }

        private void EventBinding()
        {
            _view.GetMSWord += OnGetMSWord;
        }

        private byte[] OnGetMSWord(long id) => _printClient.GetMSWord(id);
    }
}
