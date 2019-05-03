using System;
namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for Address DTO.
    /// </summary>
    public interface IAddressDTO
    {
        string cityName { get; set; }
        string postalCode { get; set; }
        string streetName { get; set; }
        string streetNumber { get; set; }
    }
}
