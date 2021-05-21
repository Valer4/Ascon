using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Managers.Repositories.Interfaces.ConcreteDefinitions;
using BusinessLogicLayer.LogicMain.Presenters.Interfaces.Repositories.ConcreteDefinitions;
using System.Reflection;

namespace BusinessLogicLayer.LogicMain.Presenters.Repositories.Classes.ConcreteDefinitions
{
    public class DetailRelationRepositoryPresenter : AbstractRepositoryPresenter<DetailRelationEntity>, IDetailRelationRepositoryPresenter
    {
        private const string _detailNotSelected = "Деталь не выбрана.";

        IDetailRelationRepositoryManager _detailRelationEntityManager;

        public DetailRelationRepositoryPresenter(IDetailRelationRepositoryManager detailRelationEntityManager) =>
            _detailRelationEntityManager = detailRelationEntityManager;

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
                if(property.CanWrite)
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

        public override string Delete(DetailRelationEntity selectedDetail)
        {
            if(null == selectedDetail) return _detailNotSelected;

            _detailRelationEntityManager.Delete(selectedDetail.Id);

            return string.Empty;
        }

        private void SetAmount(DetailRelationEntity detail, string amount)
        {
            if(short.TryParse(amount, out short value))
                detail.Amount = value;
        }
    }
}
