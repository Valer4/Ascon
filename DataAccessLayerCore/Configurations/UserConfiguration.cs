using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayerCore.Configurations
{
	internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
	{
		public void Configure(EntityTypeBuilder<UserEntity> builder)
		{
			builder.ToTable("users");
			builder.Property(x => x.Id).IsRequired();
			builder.Property(x => x.UserName).IsRequired().HasMaxLength(64);
			builder.Property(x => x.Password).IsRequired().HasMaxLength(64);
		}
	}
}
