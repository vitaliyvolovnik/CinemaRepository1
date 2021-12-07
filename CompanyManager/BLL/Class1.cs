using DLL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BLL
{
    public class Class1
    {
        public void test()
        {
            var op = new DbContextOptionsBuilder<CinemaContext>();
            var option = op.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaTest1Db;Integrated Security=True;Connect Timeout=30").Options;
            var db = new CinemaContext(option);
        }
    }
}