namespace BusinessLogicLayer.Entities
{
    public abstract class AbstractEntity<TId>
    {
        public TId Id { get; set; }
    }
}
