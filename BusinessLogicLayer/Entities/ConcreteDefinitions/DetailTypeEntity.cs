using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Entities.ConcreteDefinitions
{
    public class DetailTypeEntity : AbstractEntity<long>
    {
        public bool Root { get; set; }
        public string Name { get; set; }
    }
}
