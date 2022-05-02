using CommonHelpers.Any.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace UserInterfaceLayer.Clients.Authorization
{
    internal class AuthorizationClient : IAuthorizationClient
    {
        protected IRestApi _restApi;
        protected IStreamHelper _streamHelper;
        protected string _controllerAddress;
        public AuthorizationClient(IRestApi restApi, IStreamHelper streamHelper, string controllerAddress)
        {
            _restApi = restApi;
            _streamHelper = streamHelper;
            _controllerAddress = controllerAddress;
        }

        public void GetAuthorization()
        {
            string methodName = "token"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restApi.GetHttpData(
                url: $"https://{Program.ConnectInfo.HostAddress}/{_controllerAddress}/{methodName}",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _streamHelper.ObjToJson(new { Configurator.UserInfo.UserName, Configurator.UserInfo.Password }, Encoding.UTF8),
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            var response = new {
                accessToken = string.Empty,
                username = string.Empty };
            var result = JsonConvert.DeserializeAnonymousType(
                Encoding.UTF8.GetString(byteArray),
                response);
            Program.AccessToken = result.accessToken;
        }
    }
}
