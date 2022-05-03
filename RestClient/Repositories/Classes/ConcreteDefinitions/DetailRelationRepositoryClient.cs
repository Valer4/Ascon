using BusinessLogicLayer;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using RestClient.Repositories.Interfaces.ConcreteDefinitions;
using System.Text;

namespace RestClient.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryClient :
        AbstractRepositoryClient<DetailRelationEntity, long>,
        IDetailRelationRepositoryClient
    {
        public DetailRelationRepositoryClient(
            RestClientConfigurator restClientConfigurator,
            string controllerAddress
        )
            : base(
                restClientConfigurator,
                controllerAddress
            )
        {
        }

        public string Add(
            string accessToken,
            DetailRelationEntity selectedDetail,
            bool isRoot,
            string name,
            string amount
        )
        {
            string methodName = "add"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restClientConfigurator.RestApi.GetHttpData(
                url: $"https://{ _restClientConfigurator.ConnectInfo.HostAddress }/{ _controllerAddress }/{ methodName }",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _restClientConfigurator.RestApi.StreamHelper.ObjToJson(
                    new
                    {
                        selectedDetail,
                        isRoot,
                        name,
                        amount
                    },
                    Encoding.UTF8
                ),
                accessToken: accessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}."
            );

            string warningMessage = _restClientConfigurator.RestApi.StreamHelper.JsonToObj<string>(byteArray, Encoding.UTF8);
            return warningMessage;
        }

        public string Edit(
            string accessToken,
            DetailRelationEntity selectedDetail,
            string name,
            string amount
        )
        {
            string methodName = "edit"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restClientConfigurator.RestApi.GetHttpData(
                url: $"https://{ _restClientConfigurator.ConnectInfo.HostAddress }/{ _controllerAddress }/{ methodName }",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _restClientConfigurator.RestApi.StreamHelper.ObjToJson(
                    new
                    {
                        selectedDetail,
                        name,
                        amount
                    },
                    Encoding.UTF8
                ),
                accessToken: accessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}."
            );

            string warningMessage = _restClientConfigurator.RestApi.StreamHelper.JsonToObj<string>(byteArray, Encoding.UTF8);
            return warningMessage;
        }
    }
}
