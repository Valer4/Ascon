using BusinessLogicLayer;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Linq;

namespace UserInterfaceLayer.Forms.IViews
{
    internal interface IDetailRelationRepositoryView
    {
        IQueryable<DetailRelationEntity> AllDetails { get; set; }

        event SimpleEventHandler LoadData;
        event ReturnDelegate<string, DetailRelationEntity, bool, string, string> AddDetail;
        event ReturnDelegate<string, DetailRelationEntity, string, string> EditDetail;
        event ReturnDelegate<string, DetailRelationEntity> DeleteDetail;
    }
}
