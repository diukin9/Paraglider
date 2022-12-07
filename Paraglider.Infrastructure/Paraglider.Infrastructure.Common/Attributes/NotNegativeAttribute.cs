using System.ComponentModel.DataAnnotations;

namespace Paraglider.Infrastructure.Common.Attributes;

public class NotNegativeAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null) return false;
        return Convert.ToInt32(value) > 0;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Значение было null.");

        return Convert.ToInt32(value) > 0
            ? new ValidationResult("Переданное значение должно быть больше нуля.")
            : ValidationResult.Success;
    }
}
