using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Repositories.Interfaces;

namespace BusinessLogicLayer.LogicMain.Presenters.Interfaces.Repositories.ConcreteDefinitions
{
    public interface IDetailRelationRepositoryPresenter : IAbstractRepositoryPresenter<DetailRelationEntity, long>
    {
        string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount);

        string Edit(DetailRelationEntity selectedDetail, string name, string amount);
    }
}
