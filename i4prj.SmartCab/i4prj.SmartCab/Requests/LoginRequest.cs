using System;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Validation;
using Prism.Mvvm;

namespace i4prj.SmartCab.Requests
{
    public class LoginRequest : ValidationBase, ILoginRequest
    {
        /// <summary>
        /// Request to be filled out and submitted to IBackendApiService to login.
        /// </summary>
        public LoginRequest()
        {
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:i4prj.SmartCab.Requests.CreateCustomerRequest"/> is valid.
        /// </summary>
        /// <value><c>true</c> if is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                return !HasErrors;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:i4prj.SmartCab.Requests.CreateCustomerRequest"/> is invalid.
        /// </summary>
        /// <value><c>true</c> if is invalid; otherwise, <c>false</c>.</value>
        public bool IsInvalid
        {
            get
            {
                return HasErrors;
            }
        }
    }
}
