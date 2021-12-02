using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Context
{
    internal class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingLog> BookingLogs { get; set; }
        public DbSet<BuyFilm> BuyFilms { get; set; }
        public DbSet<CinemaHall> Halls { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>().HasOne(x => x.Film);
            modelBuilder.Entity<Session>().HasOne(x=>x.Hall).WithMany(x=>x.Sessions);
            modelBuilder.Entity<Session>().HasMany(x => x.Bookings).WithOne(x => x.Session);
            modelBuilder.Entity<BookingLog>().HasOne(x => x.Booking).WithOne(x=>x.BookingLog);
            modelBuilder.Entity<BookingLog>().HasOne(x => x.Employee).WithMany(x => x.BookingLogs);
            modelBuilder.Entity<BuyFilm>().HasOne(x => x.Film).WithOne(x=>x.BFilms);
        }
    }
}
