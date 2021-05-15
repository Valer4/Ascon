using BusinessLogicLayer.Entities.Classes.ConcreteDefinitions;
using System.Linq;

namespace UserInterfaceLayer.Forms.IViews
{
    public interface IDetailRelationRepositoryView
    {
        IQueryable<DetailRelationEntity> AllDetails { set; }

        event SimpleEventHandler GetAllDetails;
        event GenericEventHandler<DetailRelationEntity> AddDetail;
        event GenericEventHandler<DetailRelationEntity> EditDetail;
        event GenericEventHandler<DetailRelationEntity> DeleteDetail;
    }
}
