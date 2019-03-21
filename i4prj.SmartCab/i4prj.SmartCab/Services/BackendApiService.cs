using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using Newtonsoft.Json;

namespace i4prj.SmartCab.Services
{
    // TIL JWT: httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
    public class BackendApiService
    {
        private const string _baseUrl = "https://smartcabbackend.azurewebsites.net/api/";
        private const string _customerRegisterEndPoint = "Customer/Register";

        public BackendApiService()
        {

        }

        #region Actions

        public async Task<BackendApiResponse> SubmitCreateCustomerRequest(CreateCustomerRequest request)
        {
            return await PostAsync(GetEndPointUrl(request), new
            {
                email = request.Email,
                password = request.Password,
                passwordRepeated = request.PasswordConfirmation,
                name = request.Name,
                phoneNumber = request.Phone
            });

        }

        #endregion

        #region Helpers

        private async Task<BackendApiResponse> PostAsync(string endPointUrl, object data)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(data);

                    HttpResponseMessage response = await client.PostAsync(endPointUrl, new StringContent(json, Encoding.UTF8, "application/json"));

                    Debug.WriteLine("Backend API post request submitted to " + endPointUrl + " with " + json);

                    return new BackendApiResponse(response);
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine($"Backend API post to {endPointUrl} exception with message: " + e.Message);
            }

            return null;
        }

        #endregion

        #region EndPointUrlGetters

        private string GetEndPointUrl(CreateCustomerRequest request)
        {
            return _baseUrl + _customerRegisterEndPoint;
        }

        #endregion
    }
}
