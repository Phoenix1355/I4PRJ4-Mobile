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
        /// Gets the rides of the current customer.
        /// </summary>
        /// <returns>A response containing a list of responses.</returns>
        Task<CustomerRidesResponse> GetCustomerRides();

        Task<CreateRideResponse> SubmitCreateRideRequest(ICreateRideRequest request);

        Task<PriceResponse> SubmitCalculatePriceRequest(ICalculatePriceRequest request);
    }
}
