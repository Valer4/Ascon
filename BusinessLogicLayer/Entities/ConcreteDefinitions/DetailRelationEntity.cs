using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Entities.ConcreteDefinitions
{
    public class DetailRelationEntity : AbstractEntity<long>
    {
        public bool Root { get; set; }
        public long? RelationId { get; set; }
        public long? ParentId { get; set; }
        public long TypeId { get; set; }
        public string Name { get; set; }
        public short? Amount { get; set; }

        public string Text => Root ? Name : $"{Name} ({Amount})";
    }
}
