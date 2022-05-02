using CommonHelpers.Any.Interfaces;
#if REST
using System.Collections.Generic;
#endif
using System.Linq;
#if REST
using System.Text;
#endif
using UserInterfaceLayer.Clients.Repositories.Interfaces;
using WcfService.Services.Repositories.Interfaces;

namespace UserInterfaceLayer.Clients.Repositories.Classes
{
    internal abstract class AbstractRepositoryClient<TEntity, TId, TInterfaceEntityService> :
        IAbstractRepositoryClient<TEntity, TId>
            where TInterfaceEntityService : IAbstractRepositoryService<TEntity, TId>
    {
        protected IRestApi _restApi;
        protected IStreamHelper _streamHelper;
        protected string _controllerAddress;
        public AbstractRepositoryClient(IRestApi restApi, IStreamHelper streamHelper, string controllerAddress)
        {
            _restApi = restApi;
            _streamHelper = streamHelper;
            _controllerAddress = controllerAddress;
        }

        #region Entity.
        public TEntity Get(TId id) => new ChannelsManager().GetChannel<TInterfaceEntityService>().Get(id);

        public void Delete(TId id) => new ChannelsManager().GetChannel<TInterfaceEntityService>().DeleteById(id);

#if WCF
        public string Delete(TEntity entity) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().Delete(entity);
#elif REST
        public string Delete(TEntity entity)
        {
            string methodName = "delete"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restApi.GetHttpData(
                url: $"https://{Program.ConnectInfo.HostAddress}/{_controllerAddress}/{methodName}",
                method: "POST",
                contentType: "application/json; charset=utf-8",
                sentData: _streamHelper.ObjToJson(entity, Encoding.UTF8),
                accessToken: Program.AccessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            string warningMessage = _streamHelper.JsonToObj<string>(byteArray, Encoding.UTF8);
            return warningMessage;
        }
#endif
        #endregion

        #region Collection.
//#if WCF
        public IQueryable<TEntity> GetAll() => new ChannelsManager().GetChannel<TInterfaceEntityService>().GetAll();
/*#elif REST
        public IQueryable<TEntity> GetAll()
        {
            string methodName = "all"; // new StackTrace().GetFrame(0).GetMethod().Name;

            byte[] byteArray = _restApi.GetHttpData(
                url: $"https://{Program.ConnectInfo.HostAddress}/{_controllerAddress}/{methodName}",
                method: "GET",
                contentType: "application/json; charset=utf-8",
                sentData: new byte[0],
                accessToken: Program.AccessToken,
                useCertificate: false,
                msgBadStatusCode: "Ошибка. HttpStatusCode = {0}.");

            List<TEntity> result = _streamHelper.JsonToObj<List<TEntity>>(byteArray, Encoding.UTF8);
            return result.AsQueryable();
        }
#endif*/
        public void AddCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().AddCollection(collection);
        public void EditCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().EditCollection(collection);
        public void DeleteCollection(IQueryable<TEntity> collection) =>
            new ChannelsManager().GetChannel<TInterfaceEntityService>().DeleteCollection(collection);
        #endregion
    }
}
