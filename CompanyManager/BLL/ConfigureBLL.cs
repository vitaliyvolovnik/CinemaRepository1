using DLL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public static class ConfigureBLL
    {
        public static void ConfigereService(ServiceCollection collection)
        {
            collection.AddDbContext<CinemaContext>(o=>o.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CinemaTestDb;Integrated Security=True;Connect Timeout=30;"));
        }
    }
}
