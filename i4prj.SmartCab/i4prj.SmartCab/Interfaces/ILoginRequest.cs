using System;
namespace i4prj.SmartCab.Interfaces
{
    public interface ILoginRequest
    {
        string Email { get; set; }
        string Password { get; set; }
        bool IsValid { get; }
        bool IsInvalid { get; }
    }
}
