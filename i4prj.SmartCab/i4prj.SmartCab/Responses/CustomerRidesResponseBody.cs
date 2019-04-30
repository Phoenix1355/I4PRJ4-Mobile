﻿using System.Collections.Generic;
using i4prj.SmartCab.Converters;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Response body from IBackendApiService when reqeusting the rides of the logged in Customer.
    /// </summary>
    public class CustomerRidesResponseBody : BaseResponseBody
    {
        [JsonConverter(typeof(ConcreteConverter<IEnumerable<RideDTO>>))]
        public IEnumerable<IRideDTO> rides;
    }
}