using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace i4prj.SmartCab.UnitTests.Mocks
{
    // From: http://anthonygiretti.com/2018/09/06/how-to-unit-test-a-class-that-consumes-an-httpclient-with-ihttpclientfactory-in-asp-net-core/
    public class FakeHttpMessageHandler : DelegatingHandler
    {
        public HttpResponseMessage FakeResponse { get; set; }

        public FakeHttpMessageHandler(HttpResponseMessage responseMessage = null)
        {
            if (responseMessage != null) FakeResponse = responseMessage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(FakeResponse);
        }
    }
}
