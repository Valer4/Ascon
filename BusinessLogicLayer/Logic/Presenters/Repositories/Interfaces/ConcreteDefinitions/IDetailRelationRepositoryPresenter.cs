using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Logic.Presenters.Repositories.Interfaces;

namespace BusinessLogicLayer.Logic.Presenters.Interfaces.Repositories.ConcreteDefinitions
{
    public interface IDetailRelationRepositoryPresenter : IAbstractRepositoryPresenter<DetailRelationEntity>
    {
        string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount);

        string Edit(DetailRelationEntity selectedDetail, string name, string amount);
    }
}
