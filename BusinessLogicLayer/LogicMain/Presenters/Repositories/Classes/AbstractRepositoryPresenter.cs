using BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces;
using System;

namespace BusinessLogicLayer.LogicMain.Presenters.Repositories.Classes
{
    public abstract class AbstractRepositoryPresenter<TEntity> : IAbstractRepositoryPresenter<TEntity>
    {
        public virtual string Delete(TEntity entity) => throw new NotImplementedException();
    }
}
