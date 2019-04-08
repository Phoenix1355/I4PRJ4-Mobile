using System;
namespace i4prj.SmartCab.Interfaces
{
    public interface ICreateCustomerRequest
    {
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Password { get; set; }
        string PasswordConfirmation { get; set; }
        bool IsValid { get; }
        bool IsInvalid { get; }
    }
}
