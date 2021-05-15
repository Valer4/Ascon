namespace BusinessLogicLayer.Entities.Classes.ConcreteDefinitions
{
    public class DetailTypeEntity : AbstractEntity<long>
    {
        public bool Root { get; set; }
        public string Name { get; set; }
    }
}
