using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using System.Data.Entity.ModelConfiguration;

namespace DataAccessLayer.Configurations
{
    internal class DetailTypeConfiguration : EntityTypeConfiguration<DetailTypeEntity>
    {
        internal DetailTypeConfiguration()
        {
            ToTable("detail_types");
            Property(x => x.Id).IsRequired();
            Property(x => x.IsRoot).IsRequired().HasColumnName("root");;
            Property(x => x.Name).IsRequired().HasMaxLength(64);
        }
    }
}
