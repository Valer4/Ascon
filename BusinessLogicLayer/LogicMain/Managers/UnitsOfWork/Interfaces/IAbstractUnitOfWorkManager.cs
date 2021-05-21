namespace BusinessLogicLayer.LogicMain.Managers.UnitsOfWorkManagers.Interfaces
{
    public interface IAbstractUnitOfWorkManager<TModel>
    {
        TModel Get();
    }
}
