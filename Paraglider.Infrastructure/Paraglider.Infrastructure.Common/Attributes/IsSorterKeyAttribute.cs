using System.ComponentModel.DataAnnotations;

namespace Paraglider.Infrastructure.Common.Attributes;

public class IsSortingKeyAttribute : ValidationAttribute
{
    private Type _type { get; set; }

    public IsSortingKeyAttribute(Type type)
    {
        if (!type.IsEnum) throw new ArgumentException("Type should be enum");
        _type = type;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null) return ValidationResult.Success;
        var name = (string)value;
        if (Enum.GetNames(_type).Any(x => x == name)) return ValidationResult.Success;
        return new ValidationResult("Invalid sorting key");
    }
}
