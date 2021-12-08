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
    public class BookingRepository : BaseRepository<Booking> { 
        public BookingRepository(CinemaContext f) : base(f) { }
        public override async Task<IEnumerable<Booking>> FindBuConditionAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await this.Entities
                .Where(predicate)
                .Include(x => x.Session)
                .Include(x => x.Seat)
                .Include(x => x.Employee)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        public override async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await this.Entities
                .Include(x => x.Session)
                .Include(x => x.Seat)
                .Include(x => x.Employee)
                .ToListAsync()
                .ConfigureAwait(false);
        }

       
    }
    
}
