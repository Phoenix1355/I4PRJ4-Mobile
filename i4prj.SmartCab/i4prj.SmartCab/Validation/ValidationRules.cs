using System;
namespace i4prj.SmartCab.Validation
{
    /// <summary>
    /// Validation rules.
    /// </summary>
    public static class ValidationRules
    {
        //Regex taken from: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        public const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        public const string PhoneRegex = "^[1-9][0-9]{7}$";
        public const string PostalCodeRegex = "^[0-9]{4}$";
        public const string CityNameRegex = @"^[a-åA-Å\s]{2,25}$";
        public const string StreetNameRegex = @"^[a-åA-Å]{2,25}$";
        public const string StreetNumberRegex = @"^[0-9a-Å\s]{1,6}$";
        // Oh wait, no reference. HAD TO DO IT MY SELF, MICHAEL!
        public const string PasswordRegex = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^a-zA-Z0-9]).{8,}$";

        // v1
        //public const string PasswordRegex = "^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w]).{8,}$";
    }
}
