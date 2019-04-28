using System;
using System.Net;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Responses;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Responses
{
    [TestFixture]
    public class CustomerRidesResponseTests
    {
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
                Content = new StringContent("{\n\t\"rides\": [{\n\t\t\"customerId\": \"string\",\n\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\"startDestination\": {\n\t\t\t\"cityName\": \"string\",\n\t\t\t\"postalCode\": 0,\n\t\t\t\"streetName\": \"string\",\n\t\t\t\"streetNumber\": 0\n\t\t},\n\t\t\"endDestination\": {\n\t\t\t\"cityName\": \"string\",\n\t\t\t\"postalCode\": 0,\n\t\t\t\"streetName\": \"string\",\n\t\t\t\"streetNumber\": 0\n\t\t},\n\t\t\"confirmationDeadline\": \"2019-04-28T10:19:36.259Z\",\n\t\t\"passengerCount\": 0,\n\t\t\"price\": 0,\n\t\t\"status\": \"LookingForMatch\"\n\t}]\n}", Encoding.UTF8, "application/json")
            };

            // Act
            CustomerRidesResponse _uut = new CustomerRidesResponse(httpResponse);

            // Assert
            Assert.That(_uut.Body, Is.Not.Null);
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
