using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace i4prj.SmartCab.CustomAttributes
{
    /// <summary>
    /// Custom attribute class used for validation with data annotations.
    /// This class is used to make sure the user enters a value for a given property, if value of the property of <see cref="BoolProperty"/> is true.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class RequiredIfAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// Sets the <see cref="BoolProperty"/> to the given value.
        /// </summary>
        /// <param name="boolProperty">The name of the bool property, that defines if a value is required.</param>
        /// <param name="errorMessage">The error message to be shown, if the property is not valid.</param>
        public RequiredIfAttribute(string boolProperty, string errorMessage) : base(errorMessage)
        {
            BoolProperty = boolProperty;
        }

        public string BoolProperty { get; private set; }


        /// <summary>
        /// Checks if the value of the <see cref="BoolProperty"/> is true.
        /// Returns null if it is false. If it is true, it checks wether the property to validate is an empty string. If it is, it returns an error.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult"></see> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(BoolProperty);

            bool valueOfProperty = (bool)property.GetValue(validationContext.ObjectInstance, null);

            if (valueOfProperty)
            {
                if (object.Equals(value, ""))
                {
                    return new ValidationResult(base.ErrorMessage,new List<string>
                    {
                        validationContext.MemberName,
                    });
                }
            }

            return null;
        }
    }
}
