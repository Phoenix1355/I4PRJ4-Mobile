using System;
using i4prj.SmartCab.Converters;
using i4prj.SmartCab.Interfaces;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Models
{
    public class RideDTO : IRideDTO
    {
        public string customerId { get; set; }
        public DateTime departureTime { get; set; }
        [JsonConverter(typeof(ConcreteConverter<AddressDTO>))]
        public IAddressDTO startDestination { get; set; }
        [JsonConverter(typeof(ConcreteConverter<AddressDTO>))]
        public IAddressDTO endDestination { get; set; }
        public DateTime confirmationDeadline { get; set; }
        public int passengerCount { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }
}
