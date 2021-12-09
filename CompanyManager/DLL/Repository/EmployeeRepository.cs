using DLL.Context;
using DLL.Models;
using DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(CinemaContext context) : base(context) { }
        public override async Task<IEnumerable<Employee>> FindBuConditionAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await this.Entities
               .Where(predicate)
               .Include(x => x.Bookings)
               .ToListAsync()
               .ConfigureAwait(false);

        }
        public override async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await this.Entities
                .Include(x => x.Bookings)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        public async Task<bool>FireAsync(Employee employee)
        {
            var emp = this.Entities.Where(x => x.Name == employee.Name && x.Surname == employee.Surname && x.Mail == employee.Mail);
            if(emp.Count()==1)
            {
                var e = emp.First();
                if (!e.isFire)
                {
                    e.isFire = true;
                    return true;
                }
            }
            return false;
        }
    }
}
