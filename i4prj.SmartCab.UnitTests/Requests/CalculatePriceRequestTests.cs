using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Services;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Requests
{
    public class CalculatePriceRequestTests
    {
        private CalculatePriceRequest _uut;

        private ICreateRideRequest _rideRequest;
        private ITimeService _timeService;

        [SetUp]
        public void SetUp()
        {
            _timeService=new TimeService();
            _rideRequest = new CreateRideRequest(_timeService);
        }

        #region Ctor

        [Test]
        public void Ctor_CreateCalculateRideRequest_AddressSpecificationsAreEqual()
        {
            _uut = new CalculatePriceRequest(_rideRequest);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(_uut.DestinationCityName, _rideRequest.DestinationCityName);
                Assert.AreEqual(_uut.DestinationPostalCode, _rideRequest.DestinationPostalCode);
                Assert.AreEqual(_uut.DestinationStreetName, _rideRequest.DestinationStreetName);
                Assert.AreEqual(_uut.DestinationStreetNumber, _rideRequest.DestinationStreetNumber);
                Assert.AreEqual(_uut.OriginCityName, _rideRequest.OriginCityName);
                Assert.AreEqual(_uut.OriginPostalCode, _rideRequest.OriginPostalCode);
                Assert.AreEqual(_uut.OriginStreetName, _rideRequest.OriginStreetName);
                Assert.AreEqual(_uut.OriginStreetNumber, _rideRequest.OriginStreetNumber);
            });
        }
        #endregion
    }
}
