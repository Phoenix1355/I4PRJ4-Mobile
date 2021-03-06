﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Validation;
using Prism.Common;
using Prism.Services;
using Xamarin.Forms;

namespace i4prj.SmartCab.Requests
{
    public class CreateRideRequest : ValidationBase, ICreateRideRequest
    {
        private ITimeService _timeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRideRequest"/> class.
        /// Sets default values for the request.
        /// </summary>
        public CreateRideRequest(ITimeService timeService)
        {
            _timeService = timeService;
            AmountOfPassengers = 1;
            SetDefaultAddressValues();
            SetDefaultTimeValues();
        }

        private void SetDefaultAddressValues()
        {   
            OriginCityName = string.Empty;
            OriginPostalCode = string.Empty;
            OriginStreetName = string.Empty;
            OriginStreetNumber = string.Empty;
            DestinationCityName = string.Empty;
            DestinationPostalCode = string.Empty;
            DestinationStreetName = string.Empty;
            DestinationStreetNumber = string.Empty;
        }

        private void SetDefaultTimeValues()
        {
            DepartureDate = _timeService.GetCurrentDate();
            ConfirmationDeadlineDate = _timeService.GetCurrentDate();
            DepartureTime = _timeService.GetCurrentTime().Add(new TimeSpan(0, 1, 0, 0));
            ConfirmationDeadlineTime = _timeService.GetCurrentTime().Add(new TimeSpan(0, 0, 30, 0));
            CurrentDate = _timeService.GetCurrentDate();
        }

        /*
        //SUPER HACK - er nok ikke lige den bedste måde at gøre det på.
        private async void CheckTime()
        {
            if (DepartureDate.Date == ConfirmationDeadlineDate.Date)
            {
                if (ConfirmationDeadlineTime > DepartureTime)
                {
                    ConfirmationDeadlineTime = DepartureTime;
                    IPageDialogService p = new PageDialogService(new ApplicationProvider());
                    await p.DisplayAlertAsync("Fejl", "Svartiden kan ikke være senere end afgangstiden", "Ok");
                }
            }
        }
        */

        public DateTime CurrentDate { get; private set; }

        private bool _isShared;
        [Required]
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
        [Range(1,4)]
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
        [Required]
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
        [Required]
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

        /// <summary>
        /// Returns true if this <see cref="CreateRideRequest"/> is valid. Otherwise false
         /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                return !HasErrors;
            }

        }

        /// <summary>
        /// Returns true if this <see cref="CreateRideRequest"/> is invalid. Otherwise false
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is invalid; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalid
        {
            get
            {
                return HasErrors;
            }
        }

    }
}
