using System;
using i4prj.SmartCab.Converters;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Body for the CreateRideResponse
    /// </summary>
    /// <seealso cref="i4prj.SmartCab.Responses.BaseResponseBody" />
    public class CreateRideResponseBody : BaseResponseBody
    {
        public int id { get; set; }

        [JsonConverter(typeof(ConcreteConverter<AddressDTO>))]
        public IAddressDTO startDestination { get; set; }

        [JsonConverter(typeof(ConcreteConverter<AddressDTO>))]
        public IAddressDTO endDestination { get; set; }

        public DateTime departureTime { get; set; }
        public DateTime confirmationDeadline { get; set; }
        public DateTime createdOn { get; set; }
        public double price { get; set; }
        public string status { get; set; }
    }
}
