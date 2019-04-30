using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Responses;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Responses
{
    [TestFixture]
    public class CustomerRidesResponseTests
    {
        /*
        Contents of _validJson:       
        {
            "rides": [{
                "customerId": "ABC",
                "departureTime": "2019-04-28T10:00:00.000Z",
                "startDestination": {
                    "cityName": "Aarhus C",
                    "postalCode": 8000,
                    "streetName": "Bånegårdspladsen",
                    "streetNumber": 1
                },
                "endDestination": {
                    "cityName": "Skanderborg",
                    "postalCode": 8660,
                    "streetName": "Sverigesvej",
                    "streetNumber": 10
                },
                "confirmationDeadline": "2019-04-28T08:15:00.000Z",
                "passengerCount": 2,
                "price": 199.95,
                "status": "LookingForMatch"
            }]
        }
        */
        private string _validJson = "{\n\t\"rides\": [{\n\t\t\"customerId\": \"ABC\",\n\t\t\"departureTime\": \"2019-04-28T10:00:00.000Z\",\n\t\t\"startDestination\": {\n\t\t\t\"cityName\": \"Aarhus C\",\n\t\t\t\"postalCode\": 8000,\n\t\t\t\"streetName\": \"Bånegårdspladsen\",\n\t\t\t\"streetNumber\": 1\n\t\t},\n\t\t\"endDestination\": {\n\t\t\t\"cityName\": \"Skanderborg\",\n\t\t\t\"postalCode\": 8660,\n\t\t\t\"streetName\": \"Sverigesvej\",\n\t\t\t\"streetNumber\": 10\n\t\t},\n\t\t\"confirmationDeadline\": \"2019-04-28T08:15:30.000Z\",\n\t\t\"passengerCount\": 2,\n\t\t\"price\": 199.95,\n\t\t\"status\": \"LookingForMatch\"\n\t}]\n}";

        /// <summary>
        /// Test to see if Body property is set when constructed by valid json
        /// </summary>
        [Test]
        public void Ctor_ValidJson_BodyIsNotNull()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(_validJson, Encoding.UTF8, "application/json")
            };

            // Act
            CustomerRidesResponse _uut = new CustomerRidesResponse(httpResponse);

            // Assert
            Assert.That(_uut.Body, Is.Not.Null);
        }

        [Test]
        public void Ctor_ValidJson_BodyHasPopulatedDTOs()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(_validJson, Encoding.UTF8, "application/json")
            };

            // Act
            CustomerRidesResponse _uut = new CustomerRidesResponse(httpResponse);

            // Assert
            Assert.Multiple(() => {
                Assert.That(_uut.Body, Is.Not.Null);

                var rides = _uut.Body.rides.ToList();

                Assert.That(rides[0].departureTime, Is.TypeOf<DateTime>());

                Assert.That(rides[0].startDestination.cityName, Is.EqualTo("Aarhus C").IgnoreCase);
                Assert.That(rides[0].startDestination.postalCode, Is.EqualTo("8000").IgnoreCase);
                Assert.That(rides[0].startDestination.streetName, Is.EqualTo("Bånegårdspladsen").IgnoreCase);
                Assert.That(rides[0].startDestination.streetNumber, Is.EqualTo("1").IgnoreCase);

                Assert.That(rides[0].endDestination.cityName, Is.EqualTo("Skanderborg").IgnoreCase);
                Assert.That(rides[0].endDestination.postalCode, Is.EqualTo("8660").IgnoreCase);
                Assert.That(rides[0].endDestination.streetName, Is.EqualTo("Sverigesvej").IgnoreCase);
                Assert.That(rides[0].endDestination.streetNumber, Is.EqualTo("10").IgnoreCase);

                Assert.That(rides[0].confirmationDeadline, Is.TypeOf<DateTime>());

                Assert.That(rides[0].passengerCount, Is.EqualTo(2));

                Assert.That(rides[0].price, Is.EqualTo(199.95));

                Assert.That(rides[0].status, Is.EqualTo("LookingForMatch").IgnoreCase);
            });
        }

        /// <summary>
        /// Test to see if Body property is null when constructed by invalid json
        /// </summary>
        [Test]
        public void Ctor_InvalidJson_BodyIsNull()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("agsdhsdh", Encoding.UTF8, "application/json")
            };

            // Act
            CustomerRidesResponse _uut = new CustomerRidesResponse(httpResponse);

            // Assert
            Assert.That(_uut.Body, Is.Null);
        }
    }
}
