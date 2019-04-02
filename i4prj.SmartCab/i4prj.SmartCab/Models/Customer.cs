using System;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Responses;

namespace i4prj.SmartCab.Models
{
    /// <summary>
    /// Customer model.
    /// </summary>
    public class Customer : ICustomer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Models.Customer"/> class.
        /// </summary>
        public Customer()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Models.Customer"/> class.
        /// </summary>
        /// <param name="customer">Customer.</param>
        public Customer(LoginResponseBody.Customer customer)
        {
            Name = customer.name;
            Email = customer.email;
            PhoneNumber = customer.phoneNumber;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Models.Customer"/> class.
        /// </summary>
        /// <param name="customer">Customer.</param>
        public Customer(CreateCustomerResponseBody.Customer customer)
        {
            Name = customer.name;
            Email = customer.email;
            PhoneNumber = customer.phoneNumber;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}