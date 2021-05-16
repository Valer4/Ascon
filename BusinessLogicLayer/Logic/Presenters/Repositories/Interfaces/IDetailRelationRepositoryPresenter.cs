using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace BusinessLogicLayer.Logic.Presenters.Interfaces.Repositories
{
    public interface IDetailRelationRepositoryPresenter
    {
        string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount);

        string Edit(DetailRelationEntity selectedDetail, string name, string amount);

        string Delete(DetailRelationEntity selectedDetail);
    }
}
