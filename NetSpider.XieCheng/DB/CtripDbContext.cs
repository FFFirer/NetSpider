using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using NetSpider.XieCheng.DB.Entities;

namespace NetSpider.XieCheng.DB
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
