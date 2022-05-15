using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace RestClient.Repositories.Interfaces.ConcreteDefinitions
{
	public interface IDetailRelationRepositoryClient : IAbstractRepositoryClient<DetailRelationEntity, long>
	{
		string Add(
			string accessToken,
			DetailRelationEntity selectedDetail,
			bool isRoot,
			string name,
			string amount
		);

		string Edit(
			string accessToken,
			DetailRelationEntity selectedDetail,
			string name,
			string amount
		);
	}
}
