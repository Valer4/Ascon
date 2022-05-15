using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.LogicMain.Managers.Common
{
	internal class DetailRelationRepositoryHelper
	{
		internal DetailRelationEntity Find
			(long id, IQueryable<DetailRelationEntity> allDetailRelations) =>
				allDetailRelations.Where(x => id == x.Id).
					SingleOrDefault();

		/// <summary>
		/// Возвращает предков.
		/// </summary>
		internal IEnumerable<DetailRelationEntity> GetAncestors
			(long? parentId, IQueryable<DetailRelationEntity> allDetailRelations, ICollection<DetailRelationEntity> ancestors)
		{
			if (null != parentId)
			{
				IQueryable<DetailRelationEntity> parents = GetParents(parentId, allDetailRelations);
				foreach (DetailRelationEntity parent in parents)
				{
					ancestors.Add(parent);
					GetAncestors(parent.ParentId, allDetailRelations, ancestors);
				}
			}
			return ancestors;
		}
		/// <summary>
		/// Возвращает потомков.
		/// </summary>
		internal IEnumerable<DetailRelationEntity> GetDescendants
			(long typeId, IQueryable<DetailRelationEntity> allDetailRelations, ICollection<DetailRelationEntity> descendants)
		{
			IQueryable<DetailRelationEntity> childs = GetChilds(typeId, allDetailRelations);
			foreach (DetailRelationEntity child in childs)
			{
				descendants.Add(child);
				GetDescendants(child.TypeId, allDetailRelations, descendants);
			}
			return descendants;
		}

		internal IQueryable<DetailRelationEntity> GetParents
			(long? parentId, IQueryable<DetailRelationEntity> allDetailRelations) =>
				allDetailRelations.Where(x => parentId == x.TypeId);
		internal IQueryable<DetailRelationEntity> GetChilds
			(long typeId, IQueryable<DetailRelationEntity> allDetailRelations) =>
				allDetailRelations.Where(x => typeId == x.ParentId);
	}
}
