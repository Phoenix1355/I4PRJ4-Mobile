using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace i4prj.SmartCab.Requests
{
    /// <summary>
    /// Custom attribute for validation with data annotations. It is used for comparing two string attributes.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class PropertyCompareAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyCompareAttribute"/> class.
        /// </summary>
        /// <param name="propertiesShallBeEqual">if true, the class checks if two properties are equal. If false it checks if the properties are different.</param>
        /// <param name="firstValue">The name of the first property to be compared</param>
        /// <param name="secondValue">The name of the second property to be compared.</param>
        /// <param name="errorMessage">Error message to be shown, if the property is not valid.</param>
        public PropertyCompareAttribute(bool propertiesShallBeEqual, string firstValue,string secondValue,string errorMessage) : base(errorMessage)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
            PropertiesShallBeEqual = propertiesShallBeEqual;
        }

        public bool PropertiesShallBeEqual { get; private set; }
        public string FirstValue { get; private set; }
        public string SecondValue { get; private set; }

        /// <summary>
        /// Checks if the value of the properties are equal.
        /// Depending on the value of <see cref="PropertiesShallBeEqual"/>, finds out if the property is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="System.ComponentModel.DataAnnotations.ValidationResult"></see> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(FirstValue);

            var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            if (PropertiesShallBeEqual)
            {
                if (!object.Equals(value, otherValue))
                {
                    return new ValidationResult(base.ErrorMessage, new List<string>
                    {
                        validationContext.MemberName,
                    });
                }
            }
            else
            {
                if (object.Equals(value, otherValue) && (string)value!="")
                {
                    return new ValidationResult(base.ErrorMessage, new List<string>
                    {
                        validationContext.MemberName,
                    });
                }
            }
           
            return null;
        }

    }
}
