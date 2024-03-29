﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class CinemaHall
    {

        public int Id { get; set; }
        public List<Session> Sessions { get; set; }
        public int HallNumber { get; set; }
        public int Rows { get; set; }
        public int SeatsInRow { get; set; }
        public string ScreenDiagonal { get; set; } = "";
        public List<Seat> Seats { get; set; }
        public override string ToString()
        {
            return $"Hall {HallNumber}";
        }


    }
}
