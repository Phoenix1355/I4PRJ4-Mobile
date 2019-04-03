using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;

namespace i4prj.SmartCab.Adapters
{
    public class HttpClientAdapter : IHttpHandler
    {
        private readonly HttpClient _client;

        public HttpClientAdapter(HttpClient client)
        {
            _client = client;
        }

        public HttpRequestHeaders DefaultRequestHeaders => _client.DefaultRequestHeaders;

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return _client.GetAsync(requestUri);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return _client.PostAsync(requestUri, content);
        }
    }
}
