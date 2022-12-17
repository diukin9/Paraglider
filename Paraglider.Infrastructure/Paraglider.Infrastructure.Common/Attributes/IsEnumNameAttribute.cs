using System.ComponentModel.DataAnnotations;

namespace Paraglider.Infrastructure.Common.Attributes;

public class IsEnumNameAttribute : ValidationAttribute
{
    private readonly Type _type;

    public IsEnumNameAttribute(Type type)
    {
        _type = type;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;

        if (!_type.IsEnum)
            return new ValidationResult("Необходимо передать Enum.");
        else if (!Enum.GetNames(_type).Any(x => x == (string)value))
            return new ValidationResult($"Такого имени у enum '{_type.Name}' нет.");
        else return ValidationResult.Success;
    }
}
