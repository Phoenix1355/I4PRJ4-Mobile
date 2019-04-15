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
                RaisePropertyChanged(nameof(EmailIsDirty));
                RaisePropertyChanged(nameof(EmailHasErrors));
            }
        }

        public string EmailErrors => string.Join("\n", GetErrors(nameof(Email)).Cast<string>());

        public bool EmailIsDirty => IsDirty(nameof(Email));

        public bool EmailHasErrors => ((List<string>)(GetErrors(nameof(Email)))).Count != 0;
        #endregion

        #region Password
        private string _password;

        // No validation on Password as it would seem as if we were validating the correctness of the entered password
        public string Password
        {
            get { return _password; }
            set {
                // Even though there is no validation rules on this property
                // ValidateProperty is still needed to RaisePropertyChanged
                ValidateProperty(value);
                SetProperty(ref _password, value);
            }
        }
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
        public override bool IsValid => !HasErrors && IsDirty(nameof(Email)) && IsDirty(nameof(Password));

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:i4prj.SmartCab.Requests.LoginRequest"/> is invalid.
        /// </summary>
        /// <value><c>true</c> if is invalid; otherwise, <c>false</c>.</value>
        public override bool IsInvalid => !IsValid;
        #endregion
    }
}
