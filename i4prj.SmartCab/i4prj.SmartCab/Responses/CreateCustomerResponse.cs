using System;
using System.Diagnostics;
using System.Net.Http;
using i4prj.SmartCab.Converters;
using i4prj.SmartCab.Interfaces;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Response from IBackendApiService when submitting a request to create a Customer.
    /// </summary>
    public class CreateCustomerResponse : BackendApiResponse
    {
        public CreateCustomerResponseBody Body { get => (CreateCustomerResponseBody)_body; private set => _body = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Responses.CreateCustomerResponse"/> class.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        public CreateCustomerResponse(HttpResponseMessage responseMessage)
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
                Body = JsonConvert.DeserializeObject<CreateCustomerResponseBody>(responseBodyAsText);
                Debug.WriteLine("Http-result parset uden fejl.");

            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine("Http-result kunne ikke parses som json. Fejl: " + e.Message);
            }
        }
    }

    #region ResponseBodyJsonFormat

    /// <summary>
    /// Response body from IBackendApiService when submitting a request to create a Customer.
    /// </summary>
    public class CreateCustomerResponseBody : BackendApiResponseBody
    {
        public string token { get; set; }

        [JsonConverter(typeof(ConcreteConverter<Customer>))]
        public IApiResponseCustomer customer { get; set; }

        public class Customer : IApiResponseCustomer
        {
            public string name { get; set; }
            public string email { get; set; }
            public string phoneNumber { get; set; }
        }
    }

    #endregion
}