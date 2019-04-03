using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Validation;
using Prism.Mvvm;

namespace i4prj.SmartCab.Requests
{
    /// <summary>
    /// Request to be filled out and submitted to IBackendApiService to create a new Customer.
    /// </summary>
    public class CreateCustomerRequest : ValidationBase, ICreateCustomerRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:i4prj.SmartCab.Requests.CreateCustomerRequest"/> class.
        /// </summary>
        public CreateCustomerRequest()
        {
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Password = string.Empty;
            PasswordConfirmation = string.Empty;
        }

        private string _name;

        [Required(ErrorMessage = ValidationMessages.NameRequired)]
        [StringLength(255, MinimumLength = 3, ErrorMessage = Validation.ValidationMessages.NameLength)]
        public string Name
        {
            get { return _name; }
            set {
                ValidateProperty(value);
                SetProperty(ref _name, value); 
            }
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

        private string _phone;

        [Required(ErrorMessage = ValidationMessages.PhoneRequired)]
        [RegularExpression(ValidationRules.PhoneRegex, ErrorMessage = ValidationMessages.PhoneRegex)]
        public string Phone
        {
            get { return _phone; }
            set {
                ValidateProperty(value);
                SetProperty(ref _phone, value); 
            }
        }

        private string _password;

        [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
        [RegularExpression(ValidationRules.PasswordRegex, ErrorMessage = ValidationMessages.PasswordRegex)]
        public string Password
        {
            get { return _password; }
            set {
                ValidateProperty(value);
                SetProperty(ref _password, value); 
            }
        }

        private string _passwordConfirmation;

        [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
        [Compare(nameof(Password), ErrorMessage = ValidationMessages.PasswordConfirmationComparison)]
        public string PasswordConfirmation
        {
            get { return _passwordConfirmation; }
            set {
                ValidateProperty(value);
                SetProperty(ref _passwordConfirmation, value); 
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
