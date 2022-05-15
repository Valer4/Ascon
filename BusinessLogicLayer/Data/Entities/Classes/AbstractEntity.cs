using BusinessLogicLayer.Data.Entities.Interfaces;

namespace BusinessLogicLayer.Data.Entities.Classes
{
	public abstract class AbstractEntity<TId> : IAbstractEntity<TId>
	{
		public TId Id { get; set; }
	}
}
