using System.ComponentModel.DataAnnotations;

namespace Paraglider.Infrastructure.Common.Attributes;

public class NotEmptyGuidAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Значение было null.");

        if (value is not Guid guid)
            return new ValidationResult("Переданное значение должно быть 'Guid'.");
        else if (guid == Guid.Empty)
            return new ValidationResult("Переданное значение было пустым.");
        else return ValidationResult.Success;
    }
}