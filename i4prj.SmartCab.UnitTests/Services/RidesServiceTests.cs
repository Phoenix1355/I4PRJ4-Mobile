using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        private IRide _openRide1;
        private IRide _openRide2;
        private IRide _archivedRide1;

        [SetUp]
        public void SetUp()
        {
            // UUT
            _uut = new RidesService();

            // Dummy list for testing purposes
            _openRide1 = new Ride
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
            };

            _openRide2 = new Ride
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
            };

            _archivedRide1 = new Ride
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
            };

            _dummyList = new List<IRide>
            {
                _openRide1,
                _openRide2,
                _archivedRide1
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
            var result = _uut.GetArchivedRides(_dummyList);

            // Assert
            Assert.That(result.ToList().Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetArchivedRides_OfListWithTwoOpenAndOneArchivedRide_ReturnedListContainsOnlyArchivedRide()
        {
            // Act
            var result = _uut.GetArchivedRides(_dummyList);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.ToList(), Does.Contain(_archivedRide1));
                Assert.That(result.ToList(), Does.Not.Contain(_openRide1));
                Assert.That(result.ToList(), Does.Not.Contain(_openRide2));
            });
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
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.ToList(), Does.Contain(_openRide1));
                Assert.That(result.ToList(), Does.Contain(_openRide2));
                Assert.That(result.ToList(), Does.Not.Contain(_archivedRide1));
            });
        }
        #endregion
    }
}