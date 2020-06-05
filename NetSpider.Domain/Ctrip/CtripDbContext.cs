using Microsoft.EntityFrameworkCore;

namespace NetSpider.Domain.Ctrip
{
    public class CtripDbContext : DbContext
    {
        public CtripDbContext(DbContextOptions<CtripDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>().ToTable("airlines");
            modelBuilder.Entity<Airport>().ToTable("airports");
            modelBuilder.Entity<Cabin>().ToTable("cabins");
            modelBuilder.Entity<Characteristic>().ToTable("characteristics");
            modelBuilder.Entity<Flight>().ToTable("flights");
        }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Flight> Flights { get; set; }
    }
}
