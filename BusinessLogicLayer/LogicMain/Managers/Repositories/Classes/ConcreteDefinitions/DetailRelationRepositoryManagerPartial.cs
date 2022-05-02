using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.LogicMain.Managers.Repositories.Classes.ConcreteDefinitions
{
    public partial class DetailRelationRepositoryManager
    {
        #region Добавление.        
        public void AddDetailRelation(DetailRelationEntity detailRelation)
        {
            IQueryable<DetailRelationEntity> allDetailRelations = Repository.GetAll();

            string name = detailRelation.Name;
            DetailRelationEntity sameName = allDetailRelations.Where(x => name == x.Name).FirstOrDefault();
            if (null == sameName)
            {
                DetailTypeEntity detailType = AddDetailType(detailRelation);

                try
                {
                    Repository.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception("Произошла ошибка при обращении к базе данных.", ex);
                }

                if ( ! detailRelation.IsRoot)
                    AddChildDetailRelation(detailType.Id, detailRelation);
            }
            else
            {
                if (detailRelation.IsRoot)
                    throw new Exception("Узел с таким названием уже существует.");

                long typeId = sameName.TypeId;

                IQueryable<DetailRelationEntity> neighbors = Helper.GetChilds((long)detailRelation.ParentId, allDetailRelations);
                if (neighbors.Any(x => typeId == x.TypeId))
                    throw new Exception("Добавление дублей не допускается.");

                IEnumerable<DetailRelationEntity> ancestors = Helper.GetAncestors(
                    detailRelation.ParentId, allDetailRelations, new List<DetailRelationEntity>());
                IEnumerable<DetailRelationEntity> descendants = Helper.GetDescendants(
                    typeId, allDetailRelations, new List<DetailRelationEntity>());

                if ( ! ancestors.Any(x => typeId == x.TypeId)
                &&   ! descendants.Any(x => typeId == x.TypeId))
                    AddChildDetailRelation(typeId, detailRelation);
                else
                    throw new Exception("Рекурсивное добавление не допускается.");
            }
        }

        private DetailTypeEntity AddDetailType(DetailRelationEntity detailRelation)
        {
            var detailType = new DetailTypeEntity();
            detailType.IsRoot = detailRelation.IsRoot;
            detailType.Name = detailRelation.Name;
            Repository.DetailTypeRepository.Add(detailType);
            return detailType;
        }
        private ChildDetailRelationEntity AddChildDetailRelation(long TypeId, DetailRelationEntity detailRelation)
        {
            var childDetailRelation = new ChildDetailRelationEntity();
            childDetailRelation.ParentId = (long)detailRelation.ParentId;
            childDetailRelation.TypeId = TypeId;
            childDetailRelation.Amount = (short)detailRelation.Amount;
            Repository.ChildDetailRelationRepository.Add(childDetailRelation);
            return childDetailRelation;
        }
        #endregion

        #region Редактирование.
        public void EditDetailRelation(DetailRelationEntity detailRelation)
        {
            IQueryable<DetailTypeEntity> allDetailTypes = Repository.DetailTypeRepository.GetAll();
            long typeId = detailRelation.TypeId;
            DetailTypeEntity detailType = allDetailTypes.Where(x => typeId == x.Id).SingleOrDefault();

            if (null == detailType)
                throw new Exception("Редактирование не доступно, т.к. объект удалён.");

            string name = detailRelation.Name;
            if (allDetailTypes.Where(x => name == x.Name && typeId != x.Id).Any())
                throw new Exception("Узел с таким названием уже существует.");

            detailType.Name = detailRelation.Name;

            if ( ! detailRelation.IsRoot)
            {
                long relationId = (long)detailRelation.RelationId;
                ChildDetailRelationEntity childDetailRelation =
                    Repository.ChildDetailRelationRepository.GetAll().
                        Where(x => relationId == x.Id).SingleOrDefault();

                if (null != childDetailRelation)
                    childDetailRelation.Amount = (short)detailRelation.Amount;
            }
        }
        #endregion

        #region Удаление.
        public void DeleteDetailRelation(long id)
        {
            IQueryable<DetailRelationEntity> allDetailRelations = Repository.GetAll();

            DetailRelationEntity current = Helper.Find(id, allDetailRelations);

            if (null != current)
                DeleteRecursive(current, allDetailRelations);
        }
        public void DeleteRecursive(DetailRelationEntity detailRelation, IQueryable<DetailRelationEntity> allDetailRelations)
        {
            if ( ! detailRelation.IsRoot)
                Repository.ChildDetailRelationRepository.Delete((long)detailRelation.RelationId);

            long typeId = detailRelation.TypeId;
            if (allDetailRelations.Where(x => typeId == x.TypeId).Count() == 1)
            {
                Repository.DetailTypeRepository.Delete(typeId);

                IQueryable<DetailRelationEntity> childs = Helper.GetChilds(typeId, allDetailRelations);

                foreach (DetailRelationEntity child in childs)
                    DeleteRecursive(child, allDetailRelations);
            }
        }
        #endregion
    }
}
