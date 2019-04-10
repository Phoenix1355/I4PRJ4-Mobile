using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
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

        [Required(ErrorMessage = ValidationMessages.EmailRequired)]
        [RegularExpression(ValidationRules.EmailRegex, ErrorMessage = ValidationMessages.EmailRequired)]
        public string Email
        {
            get { return _email; }
            set {
                ValidateProperty(value);
                SetProperty(ref _email, value); 
            }
        }

        private string _password;

        // No validation on Password as it would seem as if we were validating the correctness of the entered password
        public string Password
        {
            get { return _password; }
            set {
                ValidateProperty(value);
                SetProperty(ref _password, value); 
            }
        }

        /// <summary>
        /// Validates the property.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property name.</param>
        protected override void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            base.ValidateProperty(value, propertyName);

            RaisePropertyChanged("IsValid");
            RaisePropertyChanged("IsInvalid");
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:i4prj.SmartCab.Requests.LoginRequest"/> is valid.
        /// </summary>
        /// <value><c>true</c> if is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get
            {
                return !HasErrors && IsDirty(nameof(Email)) && IsDirty(nameof(Password));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:i4prj.SmartCab.Requests.LoginRequest"/> is invalid.
        /// </summary>
        /// <value><c>true</c> if is invalid; otherwise, <c>false</c>.</value>
        public bool IsInvalid
        {
            get
            {
                return !IsValid;
            }
        }
    }
}
