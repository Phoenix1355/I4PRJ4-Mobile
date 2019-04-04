using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.UnitTests.Mocks;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class AzureApiServiceTests
    {
        private ICreateCustomerRequest _fakeCreateCustomerRequest;
        private ILoginRequest _fakeLoginRequest;
        private ISessionService _fakeSessionService;

        [SetUp]
        public void SetUp()
        {
            _fakeCreateCustomerRequest = Substitute.For<ICreateCustomerRequest>();
            _fakeLoginRequest = Substitute.For<ILoginRequest>();
            _fakeSessionService = Substitute.For<ISessionService>();
        }

        // The following HttpClient tests have been adapted from: http://anthonygiretti.com/2018/09/06/how-to-unit-test-a-class-that-consumes-an-httpclient-with-ihttpclientfactory-in-asp-net-core/

        // Success response
        [Test]
        public async Task SubmitCreateCustomerCommand_ReceivesValidJsonResponse_ReturnsExpectedValue()
        {
            // Arange
            // Fake Http Response
            var fakeHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                /*
                {
                    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJmcmFuay5hbmRlcnNlbkBnbWFpbC5jb20iLCJVc2VySWQiOiJiNzI1ZDc3Yy1hNmI3LTRiMmItYjA5Ni1iNzEwMDcwY2NmNzgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA0LzA0LzIwMTkgMTQ6NDk6MDkiLCJleHAiOjE1NTQzODkzNDl9.eWRgwE2orLoLV88-9xtM_sbQfdh8Sy3HtAH1ersBhkE",
                    "customer": {
                        "name": "Frank Andersen",
                        "email": "frank.andersen@gmail.com",
                        "phoneNumber": "31133165"
                    }
                }               
                */
                Content = new StringContent("{\n\t\"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJmcmFuay5hbmRlcnNlbkBnbWFpbC5jb20iLCJVc2VySWQiOiJiNzI1ZDc3Yy1hNmI3LTRiMmItYjA5Ni1iNzEwMDcwY2NmNzgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA0LzA0LzIwMTkgMTQ6NDk6MDkiLCJleHAiOjE1NTQzODkzNDl9.eWRgwE2orLoLV88-9xtM_sbQfdh8Sy3HtAH1ersBhkE\",\n\t\"customer\": {\n\t\t\"name\": \"Frank Andersen\",\n\t\t\"email\": \"frank.andersen@gmail.com\",\n\t\t\"phoneNumber\": \"31133165\"\n\t}\n}", Encoding.UTF8, "application/json")
            };


            // Fake HttpHandler
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpResponse);

            // Fake HttpClient
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            var uut = new AzureApiService(fakeHttpClient, _fakeSessionService);

            // Act
            var response = await uut.SubmitCreateCustomerRequest(_fakeCreateCustomerRequest);

            // Assert
            Assert.Multiple(() => {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<CreateCustomerResponse>());

                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<CreateCustomerResponseBody>());
            });
        }

        // Bad request response
        [Test]
        public async Task SubmitCreateCustomerCommand_ReceivesBadRequestResponse_ReturnsExpectedValue()
        {
            // Arange
            // Fake Http Response
            var fakeHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    token = (string)null,
                    customer = (object)null,
                    errors = new Dictionary<string, IList<string>>() {
                        { "error", new List<string>() { "User name is already taken" } }
                    }
                }), Encoding.UTF8, "application/json")
            };

            // Fake HttpHandler
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpResponse);

            // Fake HttpClient
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            var uut = new AzureApiService(fakeHttpClient, _fakeSessionService);

            // Act
            var response = await uut.SubmitCreateCustomerRequest(_fakeCreateCustomerRequest);

            // Assert
            Assert.Multiple(() => {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<CreateCustomerResponse>());

                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<CreateCustomerResponseBody>());

                Assert.That(response.GetErrors().Count, Is.EqualTo(1));
            });
        }
    }
}

