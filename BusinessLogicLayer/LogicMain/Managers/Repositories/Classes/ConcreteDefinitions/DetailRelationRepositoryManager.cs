using BusinessLogicLayer.Data.DataAccessInterfaces.Repositories.ConcreteDefinitions;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Managers.Common;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces.ConcreteDefinitions;
using System;

namespace BusinessLogicLayer.LogicMain.Managers.Repositories.Classes.ConcreteDefinitions
{
	public partial class DetailRelationRepositoryManager : AbstractRepositoryManager<DetailRelationEntity, long, IDetailRelationRepository>,
        IDetailRelationRepositoryManager
    {
        public DetailRelationRepositoryManager(IDetailRelationRepository detailRelationRepository) :
            base(detailRelationRepository) {}

        private DetailRelationRepositoryHelper _helper;
        private DetailRelationRepositoryHelper Helper
        {
            get
            {
                if (null == _helper)
                    _helper = new DetailRelationRepositoryHelper();
                return _helper;
            }
        }

#region Entity

        private void CheckData(DetailRelationEntity selectedDetail, bool isAdd = false)
        {
            if (string.IsNullOrWhiteSpace(selectedDetail.Name))
                throw new Exception("Несоответствие данных.");

            if (selectedDetail.IsRoot)
            {
                if (null != selectedDetail.RelationId) throw new Exception("Несоответствие данных.");
                if (null != selectedDetail.ParentId) throw new Exception("Несоответствие данных.");
                if (null != selectedDetail.Amount) throw new Exception("Несоответствие данных.");
            }
            else
            {
                if (isAdd)
                {
                    if (null != selectedDetail.RelationId) throw new Exception("Несоответствие данных.");
                }
                else
                    if (null == selectedDetail.RelationId) throw new Exception("Несоответствие данных.");

                if (null == selectedDetail.ParentId) throw new Exception("Несоответствие данных.");
                if (null == selectedDetail.Amount) throw new Exception("Количество деталей не указано.");

                if (selectedDetail.Amount < 1
                || selectedDetail.Amount > 9999) throw new Exception("Количество деталей указано не верно.");
            }
        }

        public override void Add(DetailRelationEntity selectedDetail)
        {
            CheckData(selectedDetail, isAdd: true);
            AddDetailRelation(selectedDetail);
            Save("Произошла ошибка при обращении к базе данных.");
        }

        public override void Edit(DetailRelationEntity selectedDetail)
        {
            CheckData(selectedDetail);
            EditDetailRelation(selectedDetail);
            Save("Произошла ошибка при обращении к базе данных.");
        }
        public override void Delete(long id)
        {
            DeleteDetailRelation(id);
            Save("Произошла ошибка при обращении к базе данных.");
        }

#endregion
    }
}
