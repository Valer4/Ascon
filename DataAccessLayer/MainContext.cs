using BusinessLogicLayer.Data.Entities.Classes.ConcreteDefinitions;
using DataAccessLayer.Configurations;
using System.Data.Entity;

namespace DataAccessLayer
{
	public class MainContext : DbContext
	{
		public MainContext(string nameOrConnectionString) : base(nameOrConnectionString) {}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserConfiguration());
			modelBuilder.Configurations.Add(new RoleConfiguration());

			modelBuilder.Configurations.Add(new DetailTypeConfiguration());
			modelBuilder.Configurations.Add(new ChildDetailRelationConfiguration());
		}

		public DbSet<UserEntity> Users { get; set; }
		public DbSet<RoleEntity> Roles { get; set; }

		public DbSet<DetailTypeEntity> DetailTypes { get; set; }
		public DbSet<ChildDetailRelationEntity> ChildDetailRelations { get; set; }
	}
}
