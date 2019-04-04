using System;
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

        // Adapted from: http://anthonygiretti.com/2018/09/06/how-to-unit-test-a-class-that-consumes-an-httpclient-with-ihttpclientfactory-in-asp-net-core/
        [Test]
        public async Task SubmitCreateCustomerCommand_WithRequest_ReturnsExpectedResponseType()
        {
            // Arange
            // Fake Http Response
            var fakeHttpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    token = "Test"
                }), Encoding.UTF8, "application/json")
            };

            // Fake HttpHandler
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeHttpResponse);

            // Fake HttpClient
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            // UUT
            var uut = new AzureApiService(fakeHttpClient, _fakeSessionService);

            // Act
            var response = await uut.SubmitCreateCustomerRequest(_fakeCreateCustomerRequest);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response, Is.TypeOf<CreateCustomerResponse>());
        }
    }
}
