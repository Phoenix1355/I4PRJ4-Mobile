using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.CompilerServices;
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
            DepartureDate = DateTime.Now;
            DepartureTime = TimeSpan.Zero;
            ConfirmationDeadlineDate = DateTime.Today;
            ConfirmationDeadlineTime = TimeSpan.Zero;
        }

        private bool _isShared;
        public bool IsShared
        {
            get { return _isShared;}
            set
            {
                ValidateProperty(value);
                SetProperty(ref _isShared, value);
            }
        }

        [Required]
        //[RegularExpression]
        private DateTime _departureDate;
        public DateTime DepartureDate
        {
            get { return _departureDate; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _departureDate,value);
            }
        }

        private TimeSpan _departureTime;

        public TimeSpan DepartureTime
        {
            get { return _departureTime; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _departureTime, value);
            }
        }

        private DateTime _confirmationDeadlineDate;
        public DateTime ConfirmationDeadlineDate
        {
            get { return _confirmationDeadlineDate; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _confirmationDeadlineDate, value);
            }
        }

        private TimeSpan _confirmationDeadlineTime;
        public TimeSpan ConfirmationDeadlineTime
        {
            get { return _confirmationDeadlineTime; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _confirmationDeadlineTime, value);
            }
        }

        private UInt16 _amountOfPassengers;

        [Required]
        public UInt16 AmountOfPassengers
        {
            get { return _amountOfPassengers; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _amountOfPassengers, value);
            }
        }

        private Address _originAddress;
        public Address OriginAddress
        {
            get { return _originAddress;}
            set
            {
                ValidateProperty(value);
                SetProperty(ref _originAddress, value);
            }
        }
        private Address _destinationAddress;
        public Address DestinationAddress
        {
            get { return _destinationAddress; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _destinationAddress, value);
            }
        }


        public class Address
        {
            public string cityName { get; set; }
            public string postalCode { get; set; }

            public string streetName { get; set; }

            public string streetNumber { get; set; }
        }

        protected override void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            base.ValidateProperty(value, propertyName);

            RaisePropertyChanged("IsValid");
            RaisePropertyChanged("IsInvalid");
        }

        public bool IsValid
        {
            get
            {
                return !HasErrors;
            }

        }

        public bool IsInvalid
        {
            get
            {
                return HasErrors;
            }
        }

    }
}
