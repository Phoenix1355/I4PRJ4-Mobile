using System;
using System.Threading;
using i4prj.SmartCab.Models;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Models
{
    [TestFixture]
    public class RideTests
    {
        private RideDTO _rideDTO;

        [SetUp]
        public void SetUp()
        {
            _rideDTO = new RideDTO
            {
                confirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromHours(1)),
                customerId = "123-56",
                departureTime = DateTime.Now.Add(TimeSpan.FromHours(1)),
                endDestination = new AddressDTO
                {
                    cityName = "Aarhus C",
                    postalCode = "8000",
                    streetName = "Læssøesgade",
                    streetNumber = "45"
                },
                passengerCount = 2,
                price = 200.00,
                startDestination = new AddressDTO
                {
                    cityName = "Aarhus C",
                    postalCode = "8000",
                    streetName = "Læssøesgade",
                    streetNumber = "45"
                },
                status = "LookingForMatch"
            };
        }

        #region constructor
        [TestCase]
        public void Ctor_WithRideDTO_PropertiesAreSet()
        {
            // Act
            var uut = new Ride(_rideDTO);

            // Assert
            Assert.Multiple(() => {
                Assert.That(uut.ConfirmationDeadline, Is.EqualTo(_rideDTO.confirmationDeadline));
                Assert.That(uut.DepartureTime, Is.EqualTo(_rideDTO.departureTime));
                Assert.That(uut.AmountOfPassengers, Is.EqualTo(_rideDTO.passengerCount));
                Assert.That(uut.Price, Is.EqualTo(_rideDTO.price));
                Assert.That(uut.Status.ToString(), Is.EqualTo(_rideDTO.status));

                Assert.That(uut.Destination.CityName, Is.EqualTo(_rideDTO.endDestination.cityName));
                Assert.That(uut.Destination.PostalCode, Is.EqualTo(int.Parse(_rideDTO.endDestination.postalCode)));
                Assert.That(uut.Destination.StreetName, Is.EqualTo(_rideDTO.endDestination.streetName));
                Assert.That(uut.Destination.StreetNumber, Is.EqualTo(int.Parse(_rideDTO.endDestination.streetNumber)));

                Assert.That(uut.Origin.CityName, Is.EqualTo(_rideDTO.startDestination.cityName));
                Assert.That(uut.Origin.PostalCode, Is.EqualTo(int.Parse(_rideDTO.startDestination.postalCode)));
                Assert.That(uut.Origin.StreetName, Is.EqualTo(_rideDTO.startDestination.streetName));
                Assert.That(uut.Origin.StreetNumber, Is.EqualTo(int.Parse(_rideDTO.startDestination.streetNumber)));
            });
        }

        [TestCase("LookingForMatch")]
        public void Ctor_WithRideDTOLookingForMatch_StatusIsCorrect(string rideStatus)
        {
            // Arrange
            _rideDTO.status = rideStatus;

            // Act
            var uut = new Ride(_rideDTO);

            // Assert
            Assert.That(uut.Status, Is.EqualTo(Ride.RideStatus.LookingForMatch));
        }

        [TestCase("Debited")]
        public void Ctor_WithRideDTODebited_StatusIsCorrect(string rideStatus)
        {
            // Arrange
            _rideDTO.status = rideStatus;

            // Act
            var uut = new Ride(_rideDTO);

            // Assert
            Assert.That(uut.Status, Is.EqualTo(Ride.RideStatus.Debited));
        }

        [TestCase("WaitingForAccept")]
        public void Ctor_WithRideDTOWaitingForAccept_StatusIsCorrect(string rideStatus)
        {
            // Arrange
            _rideDTO.status = rideStatus;

            // Act
            var uut = new Ride(_rideDTO);

            // Assert
            Assert.That(uut.Status, Is.EqualTo(Ride.RideStatus.WaitingForAccept));
        }

        [TestCase("Accepted")]
        public void Ctor_WithRideDTOAccepted_StatusIsCorrect(string rideStatus)
        {
            // Arrange
            _rideDTO.status = rideStatus;

            // Act
            var uut = new Ride(_rideDTO);

            // Assert
            Assert.That(uut.Status, Is.EqualTo(Ride.RideStatus.Accepted));
        }

        [TestCase("Expired")]
        public void Ctor_WithRideDTOExpired_StatusIsCorrect(string rideStatus)
        {
            // Arrange
            _rideDTO.status = rideStatus;

            // Act
            var uut = new Ride(_rideDTO);

            // Assert
            Assert.That(uut.Status, Is.EqualTo(Ride.RideStatus.Expired));
        }
        #endregion


        #region TimeRemainingProperty
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TimeRemainingProperty_WhenAccessed_ReturnsCorrectTimeRemaining(int seconds)
        {
            // Arrange
            var rideDTO = new RideDTO
            {
                confirmationDeadline = DateTime.Now.Add(TimeSpan.FromHours(1)),
                customerId = "123-56",
                departureTime = DateTime.Now.Add(TimeSpan.FromHours(2)),
                endDestination = new AddressDTO
                {
                    cityName = "Aarhus C",
                    postalCode = "8000",
                    streetName = "Læssøesgade",
                    streetNumber = "45"
                },
                passengerCount = 2,
                price = 200.00,
                startDestination = new AddressDTO
                {
                    cityName = "Aarhus C",
                    postalCode = "8000",
                    streetName = "Læssøesgade",
                    streetNumber = "45"
                },
                status = "LookingForMatch"
            };

            var uut = new Ride(rideDTO);

            var lowerTimeSpan = TimeSpan.FromHours(1).Subtract(TimeSpan.FromSeconds(seconds + 1));
            var upperTimeSpan = TimeSpan.FromHours(1).Subtract(TimeSpan.FromSeconds(seconds));

            // Act
            // Wait miliseconds
            Thread.Sleep(seconds * 1000);

            // Assert
            Assert.Multiple(() => {
                Assert.That(uut.TimeRemaining, Is.GreaterThanOrEqualTo(lowerTimeSpan));
                Assert.That(uut.TimeRemaining, Is.LessThanOrEqualTo(upperTimeSpan));
            });
        }
        #endregion


        #region IsOpen
        [TestCase]
        public void IsOpen_WithFutureDeadlineAndIsLookingForMatch_ReturnsTrue()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the future
            uut.ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.LookingForMatch;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(true));
        }

        [TestCase]
        public void IsOpen_WithFutureDeadlineAndDebited_ReturnsTrue()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the future
            uut.ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.Debited;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(true));
        }

        [TestCase]
        public void IsOpen_WithFutureDeadlineAndIsWaitingForAccept_ReturnsTrue()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the future
            uut.ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.WaitingForAccept;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(true));
        }

        [TestCase]
        public void IsOpen_WithFutureDeadlineAndAccepted_ReturnsTrue()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the future
            uut.ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.Accepted;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(true));
        }

        [TestCase]
        public void IsOpen_WithPastDeadlineAndIsLookingForMatch_ReturnsFalse()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the past
            uut.ConfirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.LookingForMatch;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(false));
        }

        [TestCase]
        public void IsOpen_WithPastDeadlineAndDebited_ReturnsFalse()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the past
            uut.ConfirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.Debited;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(false));
        }

        [TestCase]
        public void IsOpen_WithPastDeadlineAndIsWaitingForAccept_ReturnsFalse()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the past
            uut.ConfirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.WaitingForAccept;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(false));
        }

        [TestCase]
        public void IsOpen_WithPastDeadlineAndAccepted_ReturnsFalse()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the past
            uut.ConfirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.Accepted;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(false));
        }

        [TestCase]
        public void IsOpen_WithFutureDeadlineAndExpired_ReturnsFalse()
        {
            // Arrange and Act
            var uut = new Ride();

            // Confirmation deadline in the future
            uut.ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromMinutes(1));

            // Not expired
            uut.Status = Ride.RideStatus.Expired;

            // Assert
            Assert.That(uut.IsOpen(), Is.EqualTo(false));
        }
        #endregion


        #region IsArchived
        // IsArchived is the opposite of IsOpen. Therefore tested through
        // IsOpen tests
        #endregion
    }
}
