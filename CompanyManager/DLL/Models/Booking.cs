using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class Booking
    {
        public Session Session { get; set; }
        public int Row { get; set; }
        public int SeatInRow { get; set; }
        public bool IsBooking { get; set; }
        public BookingLog BookingLog { get; set; }
    }
}
