using System;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    public class CreateCustomerResponse : BackendApiResponse
    {
        public CreateCustomerResponseBody Body { get => (CreateCustomerResponseBody)_body; private set => _body = value; }

        public CreateCustomerResponse(HttpResponseMessage responseMessage)
            : base(responseMessage)
        {
        }

        protected override async void MakeBody()
        {
            string responseBodyAsText = await HttpResponseMessage.Content.ReadAsStringAsync();
            try
            {
                Body = JsonConvert.DeserializeObject<CreateCustomerResponseBody>(responseBodyAsText);
                Debug.WriteLine("Http-result parset uden fejl.");

            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                Debug.WriteLine("Http-result kunne parses som json. Fejl: " + e.Message);
            }
        }
    }

    #region ResponseFormatClasses
    public class CreateCustomerResponseBody : BackendApiResponseBody
    {
        public string token { get; set; }

        public Customer customer { get; set; }

        public class Customer
        {
            public string name { get; set; }
            public string email { get; set; }
            public string phoneNumber { get; set; }
        }
    }
    #endregion
}