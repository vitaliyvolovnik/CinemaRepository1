using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Seat
    {
        public int ID { get; set; }
        public CinemaHall Hall { get; set; }
        public int Row { get; set; }
        public int SeatInRow { get; set; }
        public string Type { get; set; } = "";
        public List<Booking> Bookings { get; set; }
    }
}
