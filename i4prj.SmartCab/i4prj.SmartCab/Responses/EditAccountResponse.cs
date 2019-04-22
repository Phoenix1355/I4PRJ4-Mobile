using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace i4prj.SmartCab.Responses
{
    public class EditAccountResponse : BackendApiResponse
    {
        public EditAccountResponseBody Body { get => (EditAccountResponseBody)_body; private set => _body = value; }

        public EditAccountResponse(HttpResponseMessage responseMessage)
            : base(responseMessage)
        {

        }

        protected override void MakeBody()
        {

        }

        public class EditAccountResponseBody : BackendApiResponseBody
        {

        }
    }
}
