namespace BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions
{
    public class ChildDetailRelationEntity : AbstractEntity<long>
    {
        public long ParentId { get; set; }
        public long TypeId { get; set; }
        public short Amount { get; set; }
    }
}
