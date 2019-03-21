using System;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;

namespace i4prj.SmartCab.Models
{
    public class BackendApiResponse
    {
        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public BackendApiResponse(HttpResponseMessage responseMessage)
        {
            HttpResponseMessage = responseMessage;
        }

        public bool WasSuccessfull()
        {
            return HttpResponseMessage.IsSuccessStatusCode;
        }

        public bool WasUnsuccessfull()
        {
            return HttpResponseMessage.StatusCode == HttpStatusCode.BadRequest;
        }

        public bool WasUnauthorized()
        {
            return HttpResponseMessage.StatusCode == HttpStatusCode.Unauthorized;
        }

        // TODO: Not hardcode this
        public bool HasErrors()
        {
            return true;
        }

        // TODO: Not hardcode this
        public string GetFirstError()
        {
            return "";
        }

        // TODO: Not hardcode this
        public IEnumerable<string> GetErrors()
        {
            return new List<string>();
        }
    }
}
