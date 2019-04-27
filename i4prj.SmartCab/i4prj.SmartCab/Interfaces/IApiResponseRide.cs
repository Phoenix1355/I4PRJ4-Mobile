using System;
namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for IApiResponseRide.
    /// </summary>
    public interface IApiResponseRide
    {
        string customerId { get; set; }
        DateTime departureTime { get; set; }
        IApiResponseAddress startDestination { get; set; }
        IApiResponseAddress endDestination { get; set; }
        DateTime confirmationDeadline { get; set; }
        int passengerCount { get; set; }
        double price { get; set; }
        string status { get; set; }
    }
}
