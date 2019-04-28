using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Services;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class RidesServiceTests
    {
        private RidesService _uut;
        private IEnumerable<IRide> _dummyList;

        [SetUp]
        public void SetUp()
        {
            // UUT
            _uut = new RidesService();

            // Dummy list for testing purposes
            _dummyList = new List<IRide>
            {
                // Open rides
                new Ride
                {
                    ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromHours(2)),
                    DepartureTime = DateTime.Now.Add(TimeSpan.FromHours(4)),
                    AmountOfPassengers = 2,
                    Status = Ride.RideStatus.LookingForMatch,
                    Destination = new Address
                    {
                        StreetName = "Bånegårdsgade",
                        CityName = "Aarhus C",
                        PostalCode = 8000,
                        StreetNumber = 1
                    },
                    Origin = new Address
                    {
                        StreetName = "Lundbyesgade",
                        CityName = "Aarhus C",
                        PostalCode = 8000,
                        StreetNumber = 8
                    }
                },
                new Ride
                {
                    ConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromHours(1)),
                    DepartureTime = DateTime.Now.Add(TimeSpan.FromHours(4)),
                    AmountOfPassengers = 2,
                    Status = Ride.RideStatus.LookingForMatch,
                    Destination = new Address
                    {
                        StreetName = "Bånegårdsgade",
                        CityName = "Aarhus C",
                        PostalCode = 8000,
                        StreetNumber = 1
                    },
                    Origin = new Address
                    {
                        StreetName = "Lundbyesgade",
                        CityName = "Aarhus C",
                        PostalCode = 8000,
                        StreetNumber = 8
                    }
                },
                // Archived rides
                new Ride
                {
                    ConfirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromHours(2)),
                    DepartureTime = DateTime.Now.Subtract(TimeSpan.FromHours(1)),
                    AmountOfPassengers = 2,
                    Status = Ride.RideStatus.Expired,
                    Destination = new Address
                    {
                        StreetName = "Bånegårdsgade",
                        CityName = "Aarhus C",
                        PostalCode = 8000,
                        StreetNumber = 1
                    },
                    Origin = new Address
                    {
                        StreetName = "Lundbyesgade",
                        CityName = "Aarhus C",
                        PostalCode = 8000,
                        StreetNumber = 8
                    }
                }
            };
        }

        #region CreateRidesFromDTO
        [Test]
        public void CreateRidesFromDTO_TwoValidDTOs_ReturnsListOfTwoRides()
        {
            // Arrange
            IEnumerable<IRideDTO> dtos = new List<IRideDTO> 
            {
                new RideDTO
                {
                    confirmationDeadline = DateTime.Now,
                    departureTime = DateTime.Now,
                    status = Ride.RideStatus.Accepted.ToString(),
                    startDestination = new AddressDTO
                    {
                        streetName = "Bånegårdsgade",
                        cityName = "Aarhus C",
                        postalCode = "8000",
                        streetNumber = "1"
                    },
                    endDestination = new AddressDTO
                    {
                        streetName = "Lundbyesgade",
                        cityName = "Aarhus C",
                        postalCode = "8000",
                        streetNumber = "8"
                    }
                },
                new RideDTO
                {
                    confirmationDeadline = DateTime.Now,
                    departureTime = DateTime.Now,
                    status = Ride.RideStatus.Accepted.ToString(),
                    startDestination = new AddressDTO
                    {
                        streetName = "Bånegårdsgade",
                        cityName = "Aarhus C",
                        postalCode = "8000",
                        streetNumber = "1"
                    },
                    endDestination = new AddressDTO
                    {
                        streetName = "Lundbyesgade",
                        cityName = "Aarhus C",
                        postalCode = "8000",
                        streetNumber = "8"
                    }
                }
            };

            // Act
            var result = _uut.CreateRidesFromDTO(dtos);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        #endregion

        #region GetArchivedRides
        [Test]
        public void GetArchivedRides_OfListWithTwoOpenAndOneArchivedRide_ReturnsListOfOneRide()
        {
            // Act
            var result = _uut.GetOpenRides(_dummyList);

            // Assert
            Assert.That(result.ToList().Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetArchivedRides_OfListWithTwoOpenAndOneArchivedRide_ReturnedListContainsOnlyArchivedRide()
        {
            // Act
            var result = _uut.GetOpenRides(_dummyList);

            // Assert
            Assert.That(result.ToList().Count(), Is.EqualTo(2));
        }
        #endregion

        #region GetOpenRides
        [Test]
        public void GetOpenRides_OfListWithTwoOpenAndOneArchivedRide_ReturnsListOfTwoRides()
        {
            // Act
            var result = _uut.GetOpenRides(_dummyList);

            // Assert
            Assert.That(result.ToList().Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetOpenRides_OfListWithTwoOpenAndOneArchivedRide_ReturnedListContainsOnlyOpenRides()
        {
            // Act
            var result = _uut.GetOpenRides(_dummyList);

            // Assert
            Assert.That(result.ToList().Count(), Is.EqualTo(2));
        }
        #endregion
    }
}