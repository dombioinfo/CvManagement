// using Microsoft.EntityFrameworkCore;
using BlazorBaseModel.Db;
// using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BlazorBaseApi
{
    public class MysqlDbContext : DbContext
    {
        // Remplacé par la surcharge OnModelCreating
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Profil> Profils { get; set; } = default!;
        public DbSet<Candidature> Candidatures { get; set; } = default!;
        public DbSet<Personne> Personnes { get; set; } = default!;
        public DbSet<Adresse> Adresses { get; set; } = default!;
        public DbSet<Cv> Cvs { get; set; } = default!;
        public DbSet<ListeType> ListeTypes { get; set; } = default!;
        public DbSet<ListeItem> ListeItems { get; set; } = default!;

        // public DbSet<GenericObject> GenericObject { get; set; } = default!;

        public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // var entitiesAssembly = typeof(GenericObject).Assembly;
            // Console.WriteLine($"-------------Assembly : {entitiesAssembly}");
            // modelBuilder.RegisterAllEntities<GenericObject>(entitiesAssembly);
            modelBuilder.Ignore<GenericObject>();

            /// Note : Le code ci-dessous semble forcer la création de la table GenericObject en mode EF code first
            // modelBuilder.Entity<GenericObject>()
            //     .Property(o => o.DateCreation)
            //     .HasDefaultValueSql("getdate()");

            // modelBuilder.Entity<GenericObject>()
            //     .Property(o => o.DateUpdate)
            //     .HasComputedColumnSql("NOW()", stored: true);

            // modelBuilder.Entity<ListeType>()
            //     .HasMany<ListeItem>(s => s.ListeItems)
            //     .WithMany()
            //     .HasForeignKey(s => s.ListeItem)
            //     .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
