using DLL.Context;
using DLL.Models;
using DLL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DLL.Repository
{
    public  class HallRepository : BaseRepository<CinemaHall>
    {
        public HallRepository(CinemaContext context) : base(context) { }
        public override async Task<IEnumerable<CinemaHall>> GetAllAsync()
        {
            return await this.Entities
                .Include(x => x.Seats)
                .Include(x => x.Sessions)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }
        public override async Task<IEnumerable<CinemaHall>> FindBuConditionAsync(Expression<Func<CinemaHall, bool>> prediat)
        {
            return await this.Entities
                .Include(x => x.Seats)
                .Include(x => x.Sessions)
                .Where(prediat)
                .ToListAsync()
                .ConfigureAwait(false);
        }

    }
}
