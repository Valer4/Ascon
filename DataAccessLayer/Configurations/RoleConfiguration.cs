using BusinessLogicLayer.Entities.Classes.ConcreteDefinitions;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configurations
{
    public class RoleConfiguration : EntityTypeConfiguration<RoleEntity>
    {
        public RoleConfiguration()
        {
            ToTable("roles");
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(64);
        }
    }
}
