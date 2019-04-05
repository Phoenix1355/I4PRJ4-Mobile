using System;
using System.Collections.Generic;
using System.Text;

namespace i4prj.SmartCab.Interfaces
{
    public interface IRide
    {
        IAddress Origin { get; set; }
        IAddress Destination { get; set; }
        DateTime DepartureTime { get; set; }
        DateTime ConfirmationDeadline { get; set; }
        int AmountOfPassengers { get; set; }
        bool Shared { get; set; }
        double Price { get; set; }
    }
}
