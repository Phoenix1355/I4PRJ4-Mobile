using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace i4prj.SmartCab.CustomAttributes
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        public RequiredIfAttribute(string boolProperty, string errorMessage) : base(errorMessage)
        {
            BoolProperty = boolProperty;
        }

        public string BoolProperty { get; private set; }

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
