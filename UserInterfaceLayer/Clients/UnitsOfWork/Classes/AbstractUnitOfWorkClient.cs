using UserInterfaceLayer.Clients.UnitsOfWork.Interfaces;
using WcfService.Services.UnitsOfWork.Interfaces;

namespace UserInterfaceLayer.Clients.UnitsOfWork.Classes
{
    internal abstract class AbstractUnitOfWorkClient<TModel, TInterfaceUnitOfWorkService> : IAbstractUnitOfWorkClient<TModel>
        where TInterfaceUnitOfWorkService : IAbstractUnitOfWorkService<TModel>
    {
        public TModel Get() => new ChannelsManager().GetChannel<TInterfaceUnitOfWorkService>().Get();
    }
}
