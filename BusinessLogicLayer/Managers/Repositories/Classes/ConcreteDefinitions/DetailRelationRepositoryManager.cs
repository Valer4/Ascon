using BusinessLogicLayer.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Managers.Common;
using BusinessLogicLayer.Managers.Repositories.Classes;
using BusinessLogicLayer.Managers.Repositories.Interfaces.ConcreteDefinitions;
using System;
using System.Linq;

namespace BusinessLogicLayer.Managers.EntityManagers.Classes.ConcreteDefinitions
{
    public partial class DetailRelationRepositoryManager : AbstractRepositoryManager<DetailRelationEntity, long, IDetailRelationRepository>,
        IDetailRelationRepositoryManager
    {
        public DetailRelationRepositoryManager(IDetailRelationRepository detailRelationRepository) :
            base(detailRelationRepository) {}

        private DetailRelationRepositoryHelper _helper;
        private DetailRelationRepositoryHelper _Helper
        {
            get
            {
                if (_helper == null)
                    _helper = new DetailRelationRepositoryHelper();
                return _helper;
            }
        }

        #region Entity.
        private void CheckData(DetailRelationEntity entity, bool add = false)
        {
            if(string.IsNullOrWhiteSpace(entity.Name))
                throw new Exception("Несоответствие данных.");

            if(entity.Root)
            {
                if(entity.RelationId != null) throw new Exception("Несоответствие данных.");
                if(entity.ParentId != null) throw new Exception("Несоответствие данных.");
                if(entity.Amount != null) throw new Exception("Несоответствие данных.");
            }
            else
            {
                if(add)
                {
                    if(entity.RelationId != null) throw new Exception("Несоответствие данных.");
                }
                else
                    if(entity.RelationId == null) throw new Exception("Несоответствие данных.");

                if(entity.ParentId == null) throw new Exception("Несоответствие данных.");
                if(entity.Amount == null) throw new Exception("Несоответствие данных.");

                if(entity.Amount < 1
                || entity.Amount > 9999) throw new Exception("Несоответствие данных.");
            }
        }

        public override void Add(DetailRelationEntity entity)
        {
            CheckData(entity, add: true);
            AddDetailRelation(entity);
            Save("Произошла ошибка при обращении к базе данных.");
        }

        public override void Edit(DetailRelationEntity entity)
        {
            CheckData(entity);
            EditDetailRelation(entity);
            Save("Произошла ошибка при обращении к базе данных.");
        }
        public override void Delete(DetailRelationEntity entity)
        {
            CheckData(entity);
            DeleteDetailRelation(entity);
            Save("Произошла ошибка при обращении к базе данных.");
        }
        #endregion
    }
}
