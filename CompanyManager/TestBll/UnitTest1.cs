using BLL.Services;
using DLL.Context;
using DLL.Models;
using DLL.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestBll
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CinemaContext>();
            var option = optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaTEst3Db;Integrated Security=True;Connect Timeout=30").Options;

            CinemaContext cont = new(option);
            EmployeeRepository context = new(cont);
            EmployeeService employee = new(context);
            Employee e = new() { Name = "hfdjasl", Surname = "fhdjlka", Mail = "fhjlkadshljfkdsahkjl" };
            var list = await employee.FireAsync(e);
        }
    }
}