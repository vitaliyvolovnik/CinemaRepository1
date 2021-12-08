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
    public class SessionRepositoryv : BaseRepository<Session>
    {
        public SessionRepositoryv(CinemaContext f) : base(f) { }
        public override async Task<IEnumerable<Session>> FindBuConditionAsync(Expression<Func<Session, bool>> prediat)
        {
            return await this.Entities
                .Include(x=>x.Bookings)
                .Include(x=>x.Hall)
                .Include(x=>x.Film)
                .Where(prediat)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        public override  async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await this.Entities
                .Include(x => x.Bookings)
                .Include(x => x.Hall)
                .Include(x => x.Film)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
