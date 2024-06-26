﻿using LetishteNet5.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LetishteNet5.Data
{
    public class FlightManagerDbContext : IdentityDbContext<User>
    {
        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Flight>()
                .HasMany(f => f.Reservations)
                .WithOne(r => r.Flight)
                .HasForeignKey(f => f.FlightId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
