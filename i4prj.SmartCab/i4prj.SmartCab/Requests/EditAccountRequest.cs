using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using i4prj.SmartCab.CustomAttributes;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Validation;

namespace i4prj.SmartCab.Requests
{
    public class EditAccountRequest : ValidationBase, IEditAccountRequest
    {
        public EditAccountRequest()
        {
            Name = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            ChangePassword = false;
            //OldPassword = string.Empty;
            //Password = string.Empty;
            //RepeatedPassword = string.Empty;
        }

        private string _name;

        [Required(ErrorMessage = ValidationMessages.NameRequired)]
        [StringLength(255, MinimumLength = 3, ErrorMessage = Validation.ValidationMessages.NameLength)]
        public string Name
        {
            get { return _name; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _name, value);

                RaisePropertyChanged(nameof(NameErrors));
                RaisePropertyChanged(nameof(NameHasErrors));
            }
        }

        public string NameErrors => string.Join("\n", GetErrors(nameof(Name)).Cast<string>());
        public bool NameHasErrors => ((List<string>) (GetErrors(nameof(Name)))).Count != 0;

        private string _phoneNumber;

        [Required(ErrorMessage = ValidationMessages.PhoneRequired)]
        [RegularExpression(ValidationRules.PhoneRegex, ErrorMessage = ValidationMessages.PhoneRegex)]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _phoneNumber, value);
                RaisePropertyChanged(nameof(PhoneNumberErrors));
                RaisePropertyChanged(nameof(PhoneNumberHasErrors));
            }
        } 


        public string PhoneNumberErrors => string.Join("\n", GetErrors(nameof(PhoneNumber)).Cast<string>());
        public bool PhoneNumberHasErrors => ((List<string>)(GetErrors(nameof(PhoneNumber)))).Count != 0;

        private string _email;

        [Required(ErrorMessage = ValidationMessages.EmailRequired)]
        [RegularExpression(ValidationRules.EmailRegex, ErrorMessage = ValidationMessages.EmailRegex)]
        public string Email
        {
            get { return _email; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _email, value);

                RaisePropertyChanged(nameof(EmailErrors));
                RaisePropertyChanged(nameof(EmailHasErrors));
            }
        }

        public string EmailErrors => string.Join("\n", GetErrors(nameof(Email)).Cast<string>());
        public bool EmailHasErrors => ((List<string>)(GetErrors(nameof(Email)))).Count != 0;

        private bool _changePassword;
        [Required]
        public bool ChangePassword
        {
            get { return _changePassword; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _changePassword,value);

                OldPassword = string.Empty;
                Password = string.Empty;
                RepeatedPassword = string.Empty;

            }
        }

        private string _oldPassword;

        [RegularExpression(ValidationRules.PasswordRegex, ErrorMessage = ValidationMessages.PasswordRegex)]
        [RequiredIf(nameof(ChangePassword),ValidationMessages.PasswordRequired)]
        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _oldPassword, value);

                RaisePropertyChanged(nameof(OldPasswordErrors));
                RaisePropertyChanged(nameof(OldPasswordHasErrors));

                Password = Password;
            }
        }

        public string OldPasswordErrors => string.Join("\n", GetErrors(nameof(OldPassword)).Cast<string>());
        public bool OldPasswordHasErrors => ((List<string>)(GetErrors(nameof(OldPassword)))).Count != 0;

        private string _password;

        [RegularExpression(ValidationRules.PasswordRegex, ErrorMessage = ValidationMessages.PasswordRegex)]
        [PropertyCompare(false, nameof(OldPassword), nameof(Password), ValidationMessages.OldPassword)]
        [RequiredIf(nameof(ChangePassword), ValidationMessages.PasswordRequired)]
        public string Password
        {
            get { return _password; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _password, value);

                RaisePropertyChanged(nameof(PasswordErrors));
                RaisePropertyChanged(nameof(PasswordHasErrors));

                RepeatedPassword = RepeatedPassword;
            }
        }

        public string PasswordErrors => string.Join("\n", GetErrors(nameof(Password)).Cast<string>());
        public bool PasswordHasErrors => ((List<string>)(GetErrors(nameof(Password)))).Count != 0;

        private string _repeatedPassword;

        [RegularExpression(ValidationRules.PasswordRegex, ErrorMessage = ValidationMessages.PasswordRegex)]
        [PropertyCompare(true,nameof(Password),nameof(RepeatedPassword),ValidationMessages.PasswordConfirmationComparison)]
        [RequiredIf(nameof(ChangePassword), ValidationMessages.PasswordRequired)]
        public string RepeatedPassword
        {
            get { return _repeatedPassword; }
            set
            {
                ValidateProperty(value);
                SetProperty(ref _repeatedPassword, value);
      
                RaisePropertyChanged(nameof(RepeatedPasswordErrors));
                RaisePropertyChanged(nameof(RepeatedPasswordHasErrors));
            }
        }

        public string RepeatedPasswordErrors => string.Join("\n", GetErrors(nameof(RepeatedPassword)).Cast<string>());
        public bool RepeatedPasswordHasErrors => ((List<string>)(GetErrors(nameof(RepeatedPassword)))).Count != 0;

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
        public override bool IsValid => !HasErrors && IsDirty(nameof(Name)) && IsDirty(nameof(Email)) && IsDirty(nameof(PhoneNumber)) && IsDirty(nameof(Password)) && IsDirty(nameof(RepeatedPassword));
        #endregion
    }
}
