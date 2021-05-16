﻿using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.Logic.Presenters.Interfaces.Repositories;
using BusinessLogicLayer.Managers.Repositories.Interfaces.ConcreteDefinitions;
using System.Reflection;

namespace BusinessLogicLayer.Logic.Presenters.Classes.Repositories
{
    public class DetailRelationRepositoryPresenter : IDetailRelationRepositoryPresenter
    {
        IDetailRelationRepositoryManager _detailRelationEntityManager;
        public DetailRelationRepositoryPresenter(IDetailRelationRepositoryManager detailRelationEntityManager) =>
            _detailRelationEntityManager = detailRelationEntityManager;

        private const string _detailNotSelected = "Деталь не выбрана.";

        public string Add(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount)
        {
            DetailRelationEntity detail = new DetailRelationEntity();

            detail.Name = name;

            if(isRoot)
                detail.IsRoot = true;
            else
            {
                if(null == selectedDetail) return _detailNotSelected;

                detail.ParentId = selectedDetail.TypeId;

                SetAmount(detail, amount);
            }

            _detailRelationEntityManager.Add(detail);

            return string.Empty;
        }

        public string Edit(DetailRelationEntity selectedDetail, string name, string amount)
        {
            if(null == selectedDetail) return _detailNotSelected;

            DetailRelationEntity editableDetail = new DetailRelationEntity();

            PropertyInfo[] properties = typeof(DetailRelationEntity).GetProperties();
            foreach(PropertyInfo property in properties)
                property.SetValue(editableDetail, property.GetValue(selectedDetail));

            if(editableDetail.IsRoot)
                editableDetail.Name = name;
            else
            {
                if( ! string.IsNullOrWhiteSpace(name))
                    editableDetail.Name = name;

                SetAmount(editableDetail, amount);
            }

            _detailRelationEntityManager.Edit(editableDetail);

            return string.Empty;
        }

        public string Delete(DetailRelationEntity selectedDetail)
        {
            if(null == selectedDetail) return _detailNotSelected;

            _detailRelationEntityManager.Delete(selectedDetail);

            return string.Empty;
        }

        private void SetAmount(DetailRelationEntity detail, string amount)
        {
            if(short.TryParse(amount, out short value))
                detail.Amount = value;
        }
    }
}