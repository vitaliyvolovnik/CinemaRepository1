﻿using DLL.Models;
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
                                                        x.Seat.SeatInRow == booking.Seat.SeatInRow&&
                                                        x.Session.Id == booking.Session.Id))as List<Booking>;
            if (list.Count == 1)
            {
                if (list.First().IsCansel)
                {
                    await bookingRepository.ChangeUserAsync(booking);
                    return true;
                }
                else return false;
            }
            await bookingRepository.CreateAsync(booking);
            return true;
        }
        public async Task<bool> PaidTicketAsync(Booking booking)
        {
            var list = (await bookingRepository.FindBuConditionAsync(x => x.Seat.Row == booking.Seat.Row && x.Seat.SeatInRow == booking.Seat.SeatInRow&&x.IsPaid==false)) as List<Booking>;
            if (list.Count == 0) return false;
            
            return await bookingRepository.PaidBookingAsync(booking);
        }
        public async Task<bool> CanselTicket(Booking booking)
        {
            return  await bookingRepository.CanselBookingAsync(booking);

        }
        public async Task<List<Booking>> ReturnBySessionTicket(Session session)
        {
            return (List<Booking>)(await bookingRepository.FindBuConditionAsync(x => x.Session.Id == session.Id));
        }
        public async Task<List<Booking>> ReturnByClientNumberTicket(string Number, Session session)
        {
            return (List<Booking>)(await bookingRepository.FindBuConditionAsync(x => x.Session.Id == session.Id&&x.ClientPhoneNumber==Number));
        }



    }
}