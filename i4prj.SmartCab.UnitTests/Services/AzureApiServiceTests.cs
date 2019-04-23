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
        private ICreateRideRequest _fakeCreateRideRequest;
        private ICalculatePriceRequest _fakeCalculatePriceRequest;
        private ILoginRequest _fakeLoginRequest;
        private ISessionService _fakeSessionService;
        private FakeHttpMessageHandler _fakeHttpMessageHandler;
        private HttpClient _fakeHttpClient;

        private AzureApiService _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeCreateCustomerRequest = Substitute.For<ICreateCustomerRequest>();
            _fakeLoginRequest = Substitute.For<ILoginRequest>();
            _fakeSessionService = Substitute.For<ISessionService>();

            _fakeHttpMessageHandler = new FakeHttpMessageHandler();
            _fakeHttpClient = new HttpClient(_fakeHttpMessageHandler);

            _uut = new AzureApiService(_fakeHttpClient, _fakeSessionService);
            _fakeCalculatePriceRequest = Substitute.For<ICalculatePriceRequest>();
            _fakeCreateRideRequest = Substitute.For<ICreateRideRequest>();
        }

        // The following HttpClient tests have been adapted from: http://anthonygiretti.com/2018/09/06/how-to-unit-test-a-class-that-consumes-an-httpclient-with-ihttpclientfactory-in-asp-net-core/

        /// <summary>
        /// Test to see whether a successfull response with valid json will be transformed correctly
        /// </summary>
        [Test]
        public async Task SubmitCreateCustomerRequest_ReceivesValidJsonResponse_ReturnsExpectedValue()
        {
            // Arrange: Fake Http Response
            _fakeHttpMessageHandler.FakeResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n\t\"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJmcmFuay5hbmRlcnNlbkBnbWFpbC5jb20iLCJVc2VySWQiOiJiNzI1ZDc3Yy1hNmI3LTRiMmItYjA5Ni1iNzEwMDcwY2NmNzgiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA0LzA0LzIwMTkgMTQ6NDk6MDkiLCJleHAiOjE1NTQzODkzNDl9.eWRgwE2orLoLV88-9xtM_sbQfdh8Sy3HtAH1ersBhkE\",\n\t\"customer\": {\n\t\t\"name\": \"Frank Andersen\",\n\t\t\"email\": \"frank.andersen@gmail.com\",\n\t\t\"phoneNumber\": \"31133165\"\n\t}\n}", Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _uut.SubmitCreateCustomerRequest(_fakeCreateCustomerRequest);

            // Assert
            Assert.Multiple(() => {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<CreateCustomerResponse>());

                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<CreateCustomerResponseBody>());
            });
        }

        /// <summary>
        /// Test to see whether an unsuccessfull response with valid json will be transformed correctly
        /// </summary>
        [Test]
        public async Task SubmitCreateCustomerRequest_ReceivesBadRequestResponse_ReturnsExpectedValue()
        {
            // Arrange: Fake Http Response
            _fakeHttpMessageHandler.FakeResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("{\n\t\"token\": null,\n\t\"customer\": \"\",\n\t\"errors\": {\n\t\t\"error\": [\"User name is already taken\"]\n\t}\n}")
            };

            // Act
            var response = await _uut.SubmitCreateCustomerRequest(_fakeCreateCustomerRequest);

            // Assert
            Assert.Multiple(() => {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<CreateCustomerResponse>());

                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<CreateCustomerResponseBody>());

                Assert.That(response.GetErrors().Count, Is.EqualTo(1));
            });
        }

        /// <summary>
        /// Test to see whether a successfull response with valid json will be transformed correctly
        /// </summary>
        [Test]
        public async Task SubmitLoginRequest_ReceivesValidJsonResponse_ReturnsExpectedValue()
        {
            // Arrange: Fake Http Response
            _fakeHttpMessageHandler.FakeResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"tokenValue\",\n  \"customer\": {\n    \"name\": \"customer name\",\n    \"email\": \"some@email.com\",\n    \"phoneNumber\": \"12345678\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _uut.SubmitLoginRequest(_fakeLoginRequest);

            // Assert
            Assert.Multiple(() => {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<LoginResponse>());

                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<LoginResponseBody>());
            });
        }

        /// <summary>
        /// Test to see whether an unsuccessfull response with valid json will be transformed correctly
        /// </summary>
        [Test]
        public async Task SubmitLoginRequest_ReceivesBadRequestResponse_ReturnsExpectedValue()
        {
            // Arrange: Fake Http Response
            _fakeHttpMessageHandler.FakeResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("{\n\t\"token\": null,\n\t\"customer\": null,\n\t\"errors\": {\n\t\t\"error\": [\"Bad credentials\"]\n\t}\n}", Encoding.UTF8, "application/json")
            };

            // Act
            var response = await _uut.SubmitCreateCustomerRequest(_fakeCreateCustomerRequest);

            // Assert
            Assert.Multiple(() => {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<CreateCustomerResponse>());

                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<CreateCustomerResponseBody>());

                Assert.That(response.GetErrors().Count, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task SubmitCreateRideRequest_ReceivesSuccessfullResponse_ReturnsNewCreateRideResponse()
        {
            var fakeHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    id = 1,
                    startDestination = new { cityName = "Test", postalCode = 1234, streetName = "Tester", streetNumber = 1 },
                    endDestination = new { cityName = "Tester", postalCode = 4321, streetName = "Test", streetNumber = 2 },
                    departureTime = DateTime.Now.Add(new TimeSpan(0, 0, 30)),
                    confirmationDeadline = DateTime.Now.Subtract(new TimeSpan(0, 2, 0)),
                    passengerCount = 1,
                    createdOn = DateTime.Now,
                    price = 100,
                    status = 0,
                }),Encoding.UTF8,"application/json"),
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpResponse);
            var fakeHttpClient=new HttpClient(fakeHttpMessageHandler);
            var uut = new AzureApiService(fakeHttpClient,_fakeSessionService);

            var response = await uut.SubmitCreateRideRequest(_fakeCreateRideRequest);

            Assert.Multiple(() =>
            {
                Assert.That(response,Is.Not.Null);
                Assert.That(response,Is.TypeOf<CreateRideResponse>());
                Assert.That(response.Body,Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<CreateRideResponse.CreateRideResponseBody>());
            });
        }

        [Test]
        public async Task SubmitCreateRideRequest_ReceivesBadRequestResponse_ReturnsError()
        {
            var fakeHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    errors = new Dictionary<string, IList<string>>() {
                        { "error", new List<string>() { "Address not valid" } }
                    },

                }), Encoding.UTF8, "application/json"),
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpResponse);
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            var uut = new AzureApiService(fakeHttpClient, _fakeSessionService);

            var response = await uut.SubmitCreateRideRequest(_fakeCreateRideRequest);

            Assert.Multiple(() =>
            {
                Assert.That(response,Is.Not.Null);
                Assert.That(response,Is.TypeOf<CreateRideResponse>());
                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<CreateRideResponse.CreateRideResponseBody>());
                Assert.That(response.GetErrors().Count,Is.EqualTo(1));
            });
        }

        [Test]
        public async Task SubmitCalculatePriceRequest_RecievesSuccessfullResponse_ReturnsNewPriceResponse()
        {
            var fakeHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    price = 100.00,
                }), Encoding.UTF8, "application/json"),
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpResponse);
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            var uut = new AzureApiService(fakeHttpClient, _fakeSessionService);

            var response = await uut.SubmitCalculatePriceRequest(_fakeCalculatePriceRequest);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<PriceResponse>());
                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<PriceResponse.PriceResponseBody>());
            });
        }

        [Test]
        public async Task SubmitCalculatePriceRequest_ReceivesBadRequestResponse_ReturnsError()
        {
            var fakeHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    errors = new Dictionary<string, IList<string>>() {
                        { "error", new List<string>() { "Address not valid" } }
                    },

                }), Encoding.UTF8, "application/json"),
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpResponse);
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            var uut = new AzureApiService(fakeHttpClient, _fakeSessionService);

            var response = await uut.SubmitCalculatePriceRequest(_fakeCalculatePriceRequest);

            Assert.Multiple(() =>
            {
                Assert.That(response, Is.Not.Null);
                Assert.That(response, Is.TypeOf<PriceResponse>());
                Assert.That(response.Body, Is.Not.Null);
                Assert.That(response.Body, Is.TypeOf<PriceResponse.PriceResponseBody>());
                Assert.That(response.GetErrors().Count, Is.EqualTo(1));
            });
        }
    }
}

