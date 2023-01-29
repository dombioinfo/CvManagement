using Microsoft.EntityFrameworkCore;
using BlazorBaseModel.Db;
using System.Reflection;

namespace BlazorBaseApi
{
    public class MysqlDbContext : DbContext
    {
        // Remplacé par la surcharge OnModelCreating
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Profil> Profil { get; set; } = default!;
        public DbSet<Candidature> Candidature { get; set; } = default!;
        public DbSet<Personne> Personne { get; set; } = default!;
        public DbSet<Adresse> Adresse { get; set; } = default!;

        public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = typeof(GenericObject).Assembly;
            Console.WriteLine($"{entitiesAssembly}");
            modelBuilder.RegisterAllEntities<GenericObject>(entitiesAssembly);

            modelBuilder.Entity<GenericObject>()
                .Property(o => o.DateCreation)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<GenericObject>()
                .Property(o => o.DateUpdate)
                .HasComputedColumnSql("NOW()", stored: true);
        }
    }
}
