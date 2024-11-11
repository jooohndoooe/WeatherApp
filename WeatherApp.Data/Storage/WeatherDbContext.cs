using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Data.Storage
{
    public class WeatherDbContext : DbContext
    {
        public virtual DbSet<City> Cities { get; set; }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>(builder =>
            {
                builder.HasKey(e => e.LocationKey);
                builder.ToTable("City");
            });
        }
    }
}
