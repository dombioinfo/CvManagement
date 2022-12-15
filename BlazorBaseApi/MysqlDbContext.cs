using Microsoft.EntityFrameworkCore;
using BlazorBaseModel.Db;

namespace BlazorBaseApi
{
    public class MysqlDbContext : DbContext
    {
        // Remplacé par la surcharge OnModelCreating
        public DbSet<WeatherForecast> WeatherForecast { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Profil> Profil { get; set; }

        public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = typeof(GenericObject).Assembly;
            modelBuilder.RegisterAllEntities<GenericObject>(entitiesAssembly);
        }
    }
}
