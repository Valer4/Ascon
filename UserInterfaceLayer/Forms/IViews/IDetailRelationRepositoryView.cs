using BusinessLogicLayer;
using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Linq;

namespace UserInterfaceLayer.Forms.IViews
{
    internal interface IDetailRelationRepositoryView
    {
        IQueryable<DetailRelationEntity> AllDetails { get; set; }

        event SimpleEventHandler LoadData;
        event ParamReturnDelegate<string, DetailRelationEntity, bool, string, string> AddDetail;
        event ParamReturnDelegate<string, DetailRelationEntity, string, string> EditDetail;
        event ParamReturnDelegate<string, DetailRelationEntity> DeleteDetail;
    }
}
