namespace UserInterfaceLayer.Clients.UnitsOfWork.Interfaces
{
    internal interface IAbstractUnitOfWorkClient<TModel>
    {
        TModel Get();
    }
}
