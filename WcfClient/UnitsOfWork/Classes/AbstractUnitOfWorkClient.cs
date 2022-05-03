using WcfClient.UnitsOfWork.Interfaces;
using WcfService;
using WcfService.Services.UnitsOfWork.Interfaces;

namespace WcfClient.UnitsOfWork.Classes
{
	internal abstract class AbstractUnitOfWorkClient<TModel, TInterfaceUnitOfWorkService> : IAbstractUnitOfWorkClient<TModel>
        where TInterfaceUnitOfWorkService : IAbstractUnitOfWorkService<TModel>
    {
        private readonly WcfClientConfigurator _wcfClientConfigurator;
        public AbstractUnitOfWorkClient(WcfClientConfigurator wcfClientConfigurator)
        {
            _wcfClientConfigurator = wcfClientConfigurator;
        }

        public TModel Get() => new ChannelsManager(_wcfClientConfigurator).GetChannel<TInterfaceUnitOfWorkService>().Get();
    }
}
