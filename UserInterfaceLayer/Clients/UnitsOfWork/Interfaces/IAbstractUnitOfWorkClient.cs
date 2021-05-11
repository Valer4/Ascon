namespace UserInterfaceLayer.Clients.UnitsOfWork.Interfaces
{
    public interface IAbstractUnitOfWorkClient<TModel>
    {
        TModel Get();
    }
}
