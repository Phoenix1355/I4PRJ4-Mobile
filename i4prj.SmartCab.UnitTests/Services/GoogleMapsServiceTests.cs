using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Services;
using NUnit.Framework;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class GoogleMapsServiceTests
    {
        private GoogleMapsService _uut;


        [SetUp]
        public void SetUp()
        {
            _uut = new GoogleMapsService();
        }

        //Not testable. Geocoding doesnt work in test environment.
        /*
        [TestCase("Bispehavevej 1 Aarhus V 8210")]
        public async Task GetPosition_InputValidAddresses_LocationProvided(string address)
        {
            Location result = await _uut.GetPosition(address);

            Assert.That(result != null);

        }
        */

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

        //Doesnt make sense to test. The only thing it does is call an API (xamarin.essentieals.Location), and this code is already tested.
        /*
        [TestCase(0, 0, 1, 1,0,15)]
        public void GetMapRadius_PassDifferentParamters_AssertOnResults(double fromLon, double fromLat,
            double toLon, double toLat,double margin, double result)
        {
            Location a = new Location(toLon, toLat);
            Location b = new Location(fromLon, fromLat);
            Assert.AreEqual(_uut.GetMapRadius(a,b,margin),result);
        }
        */
    }
}

