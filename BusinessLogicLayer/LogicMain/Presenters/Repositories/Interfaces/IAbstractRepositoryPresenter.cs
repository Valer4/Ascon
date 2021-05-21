namespace BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces
{
    public interface IAbstractRepositoryPresenter<TEntity>
    {
        string Delete(TEntity entity);
    }
}
