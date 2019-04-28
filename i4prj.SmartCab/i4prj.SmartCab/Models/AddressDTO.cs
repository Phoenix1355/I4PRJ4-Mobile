using System;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Models
{
    public class AddressDTO : IAddressDTO
    {
        public string cityName { get; set; }
        public string postalCode { get; set; }
        public string streetName { get; set; }
        public string streetNumber { get; set; }
    }
}
