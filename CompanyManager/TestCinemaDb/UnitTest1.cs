using DLL.Context;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace TestCinemaDb
{
    
    public class UnitTest1
    {
        private CinemaContext context;
        [Fact]
        public void Test1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option = optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaTEst2Db;Integrated Security=True;Connect Timeout=30").Options;
            try
            {
                context = new(option);
                Assert.NotNull(context);
            }
            catch (System.Exception ex)
            {

                Assert.False(false,ex.Message);
            }
        }
        [Fact]
        public void TestSaveReadFilm()
        {
            Test1();
            context.Films.Add(new DLL.Models.Film() { Name="Terminator", Genre="triler", PlayTime=new System.DateTime(1,1,1,1,28,0),Price=100});
            context.SaveChanges();
            var film = context.Films.Where(x => x.Name == "Terminator").ToList(); 
        }
        [Fact]
        public void TestSaveReadHall()
        {
            Test1();
            context.Halls.Add(new DLL.Models.CinemaHall()
            {
                ScreenDiagonal="23.4",
                Rows=5,
                SeatsInRow=6,
                HallNumber=1
            });
            context.SaveChanges();
            var film = context.Halls.First(x => x.Rows == 5) ;
        }

        [Fact]
        public void TestSaveReadSeats()
        {
            Test1();
            var hall = context.Halls.First(x => x.Rows == 5);
            context.Seat.Add(new DLL.Models.Seat()
            {
                Row=2,
                SeatInRow=2,
                Type="Premium",
                Hall = hall

            });
            context.SaveChanges();
            var film = context.Seat.First(x => x.Row == 2);
        }
        [Fact]
        public void TestSaveReadEmployee()
        {
            Test1();

            context.Employees.Add(new DLL.Models.Employee()
            {
                Name = "Roma",
                DayOfBirdh = new System.DateTime(2004, 5, 5),
                Mail="RomaWork@gmail.com",
                Password="jdfkalpioej",
                Surname="Baranov",
                Role="kasur"
            });
            context.SaveChanges();
            Employee employee = context.Employees.First(x => x.Mail == "RomaWork@gmail.com");
        }
        [Fact]
        public void TestSaveReadSession()
        {
            Test1();
            var hall = context.Halls.First(x => x.Rows == 5);
            var film = context.Films.First(x => x.Name == "Terminator");
            context.Sessions.Add(new()
            {
                Hall = hall,
                StartTime = new System.DateTime(2021, 12, 13, 15, 40, 0),
                Film = film,
                EndTime = new System.DateTime(2021, 12, 13, 17, 0, 0),
                TiketPrice = 100,
                PremiumTiketPrice=180,
                
            }) ;
            context.SaveChanges();
            var session = context.Sessions.First(x => x.Film.Name == "Terminator");
            
        }
        [Fact]
        public void TestSaveReadBooking()
        {
            Test1();
            var session = context.Sessions.First(x => x.Film.Name == "Terminator");
            Employee employee = context.Employees.First(x => x.Mail == "RomaWork@gmail.com");
            var film = context.Seat.First(x => x.Row == 2);
            context.Bookings.Add(new Booking()
            {
                Employee= employee,
                Session = session,
                IsPaid=true,
                IsBooking=true,
                Seat = film,
                ClientPhoneNumber= "8390328903"
            });
            context.SaveChanges();
        }
    }
}