using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.ServiceModel;

namespace BusinessLogicLayer.Services.Repositories.Interfaces.ConcreteDefinitions
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IDetailRelationRepositoryService : IAbstractRepositoryService<DetailRelationEntity, long>
    {
    }
}
