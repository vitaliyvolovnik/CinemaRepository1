using DLL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository.Interfaces
{
    public class BaseRepository<T> : IRepository<T> where T:class
    {
        public BaseRepository(CinemaContext cinema)
        {
            _cinemaContext = cinema;
        }
        private DbSet<T> _entities;
        protected CinemaContext _cinemaContext;
        protected DbSet<T> Entities => this._entities ??= _cinemaContext.Set<T>();
        public virtual async Task CreateAsync(T obj)
        {
            await Entities.AddAsync(obj).ConfigureAwait(false);
            await _cinemaContext.SaveChangesAsync();
        }
        public virtual async Task<IEnumerable<T>> FindBuConditionAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.Entities.Where(predicate).ToListAsync().ConfigureAwait(false); 
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.Entities.ToListAsync().ConfigureAwait(false);
        }
        public virtual async Task SaveChangesAsync()
        {
            await _cinemaContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
