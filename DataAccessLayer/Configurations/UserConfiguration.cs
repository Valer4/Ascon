using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configurations
{
    internal class UserConfiguration : EntityTypeConfiguration<UserEntity>
    {
        internal UserConfiguration()
        {
            ToTable("users");

            Property(x => x.Id).IsRequired();
            Property(x => x.Login).IsRequired().HasMaxLength(64);
            Property(x => x.Password).IsRequired().HasMaxLength(64);
        }
    }
}
