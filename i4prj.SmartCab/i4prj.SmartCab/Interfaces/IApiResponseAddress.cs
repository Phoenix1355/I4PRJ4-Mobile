using System;
namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for IApiResponseAddress.
    /// </summary>
    public interface IApiResponseAddress
    {
        string cityName { get; set; }
        string postalCode { get; set; }
        string streetName { get; set; }
        string streetNumber { get; set; }
    }
}
