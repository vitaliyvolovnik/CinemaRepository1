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
    public class SeatRepository: BaseRepository<Seat>
    {
        public SeatRepository(CinemaContext context) : base(context) { }
        public override async Task<IEnumerable<Seat>> GetAllAsync()
        {
            return await this.Entities
                .Include(x=>x.Hall)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }
        public override async Task<IEnumerable<Seat>> FindBuConditionAsync(Expression<Func<Seat, bool>> prediat)
        {
            return await this.Entities
                .Include(x => x.Hall)
                .Where(prediat)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
