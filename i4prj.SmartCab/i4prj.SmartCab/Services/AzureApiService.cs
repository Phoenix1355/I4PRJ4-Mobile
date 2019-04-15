using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using Microsoft.AppCenter;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace i4prj.SmartCab.Services
{
    /// <summary>
    /// Backend API service on Azure to which all system requests are sent.
    /// </summary>
    public class AzureApiService : IBackendApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionService _sessionService;

        private const string _baseUrl = "https://smartcabbackend.azurewebsites.net/api/";
        private const string _customerRegisterEndPoint = _baseUrl + "Customer/Register";
        private const string _customerLoginEndPoint = _baseUrl + "Customer/Login";
        private const string _customerRidesEndPoint = _baseUrl + "Customer/Rides";

        public AzureApiService(HttpClient httpHandler, ISessionService sessionService)
        {
            _httpClient = httpHandler;
            _sessionService = sessionService;
        }

        #region Actions

        /// <summary>
        /// Submits the create customer request.
        /// </summary>
        /// <returns>The create customer request.</returns>
        /// <param name="request">Request.</param>
        public async Task<CreateCustomerResponse> SubmitCreateCustomerRequest(ICreateCustomerRequest request)
        {
            var result = await PostAsync(GetEndPointUrl(request), new
            {
                email = request.Email,
                password = request.Password,
                passwordRepeated = request.PasswordConfirmation,
                name = request.Name,
                phoneNumber = request.Phone
            });

            return result != null ? new CreateCustomerResponse(result) : null;
        }

        /// <summary>
        /// Submits the login request.
        /// </summary>
        /// <returns>The login request.</returns>
        /// <param name="request">Request.</param>
        public async Task<LoginResponse> SubmitLoginRequest(ILoginRequest request)
        {
            var result = await PostAsync(GetEndPointUrl(request), new
            {
                email = request.Email,
                password = request.Password
            });

            return result != null ? new LoginResponse(result) : null;
        }

        /// <summary>
        /// Gets the current customers rides. NOT YET IMPLEMENTED. TODO: Implement
        /// </summary>
        /// <returns>The rides.</returns>
        /*public async Task<HttpResponseMessage> GetRides()
        {
            var result = await GetAsync(_baseUrl + _customerRidesEndPoint);

            return result;
        }*/

        #endregion


        #region EndPointUrlGetters

        private string GetEndPointUrl(ICreateCustomerRequest request)
        {
            return _customerRegisterEndPoint;
        }

        private string GetEndPointUrl(ILoginRequest request)
        {
            return _customerLoginEndPoint;
        }

        #endregion

        #region Helpers

        private async Task<HttpResponseMessage> PostAsync(string endPointUrl, object data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);

                HttpResponseMessage response = await (await GetClient()).PostAsync(endPointUrl, new StringContent(json, Encoding.UTF8, "application/json"));

                Debug.WriteLine("Backend API post request submitted to " + endPointUrl + " with " + json);

                return response;

            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Backend API post to {endPointUrl} exception with message: " + e.Message);
            }

            return null;
        }

        private async Task<HttpResponseMessage> GetAsync(string endPointUrl)
        {
            try
            {
                HttpResponseMessage response = await (await GetClient()).GetAsync(endPointUrl);

                Debug.WriteLine("Backend API get request submitted to " + endPointUrl + ". Response: " + response);

                return response;
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Backend API get to {endPointUrl} exception with message: " + e.Message);
            }

            return null;
        }

        private async Task<HttpClient> GetClient()
        {
            _httpClient.DefaultRequestHeaders.Clear();

            // Add backend authorization
            if (_sessionService.Token != null) _httpClient.DefaultRequestHeaders.Add("authorization", "Bearer " + _sessionService.Token);

            // Add custom install ID header (used for Push notifications)
            // Just keept here until permanent solution is found
            // as to keep the ID updated at the backend
            System.Guid? installId = await AppCenter.GetInstallIdAsync();
            if (installId != null) _httpClient.DefaultRequestHeaders.Add("X-Install-ID", installId.ToString());


            return _httpClient;
        }

        #endregion
    }
}
