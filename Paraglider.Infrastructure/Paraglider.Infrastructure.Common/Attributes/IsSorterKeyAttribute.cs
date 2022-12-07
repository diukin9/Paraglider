using System.ComponentModel.DataAnnotations;

namespace Paraglider.Infrastructure.Common.Attributes;

public class IsSorterKeyAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        throw new NotImplementedException();
    }
}
