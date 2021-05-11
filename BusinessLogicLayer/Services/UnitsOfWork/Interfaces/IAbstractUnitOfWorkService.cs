using System.ServiceModel;

namespace BusinessLogicLayer.Services.UnitsOfWork.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAbstractUnitOfWorkService<TModel>
    {
        [OperationContract]
        TModel Get();
    }
}
