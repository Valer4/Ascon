namespace BusinessLogicLayer.Data.Entities.Interfaces
{
    public interface ITreeLogicalNode<TId, TParentId, TTypeId> : IAbstractEntity<TId>
    {
        bool IsRoot { get; set; }
        TParentId ParentId { get; set; }
        TTypeId TypeId { get; set; }

        string Text { get; }
    }
}
