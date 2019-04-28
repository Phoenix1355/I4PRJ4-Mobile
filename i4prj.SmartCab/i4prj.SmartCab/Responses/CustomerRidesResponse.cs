using System;
using System.Diagnostics;
using System.Net.Http;
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
            // Get json body as string
            string responseBodyAsText = await HttpResponseMessage.Content.ReadAsStringAsync();
            Debug.WriteLine("Http response body: " + responseBodyAsText);

            // Convert json string body CustomerRidesResponseBody
            try
            {
                Body = JsonConvert.DeserializeObject<CustomerRidesResponseBody>(responseBodyAsText);
                Debug.WriteLine("Http-result parset uden fejl.");

            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine("Http-result kunne ikke parses som json. Fejl: " + e.Message);
            }
        }
    }
}