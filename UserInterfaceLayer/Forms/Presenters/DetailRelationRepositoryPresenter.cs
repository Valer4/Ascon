using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Reflection;
using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Forms.IViews;

namespace UserInterfaceLayer.Forms.Presenters
{
    public class DetailRelationRepositoryPresenter
    {
        private const string _detailNotSelected = "Деталь не выбрана.";

        private readonly IDetailRelationRepositoryView _view;
        private readonly IDetailRelationRepositoryClient _detailRelationEntityClient;

        public DetailRelationRepositoryPresenter
            (IDetailRelationRepositoryView view, IDetailRelationRepositoryClient detailRelationEntityClient)
        {
            _view = view;
            _detailRelationEntityClient = detailRelationEntityClient;

            EventBinding();
        }
        private void EventBinding()
        {
            _view.LoadData += OnLoadData;
            _view.AddDetail += OnAddDetail;
            _view.EditDetail += OnEditDetail;
            _view.DeleteDetail += OnDeleteDetail;
        }

        public void Update() =>
            _view.AllDetails = _detailRelationEntityClient.GetAll();

        public void OnLoadData() => Update();

        public string OnAddDetail(DetailRelationEntity selectedDetail, bool isRoot, string name, string amount)
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

            _detailRelationEntityClient.Add(detail);
            Update();

            return string.Empty;
        }

        public string OnEditDetail(DetailRelationEntity selectedDetail, string name, string amount)
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

            _detailRelationEntityClient.Edit(editableDetail);
            Update();

            return string.Empty;
        }

        public string OnDeleteDetail(DetailRelationEntity selectedDetail)
        {
            if(null == selectedDetail) return _detailNotSelected;

            _detailRelationEntityClient.Delete(selectedDetail);
            Update();

            return string.Empty;
        }

        private void SetAmount(DetailRelationEntity detail, string amount)
        {
            if(short.TryParse(amount, out short value))
                detail.Amount = value;
        }
    }
}
