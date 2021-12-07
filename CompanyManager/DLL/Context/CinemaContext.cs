using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CinemaHall> Halls { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Seat> Seat { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         

            modelBuilder.Entity<Session>()
                .HasOne(x => x.Hall)
                .WithMany(x => x.Sessions);

            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Session)
                .WithMany(x => x.Bookings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.Bookings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Seat)
                .WithMany(x => x.Bookings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Seat>()
                .HasOne(x => x.Hall)
                .WithMany(x => x.Seats)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
