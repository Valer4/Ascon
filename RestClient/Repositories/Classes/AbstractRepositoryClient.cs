using BusinessLogicLayer;
using RestClient.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestClient.Repositories.Classes
{
    public abstract class AbstractRepositoryClient<TEntity, TId> : IAbstractRepositoryClient<TEntity, TId>
    {
        protected readonly RestClientConfigurator _restClientConfigurator;
        protected readonly string _controllerAddress;

        public AbstractRepositoryClient(
            RestClientConfigurator restClientConfigurator,
            string controllerAddress
        )
        {
            _restClientConfigurator = restClientConfigurator;
            _controllerAddress = controllerAddress;
        }

#region Entity.

        public TEntity Get(string accessToken, TId id) => throw new NotImplementedException();
        public void Delete(string accessToken, TId id) => throw new NotImplementedException();

        public string Delete(string accessToken, TEntity entity)
        {
            string methodName = "delete"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restClientConfigurator.RestApi.GetHttpData(
                url: $"https://{ _restClientConfigurator.ConnectInfo.HostAddress }/{ _controllerAddress }/{ methodName }",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _restClientConfigurator.RestApi.StreamHelper.ObjToJson(entity, Encoding.UTF8),
                accessToken: accessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            string warningMessage = _restClientConfigurator.RestApi.StreamHelper.JsonToObj<string>(byteArray, Encoding.UTF8);
            return warningMessage;
        }

#endregion

#region Collection.

        public IQueryable<TEntity> GetAll(string accessToken)
        {
            string methodName = "all"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restClientConfigurator.RestApi.GetHttpData(
                url: $"https://{ _restClientConfigurator.ConnectInfo.HostAddress }/{ _controllerAddress }/{ methodName }",
                method: "GET",
                contentType: "application/json; charset=utf-8",
                sentData: new byte[0],
                accessToken: accessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            List<TEntity> result = _restClientConfigurator.RestApi.StreamHelper.JsonToObj<List<TEntity>>(byteArray, Encoding.UTF8);
            return result.AsQueryable();
        }

        public void AddCollection(string accessToken, IQueryable<TEntity> collection) => throw new NotImplementedException();
        public void EditCollection(string accessToken, IQueryable<TEntity> collection) => throw new NotImplementedException();
        public void DeleteCollection(string accessToken, IQueryable<TEntity> collection) => throw new NotImplementedException();

#endregion
    }
}
