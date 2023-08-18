using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AttributeValidators
{
    public class SexAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (value.ToString() == "M"
                || value.ToString() == "F")
                return ValidationResult.Success;

            return new ValidationResult("Sex must be F or M (uppercase)"); 
        }
    }
}
