using System;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Responses
{
    public class EditAccountResponse : BaseResponse
    {
        public EditAccountResponseBody Body { get => (EditAccountResponseBody)_body; private set => _body = value; }

        public EditAccountResponse(HttpResponseMessage responseMessage)
            : base(responseMessage)
        {
        }

        protected override async void MakeBody()
        {
            // Get json body as string
            string responseBodyAsText = await HttpResponseMessage.Content.ReadAsStringAsync();
            Debug.WriteLine("Http response body: " + responseBodyAsText);

            // Convert json string body
            try
            {
                Body = JsonConvert.DeserializeObject<EditAccountResponseBody>(responseBodyAsText);
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
