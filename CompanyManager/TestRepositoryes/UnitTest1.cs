using DLL.Context;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestRepositoryes
{
    public class UnitTest1
    {
        [Fact]
        public async void TestFilmRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option = optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaTEst3Db;Integrated Security=True;Connect Timeout=30").Options;
            
           
            CinemaContext context = new(option);


            FilmRepository filmRepository = new(context);
            await filmRepository.CreateAsync(new DLL.Models.Film() {Name="qwerty",Genre="ytrewq",Price=200,PlayTime=new System.DateTime(1,1,1,1,12,0) });
            var film = await filmRepository.GetAllAsync();
            var film1 = await filmRepository.FindBuConditionAsync(x => x.Name == "qwerty");
        }
        [Fact]
        public async void TestHallRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option = optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaTEst3Db;Integrated Security=True;Connect Timeout=30").Options;


            CinemaContext context = new(option);


            HallRepository rep = new(context);
            await rep.CreateAsync(new DLL.Models.CinemaHall() { HallNumber=1,ScreenDiagonal="12.5",Rows=2,SeatsInRow=2});
            var hall= await rep.GetAllAsync();
            var hall1 = await rep.FindBuConditionAsync(x=>x.HallNumber==1);
        }
        
    }
}