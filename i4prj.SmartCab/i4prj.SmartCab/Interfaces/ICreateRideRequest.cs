using System;
using System.Collections.Generic;
using System.Text;

namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for the ICreateRideRequest
    /// </summary>
    public interface ICreateRideRequest
    {
        string OriginCityName { get; set; }
        string DestinationCityName { get; set; }
        string OriginPostalCode { get; set; }
        string DestinationPostalCode { get; set; }
        string OriginStreetName { get; set; }
        string DestinationStreetName { get; set; }
        string OriginStreetNumber { get; set; }
        string DestinationStreetNumber { get; set; }
        bool IsShared { get; set; }
        DateTime DepartureDate { get; set; }
        DateTime ConfirmationDeadlineDate { get; set; }
        TimeSpan DepartureTime { get; set; }
        TimeSpan ConfirmationDeadlineTime { get; set; }
        double AmountOfPassengers { get; set; }

        bool IsValid { get; }

        string CreateStringAddress(string type);
    }
}
