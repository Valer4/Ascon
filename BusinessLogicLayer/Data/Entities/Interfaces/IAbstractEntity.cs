namespace BusinessLogicLayer.Data.Entities.Interfaces
{
    public interface IAbstractEntity<TId>
    {
        TId Id { get; set; }
    }
}
