using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Common;
using FlightManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Data
{
    public class FlightManagerDbContext : DbContext
    {
        public FlightManagerDbContext()
        {
        }

        public FlightManagerDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationFlight> ReservationFlights { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConfiguration.DefConnectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(FlightManagerDbContext).Assembly);
        }
    }
}
