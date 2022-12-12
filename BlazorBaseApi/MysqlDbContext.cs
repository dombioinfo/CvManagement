using Microsoft.EntityFrameworkCore;
using BlazorBaseModel.Db;

namespace BlazorBaseApi
{
    public class MysqlDbContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecast { get; set; }

        public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options) { }
    }
}
