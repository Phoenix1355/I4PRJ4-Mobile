using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace i4prj.SmartCab.Services
{
    /// <summary>
    /// Backend API service on Azure to which all system requests are sent.
    /// </summary>
    public class AzureApiService : IBackendApiService
    {
        private const string _baseUrl = "https://smartcabbackend.azurewebsites.net/api/";
        private const string _customerRegisterEndPoint = _baseUrl + "Customer/Register";
        private const string _customerLoginEndPoint = _baseUrl + "Customer/Login";
        private const string _customerRidesEndPoint = _baseUrl + "Customer/Rides";
        private const string _createRideEndPoint = _baseUrl + "Rides/Create";
        private const string _calculatePriceEndPoint = _baseUrl + "Price";

        public AzureApiService()
        {

        }

        #region Actions

        /// <summary>
        /// Submits the create customer request.
        /// </summary>
        /// <returns>The create customer request.</returns>
        /// <param name="request">Request.</param>
        public async Task<CreateCustomerResponse> SubmitCreateCustomerRequest(CreateCustomerRequest request)
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
        /// Submits the create ride request.
        /// </summary>
        /// <param name="request">A CreateRideRequest submitted by the user</param>
        /// <returns>The CreateRideResponse</returns>
        public async Task<CreateRideResponse> SubmitCreateRideRequest(CreateRideRequest request)
        {
          
            var result = await PostAsync(GetEndPointUrl(request), new
            {
                isShared=request.IsShared,
                departureTime = request.DepartureDate+request.DepartureTime,
                confirmationDeadline = request.ConfirmationDeadlineDate+request.ConfirmationDeadlineTime,
                passengerCount = request.AmountOfPassengers,
                startDestination = request.OriginAddress,
                endDestination=request.DestinationAddress
            });

            return result != null ? new CreateRideResponse(result) : null;
        }

        /// <summary>
        /// Submits the calculate price request.
        /// </summary>
        /// <param name="request">A CalculatePriceRequest constisting of two addresses.</param>
        /// <returns>The price calculated for the specific ride</returns>
        public async Task<PriceResponse> SubmitCalculatePriceRequest(CalculatePriceRequest request)
        {
            var result = await PostAsync(GetEndPointUrl(request), new
            {
                startAddress=request.Origin,
                endAddress=request.Destination,
            });

            return result != null ? new PriceResponse(result) : null;
        }

        /// <summary>
        /// Submits the login request.
        /// </summary>
        /// <returns>The login request.</returns>
        /// <param name="request">Request.</param>
        public async Task<LoginResponse> SubmitLoginRequestRequest(LoginRequest request)
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
        public async Task<HttpResponseMessage> GetRides()
        {
            var result = await GetAsync(_baseUrl + _customerRidesEndPoint);

            return result;
        }

        

        #endregion


        #region EndPointUrlGetters

        private string GetEndPointUrl(CreateCustomerRequest request)
        {
            return _customerRegisterEndPoint;
        }

        private string GetEndPointUrl(LoginRequest request)
        {
            return _customerLoginEndPoint;
        }

        private string GetEndPointUrl(CreateRideRequest request)
        {
            return _createRideEndPoint;
        }

        private string GetEndPointUrl(CalculatePriceRequest request)
        {
            return _calculatePriceEndPoint;
        }

        #endregion

        #region Helpers

        private async Task<HttpResponseMessage> PostAsync(string endPointUrl, object data)
        {
            try
            {
                using (var client = GetClient())
                {
                    string json = JsonConvert.SerializeObject(data);
                    Debug.WriteLine(json);
                    HttpResponseMessage response = await client.PostAsync(endPointUrl, new StringContent(json, Encoding.UTF8, "application/json"));

                    Debug.WriteLine("Backend API post request submitted to " + endPointUrl + " with " + json);

                    return response;
                }
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
                using (var client = GetClient())
                {
                    HttpResponseMessage response = await client.GetAsync(endPointUrl);

                    Debug.WriteLine("Backend API get request submitted to " + endPointUrl + ". Response: " + response);

                    return response;
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Backend API get to {endPointUrl} exception with message: " + e.Message);
            }

            return null;
        }

        private HttpClient GetClient()
        {
            var client = new HttpClient();
            if (LocalSessionService.Instance.Token != null) client.DefaultRequestHeaders.Add("authorization", "Bearer " + LocalSessionService.Instance.Token);
            return client;
        }

        #endregion
    }
}
