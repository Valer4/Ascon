namespace BusinessLogicLayer.Data.DataAccessInterfaces.UnitsOfWork
{
    public interface IAbstractUnitOfWork<TModel>
    {
        TModel Get();
    }
}
