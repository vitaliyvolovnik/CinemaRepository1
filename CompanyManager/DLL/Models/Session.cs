using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class Session
    {
        public int Id { get; set; }
        public List<Booking> Bookings { get; set; }
        public CinemaHall Hall { get; set; }    
        public Film Film { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TiketPrice { get; set; }
        public float PremiumTiketPrice { get; set; }
        public override string ToString()
        {
            return Id + Film.Name + Hall.HallNumber;
        }


    }
}
