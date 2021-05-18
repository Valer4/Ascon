using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Linq;

namespace UserInterfaceLayer.Forms.IViews
{
    public interface IDetailRelationRepositoryView
    {
        IQueryable<DetailRelationEntity> AllDetails { get; set; }

        event SimpleEventHandler LoadData;
        event AddEventHandler<DetailRelationEntity> AddDetail;
        event EditEventHandler<DetailRelationEntity> EditDetail;
        event DeleteEventHandler<DetailRelationEntity> DeleteDetail;
    }

    public delegate string AddEventHandler<T>(T entity, bool isRoot, string name, string amount);
    public delegate string EditEventHandler<T>(T entity, string name, string amount);
    public delegate string DeleteEventHandler<T>(T entity);
}
