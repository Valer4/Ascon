using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Linq;

namespace UserInterfaceLayer.Clients.Repositories.Interfaces
{
    public interface IAbstractRepositoryClient<T>
    {
        IQueryable<T> GetAll();
    }
}
