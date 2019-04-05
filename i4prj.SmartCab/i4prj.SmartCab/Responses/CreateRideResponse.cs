using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace i4prj.SmartCab.Responses
{
    public class CreateRideResponse : BackendApiResponse
    {
        public CreateRideResponseBody Body { get => (CreateRideResponseBody)_body; private set => _body = value; }

        public CreateRideResponse(HttpResponseMessage responseMessage)
            :base(responseMessage)
        {

        }

        protected override async void MakeBody()
        {
            
        }

        #region ResponseFormatClasses

        public class CreateRideResponseBody : BackendApiResponseBody
        {
            public CreateRideResponseBody()
            {
                startDestination=new Address();
                endDestination=new Address();
            }

            public int id { get; set; }
            public Address startDestination { get; set; }
            public Address endDestination { get; set; }
            public DateTime departureTime { get; set; }
            public DateTime confirmationDeadline { get; set; }
            public DateTime createdOn { get; set; }
            public double price { get; set; }


            public class Address
            {
                public string cityName { get; set; }
                public int postalCode { get; set; }
                public string streetName { get; set; }
                public int streetNumber { get; set; }
            }
        }

        #endregion
    }
}
