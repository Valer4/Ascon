using System.Linq;
using UserInterfaceLayer.Clients.Repositories.Interfaces;
using WcfService.Services.Repositories.Interfaces;
using IRest = RestClient.Repositories.Interfaces;
using IWcf = WcfClient.Repositories.Interfaces;

namespace UserInterfaceLayer.Clients.Repositories.Classes
{
	internal abstract class AbstractRepositoryClient<TEntity, TId, TInterfaceEntityService> :
		IAbstractRepositoryClient<TEntity, TId>
			where TInterfaceEntityService : IAbstractRepositoryService<TEntity, TId>
	{
		protected readonly IWcf.IAbstractRepositoryClient<TEntity, TId> _wcfDetailAbstract;
		protected readonly IRest.IAbstractRepositoryClient<TEntity, TId> _restDetailAbstract;

		internal AbstractRepositoryClient(
			IWcf.IAbstractRepositoryClient<TEntity, TId> wcfDetailAbstract,
			IRest.IAbstractRepositoryClient<TEntity, TId> restDetailAbstract
		)
		{
			_wcfDetailAbstract = wcfDetailAbstract;
			_restDetailAbstract = restDetailAbstract;
		}

#region Entity.

		public TEntity Get(TId id)
		{
			if (ChannelType.Wcf == Program.ChannelType)
				return _wcfDetailAbstract.Get(id);

			return _restDetailAbstract.Get(Program.AccessToken, id);
		}
		public void Delete(TId id)
		{
			if (ChannelType.Wcf == Program.ChannelType)
				_wcfDetailAbstract.Delete(id);

			_restDetailAbstract.Delete(Program.AccessToken, id);
		}

		public string Delete(TEntity entity)
		{
			if (ChannelType.Wcf == Program.ChannelType)
				return _wcfDetailAbstract.Delete(entity);

			return _restDetailAbstract.Delete(Program.AccessToken, entity);
		}

#endregion

#region Collection.

		public IQueryable<TEntity> GetAll()
		{
			if (ChannelType.Wcf == Program.ChannelType)
				return _wcfDetailAbstract.GetAll();

			return _restDetailAbstract.GetAll(Program.AccessToken);
		}

		public void AddCollection(IQueryable<TEntity> collection)
		{
			if (ChannelType.Wcf == Program.ChannelType)
				_wcfDetailAbstract.AddCollection(collection);

			_restDetailAbstract.AddCollection(Program.AccessToken, collection);
		}
		public void EditCollection(IQueryable<TEntity> collection)
		{
			if (ChannelType.Wcf == Program.ChannelType)
				_wcfDetailAbstract.EditCollection(collection);

			_restDetailAbstract.EditCollection(Program.AccessToken, collection);
		}
		public void DeleteCollection(IQueryable<TEntity> collection)
		{
			if (ChannelType.Wcf == Program.ChannelType)
				_wcfDetailAbstract.DeleteCollection(collection);

			_restDetailAbstract.DeleteCollection(Program.AccessToken, collection);
		}

#endregion
	}
}
