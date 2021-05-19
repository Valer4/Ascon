using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Linq;

namespace UserInterfaceLayer.Forms.IViews
{
    public interface IDetailRelationRepositoryView
    {
        IQueryable<DetailRelationEntity> AllDetails { get; set; }

        event SimpleEventHandler LoadData;
        event AddEventHandler<string, DetailRelationEntity> AddDetail;
        event EditEventHandler<string, DetailRelationEntity> EditDetail;
        event ParamReturnDelegate<string, DetailRelationEntity> DeleteDetail;
    }

    public delegate TReturn AddEventHandler<TReturn, T>(T entity, bool isRoot, string name, string amount);
    public delegate TReturn EditEventHandler<TReturn, T>(T entity, string name, string amount);
    public delegate string DeleteEventHandler<T>(T entity);
}
