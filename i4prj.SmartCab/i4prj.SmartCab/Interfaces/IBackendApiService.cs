using System;
using System.Net.Http;
using System.Threading.Tasks;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;

namespace i4prj.SmartCab.Interfaces
{
    public interface IBackendApiService
    {
        Task<CreateCustomerResponse> SubmitCreateCustomerRequest(CreateCustomerRequest request);
        Task<LoginResponse> SubmitLoginRequestRequest(LoginRequest request);
        Task<HttpResponseMessage> GetRides();
    }
}
