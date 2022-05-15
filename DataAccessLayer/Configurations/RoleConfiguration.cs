using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configurations
{
	internal class RoleConfiguration : EntityTypeConfiguration<RoleEntity>
	{
		internal RoleConfiguration()
		{
			ToTable("roles");
			Property(x => x.Id).IsRequired();
			Property(x => x.Name).IsRequired().HasMaxLength(64);
		}
	}
}
