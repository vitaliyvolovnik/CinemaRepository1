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
        public async Task<IEnumerable<CinemaHall>> GetAllWithoutSeatsAsync()
        {
            return await this.Entities
                .Include(x => x.Sessions)
                .ToArrayAsync()
                .ConfigureAwait(false);
        }
        public async Task<IEnumerable<CinemaHall>> FindBuConditionWithoutSeatsAsync(Expression<Func<CinemaHall, bool>> predicate)
        {
            return await this.Entities
                .Where(predicate)
                .Include(x => x.Sessions)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        public override async Task<IEnumerable<CinemaHall>> FindBuConditionAsync(Expression<Func<CinemaHall, bool>> predicate)
        {
            return await this.Entities
                .Where(predicate)
                .Include(x => x.Seats)
                .Include(x => x.Sessions)
                .ToListAsync()
                .ConfigureAwait(false);
        }

    }
}
