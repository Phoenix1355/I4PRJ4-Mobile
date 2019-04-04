using System;
namespace i4prj.SmartCab.Validation
{
    /// <summary>
    /// Validation messages.
    /// </summary>
    public static class ValidationMessages
    {
        public const string NameRequired = "Navn skal udfyldes.";
        public const string NameLength = "Navn skal være mellem 3 og 255 karakterer langt.";

        public const string EmailRequired = "E-mail skal udfyldes.";
        public const string EmailRegex = "E-mail format er ugyldigt.";

        public const string PhoneRequired = "Du skal angive telefonnummer.";
        public const string PhoneRegex = "Telefonnummer er ugyldigt.";

        public const string PasswordRequired = "Du skal angive kodeord.";
        public const string PasswordRegex = "Kodeord er ugyldigt.";

        public const string PasswordConfirmationComparison = "Kodeord matcher ikke.";

    }
}
