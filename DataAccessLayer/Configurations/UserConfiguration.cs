using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<UserEntity>
    {
        public UserConfiguration()
        {
            ToTable("users");

            Property(x => x.Id).IsRequired();
            Property(x => x.Login).IsRequired().HasMaxLength(64);
            Property(x => x.Password).IsRequired().HasMaxLength(64);
        }
    }
}
