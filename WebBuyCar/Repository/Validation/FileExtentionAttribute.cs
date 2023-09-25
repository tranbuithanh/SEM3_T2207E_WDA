using System;
using System.ComponentModel.DataAnnotations;

namespace WebBuyCar.Repository.Validation
{
	public class FileExtentionAttribute: ValidationAttribute
	{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName); /* lay ten file vd: 123.jpg */
                string[] extensions = { "jpg", "png", "jpeg" };
                bool result = extensions.Any(x => extension.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult("Error File please check file Images");
                }
            }
            return ValidationResult.Success;
        }
    }
}

