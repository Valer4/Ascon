using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using DataAccessLayerCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayerCore
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new DetailTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChildDetailRelationConfiguration());
        }

        public DbSet<UserEntity>? Users { get; set; }
        public DbSet<RoleEntity>? Roles { get; set; }

        public DbSet<DetailTypeEntity>? DetailTypes { get; set; }
        public DbSet<ChildDetailRelationEntity>? ChildDetailRelations { get; set; }
    }
}
