using System;
namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for Customer DTO.
    /// </summary>
    public interface ICustomerDTO
    {
        string name { get; set; }
        string email { get; set; }
        string phoneNumber { get; set; }
    }
}
