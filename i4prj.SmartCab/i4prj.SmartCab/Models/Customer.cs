using System;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Responses;

namespace i4prj.SmartCab.Models
{
    public class Customer : ICustomer
    {
        public Customer(LoginResponseBody.Customer customer)
        {
            Name = customer.name;
            Email = customer.email;
            PhoneNumber = customer.phoneNumber;
        }

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