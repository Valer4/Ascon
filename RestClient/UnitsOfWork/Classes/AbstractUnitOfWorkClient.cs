using RestClient.UnitsOfWork.Interfaces;
using System;

namespace RestClient.UnitsOfWork.Classes
{
	internal abstract class AbstractUnitOfWorkClient<TModel> : IAbstractUnitOfWorkClient<TModel>
    {
        public TModel Get() => throw new NotImplementedException();
    }
}
