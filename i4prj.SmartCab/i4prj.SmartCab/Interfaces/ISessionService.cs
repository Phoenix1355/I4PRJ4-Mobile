using System;
namespace i4prj.SmartCab.Interfaces
{
    /// <summary>
    /// Interface for session service which persists current user information on device.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Update the specified token and customer.
        /// </summary>
        /// <param name="token">Token.</param>
        /// <param name="customer">Customer.</param>
        void Update(string token, ICustomer customer);
    }
}
