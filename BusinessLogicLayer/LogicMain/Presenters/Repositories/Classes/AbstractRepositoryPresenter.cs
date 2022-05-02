using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces;
using BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces;
using System;

namespace BusinessLogicLayer.LogicMain.Presenters.Repositories.Classes
{
    public abstract class AbstractRepositoryPresenter<TEntity, TId> : IAbstractRepositoryPresenter<TEntity, TId>
    {
        public IAbstractRepositoryManager<TEntity, TId> AbstractRepositoryManager { get; }
        public AbstractRepositoryPresenter(IAbstractRepositoryManager<TEntity, TId> abstractRepositoryManager) => AbstractRepositoryManager = abstractRepositoryManager;

        public virtual string Delete(TEntity entity) => throw new NotImplementedException();
    }
}
