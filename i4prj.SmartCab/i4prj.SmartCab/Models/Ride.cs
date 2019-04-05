using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Models
{
    public class Ride : IRide
    {
        public IAddress Origin { get; set; }
        public IAddress Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ConfirmationDeadline { get; set; }
        public int AmountOfPassengers { get; set; }
        public bool Shared { get; set; }
        public double Price { get; set; }
    }
}
