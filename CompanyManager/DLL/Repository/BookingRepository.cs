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
        public  async Task<bool> ChangeUserAsync(Booking booking)
        {
            var book = this.Entities
                .Where(x => x.Seat.Row == booking.Seat.Row && x.Seat.SeatInRow == booking.Seat.SeatInRow && x.Session.Id == booking.Session.Id)
                .Include(x => x.Employee);
            if (book.Count() > 0)
            {
                var bb = book.First();
                if (bb.IsCansel)
                {
                    bb.IsCansel = false;
                    bb.IsPaid = booking.IsPaid;
                    bb.IsBooking = true;
                    bb.Employee = booking.Employee;
                    bb.ClientPhoneNumber = booking.ClientPhoneNumber;
                    await this.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> CanselBookingAsync(Booking booking)
        {
            var book = this.Entities
               .Where(x => x.Seat.Row == booking.Seat.Row && x.Seat.SeatInRow == booking.Seat.SeatInRow && x.Session.Id == booking.Session.Id);
            if (book.Count() > 0)
            {
                var bb = book.First();
                if (!bb.IsCansel)
                {
                    bb.IsCansel = true;
                    await this.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> PaidBookingAsync(Booking booking)
        {
            var book = this.Entities
               .Where(x => x.Seat.Row == booking.Seat.Row && x.Seat.SeatInRow == booking.Seat.SeatInRow && x.Session.Id == booking.Session.Id);
            if (book.Count() > 0)
            {
                var bb = book.First();
                if (!bb.IsPaid)
                {
                    bb.IsPaid = true;
                    await this.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

    }
    
}
