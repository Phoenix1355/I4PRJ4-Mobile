using System;
using Prism.Mvvm;

namespace i4prj.SmartCab.Requests
{
    public class LoginRequest : BindableBase
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
    }
}
