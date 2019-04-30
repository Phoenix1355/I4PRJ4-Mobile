using System;
using i4prj.SmartCab.Converters;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    public class EditAccountResponseBody : BaseResponseBody
    {
        [JsonConverter(typeof(ConcreteConverter<CustomerDTO>))]
        public ICustomerDTO customer { get; set; }
    }
}
