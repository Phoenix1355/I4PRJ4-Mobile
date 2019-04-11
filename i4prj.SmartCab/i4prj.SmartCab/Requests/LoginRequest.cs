using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        [RegularExpression(ValidationRules.EmailRegex, ErrorMessage = ValidationMessages.EmailRegex)]
        public string Email
        {
            get { return _email; }
            set {
                ValidateProperty(value);
                RaisePropertyChanged(nameof(EmailErrors));
                RaisePropertyChanged(nameof(EmailIsDirty));
                RaisePropertyChanged(nameof(EmailHasErrors));
                SetProperty(ref _email, value); 
            }
        }

        public string EmailErrors
        {
            get
            {
                return string.Join("\n", GetErrors(nameof(Email)).Cast<string>());
            }
        }

        public bool EmailIsDirty
        {
            get
            {
                return IsDirty(nameof(Email));
            }
        }

        public bool EmailHasErrors
        {
            get
            {
                return ((List<string>)(GetErrors(nameof(Email)))).Count != 0;
            }
        }

        private string _password;

        // No validation on Password as it would seem as if we were validating the correctness of the entered password
        public string Password
        {
            get { return _password; }
            set {
                ValidateProperty(value);
                RaisePropertyChanged(nameof(PasswordErrors));
                SetProperty(ref _password, value);
            }
        }

        private ObservableCollection<string> _passwordErrors;
        public ObservableCollection<string> PasswordErrors
        {
            get { return _passwordErrors; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _passwordErrors, value);
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
