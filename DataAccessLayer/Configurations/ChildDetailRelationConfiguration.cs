using BusinessLogicLayer.Entities.ConcreteDefinitions;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configurations
{
    public class ChildDetailRelationConfiguration : EntityTypeConfiguration<ChildDetailRelationEntity>
    {
        public ChildDetailRelationConfiguration()
        {
            ToTable("child_detail_relations");
            Property(x => x.Id).IsRequired();
            Property(x => x.ParentId).IsRequired().HasColumnName("parent_id");
            Property(x => x.TypeId).IsRequired().HasColumnName("type_id");
            Property(x => x.Amount).IsRequired();
        }
    }
}
