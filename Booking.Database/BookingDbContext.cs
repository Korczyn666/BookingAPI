using Booking.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Database
{
    public  class BookingDbContext : DbContext
    {
        public BookingDbContext()
        {

        }

        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
        {

        }

        public DbSet<Trip> Trip { get; set; }
        public DbSet<RegisterTrip> RegisterTrip { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
              new Country { Id = 100, Name = "Poland" },
              new Country { Id = 200, Name = "USA" },
              new Country { Id = 300, Name = "UK" },
              new Country { Id = 400, Name = "Norway" }
              );
            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 1, CountryId = 100, Name = "Poland trip", Description = "Poland trip description", NumberOfSeats = 2, StartDate = DateTime.Now.AddDays(30) },
                new Trip { Id = 2, CountryId = 200, Name = "USA trip", Description = "USA trip description", NumberOfSeats = 3, StartDate = DateTime.Now.AddDays(60) },
                new Trip { Id = 3, CountryId = 300, Name = "UK trip", NumberOfSeats = 2, StartDate = DateTime.Now.AddDays(15) },
                new Trip { Id = 4, CountryId = 400, Name = "Norway trip", NumberOfSeats = 4, StartDate = DateTime.Now.AddMonths(3) }
                );
        }
    }
}
