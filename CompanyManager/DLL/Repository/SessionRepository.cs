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
    public class SessionRepository : BaseRepository<Session>
    {
        public SessionRepository(CinemaContext context) : base(context) { }
        public override async Task<IEnumerable<Session>> FindBuConditionAsync(Expression<Func<Session, bool>> predicate)
        {
            return await this.Entities
                .Where(predicate)
                .Include(x => x.Bookings).ThenInclude(x=>x.Seat)
                .Include(x => x.Hall)
                .Include(x => x.Film)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        public override  async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await this.Entities
                .Include(x => x.Bookings).ThenInclude(x => x.Seat)
                .Include(x => x.Hall)
                .Include(x => x.Film)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
