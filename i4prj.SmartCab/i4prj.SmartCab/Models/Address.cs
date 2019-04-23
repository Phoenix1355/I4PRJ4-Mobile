using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Models
{
    public class Address : IAddress
    {
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int PostalCode { get; set; }
        public int StreetNumber { get; set; }
    }
}
