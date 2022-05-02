using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using CommonHelpers.Any.Interfaces;
#if REST
using System.Text;
#endif
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Repositories.Classes.ConcreteDefinitions
{
    internal class DetailRelationRepositoryClient :
        AbstractRepositoryClient<DetailRelationEntity, long, IDetailRelationRepositoryService>,
        IDetailRelationRepositoryClient
    {
        public DetailRelationRepositoryClient(IRestApi restApi, IStreamHelper streamHelper, string controllerAddress) : base(restApi, streamHelper, controllerAddress) {}

#if WCF
        public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount) =>
            new ChannelsManager().GetChannel<IDetailRelationRepositoryService>().Add(selectedDetail, isRoot, name, amount);
#elif REST
        public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount)
        {
            string methodName = "add"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restApi.GetHttpData(
                url: $"https://{Program.ConnectInfo.HostAddress}/{_controllerAddress}/{methodName}",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _streamHelper.ObjToJson(new { selectedDetail, isRoot, name, amount }, Encoding.UTF8),
                accessToken: Program.AccessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            string warningMessage = _streamHelper.JsonToObj<string>(byteArray, Encoding.UTF8);
            return warningMessage;
        }
#endif

#if WCF
        public string Edit(DetailRelationEntity selectedDetail, string name, string amount) =>
            new ChannelsManager().GetChannel<IDetailRelationRepositoryService>().Edit(selectedDetail, name, amount);
#elif REST
        public string Edit(DetailRelationEntity selectedDetail, string name, string amount)
        {
            string methodName = "edit"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restApi.GetHttpData(
                url: $"https://{Program.ConnectInfo.HostAddress}/{_controllerAddress}/{methodName}",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _streamHelper.ObjToJson(new { selectedDetail, name, amount }, Encoding.UTF8),
                accessToken: Program.AccessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            string warningMessage = _streamHelper.JsonToObj<string>(byteArray, Encoding.UTF8);
            return warningMessage;
        }
#endif
    }
}
