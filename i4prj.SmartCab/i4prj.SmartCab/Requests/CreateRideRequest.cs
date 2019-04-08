using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using DryIoc;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Validation;

namespace i4prj.SmartCab.Requests
{
    public class CreateRideRequest : ValidationBase
    {
        public CreateRideRequest()
        {
            DepartureDate = DateTime.Now;
            DepartureTime = DateTime.Now.TimeOfDay;
            AmountOfPassengers = 1;
            ConfirmationDeadlineDate = DateTime.Now;
            ConfirmationDeadlineTime = DateTime.Now.TimeOfDay;
            ConfirmationDeadlineTime = ConfirmationDeadlineTime.Add(new TimeSpan(0, 0, 5, 0));
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

        private DateTime _departureDate;
        [Required]
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
        [Required]
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
        [Required]
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
        [Required]
        public TimeSpan ConfirmationDeadlineTime
        {
            get { return _confirmationDeadlineTime; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _confirmationDeadlineTime, value);
            }
        }

        private double _amountOfPassengers;
        [Required]
        public double AmountOfPassengers
        {
            get { return _amountOfPassengers; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _amountOfPassengers, value);
            }
        }

        private string _originCityName;
        [Required]
        [RegularExpression(ValidationRules.CityNameRegex)]
        public string OriginCityName
        {
            get { return _originCityName; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _originCityName, value);
            }
        }

        private string _destinationCityName;
        [Required]
        [RegularExpression(ValidationRules.CityNameRegex)]
        public string DestinationCityName
        {
            get { return _destinationCityName; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _destinationCityName, value);
            }
        }

        private string _originPostalCode;
        [Required(ErrorMessage = ValidationMessages.PostalCodeRequired)]
        [RegularExpression(ValidationRules.PostalCodeRegex)]
        public string OriginPostalCode
        {
            get { return _originPostalCode; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _originPostalCode, value);
            }
        }

        private string _destinationPostalCode;
        [Required(ErrorMessage = ValidationMessages.PostalCodeRequired)]
        [RegularExpression(ValidationRules.PostalCodeRegex)]
        public string DestinationPostalCode
        {
            get { return _destinationPostalCode; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _destinationPostalCode, value);
            }
        }

        private string _originStreetName;
        [Required]
        [RegularExpression(ValidationRules.StreetNameRegex)]
        public string OriginStreetName
        {
            get { return _originStreetName; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _originStreetName, value);
            }
        }

        private string _destinationStreetName;
        [Required]
        [RegularExpression(ValidationRules.StreetNameRegex)]
        public string DestinationStreetName
        {
            get { return _destinationStreetName; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _destinationStreetName, value);
            }
        }

        private string _originStreetNumber;
        [RegularExpression(ValidationRules.StreetNumberRegex)]
        public string OriginStreetNumber
        {
            get { return _originStreetNumber; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _originStreetNumber, value);
            }
        }

        private string _destinationStreetNumber;
        [RegularExpression(ValidationRules.StreetNumberRegex)]
        public string DestinationStreetNumber
        {
            get { return _destinationStreetNumber; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _destinationStreetNumber, value);
            }
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
