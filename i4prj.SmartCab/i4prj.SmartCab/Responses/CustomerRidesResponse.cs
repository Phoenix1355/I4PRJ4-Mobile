using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using i4prj.SmartCab.Converters;
using i4prj.SmartCab.Interfaces;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Response from IBackendApiService when reqeusting the logged in Customers rides.
    /// </summary>
    public class CustomerRidesResponse : BackendApiResponse
    {
        public CustomerRidesResponseBody Body { get => (CustomerRidesResponseBody)_body; private set => _body = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Responses.CustomerRidesResponse"/> class.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        public CustomerRidesResponse(HttpResponseMessage responseMessage)
            : base(responseMessage)
        {
        }

        /// <summary>
        /// Implementation of base class template method. Makes the body for this type of response.
        /// </summary>
        protected override async void MakeBody()
        {
            string responseBodyAsText = await HttpResponseMessage.Content.ReadAsStringAsync();
            Debug.WriteLine("Http response body: " + responseBodyAsText);
            try
            {
                Body = JsonConvert.DeserializeObject<CustomerRidesResponseBody>(responseBodyAsText);
                Debug.WriteLine("Http-result parset uden fejl.");

            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                Debug.WriteLine("Http-result kunne ikke parses som json. Fejl: " + e.Message);
            }
        }
    }

    #region ResponseFormatClasses
    /// <summary>
    /// Response body from IBackendApiService when reqeusting the rides of the logged in Customer.
    /// </summary>
    public class CustomerRidesResponseBody : BackendApiResponseBody
    {
        public List<RideDTO> rides;

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

        public class AddressDTO : IAddressDTO
        {
            public string cityName { get; set; }
            public string postalCode { get; set; }
            public string streetName { get; set; }
            public string streetNumber { get; set; }
        }
    }
    #endregion
}