using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public List<BookingLog> BookingLogs { get; set; }
        public DateTime DayOfBirdh { get; set; }
    }
}
