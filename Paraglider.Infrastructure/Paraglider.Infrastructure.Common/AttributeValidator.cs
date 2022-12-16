using System.ComponentModel.DataAnnotations;

namespace Paraglider.Infrastructure.Common;

public static class AttributeValidator
{
    public static ValidationResult Validate<T>(T value)
    {
        if (value is null) throw new ArgumentException("Value was null");

        var context = new ValidationContext(value);
        var errors = new List<ValidationResult>();
        _ = !Validator.TryValidateObject(value, context, errors, true);

        return new ValidationResult(string.Join("; ", errors));
    }
}