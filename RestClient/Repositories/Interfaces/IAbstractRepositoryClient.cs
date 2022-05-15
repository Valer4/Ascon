using System.Linq;

namespace RestClient.Repositories.Interfaces
{
	public interface IAbstractRepositoryClient<TEntity, TId>
	{
#region Entity.

		TEntity Get(string accessToken, TId id);
		void Delete(string accessToken, TId id);

		string Delete(string accessToken, TEntity entity);

#endregion

#region Collection.

		IQueryable<TEntity> GetAll(string accessToken);

		void AddCollection(string accessToken, IQueryable<TEntity> collection);
		void EditCollection(string accessToken, IQueryable<TEntity> collection);
		void DeleteCollection(string accessToken, IQueryable<TEntity> collection);

#endregion
	}
}
