namespace BusinessLogicLayer.DataAccessInterfaces.UnitsOfWork
{
    public interface IAbstractUnitOfWork<TModel>
    {
        TModel Get();
    }
}
