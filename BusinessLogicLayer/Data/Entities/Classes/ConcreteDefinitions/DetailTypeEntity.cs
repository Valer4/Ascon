namespace BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions
{
	public class DetailTypeEntity : AbstractEntity<long>
	{
		public bool IsRoot { get; set; }
		public string Name { get; set; }
	}
}
