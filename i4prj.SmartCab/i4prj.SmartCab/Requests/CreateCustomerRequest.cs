using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        }

        #region Name
        private string _name;

        [Required(ErrorMessage = ValidationMessages.NameRequired)]
        [StringLength(255, MinimumLength = 3, ErrorMessage = Validation.ValidationMessages.NameLength)]
        public string Name
        {
            get { return _name; }
            set {
                ValidateProperty(value);
                SetProperty(ref _name, value);

                RaisePropertyChanged(nameof(NameErrors));
                RaisePropertyChanged(nameof(NameHasErrors));
            }
        }

        public string NameErrors => string.Join("\n", GetErrors(nameof(Name)).Cast<string>());
        public bool NameHasErrors => ((List<string>)(GetErrors(nameof(Name)))).Count != 0;
        #endregion

        #region Email
        private string _email;

        [Required(ErrorMessage = ValidationMessages.EmailRequired)]
        [RegularExpression(ValidationRules.EmailRegex, ErrorMessage = ValidationMessages.EmailRegex)]
        public string Email
        {
            get { return _email; }
            set {
                ValidateProperty(value);
                SetProperty(ref _email, value);

                RaisePropertyChanged(nameof(EmailErrors));
                RaisePropertyChanged(nameof(EmailHasErrors));
            }
        }

        public string EmailErrors => string.Join("\n", GetErrors(nameof(Email)).Cast<string>());
        public bool EmailHasErrors => ((List<string>)(GetErrors(nameof(Email)))).Count != 0;
        #endregion

        #region Phone
        private string _phone;

        [Required(ErrorMessage = ValidationMessages.PhoneRequired)]
        [RegularExpression(ValidationRules.PhoneRegex, ErrorMessage = ValidationMessages.PhoneRegex)]
        public string Phone
        {
            get { return _phone; }
            set {
                ValidateProperty(value);
                SetProperty(ref _phone, value);

                RaisePropertyChanged(nameof(PhoneErrors));
                RaisePropertyChanged(nameof(PhoneHasErrors));
            }
        }

        public string PhoneErrors => string.Join("\n", GetErrors(nameof(Phone)).Cast<string>());
        public bool PhoneHasErrors => ((List<string>)(GetErrors(nameof(Phone)))).Count != 0;
        #endregion

        #region Password
        private string _password;

        [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
        [RegularExpression(ValidationRules.PasswordRegex, ErrorMessage = ValidationMessages.PasswordRegex)]
        public string Password
        {
            get { return _password; }
            set {
                ValidateProperty(value);
                SetProperty(ref _password, value);

                RaisePropertyChanged(nameof(PasswordErrors));
                RaisePropertyChanged(nameof(PasswordHasErrors));
            }
        }

        public string PasswordErrors => string.Join("\n", GetErrors(nameof(Password)).Cast<string>());
        public bool PasswordHasErrors => ((List<string>)(GetErrors(nameof(Password)))).Count != 0;
        #endregion

        #region PasswordConfirmation
        private string _passwordConfirmation;

        [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
        [Compare(nameof(Password), ErrorMessage = ValidationMessages.PasswordConfirmationComparison)]
        public string PasswordConfirmation
        {
            get { return _passwordConfirmation; }
            set {
                ValidateProperty(value);
                SetProperty(ref _passwordConfirmation, value);

                RaisePropertyChanged(nameof(PasswordConfirmationErrors));
                RaisePropertyChanged(nameof(PasswordConfirmationHasErrors));
            }
        }

        public string PasswordConfirmationErrors => string.Join("\n", GetErrors(nameof(PasswordConfirmation)).Cast<string>());
        public bool PasswordConfirmationHasErrors => ((List<string>)(GetErrors(nameof(PasswordConfirmation)))).Count != 0;
        #endregion

        #region ValidationBaseOverrides
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
        public override bool IsValid => !HasErrors && IsDirty(nameof(Name)) && IsDirty(nameof(Email)) && IsDirty(nameof(Phone)) && IsDirty(nameof(Password)) && IsDirty(nameof(PasswordConfirmation));
        #endregion
    }
}
