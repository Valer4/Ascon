using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace WcfClient.Repositories.Interfaces.ConcreteDefinitions
{
	public interface IDetailRelationRepositoryClient : IAbstractRepositoryClient<DetailRelationEntity, long>
	{
		string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount);

		string Edit(DetailRelationEntity selectedDetail, string name, string amount);
	}
}
