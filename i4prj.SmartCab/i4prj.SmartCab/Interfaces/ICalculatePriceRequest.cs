using System;
using System.Collections.Generic;
using System.Text;

namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for ICalculatePriceRequest
    /// </summary>
    public interface ICalculatePriceRequest
    {
        string OriginCityName { get; set; }
        string DestinationCityName { get; set; }
        string OriginPostalCode { get; set; }
        string DestinationPostalCode { get; set; }
        string OriginStreetName { get; set; }
        string DestinationStreetName { get; set; }
        string OriginStreetNumber { get; set; }
        string DestinationStreetNumber { get; set; }
    }
}
