using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Validation;

namespace i4prj.SmartCab.Requests
{
    public class CalculatePriceRequest : ValidationBase, ICalculatePriceRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatePriceRequest"/> class
        /// Sets values for the request
        /// </summary>
        /// <param name="request">The <see cref="CreateRideRequest"/> used to instantiate the CalculatePriceRequest</param>
        public CalculatePriceRequest(ICreateRideRequest request)
        {
            OriginCityName = request.OriginCityName;
            OriginPostalCode = request.OriginPostalCode;
            OriginStreetName = request.OriginStreetName;
            OriginStreetNumber = request.OriginStreetNumber;

            DestinationCityName = request.DestinationCityName;
            DestinationPostalCode = request.DestinationPostalCode;
            DestinationStreetName = request.DestinationStreetName;
            DestinationStreetNumber = request.DestinationStreetNumber;
        }

        private string _originCityName;

        public string OriginCityName
        {
            get { return _originCityName; }
            set
            {
                SetProperty(ref _originCityName, value);
            }
        }

        private string _destinationCityName;

        public string DestinationCityName
        {
            get { return _destinationCityName; }
            set
            {
                SetProperty(ref _destinationCityName, value);
            }
        }

        private string _originPostalCode;

        public string OriginPostalCode
        {
            get { return _originPostalCode; }
            set
            {
                SetProperty(ref _originPostalCode, value);
            }
        }

        private string _destinationPostalCode;

        public string DestinationPostalCode
        {
            get { return _destinationPostalCode; }
            set
            {
                SetProperty(ref _destinationPostalCode, value);
            }
        }

        private string _originStreetName;

        public string OriginStreetName
        {
            get { return _originStreetName; }
            set
            {
                SetProperty(ref _originStreetName, value);
            }
        }

        private string _destinationStreetName;

        public string DestinationStreetName
        {
            get { return _destinationStreetName; }
            set
            {
                SetProperty(ref _destinationStreetName, value);
            }
        }

        private string _originStreetNumber;

        public string OriginStreetNumber
        {
            get { return _originStreetNumber; }
            set
            {
                SetProperty(ref _originStreetNumber, value);
            }
        }

        private string _destinationStreetNumber;

        public string DestinationStreetNumber
        {
            get { return _destinationStreetNumber; }
            set
            {
                SetProperty(ref _destinationStreetNumber, value);
            }
        }

    }
}
