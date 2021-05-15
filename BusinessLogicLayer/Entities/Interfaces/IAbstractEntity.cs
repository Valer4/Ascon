namespace BusinessLogicLayer.Entities.Interfaces
{
    public interface IAbstractEntity<TId>
    {
        TId Id { get; set; }
    }
}
