using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Services;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Extensions;
using NUnit.Framework;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Mocks;
using Arg = DryIoc.Arg;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class GoogleMapsServiceTests
    {
        private GoogleMapsService _uut;
        private IXamarinEssentials _fakeGeocoding;
        private string _testString = "Bispehavevej 1 Aarhus V 8210";
        private Location _fromLocation = new Location(15, 15);
        private Location _toLocation = new Location(25,25);

        [SetUp]
        public void SetUp()
        {
            _fakeGeocoding = Substitute.For<IXamarinEssentials>();
            _uut = new GoogleMapsService(_fakeGeocoding);
           

        }
        
        [Test]
        public async Task GetPosition_GeocodingturnsOneLocation_ReturnsSaidLocation()
        {
            _fakeGeocoding.GetGeocodingResult(_testString)
                .Returns(new ObservableCollection<Location>() {new Location(10, 10)});

            Location result = await _uut.GetPosition(_testString);

            Assert.Multiple((() =>
            {
                Assert.AreEqual(result.Latitude,10);
                Assert.AreEqual(result.Longitude, 10);
            }));

        }

        [Test]
        public async Task GetPosition_GeocodingturnsTwoLocations_ReturnsFirstLocation()
        {
            _fakeGeocoding.GetGeocodingResult(_testString)
                .Returns(new ObservableCollection<Location>() { new Location(10, 10) ,new Location(15,15)});

            Location result = await _uut.GetPosition(_testString);

            Assert.Multiple((() =>
            {
                Assert.AreEqual(result.Latitude, 10);
                Assert.AreEqual(result.Longitude, 10);
            }));

        }

        [Test]
        public async Task GetPosition_GeocodingThrowsException_ReturnsNull()
        {
            _fakeGeocoding.GetGeocodingResult(_testString).Throws(new Exception());

            Location result = await _uut.GetPosition(_testString);

            Assert.That(result ==null);

        }


        [TestCase(0, 0, 10, 10, 5, 5)]
        [TestCase(-5, -5, 15, 15, 5, 5)]
        [TestCase(-5, 5, 10, -5, 2.5, 0)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        public void GetMiddlePosition_PassDifferentParameters_AssertOnResult(double fromLon, double fromLat,
            double toLon, double toLat, double resultLon, double resultLat)
        {
            Location a = new Location(toLon, toLat);
            Location b = new Location(fromLon, fromLat);
            Assert.That(_uut.GetMiddlePosition(a, b) == new Position(resultLon, resultLat));
        }

        
        [Test]
        public void GetMapRadius_PassTwoLocations_ResultIsAsExpected()
        {
            _fakeGeocoding.CalculateDistanceResult(_fromLocation,_toLocation,0.0).ReturnsForAnyArgs(10);

            var result = _uut.GetMapRadius(_fromLocation, _toLocation, 0.0);

            Assert.AreEqual(10.0,result);
        }
    }
}

