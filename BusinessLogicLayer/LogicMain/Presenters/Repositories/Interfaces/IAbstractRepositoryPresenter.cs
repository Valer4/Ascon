using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces;

namespace BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces
{
	public interface IAbstractRepositoryPresenter<TEntity, TId>
	{
		IAbstractRepositoryManager<TEntity, TId> AbstractRepositoryManager { get; }

		string Delete(TEntity entity);
	}
}
