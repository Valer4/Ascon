using System.ServiceModel;

namespace BusinessLogicLayer.Services.UnitsOfWork.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAbstractUnitOfWorkService<TDataModel>
    {
        [OperationContract]
        TDataModel Get();
    }
}
