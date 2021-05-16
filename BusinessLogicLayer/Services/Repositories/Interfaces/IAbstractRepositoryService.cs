using System.Linq;
using System.ServiceModel;

namespace BusinessLogicLayer.Services.Repositories.Interfaces
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IAbstractRepositoryService<T>
    {
        [OperationContract]
        IQueryable<T> GetAll();
    }
}
