using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class BookingLog
    {

        public int Id { get; set; }
        public int price { get; set; }
        public DateTime BookingTime { get; set; }
        public Employee Employee { get; set; }
        public Booking Booking { get; set; }


    }
}
