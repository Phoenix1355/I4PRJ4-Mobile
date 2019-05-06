using System;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;

namespace i4prj.SmartCab.Responses
{
    /// <summary>
    /// Base class from which all http responses from IBackendApiService is derived. 
    /// </summary>
    public abstract class BaseResponse
    {
        protected BaseResponseBody _body;

        public HttpResponseMessage HttpResponseMessage { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Responses.BackendApiResponse"/> class.
        /// </summary>
        /// <param name="responseMessage">Response message.</param>
        public BaseResponse(HttpResponseMessage responseMessage)
        {
            HttpResponseMessage = responseMessage;

            MakeBody();
        }

        /// <summary>
        /// Template method for derived classes to implement.
        /// </summary>
        protected abstract void MakeBody();

        /// <summary>
        /// Indicates whether this response was successfull.
        /// </summary>
        /// <returns><c>true</c>, if successfull, <c>false</c> otherwise.</returns>
        public bool WasSuccessfull()
        {
            return HttpResponseMessage.IsSuccessStatusCode;
        }

        /// <summary>
        /// Indicates whether this response was unsuccessfull.
        /// </summary>
        /// <returns><c>true</c>, if unsuccessfull, <c>false</c> otherwise.</returns>
        public bool WasUnsuccessfull()
        {
            return HttpResponseMessage.StatusCode == HttpStatusCode.BadRequest || HttpResponseMessage.StatusCode==HttpStatusCode.InternalServerError;
        }

        /// <summary>
        /// Indicates whether this response was unauthorized.
        /// </summary>
        /// <returns><c>true</c>, if unauthorized, <c>false</c> otherwise.</returns>
        public bool WasUnauthorized()
        {
            return HttpResponseMessage.StatusCode == HttpStatusCode.Unauthorized;
        }

        /// <summary>
        /// Indicates whether this response has errors.
        /// </summary>
        /// <returns><c>true</c>, if errors are present, <c>false</c> otherwise.</returns>
        public bool HasErrors()
        {
            return (_body != null && _body.errors != null && _body.errors.Count != 0);
        }

        /// <summary>
        /// Gets the first error.
        /// </summary>
        /// <returns>The first error.</returns>
        public string GetFirstError()
        {
            var errors = GetErrors();
            return errors.Count > 0 ? errors[0] : null;
        }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <returns>The errors.</returns>
        public IList<string> GetErrors()
        {
            var errors = new List<string>();

            if (_body != null && _body.errors != null)
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
