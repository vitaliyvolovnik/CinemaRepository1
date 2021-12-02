using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class BuyFilm
    {
        public int ID { get; set; }
        public Film Film { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
    }
}
