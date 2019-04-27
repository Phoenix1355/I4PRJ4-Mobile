using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Timers;
using i4prj.SmartCab.Interfaces;
using Prism.Mvvm;

namespace i4prj.SmartCab.Models
{
    public class Ride : BindableBase, IRide
    {
        public IAddress Origin { get; set; }
        public IAddress Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ConfirmationDeadline { get; set; }
        public int AmountOfPassengers { get; set; }
        public bool Shared { get; set; }
        public double Price { get; set; }
        public RideStatus Status { get; set; }
        public TimeSpan TimeRemaining { 
            get { 
                return ConfirmationDeadline - DateTime.Now;
            } 
        }

        public int Index { get; set; }

        public enum RideStatus
        {
            LookingForMatch,
            Debited,
            WaitingForAccept,
            Accepted,
            Expired,
            // Added in case it could not be read from backend string value
            Unknown
        }

        private Timer _timer;

        public Ride()
        {
            StartCountdown();
        }

        private void StartCountdown()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            RaisePropertyChanged(nameof(TimeRemaining));

            if (TimeRemaining.TotalSeconds <= 0) _timer.Stop();
        }

        public Ride(IApiResponseRide apiResponseRide)
        {
            Origin = new Address
            {
                CityName = apiResponseRide.startDestination.cityName,
                PostalCode = int.Parse(apiResponseRide.startDestination.postalCode),
                StreetName = apiResponseRide.startDestination.streetName,
                StreetNumber = int.Parse(apiResponseRide.startDestination.streetNumber)
            };

            Destination = new Address
            {
                CityName = apiResponseRide.endDestination.cityName,
                PostalCode = int.Parse(apiResponseRide.endDestination.postalCode),
                StreetName = apiResponseRide.endDestination.streetName,
                StreetNumber = int.Parse(apiResponseRide.endDestination.streetNumber)
            };

            DepartureTime = apiResponseRide.departureTime;

            ConfirmationDeadline = apiResponseRide.confirmationDeadline;

            AmountOfPassengers = apiResponseRide.passengerCount;

            // Shared property is not returned from backend
            //Shared = apiResponseRide.....

            Price = apiResponseRide.price;

            // Default value until tryParse performed
            Status = RideStatus.Unknown;

            RideStatus status;
            if (Enum.TryParse(apiResponseRide.status, out status))
            {
                if (Enum.IsDefined(typeof(RideStatus), status) | status.ToString().Contains(","))
                {
                    Status = status;
                } 
            }

            StartCountdown();
        }

        public bool IsOpen()
        {
            return !Status.Equals(RideStatus.Expired) && !Status.Equals(RideStatus.Unknown) && ConfirmationDeadline > DateTime.Now;
        }

        public bool IsArchived()
        {
            return !IsOpen();
        }
    }
}
