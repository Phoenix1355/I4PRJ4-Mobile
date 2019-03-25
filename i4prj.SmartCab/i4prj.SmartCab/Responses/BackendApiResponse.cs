using System;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace i4prj.SmartCab.Responses
{
    public abstract class BackendApiResponse
    {
        protected BackendApiResponseBody _body;

        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public BackendApiResponse(HttpResponseMessage responseMessage)
        {
            HttpResponseMessage = responseMessage;

            MakeBody();
        }

        protected abstract void MakeBody();

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

        public bool HasErrors()
        {
            return (_body != null && _body.errors.Count != 0);
        }

        public string GetFirstError()
        {
            var errors = GetErrors();
            return errors.Count > 0 ? errors[0] : null;
        }

        public IList<string> GetErrors()
        {
            var errors = new List<string>();

            if (_body != null)
            {
                foreach (KeyValuePair<string, IList<string>> kvp in _body.errors)
                {
                    foreach (string error in kvp.Value)
                    {
                        errors.Add(error);
                    }
                }
            }

            return errors;
        }
    }
}
