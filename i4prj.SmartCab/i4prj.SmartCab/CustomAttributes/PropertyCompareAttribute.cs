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
    public class PropertyCompareAttribute : ValidationAttribute
    {

        public PropertyCompareAttribute(bool propertiesShallBeEqual, string firstValue,string secondValue,string errorMessage) : base(errorMessage)
        {
            FirstValue = firstValue;
            SecondValue = secondValue;
            PropertiesShallBeEqual = propertiesShallBeEqual;
        }

        public bool PropertiesShallBeEqual { get; private set; }
        public string FirstValue { get; private set; }
        public string SecondValue { get; private set; }

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
