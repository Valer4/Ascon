namespace BusinessLogicLayer.Entities.Classes.ConcreteDefinitions
{
    public class DetailRelationEntity : TreeLogicalNode<long, long?, long>
    {
        public override string Text => Root ? Name : $"{Name} ({Amount})";

        public long? RelationId { get; set; }
        public string Name { get; set; }
        public short? Amount { get; set; }
    }
}
