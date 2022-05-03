using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using WcfService.Services.Repositories.Interfaces.ConcreteDefinitions;
using IRestDetail = RestClient.Repositories.Interfaces.ConcreteDefinitions;
using IWcfDetail = WcfClient.Repositories.Interfaces.ConcreteDefinitions;

namespace UserInterfaceLayer.Clients.Repositories.Classes.ConcreteDefinitions
{
	internal class DetailRelationRepositoryClient :
        AbstractRepositoryClient<DetailRelationEntity, long, IDetailRelationRepositoryService>,
        IDetailRelationRepositoryClient
    {
        protected readonly IWcfDetail.IDetailRelationRepositoryClient _wcfDetail;
        protected readonly IRestDetail.IDetailRelationRepositoryClient _restDetail;

        internal DetailRelationRepositoryClient(
            IWcfDetail.IDetailRelationRepositoryClient wcfDetail,
            IRestDetail.IDetailRelationRepositoryClient restDetail
        )
            : base(wcfDetail, restDetail)
        {
            _wcfDetail = wcfDetail;
            _restDetail = restDetail;
        }

        public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount)
        {
            if (ChannelType.Wcf == Program.ChannelType)
                return _wcfDetail.Add(selectedDetail, isRoot, name, amount);

            return _restDetail.Add(Program.AccessToken, selectedDetail, isRoot, name, amount);
        }

        public string Edit(DetailRelationEntity selectedDetail, string name, string amount)
        {
            if (ChannelType.Wcf == Program.ChannelType)
                return _wcfDetail.Edit(selectedDetail, name, amount);

            return _restDetail.Edit(Program.AccessToken, selectedDetail, name, amount);
        }
    }
}
