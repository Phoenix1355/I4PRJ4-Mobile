using System;
using System.Timers;
using i4prj.SmartCab.Interfaces;
using Prism.Mvvm;

namespace i4prj.SmartCab.Models
{
    public class Ride : BindableBase, IRide
    {
        #region RideStatus
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
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the starting address
        /// </summary>
        /// <value>The starting address.</value>
        public IAddress Origin { get; set; }

        /// <summary>
        /// Gets or sets destination address
        /// </summary>
        /// <value>The destination address.</value>
        public IAddress Destination { get; set; }

        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        /// <value>The departure time.</value>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the confirmation deadline.
        /// </summary>
        /// <value>The confirmation deadline.</value>
        public DateTime ConfirmationDeadline { get; set; }

        /// <summary>
        /// Gets or sets the amount of passengers.
        /// </summary>
        /// <value>The amount of passengers.</value>
        public int AmountOfPassengers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:i4prj.SmartCab.Models.Ride"/> is shared.
        /// </summary>
        /// <value><c>true</c> if shared; otherwise, <c>false</c>.</value>
        public bool Shared { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public RideStatus Status { get; set; }

        /// <summary>
        /// Gets the time remaining until expiration.
        /// </summary>
        /// <value>The time remaining.</value>
        public TimeSpan TimeRemaining { 
            get { 
                return ConfirmationDeadline - DateTime.Now;
            } 
        }

        /// <summary>
        /// Gets or sets the index of a ride when in a collection.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }
        #endregion

        #region Fields
        /// <summary>
        /// The timer.
        /// </summary>
        private Timer _timer;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor. 
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Models.Ride"/> class.
        /// </summary>
        public Ride()
        {
            StartCountdown();
        }

        /// <summary>
        /// Constructor with mapping from <see cref="T:i4prj.SmartCab.Interfaces.IApiResponseRide"/>.
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Models.Ride"/> class.
        /// </summary>
        /// <param name="apiResponseRide">API response ride.</param>
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

            // Default status until tryParse performed
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

        #endregion

        #region Countdown
        /// <summary>
        /// Starts the countdown.
        /// </summary>
        private void StartCountdown()
        {
            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += OnTimedEvent;
            _timer.Enabled = true;
        }

        /// <summary>
        /// The event handler for countdown intervals.
        /// Notifies of change in TimeRemaining property
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            RaisePropertyChanged(nameof(TimeRemaining));

            if (TimeRemaining.TotalSeconds <= 0)
            {
                Status = RideStatus.Expired;

                _timer.Stop();
            }
        }
        #region

        #region Methods
        /// <summary>
        /// Whether the ride is open or not
        /// </summary>
        /// <returns><c>true</c>, if open, <c>false</c> otherwise.</returns>
        public bool IsOpen()
        {
            return !Status.Equals(RideStatus.Expired) && !Status.Equals(RideStatus.Unknown) && ConfirmationDeadline > DateTime.Now;
        }

        /// <summary>
        /// Whether the ride is archived or not
        /// </summary>
        /// <returns><c>true</c>, if archived, <c>false</c> otherwise.</returns>
        public bool IsArchived()
        {
            return !IsOpen();
        }
        #region
    }
}
