﻿using System.Collections.Generic;
using i4prj.SmartCab.Models;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Response body from IBackendApiService when reqeusting the rides of the logged in Customer.
    /// </summary>
    public class CustomerRidesResponseBody : BackendApiResponseBody
    {
        public List<RideDTO> rides;
    }
}