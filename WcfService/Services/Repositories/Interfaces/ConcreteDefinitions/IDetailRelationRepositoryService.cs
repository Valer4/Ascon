using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.ServiceModel;

namespace WcfService.Services.Repositories.Interfaces.ConcreteDefinitions
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IDetailRelationRepositoryService : IAbstractRepositoryService<DetailRelationEntity, long>
    {
        [OperationContract]
        string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount);

        [OperationContract]
        string Edit(DetailRelationEntity selectedDetail, string name, string amount);
    }
}
