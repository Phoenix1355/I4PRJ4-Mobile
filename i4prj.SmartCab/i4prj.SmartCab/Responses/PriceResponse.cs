using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace i4prj.SmartCab.Responses
{
    public class PriceResponse : BackendApiResponse
    {
        public PriceResponse(HttpResponseMessage response)
            :base(response)
        {

        }

        protected override void MakeBody()
        {
          
        }
    }
}
