namespace BusinessLogicLayer.Managers.UnitsOfWorkManagers.Interfaces
{
    public interface IAbstractUnitOfWorkManager<TModel>
    {
        TModel Get();
    }
}
