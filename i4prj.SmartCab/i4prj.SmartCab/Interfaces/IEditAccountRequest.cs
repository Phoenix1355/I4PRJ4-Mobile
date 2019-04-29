using System;
using System.Collections.Generic;
using System.Text;

namespace i4prj.SmartCab.Interfaces
{
    public interface IEditAccountRequest
    {
        string Name { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        bool ChangePassword { get; set; }
        string OldPassword { get; set; }
        string Password { get; set; }
        string RepeatedPassword { get; set; }
    }
}
