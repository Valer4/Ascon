using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayerCore.Configurations
{
	internal class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
	{
		public void Configure(EntityTypeBuilder<RoleEntity> builder)
		{
			builder.ToTable("roles");
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.Name).IsRequired().HasMaxLength(64);
		}
	}
}
