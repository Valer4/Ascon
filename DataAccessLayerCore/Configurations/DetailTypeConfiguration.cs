using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayerCore.Configurations
{
	internal class DetailTypeConfiguration : IEntityTypeConfiguration<DetailTypeEntity>
	{
		public void Configure(EntityTypeBuilder<DetailTypeEntity> builder)
		{
			builder.ToTable("detail_types");
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.IsRoot).IsRequired().HasColumnName("root");
			builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
		}
	}
}
