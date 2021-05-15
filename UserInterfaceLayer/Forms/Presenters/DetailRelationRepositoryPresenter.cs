using UserInterfaceLayer.Clients.Repositories.Interfaces.ConcreteDefinitions;
using UserInterfaceLayer.Forms.IViews;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;

namespace UserInterfaceLayer.Forms.Presenters
{
    public class DetailRelationRepositoryPresenter
    {
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
            _view.GetAllDetails += OnGetAllDetails;
            _view.AddDetail += OnAddDetail;
            _view.EditDetail += OnEditDetail;
            _view.DeleteDetail += OnDeleteDetail;
        }

        private void OnGetAllDetails() =>
            _view.AllDetails = _detailRelationEntityClient.GetAll();

        private void OnAddDetail(DetailRelationEntity detailRelationEntity) =>
            _detailRelationEntityClient.Add(detailRelationEntity);

        private void OnEditDetail(DetailRelationEntity detailRelationEntity) =>
            _detailRelationEntityClient.Edit(detailRelationEntity);

        private void OnDeleteDetail(DetailRelationEntity detailRelationEntity) =>
            _detailRelationEntityClient.Delete(detailRelationEntity);
    }
}
