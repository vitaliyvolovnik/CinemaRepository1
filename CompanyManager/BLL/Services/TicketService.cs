using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TicketService
    {
        BookingRepository bookingRepository;
        public TicketService(BookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }
        public async Task<bool> BookingTicketAsync(Booking booking)
        {
            var list = (await bookingRepository.FindBuConditionAsync(x => x.Seat.Row == booking.Seat.Row &&
                                                        x.Seat.SeatInRow == booking.Seat.SeatInRow &&
                                                        x.Session.Id == booking.Session.Id)).ToList();
            if (list.Count == 1)
            {
                return await bookingRepository.ChangeUserAsync(booking);
            }
            await bookingRepository.CreateAsync(booking);
            return true;
        }
        public async Task<bool> PaidTicketAsync(Booking booking)
        {

            return await bookingRepository.PaidBookingAsync(booking);
        }
        public async Task<bool> CanselTicket(Booking booking)
        {
            return await bookingRepository.CanselBookingAsync(booking);

        }
        public async Task<List<Booking>> ReturnBySessionTicket(Session session)
        {
            return (await bookingRepository.FindBuConditionAsync(x => x.Session.Id == session.Id))?.ToList();
        }
        public async Task<List<Booking>> ReturnByClientNumberTicket(string Number, Session session)
        {
            return (await bookingRepository.FindBuConditionAsync(x => x.Session.Id == session.Id && x.ClientPhoneNumber == Number))?.ToList();
        }



    }
}
