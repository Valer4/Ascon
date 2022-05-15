using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces;
using System;
using System.Linq;

namespace BusinessLogicLayer.LogicMain.Managers.Repositories.Classes
{
	public abstract class AbstractRepositoryManager<TEntity, TId, TInterfaceRepository> :
		IAbstractRepositoryManager<TEntity, TId>
			where TInterfaceRepository : IAbstractRepository<TEntity, TId>
	{
		internal TInterfaceRepository Repository;

		public AbstractRepositoryManager(TInterfaceRepository repository) =>
			Repository = repository;

		public virtual void Save(string message = null)
		{
			try
			{
				Repository.Save();
			}
			catch (Exception ex)
			{
				throw new Exception(message, ex);
			}
		}

#region Entity

		public virtual TEntity Get(TId id) => Repository.Get(id);
		public virtual void Add(TEntity entity) => Repository.Add(entity);
		public virtual void Edit(TEntity entity) => Repository.Edit(entity);
		public virtual void Delete(TId id) => Repository.Delete(id);

#endregion

#region Collection

		public virtual IQueryable<TEntity> GetAll() => Repository.GetAll();
		public virtual void AddCollection(IQueryable<TEntity> collection) => Repository.AddCollection(collection);
		public virtual void EditCollection(IQueryable<TEntity> collection) => Repository.EditCollection(collection);
		public virtual void DeleteCollection(IQueryable<TEntity> collection) => Repository.DeleteCollection(collection);

#endregion
	}
}
