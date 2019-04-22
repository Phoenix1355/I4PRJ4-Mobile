using System;
using System.Net.Http;
using System.Threading.Tasks;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;

namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// API-interface containing available requests to backend service.
    /// </summary>
    public interface IBackendApiService
    {
        /// <summary>
        /// Submits the create customer request.
        /// </summary>
        /// <returns>The create customer response.</returns>
        /// <param name="request">Request.</param>
        Task<CreateCustomerResponse> SubmitCreateCustomerRequest(ICreateCustomerRequest request);

        /// <summary>
        /// Submits the login request.
        /// </summary>
        /// <returns>The login response.</returns>
        /// <param name="request">Request.</param>
        Task<LoginResponse> SubmitLoginRequest(ILoginRequest request);

        /// <summary>
        /// Submits a request to fetch rides. NOT YET IMPLEMENTED CORRECTLY! TODO: Implement
        /// </summary>
        /// <returns>A list of rides.</returns>
        //Task<HttpResponseMessage> GetRides();

        Task<CreateRideResponse> SubmitCreateRideRequest(ICreateRideRequest request);

        Task<PriceResponse> SubmitCalculatePriceRequest(ICalculatePriceRequest request);

        Task<EditAccountResponse> SubmitEditAccountRequest(IEditAccountRequest request);
    }
}
