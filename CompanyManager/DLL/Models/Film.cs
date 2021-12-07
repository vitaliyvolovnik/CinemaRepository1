using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public float Price { get; set; }
        public DateTime PlayTime { get; set; }
    }
}
