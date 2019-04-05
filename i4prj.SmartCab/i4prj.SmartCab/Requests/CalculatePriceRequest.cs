using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Validation;

namespace i4prj.SmartCab.Requests
{
    public class CalculatePriceRequest : ValidationBase
    {
        public CalculatePriceRequest(CreateRideRequest.Address origin, CreateRideRequest.Address destination)
        {
            Origin = origin;
            Destination = destination;
        }

        private CreateRideRequest.Address _origin;
        public CreateRideRequest.Address Origin
        {
            get { return _origin; }
            set { SetProperty(ref _origin, value); }
        }

        private CreateRideRequest.Address _destination;

        public CreateRideRequest.Address Destination
        {
            get { return _destination; }
            set { SetProperty(ref _destination, value);}
        }

    }
}
