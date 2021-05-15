using BusinessLogicLayer.Data.Entities.Interfaces;
using System;

namespace BusinessLogicLayer.Data.Entities.Classes
{
    public class TreeLogicalNode<TId, TParentId, TTypeId> : AbstractEntity<TId>, ITreeLogicalNode<TId, TParentId, TTypeId>
    {
        public bool IsRoot { get; set; }
        public TParentId ParentId { get; set; }
        public TTypeId TypeId { get; set; }

        public virtual string Text => throw new NotImplementedException();
    }
}
