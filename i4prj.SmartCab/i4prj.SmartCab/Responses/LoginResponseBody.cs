using System;
using i4prj.SmartCab.Converters;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Response body from IBackendApiService when submitting a request to login with Customer credentials.
    /// </summary>
    public class LoginResponseBody : BaseResponseBody
    {
        public string token { get; set; }

        [JsonConverter(typeof(ConcreteConverter<CustomerDTO>))]
        public ICustomerDTO customer { get; set; }
    }
}
