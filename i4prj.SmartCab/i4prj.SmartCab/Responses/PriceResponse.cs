using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Interfaces;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    public class PriceResponse : BaseResponse
    {
        public PriceResponseBody Body
        {
            get => (PriceResponseBody) _body;
            private set => _body = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public PriceResponse(HttpResponseMessage response)
            :base(response)
        {
        }

        /// <summary>
        ///  Implementation of base class template method. Makes the body for this type of response.
        /// </summary>
        protected override async void MakeBody()
        {
            // Get json body as string
            string responseBodyAsText = await HttpResponseMessage.Content.ReadAsStringAsync();
            Debug.WriteLine("Http response body: " + responseBodyAsText);

            // Convert json string body
            try
            {
                Body = JsonConvert.DeserializeObject<PriceResponseBody>(responseBodyAsText);
                Debug.WriteLine("Http-result parset uden fejl.");
            }
            catch (JsonReaderException e)
            {
                Debug.WriteLine("Http-result kunne ikke parses som json. Fejl: " + e.Message);
            }
            catch (JsonSerializationException e)
            {
                Debug.WriteLine("Http-result kunne ikke omsættes til objekt. Fejl: " + e.Message);
            }
        }
    }
}
