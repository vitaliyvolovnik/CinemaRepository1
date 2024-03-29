﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DLL.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Genre { get; set; } = "";
        public float Price { get; set; }
        public DateTime PlayTime { get; set; } = new DateTime(1, 1, 1);
        
       [NotMapped]
        public string PlayTimestr { get { return PlayTime.ToString("HH:mm:ss"); } }
        public override string ToString()
        {
            return Name;
        }
    }
}
