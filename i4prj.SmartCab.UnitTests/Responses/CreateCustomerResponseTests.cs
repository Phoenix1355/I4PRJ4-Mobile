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
    public class CreateCustomerResponseTests
    {
        /// <summary>
        /// Test to see if Body property is set when constructed by valid json
        /// </summary>
        [Test]
        public void Ctor_ValidJsonMatching_BodyIsNotNull()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            CreateCustomerResponse _uut = new CreateCustomerResponse(httpResponse);

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
            CreateCustomerResponse _uut = new CreateCustomerResponse(httpResponse);

            // Assert
            Assert.That(_uut.Body, Is.Null);
        }
    }
}
