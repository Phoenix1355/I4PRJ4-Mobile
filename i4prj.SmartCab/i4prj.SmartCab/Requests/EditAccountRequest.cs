using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Requests
{
    public class EditAccountRequest : IEditAccountRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool ChangePassword { get; set; }

        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }

    }
}
