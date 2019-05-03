using System;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Models
{
    public class CustomerDTO : ICustomerDTO
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
