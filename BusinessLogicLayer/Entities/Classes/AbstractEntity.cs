using BusinessLogicLayer.Entities.Interfaces;

namespace BusinessLogicLayer.Entities.Classes
{
    public abstract class AbstractEntity<TId> : IAbstractEntity<TId>
    {
        public TId Id { get; set; }
    }
}
