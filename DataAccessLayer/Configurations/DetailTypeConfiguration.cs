using BusinessLogicLayer.Entities.ConcreteDefinitions;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configurations
{
    public class DetailTypeConfiguration : EntityTypeConfiguration<DetailTypeEntity>
    {
        public DetailTypeConfiguration()
        {
            ToTable("detail_types");
            Property(x => x.Id).IsRequired();
            Property(x => x.Root).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(64);
        }
    }
}
