using WcfClient.UnitsOfWork.Interfaces;
using WcfService.Services.UnitsOfWork.Interfaces;

namespace WcfClient.UnitsOfWork.Classes
{
	internal abstract class AbstractUnitOfWorkClient<TModel, TInterfaceUnitOfWorkService> : IAbstractUnitOfWorkClient<TModel>
		where TInterfaceUnitOfWorkService : IAbstractUnitOfWorkService<TModel>
	{
		private readonly ChannelsManager _channelsManager;

		public AbstractUnitOfWorkClient(ChannelsManager channelsManager)
		{
			_channelsManager = channelsManager;
		}

		public TModel Get() => _channelsManager.GetChannel<TInterfaceUnitOfWorkService>().Get();
	}
}
