using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Validation;

namespace i4prj.SmartCab.Requests
{
    public class CreateRideRequest : ValidationBase
    {
        public CreateRideRequest()
        {
            OriginAddress=new Address();
            DestinationAddress=new Address();
        }

        private bool _isShared;
        public bool IsShared
        {
            get { return _isShared;}
            set { SetProperty(ref _isShared, value); }
        }

        [Required]
        //[RegularExpression]
        private DateTime _departureDate;
        public DateTime DepartureDate
        {
            get { return _departureDate; }
            set { SetProperty(ref _departureDate,value); }
        }

        private TimeSpan _departureTime;

        public TimeSpan DepartureTime
        {
            get { return _departureTime; }
            set { SetProperty(ref _departureTime, value); }
        }

        private DateTime _confirmationDeadlineDate;
        public DateTime ConfirmationDeadlineDate
        {
            get { return _confirmationDeadlineDate; }
            set { SetProperty(ref _confirmationDeadlineDate, value); }
        }

        private TimeSpan _confirmationDeadlineTime;

        public TimeSpan ConfirmationDeadlineTime
        {
            get { return _confirmationDeadlineTime; }
            set { SetProperty(ref _confirmationDeadlineTime, value); }
        }

        private string _origin;

        public string Origin
        {
            get { return _origin; }
            set
            {
                SetProperty(ref _origin, value);
            }
        }

        private string _destination;

        public string Destination
        {
            get { return _destination; }
            set
            {
                SetProperty(ref _destination, value);
            }
        }

        private UInt16 _amountOfPassengers;

        public UInt16 AmountOfPassengers
        {
            get { return _amountOfPassengers; }
            set { SetProperty(ref _amountOfPassengers, value); }
        }

        private Address _originAddress;
        public Address OriginAddress
        {
            get { return _originAddress;}
            set { SetProperty(ref _originAddress, value); }
        }
        private Address _destinationAddress;
        public Address DestinationAddress
        {
            get { return _destinationAddress; }
            set { SetProperty(ref _destinationAddress, value); }
        }

        public class Address
        {
            public string cityName { get; set; }
            public int postalCode { get; set; }
            public string streetName { get; set; }
            public int streetNumber { get; set; }
            public void CreateAddress(string address)
            {
                string[] strings = address.Split(' ');

                cityName = strings[0];
                postalCode = Int32.Parse(strings[1]);
                streetName = strings[2];
                streetNumber = Int32.Parse(strings[3]);

            }
        }
    }
}
