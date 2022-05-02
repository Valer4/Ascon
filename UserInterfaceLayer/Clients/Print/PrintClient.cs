using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using CommonHelpers.Any.Interfaces;
#if REST
using Newtonsoft.Json;
using System.Text;
#elif WCF
using WCFService.Services.Print;
#endif
namespace UserInterfaceLayer.Clients.Print
{
    internal class PrintClient : IPrintClient
    {
        protected IRestApi _restApi;
        protected IStreamHelper _streamHelper;
        protected string _controllerAddress;
        public PrintClient(IRestApi restApi, IStreamHelper streamHelper, string controllerAddress)
        {
            _restApi = restApi;
            _streamHelper = streamHelper;
            _controllerAddress = controllerAddress;
        }

#if WCF
        public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage) =>
            new ChannelsManager().GetChannel<IPrintService>().GetReportOnDetailInMSWord(selectedDetail, out warningMessage);
#elif REST
        public byte[] GetReportOnDetailInMSWord(DetailRelationEntity selectedDetail, out string warningMessage)
        {
            string methodName = "report"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restApi.GetHttpData(
                url: $"https://{Program.ConnectInfo.HostAddress}/{_controllerAddress}/{methodName}",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _streamHelper.ObjToJson(new { selectedDetail, warningMessage = string.Empty }, Encoding.UTF8),
                accessToken: Program.AccessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            var response = new {
                byteArray = new byte[0],
                warningMessage = string.Empty };
            var result = JsonConvert.DeserializeAnonymousType(
                Encoding.UTF8.GetString(byteArray),
                response);
            warningMessage = result.warningMessage;
            return result.byteArray;
        }
#endif
    }
}
