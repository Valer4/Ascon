using BusinessLogicLayer.Services.UnitsOfWork.Interfaces;
using UserInterfaceLayer.Clients.UnitsOfWork.Interfaces;

namespace UserInterfaceLayer.Clients.UnitsOfWork.Classes
{
    internal abstract class AbstractUnitOfWorkClient<TModel, TInterfaceUnitOfWorkService> : IAbstractUnitOfWorkClient<TModel>
        where TInterfaceUnitOfWorkService : IAbstractUnitOfWorkService<TModel>
    {
        public TModel Get() => new ChannelsManager().GetChannel<TInterfaceUnitOfWorkService>().Get();
    }
}
