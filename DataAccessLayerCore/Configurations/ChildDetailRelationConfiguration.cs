using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayerCore.Configurations
{
	internal class ChildDetailRelationConfiguration : IEntityTypeConfiguration<ChildDetailRelationEntity>
	{
		public void Configure(EntityTypeBuilder<ChildDetailRelationEntity> builder)
		{
			builder.ToTable("child_detail_relations");
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.ParentId).IsRequired().HasColumnName("parent_id");
			builder.Property(x => x.TypeId).IsRequired().HasColumnName("type_id");
			builder.Property(x => x.Amount).IsRequired();
		}
	}
}
