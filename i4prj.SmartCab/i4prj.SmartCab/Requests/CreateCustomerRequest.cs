using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using i4prj.SmartCab.Validation;
using Prism.Mvvm;

namespace i4prj.SmartCab.Requests
{
    public class CreateCustomerRequest : ValidationBase
    {
        public CreateCustomerRequest()
        {
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Password = string.Empty;
            PasswordConfirmation = string.Empty;
        }

        private string _name;

        [Required(ErrorMessage = ValidationMessages.NavnRequired)]
        [StringLength(255, MinimumLength = 3, ErrorMessage = Validation.ValidationMessages.NavnLength)]
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

        protected override void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            base.ValidateProperty(value, propertyName);

            RaisePropertyChanged("IsValid");
            RaisePropertyChanged("IsInvalid");
        }

        public bool IsValid
        {
            get
            {
                return !HasErrors;
            }
        }

        public bool IsInvalid
        {
            get
            {
                return HasErrors;
            }
        }
    }
}
