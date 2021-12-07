using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Session Session { get; set; }
        public bool IsBooking { get; set; }
        public Employee Employee { get; set; }
        public string ClientPhoneNumber { get; set; }
        public bool IsPaid { get; set; }
        public Seat Seat { get; set; }

    }
}
