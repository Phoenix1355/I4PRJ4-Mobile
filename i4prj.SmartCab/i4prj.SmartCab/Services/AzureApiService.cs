﻿using System;
using System.Diagnostics;
using System.Net.Http;
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
    public class AzureApiService : IBackendApiService
    {
        private const string _baseUrl = "https://smartcabbackend.azurewebsites.net/api/";
        private const string _customerRegisterEndPoint = _baseUrl + "Customer/Register";
        private const string _customerLoginEndPoint = _baseUrl + "Customer/Login";
        private const string _customerRidesEndPoint = _baseUrl + "Customer/Rides";

        public AzureApiService()
        {

        }

        #region Actions

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

            return new CreateCustomerResponse(result);
        }

        public async Task<LoginResponse> SubmitLoginRequestRequest(LoginRequest request)
        {
            var result = await PostAsync(GetEndPointUrl(request), new
            {
                email = request.Email,
                password = request.Password
            });

            return new LoginResponse(result);
        }

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

        #endregion

        #region Helpers

        private async Task<HttpResponseMessage> PostAsync(string endPointUrl, object data)
        {
            try
            {
                using (var client = GetClient())
                {
                    string json = JsonConvert.SerializeObject(data);

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
