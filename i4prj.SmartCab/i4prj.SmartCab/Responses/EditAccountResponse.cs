using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    public class EditAccountResponse : BackendApiResponse
    {
        public EditAccountResponseBody Body { get => (EditAccountResponseBody)_body; private set => _body = value; }


        public EditAccountResponse(HttpResponseMessage responseMessage)
            : base(responseMessage)
        {

        }

        protected override async void MakeBody()
        {
            string responseBodyAsText = await HttpResponseMessage.Content.ReadAsStringAsync();
            try
            {
                Body = JsonConvert.DeserializeObject<EditAccountResponseBody>(responseBodyAsText);
                Debug.Write(responseBodyAsText);
            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                Debug.WriteLine("Http-result kunne parses som json. Fejl: " + e.Message);
            }
        }

        public class EditAccountResponseBody : BackendApiResponseBody
        {
            
            public Customer customer { get; set; }

            public class Customer : IApiResponseCustomer
            {
                public string name { get; set; }
                public string phoneNumber { get; set; }
                public string email { get; set; }
            }
           

        }
    }
}
