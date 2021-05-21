using BusinessLogicLayer.Logic.Presenters.Repositories.Interfaces;
using System;

namespace BusinessLogicLayer.Logic.Presenters.Repositories.Classes
{
    public abstract class AbstractRepositoryPresenter<TEntity> : IAbstractRepositoryPresenter<TEntity>
    {
        public virtual string Delete(TEntity entity) => throw new NotImplementedException();
    }
}
