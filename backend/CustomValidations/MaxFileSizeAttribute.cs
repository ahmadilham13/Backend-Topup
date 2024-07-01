using System.ComponentModel.DataAnnotations;

namespace backend.CustomValidations;

public class MaxFileSizeAttribute(int maxFileSize) : ValidationAttribute
{
    private readonly int _maxFileSize = maxFileSize;

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext
        )
    {
        var file = value as IFormFile;
        if (file != null)
        {
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMessage());
            }
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        var fileSizeMB = _maxFileSize / 1024 / 1024;
        return $"Maximum allowed file size is {fileSizeMB} MB.";
    }
}