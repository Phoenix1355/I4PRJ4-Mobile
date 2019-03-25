using System;
namespace i4prj.SmartCab.Interfaces
{
    public interface ICustomer
    {
        string Name { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
    }
}
